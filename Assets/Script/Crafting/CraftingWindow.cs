using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingWindow : MonoBehaviour
{
  public GameObject craftingPanel;
  public CraftingRecipeUI[] recipeUIs;
  public static CraftingWindow instance;

   private void Awake()
   {
     instance = this;
   }

    public void Craft(CraftingRecipe recipe)
    {
       //looping through cost of making recipe
      for(int i = 0; i < recipe.costs.Length; i++)
        {
         //looping through the quantity of items that we need for our recipe to remove it from inventory
          for(int x = 0; x < recipe.costs[i].quantity; x++)
            {
                //remove the items from inventory
               Inventory.instance.RemoveItem(recipe.costs[i].item);

            }
        }  

        Inventory.instance.AddItem(recipe.itemToCraft);

        for(int i = 0; i < recipeUIs.Length; i++)
        {
            recipeUIs[i].UpdateCanCraft();
        }

    
    }

    public void OnCloseCraftingPanel()
    {
      gameObject.SetActive(false);
      Time.timeScale = 1;
    }


}
