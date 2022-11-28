using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerLevelManager : MonoBehaviour
{
    public int gold;
    public TextMeshProUGUI goldText;
    public static PlayerLevelManager instance;
    public GameObject levelupEffect;
    public TextMeshProUGUI levelText;
    public int playerLevel = 1;
    public int currentEXP;
    public int maxEXP = 500;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        //set the level text at the begining of the game
        LevelTextUpdate();
        UpdateGold();
    }

    private void Update()
    {
        UpdateGold();
    }

    public void EarnExp(int XPAmount)
    {
        //increase the current exp
        currentEXP += XPAmount;
        LevelUP();
    }
    public void EarnGold(int goldAmount)
    {
        //increase the current exp
        gold += goldAmount;
        UpdateGold();
    }

    public void LevelUP()
    {
        //loop through it and if we get exp and still current exp is bigger call codes inside of it
        while(currentEXP >= maxEXP)
        {
            //reduce current exp from max exp
            currentEXP -= maxEXP;
            //increase the calue of max exp
            maxEXP = (int)(maxEXP * 2f);
            //increase the level
            playerLevel++;
            GameObject obj = Instantiate(levelupEffect, transform.position, transform.rotation);
            Destroy(obj, 1f);
            LevelTextUpdate();

            HealthManager.instance.maxHealth += 50;
            HealthManager.instance.healthRegen += 0.2f;
            HealthManager.instance.currentHealth = HealthManager.instance.maxHealth;

            ManaManager.instance.maxMana += 10;
            ManaManager.instance.manaRegen += 0.1f;
            ManaManager.instance.currentMana = ManaManager.instance.maxMana;
        }
    }

    public void LevelTextUpdate()
    {
        levelText.text = playerLevel.ToString();
    }

    public void UpdateGold()
    {
        goldText.text = gold.ToString();
    }
}
