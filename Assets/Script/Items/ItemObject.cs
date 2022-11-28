using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
   public ItemData item;
   public GameObject talkIcon;
   public bool playerInRange;

    private void Update()
    {
        if(playerInRange == true)
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                PickUpItem();
            }
        }
        
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerInRange = true;
            //talkIcon.SetActive(true);

        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            playerInRange = false;
            //talkIcon.SetActive(false);
            
        }
    }

    public void PickUpItem()
    {
       Inventory.instance.AddItem(item);
        Destroy(gameObject);
    }
}
