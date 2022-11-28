using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Transform dropPosition;
    public ItemSlotUI[] uiSlots;
    public ItemSlot[] slots;
    [Header("Selecting Items")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName, selectedItemDescription,selectedItemstatName,selectedItemStatValue;
    public GameObject useButton,equipButton,unequipButton,dropButton;

    private int currentequipIndex;
    
    public static Inventory instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        //close inventory panel at the begining of the game
        inventoryPanel.SetActive(false);

        //get access to itemSlots list because we dont attached any script  to them
        slots = new ItemSlot[uiSlots.Length];

        //initialize the slots (loop through all slots and set them up)
        for(int x= 0; x < slots.Length; x++)
        {
            //set our slots
            slots[x] = new ItemSlot();
            //index of our slot is x
            uiSlots[x].index = x;
            //clear the slot mean holding no item
            uiSlots[x].Clear();
        }

        ClearSelectedItemWindow();
    }

    // Update is called once per frame
    void Update() 
    {
        if (Input.GetKeyDown(KeyCode.I))    
        {
            OpenInventory();
        }
    }

    public void AddItem(ItemData item) 
    {
        //check if the item is stackable
        if(item.canStack)
        {
            // Going to search if there is slot with item inside and that item is stackable
            ItemSlot slotToStackTo = GetItemStack(item);

            //if this slot exist
            if(slotToStackTo != null)
            {
                //then player is going to add 1 to the quantity of that item
                slotToStackTo.quantity++;

                //updating UI After that
                UpdateUI();
                return;
            }
        }

        //search if there is an free slot that we can add item inside
        ItemSlot emptySlot = GetEmptySlot();

        //if that slots exist
        if(emptySlot != null)
        {
            //put the item inside
            emptySlot.item = item;
            //make the quantity of that item to 1
            emptySlot.quantity = 1;
            //Update UI After that
            UpdateUI();
            return;
        }

        // if there is no item to stack or empty slot then throw that item
        ThrowItem(item);

    }
    
    public void ThrowItem(ItemData item)
    {
        Instantiate(item.dropPrefab, dropPosition.position, dropPosition.rotation);
        Debug.Log(item);
    }

    public void UpdateUI()
    {
        //loop through all the slot
        for(int x = 0; x < slots.Length; x++)
        {
            //if there is item inside
            if(slots[x].item != null)
            {
                //set that item
                uiSlots[x].Set(slots[x]);
            }
            else
            {
                //but if there is not item inside of that slot clear that slot
                uiSlots[x].Clear();
            }
        }
    }

    ItemSlot GetItemStack(ItemData item)
    {
        //loop through all slots
        for(int x = 0; x < slots.Length; x++)
        {
            //if there is slot that there is no item inside and the quantity of that item is not bigger than maxStackAmount of that item
            if(slots[x].item == item && slots[x].quantity < item.maxStackAmount)
            {
                //return that this slot can stack item
                return slots[x];
            }
        }

        return null;
    }

    ItemSlot GetEmptySlot()
    {
        //loop through all slots
        for(int x = 0; x < slots.Length; x++)
        {
            //if there is slot that there is no item inside
            if(slots[x].item == null)
            {
                //set that slot as empty slot
                return slots[x];
            }
        }

        return null;
    }

    public void SelectItem(int index)
    {
        if(slots[index].item == null)
        {
            return;
        }

        selectedItem = slots[index];
        selectedItemIndex = index;
        
        selectedItemName.text = selectedItem.item.displayName;
        selectedItemDescription.text = selectedItem.item.description;
        
        selectedItemstatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        for(int x = 0; x < selectedItem.item.statsOfItem.Length; x++)
        {
            selectedItemstatName.text += selectedItem.item.statsOfItem[x].type.ToString() + "\n";
            selectedItemStatValue.text += selectedItem.item.statsOfItem[x].value.ToString() + "\n";
        }
        
        useButton.SetActive(selectedItem.item.type == ItemType.Consumable);
        equipButton.SetActive(selectedItem.item.type == ItemType.Equipable && !uiSlots[index].equipped);
        unequipButton.SetActive(selectedItem.item.type == ItemType.Equipable && uiSlots[index].equipped);
        dropButton.SetActive(true);
        
    }

    void ClearSelectedItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemDescription.text = string.Empty;
        selectedItemstatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;
        useButton.SetActive(false);
        equipButton.SetActive(false);
        unequipButton.SetActive(false);
        dropButton.SetActive(false);
    }

    public void OnUseButton()
    {
        if(selectedItem.item.type == ItemType.Consumable)
        {
            for(int x = 0; x < selectedItem.item.statsOfItem.Length; x++)
            {
                switch(selectedItem.item.statsOfItem[x].type)
                {
                    case StatsType.Health: HealthManager.instance.Heal(selectedItem.item.statsOfItem[x].value); break;
                    case StatsType.Magic: ManaManager.instance.GetMana(selectedItem.item.statsOfItem[x].value); break;
                }
            }
        }
        RemoveSelectedItem();
    }

    public void OnEquipButton()
    {
        if(uiSlots[currentequipIndex].equipped)
        {
            UnEquip(currentequipIndex);
        }

        uiSlots[selectedItemIndex].equipped = true;
        currentequipIndex = selectedItemIndex;
        EquipManager.instance.EquipNew(selectedItem.item);
        UpdateUI();
        SelectItem(selectedItemIndex);
    }
    void UnEquip(int index)
    {
        uiSlots[index].equipped = false;
        EquipManager.instance.Unequip();
        UpdateUI();

        if(selectedItemIndex == index)
        {
            SelectItem(index);
        }
    }


    public void OnUneqipButton()
    {
        UnEquip(selectedItemIndex);
    }

    public void OnDropButton()
    {
        ThrowItem(selectedItem.item);
        RemoveSelectedItem();
    }

    void RemoveSelectedItem()
    {
        selectedItem.quantity -= 1;

        if(selectedItem.quantity == 0)
        {
            if(uiSlots[selectedItemIndex].equipped == true)
            {
                UnEquip(selectedItemIndex);
            }

            selectedItem.item = null;
            ClearSelectedItemWindow();

        } 

        UpdateUI();
    }

    public void RemoveItem (ItemData item)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item == item)
            {
                slots[i].quantity--;

                if(slots[i].quantity == 0)
                {
                    if(uiSlots[i].equipped == true)
                    {
                        UnEquip(i);
                    }

                    slots[i].item = null;
                    ClearSelectedItemWindow();
                }

                UpdateUI();
                return;
            }
        }
    }

    public bool HasItem(ItemData item, int quantity)
    {
        int amount = 0;
        for(int i = 0; i < slots.Length; i++)
        {
            if(slots[i].item == item)
            {
                amount += slots[i].quantity;
            }

            if(amount >= quantity)
            {
                return true;
            }
        }
        
        return false;
    }

    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
        Time.timeScale = 0;
        ClearSelectedItemWindow();
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
        Time.timeScale = 1;
    }
}

public class ItemSlot
{
    public ItemData item;
    public int quantity;

}
