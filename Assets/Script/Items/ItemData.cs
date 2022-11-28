using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Resource,
    Equipable,
    Consumable
}
// ctr + r+r
public enum StatsType
{
    Health,
    Magic,
    Damage
}

[CreateAssetMenu(fileName = "Item",menuName = "New Item")]
public class ItemData : ScriptableObject
{
    public string id;
    [Header("Infor")]
    public string displayName;
    public string description;
    public ItemType type;
    public Sprite icon;
    public Sprite CraftIcon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackAmount; 

    [Header("ItemStats info")]
    public ItemDataStats[] statsOfItem;

    [Header("Equip")]
    public GameObject equipPrefab;
}

[System.Serializable]
public class ItemDataStats
{
    public StatsType type;
    public float value;

}
