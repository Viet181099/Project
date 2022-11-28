using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class StatsInfo : MonoBehaviour
{
    public GameObject statsWindow;
    public float checkRate = 0.05f;
    private float lastcheckTime;
    public TextMeshProUGUI hp;
    public TextMeshProUGUI hpr;
    public TextMeshProUGUI mp;
    public TextMeshProUGUI mpr;
    public TextMeshProUGUI exp;
    // Start is called before the first frame update
    void Start()
    {
        statsWindow.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            statsWindow.SetActive(true);
        }
        if(Time.time - lastcheckTime >checkRate && statsWindow.activeInHierarchy)
        {
            lastcheckTime = Time.time;
            hp.text = ("" + HealthManager.instance.currentHealth.ToString("F0") + "/" + "" + HealthManager.instance.maxHealth).ToString();
            hpr.text = HealthManager.instance.healthRegen.ToString();
            mp.text = ("" + ManaManager.instance.currentMana.ToString("F0") + "/" + "" + ManaManager.instance.maxMana).ToString();
            mpr.text = ManaManager.instance.manaRegen.ToString();
            exp.text = ("" + PlayerLevelManager.instance.currentEXP.ToString("F0") + "/" + "" + PlayerLevelManager.instance.maxEXP).ToString();

        }
    }

    public void CloseStatsInfoPanel()
    {
        statsWindow.SetActive(false);

    }

    
}
