using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HealthManager : MonoBehaviour, IDamageable
{
    public static HealthManager instance;
    public Image healthBar;
    public float currentHealth, maxHealth, healthRegen;
    public TextMeshProUGUI perhealthBar;
  
    
    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;

        PercentHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        // getting the percentage of division between current hp and set it in ui health bar
        healthBar.fillAmount = GetPercentage();
        
        //heal the player depend on our health regeneration rate
        Heal(healthRegen * Time.deltaTime);

        PercentHealthUI();
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
        // if health reach to zero we call the die function
        if(currentHealth == 0)
        {   
            Die();
        }

    }

    public void PercentHealthUI()
    {
       perhealthBar.text = Mathf.Round( currentHealth / maxHealth * 100)  + "%";
    }
    

    public float GetPercentage()
    {
       
        return currentHealth / maxHealth;
    }

    public void Die()
    {
        Debug.Log("Player is Dead");
    }
}



public interface IDamageable
{
    void TakeDamage(float amount);
}