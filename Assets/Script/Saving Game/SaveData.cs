using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData 
{
    //player Position
    public Svec3 playerPos;
    public Svec3 playerRot;

    //PlayerStats
    public float currentHealth, maxHealth, currentMana, maxMana, healthRegen, manaRegen, attackRate;
    public int playerLevel, currentEXP, maxEXP, gold;

    //inventory
    public SInventorySlot[] inventory;
    //dropped Items
    public SDroppedItem[] droppedItems;
    //Enemies
    public SEnemies[] enemies;
}
[System.Serializable]
public struct Svec3
{
    public float x;
    public float y;
    public float z;

    public Svec3(Vector3 vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3 GetVector3()
    {
        return new Vector3(x, y, z);
    }
}

[System.Serializable]
public struct SInventorySlot
{
    public bool occupied;
    public string itemId;
    public int quantity;
    public bool equipped;
}

[System.Serializable]
public struct SDroppedItem
{
    public string itemId;
    public Svec3 position;
    public Svec3 rotation;
}

[System.Serializable]
public struct SEnemies
{
    public string prefabId;
    public Svec3 position;
    public Svec3 rotation;
}