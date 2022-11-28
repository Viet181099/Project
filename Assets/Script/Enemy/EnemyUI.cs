using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyUI : MonoBehaviour
{
    public GameObject enemyHealthUI;
    public Image healthBar;
    public Image enemyImage;
    public float enemyTimer;
    public float enemyUITime;

    // Update is called once per frame
    void Update()
    {
        enemyTimer += Time.deltaTime;
        //if timer is bigger or equal to the time that ui is active
        if(enemyTimer >= enemyUITime)
        {
            enemyHealthUI.SetActive(false);
            enemyTimer = 0;
        }
    }

    public void UpdateEnemyUI(float maxHealth, float currentHealth, Sprite icon)
    {
        enemyHealthUI.SetActive(true);
        //get percentage of enemy health
        float getPercentage = currentHealth / maxHealth;
        //set the image
        enemyImage.sprite = icon;
        //put the per centage in fillamount of image
        healthBar.fillAmount = getPercentage;
        // if current health reached to zero or les than zero
        if(currentHealth <= 0)
        {
            //disable UI
            enemyHealthUI.SetActive(false);
        }
        //set timer to zero each time we call this function
        enemyTimer = 0;

    }
}
