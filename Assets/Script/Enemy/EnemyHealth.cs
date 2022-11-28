using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour,IDamageable
{
    public int EXPToGive;
    public int goldToGive;
    public Sprite enemyImage;
    public float currentHealth, maxHealth, healthRegen;
    public ItemData[] dropOndeath;
    public GameObject bloodEffect;
    public GameObject deathEffect;

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

    }

    // Update is called once per frame
    void Update()
    {
        // getting the percentage of division between current hp and set it in ui health bar
        //healthBar.fillAmount = GetPercentage(); 
        //heal the player depend on our health regeneration rate
        Heal(healthRegen * Time.deltaTime);
    }

    public void Heal(float amount)
    {
        // pick the smallest value between two number or more number and set it as value of current health
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth);
        
    }

    public void TakeDamage(float amount)
    {
        //pick the biggest value between two or more number and set as value of current health
        currentHealth = Mathf.Max(currentHealth - amount, 0.0f);
        GameObject bloodObj = Instantiate(bloodEffect, transform.position, transform.rotation);
        Destroy(bloodObj, 1);
        FindObjectOfType<EnemyUI>().UpdateEnemyUI(maxHealth, currentHealth, enemyImage);
        // if health reach to zero we call the die function
        if(currentHealth == 0)
        {
            Die();
        }

    }

    public float GetPercentage()
    {
        return currentHealth / maxHealth;
    }

    public void Die()
    {
        PlayerLevelManager.instance.EarnExp(EXPToGive);
        PlayerLevelManager.instance.EarnGold(goldToGive);

        for(int x = 0; x < dropOndeath.Length; x++)
        {
            Instantiate(dropOndeath[x].dropPrefab, transform.position, Quaternion.identity);
        }
        Debug.Log("Enemy is Dead");
        Destroy(gameObject);
        GameObject obj = Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(obj, 0.9f);
    }
}
