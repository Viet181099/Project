using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
   //[HideInInspector]
   public ItemData[] items;
   //[HideInInspector]
   public EnemyData[] enemies;
   public static ObjectManager instance;

   private void Awake()
   {
       instance = this;

       //load all asset that we need
       items = Resources.LoadAll<ItemData>("Items");
       enemies = Resources.LoadAll<EnemyData>("Enemies");
   }
   
   public ItemData GetItemByID(string id)
   {
       for(int x = 0; x < items.Length; x++)
       {
          if(items[x].id == id)
          {
               return items[x];
          }
       }

       return null;
   }

   public EnemyData GetEnemiesByID(string id)
   {
       for(int x = 0; x < enemies.Length; x++)
       {
          if(enemies[x].id == id)
          {
               return enemies[x];
          }
       }

       return null;
   }
}
