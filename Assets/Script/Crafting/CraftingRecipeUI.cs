using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingRecipeUI : MonoBehaviour
{
    public CraftingRecipe recipe;
    public Image backgroundImage;
    public Image icon;
    public TextMeshProUGUI itemName;
    public Image[] resourceCosts;
    public Color canCraftColor, cannotCraftColor;
    public bool canCraft;

    private void OnEnable()
    {
        UpdateCanCraft();
    }


    public void UpdateCanCraft()
    {
        canCraft = true;

        for(int i = 0; i < recipe.costs.Length; i++)
        {
            //if we dont have enough item then dont craft
            if(!Inventory.instance.HasItem(recipe.costs[i].item, recipe.costs[i].quantity))
            {
                canCraft = false;
                break;
            }
        }
        
        //set the background image if we can craft to cancraftColor if not set it to cannotCraftColor
        backgroundImage.color = canCraft? canCraftColor : cannotCraftColor;
        
    }
    
    private void Start()
    {
        icon.sprite = recipe.itemToCraft.icon;
        itemName.text = recipe.itemToCraft.displayName;

        for(int i = 0; i < resourceCosts.Length; i++)
        {
            if(i < recipe.costs.Length)
            {
                resourceCosts[i].gameObject.SetActive(true);
                resourceCosts[i].sprite = recipe.costs[i].item.CraftIcon;
                resourceCosts[i].transform.GetComponentInChildren<TextMeshProUGUI>().text = recipe.costs[i].quantity.ToString();
            }
            else
            {
                resourceCosts[i].gameObject.SetActive(false);
            }
        }
    }


    public void OnclickCraftingButton()
    {
        if(canCraft)
        {
            CraftingWindow.instance.Craft(recipe);
        }
    }
}
