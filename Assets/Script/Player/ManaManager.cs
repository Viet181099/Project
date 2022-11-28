using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
   public static ManaManager instance;
    public Image manaBar;
    public float currentMana, maxMana, manaRegen;
    public TextMeshProUGUI permanaBar;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        currentMana = maxMana;
        PercentHealthUI();
    }

    // Update is called once per frame
    void Update()
    {
        // getting the percentage of division between current hp and set it in ui GetManath bar
        manaBar.fillAmount = GetPercentage();
        
        //GetMana the player depend on our GetManath regeneration rate
        GetMana(manaRegen * Time.deltaTime);
        PercentHealthUI();
    }

    public void GetMana(float amount)
    {
        // pick the smallest value between two number or more number and set it as value of current GetManath
        currentMana = Mathf.Min(currentMana + amount, maxMana);
    }

    public void PercentHealthUI()
    {
        permanaBar.text = Mathf.Round(currentMana / maxMana * 100) + "%";
    }

    public void TakeMana(float amount)
    {
        //pick the biggest value between two or more number and set as value of current GetManath
        currentMana = Mathf.Max(currentMana - amount, 0.0f);
    }

    public float GetPercentage()
    {
        return currentMana / maxMana;
    }
}
