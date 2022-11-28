using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
   public float damgetoPlayer;


   private void OnTriggerEnter2D(Collider2D other)
   {
    if(other.tag == "Player")
    {
        HealthManager.instance.GetComponent<IDamageable>().TakeDamage(damgetoPlayer);
    }
   }
   
}
