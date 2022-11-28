using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.SceneManagement;



public class SaveManager : MonoBehaviour 
{
    public static SaveManager instance;
    private void Awake()
    {
        instance = this;
      
    }

    private void Start()
    {
        StartCoroutine(LoadGame());

    }

    IEnumerator LoadGame()
    {
        yield return new WaitForEndOfFrame();
        if(PlayerPrefs.HasKey("Save")) 
           Load();
        if(PlayerPrefs.HasKey("SceneData"))
           LoadGameData();
        if(PlayerPrefs.HasKey("SaveStatData"))
           LoadPlayerstats();
        PlayerLevelManager.instance.LevelTextUpdate(); 
    }

    // Update is called once per frame
    private void Update()
    {
        //for testting
        if (Input.GetKeyDown(KeyCode.P))
            Save();    
        if(Input.GetKeyDown(KeyCode.M))
        {
            PlayerPrefs.DeleteAll();
        }    
    }

    public void Save()
    {
        SaveData data = new SaveData();
        SaveData statsData = new SaveData();
        //SaveData playerPosData = new SaveData();
        SaveData sceneData = new SaveData();

        //Saving player Position
        sceneData.playerPos = new Svec3(HealthManager.instance.transform.position);
        sceneData.playerRot = new Svec3(HealthManager.instance.transform.eulerAngles);

        //Player Stats
        statsData.currentHealth = HealthManager.instance.currentHealth;
        statsData.maxHealth = HealthManager.instance.maxHealth;
        statsData.healthRegen = HealthManager.instance.healthRegen;

        statsData.currentMana = ManaManager.instance.currentMana;
        statsData.maxMana = ManaManager.instance.maxMana;
        statsData.manaRegen = ManaManager.instance.manaRegen;

        statsData.attackRate = PlayerMovement.instance.attackRate;
        statsData.playerLevel = PlayerLevelManager.instance.playerLevel;
        statsData.currentEXP = PlayerLevelManager.instance.currentEXP;
        statsData.maxEXP = PlayerLevelManager.instance.maxEXP;
        statsData.gold = PlayerLevelManager.instance.gold;

        //inventory
        data.inventory = new SInventorySlot[Inventory.instance.slots.Length];

        for (int x = 0; x < Inventory.instance.slots.Length; x++)
        {
            data.inventory[x] = new SInventorySlot();
            data.inventory[x].occupied = Inventory.instance.slots[x].item != null;

            if (!data.inventory[x].occupied)
               continue;

            data.inventory[x].itemId = Inventory.instance.slots[x].item.id;
            data.inventory[x].quantity = Inventory.instance.slots[x].quantity;
            data.inventory[x].equipped = Inventory.instance.uiSlots[x].equipped;
        }

        //Dropped Items
        ItemObject[] droppedItems = FindObjectsOfType<ItemObject>();
        sceneData.droppedItems = new SDroppedItem[droppedItems.Length];

        for (int x = 0; x < droppedItems.Length; x++)
        {
            sceneData.droppedItems[x] = new SDroppedItem();

            sceneData.droppedItems[x].itemId = droppedItems[x].item.id;
            sceneData.droppedItems[x].position = new Svec3(droppedItems[x].transform.position);
            sceneData.droppedItems[x].rotation = new Svec3(droppedItems[x].transform.eulerAngles);
        }

        //Enemies
        EnemyController[] enemy = FindObjectsOfType<EnemyController>();
        sceneData.enemies = new SEnemies[enemy.Length];

        for (int x = 0; x < enemy.Length; x++)
        {
            sceneData.enemies[x] = new SEnemies();
            sceneData.enemies[x].prefabId = enemy[x].data.id;

            sceneData.enemies[x].position = new Svec3(enemy[x].transform.position);
            sceneData.enemies[x].rotation = new Svec3(enemy[x].transform.eulerAngles);
            
        }
        

        string rawData = JsonUtility.ToJson(data);
        string rawStatData = JsonUtility.ToJson(statsData);
        string gameData = JsonUtility.ToJson(sceneData);
        PlayerPrefs.SetString("Save", rawData);
        PlayerPrefs.SetString("SaveStatData", rawStatData);
        PlayerPrefs.SetString("SceneData", gameData);
        PlayerPrefs.SetString("CurrentScene", SceneManager.GetActiveScene().name);
        Debug.Log("Finished");
    }

    public void LoadPlayerstats()
    {
        SaveData statsData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("SaveStatData"));

        HealthManager.instance.currentHealth = statsData.currentHealth;
        HealthManager.instance.maxHealth = statsData.maxHealth;
        HealthManager.instance.healthRegen = statsData.healthRegen;
        ManaManager.instance.currentMana = statsData.currentMana;
        ManaManager.instance.maxMana = statsData.maxMana;
        ManaManager.instance.manaRegen = statsData.manaRegen;
        PlayerMovement.instance.attackRate = statsData.attackRate;
        PlayerLevelManager.instance.playerLevel = statsData.playerLevel;
        PlayerLevelManager.instance.currentEXP = statsData.currentEXP;
        PlayerLevelManager.instance.maxEXP = statsData.maxEXP;
        PlayerLevelManager.instance.gold = statsData.gold;
    }

    void Load()
    {
        SaveData data = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("Save"));

        //inventory
        int equippedItem = 999;

        for (int x = 0; x < data.inventory.Length; x++)
        {
            if (!data.inventory[x].occupied)
                continue;
            
            Inventory.instance.slots[x].item = ObjectManager.instance.GetItemByID(data.inventory[x].itemId);
            Inventory.instance.slots[x].quantity = data.inventory[x].quantity;

            if (data.inventory[x].equipped)
            {
                equippedItem = x;
            }

            Inventory.instance.UpdateUI();
        }

        if (equippedItem != 999)
        {
            Inventory.instance.SelectItem(equippedItem);
            Inventory.instance.OnEquipButton();
        }
    }

    public void LoadGameData()
    {
        SaveData sceneData = JsonUtility.FromJson<SaveData>(PlayerPrefs.GetString("SceneData"));

        //player position
        HealthManager.instance.transform.position = sceneData.playerPos.GetVector3();
        HealthManager.instance.transform.eulerAngles = sceneData.playerRot.GetVector3();

        //detroy all existing dropped items
        ItemObject[] droppedItems = FindObjectsOfType<ItemObject>();

        for (int x = 0; x < droppedItems.Length; x++)
        {
            Destroy(droppedItems[x].gameObject);
        }

        for (int x = 0; x < sceneData.droppedItems.Length; x++)
        {
            GameObject prefab = ObjectManager.instance.GetItemByID(sceneData.droppedItems[x].itemId).dropPrefab;
            Instantiate(prefab, sceneData.droppedItems[x].position.GetVector3(), Quaternion.Euler(sceneData.droppedItems[x].rotation.GetVector3()));
        }

        //destroy all existing enemies
        EnemyController[] enemies = FindObjectsOfType<EnemyController>();

        for (int x = 0; x < enemies.Length; x++)
             Destroy(enemies[x].gameObject);
        
        //Spawn saved enemies
        for (int x = 0; x < sceneData.enemies.Length; x++)
        {
            GameObject prefab = ObjectManager.instance.GetEnemiesByID(sceneData.enemies[x].prefabId).spawPrefab;
            GameObject enemyObject = Instantiate(prefab, sceneData.enemies[x].position.GetVector3(), Quaternion.Euler(sceneData.enemies[x].rotation.GetVector3()));
            EnemyController enemy = enemyObject.GetComponent<EnemyController>();
        }
    }
}
