using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopItem : MonoBehaviour
{
    public ItemData itemToBuy;
    public Image icon;
    public int itemCost;
    public TextMeshProUGUI itemCostText;
    public TextMeshProUGUI itemInfoText;
    // Start is called before the first frame update
    void Start()
    {
        icon.sprite = itemToBuy.CraftIcon;
        itemCostText.text = itemCost.ToString();

        for (int x = 0; x < itemToBuy.statsOfItem.Length; x++)
        {
            itemInfoText.text = "+ " + " " + itemToBuy.statsOfItem[x].value.ToString() 
                                     + " " + itemToBuy.statsOfItem[x].type.ToString();
        }
    }

    public void BuyItem()
    {
        if(itemCost < PlayerLevelManager.instance.gold)
        {
            PlayerLevelManager.instance.gold -= itemCost;
            PlayerLevelManager.instance.UpdateGold();
            Inventory.instance.AddItem(itemToBuy);
        }
    }

    public void OnCloseCraftingPanel()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
    }
}
