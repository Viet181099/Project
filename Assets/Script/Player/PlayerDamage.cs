using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
   {
    if(other.tag == "Enemy")
    {
        other.GetComponent<IDamageable>().TakeDamage(FindObjectOfType<EquipWeapons>().damageToEnemy);
    }
   }
}
