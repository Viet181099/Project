using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    public Image icon;
    public Button button;
    public TextMeshProUGUI quantityText;
    private ItemSlot currentSlot;
    public int index;
    public bool equipped;
    public GameObject checkIcon;

    public void OnEneable ()
    {
        checkIcon.gameObject.SetActive(equipped);
    }
    
    public void Set(ItemSlot slot)
    {
        //Set the current slot to slot
        currentSlot = slot;

        //activate the icon gameobject
        icon.gameObject.SetActive(true);

        //set the sprite of item to icon
        icon.sprite = slot.item.icon;

        //if we have more than 1 item quantity then set the number of quantity text if not empty the text
        // quantityText.text = slot.quantity > 1 ? slot.quantity.ToString() : string.Empty;
        if(slot.quantity > 1)
        {
            quantityText.text = slot.quantity.ToString();
        }
        else
        {
            quantityText.text = string.Empty;
        }

        if(checkIcon != null)
        {
            checkIcon.SetActive(equipped);
        }
    }

    public void Clear()
    {
        // set the slot empty
        currentSlot = null;

        //deactivate the icon game object
        icon.gameObject.SetActive(false);

        //clear the quantity text
        quantityText.text = string.Empty;
    }

    public void OnClickButton()
    {
        Inventory.instance.SelectItem(index);
    }
}
