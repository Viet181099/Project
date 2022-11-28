using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : MonoBehaviour
{
    public Equip currentEquip;
    public Transform weaponHolder;
    public static EquipManager instance;

    private void Awake()
    {
        instance = this;
    }
    

    public void EquipNew(ItemData item)
    {
        Unequip();
        currentEquip = Instantiate(item.equipPrefab, weaponHolder).GetComponent<Equip>();
    }

    public void Unequip()
    {
        if(currentEquip != null)
        {
            Destroy(currentEquip.gameObject);
            currentEquip = null;
        }
    }
}
