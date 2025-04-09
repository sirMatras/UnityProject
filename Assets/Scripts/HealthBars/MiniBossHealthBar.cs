using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MiniBossHealthBar : MonoBehaviour
{
    public Slider healthSlider; 
    public TMP_Text healthBar;
    private Damagable miniBossDmg;  
    public GameObject miniBoss;     

    private void Awake()
    {
        if (miniBoss == null)
        {
            GameObject boss = GameObject.FindGameObjectWithTag("MiniBoss");
            if (boss != null)
            {
                miniBossDmg = boss.GetComponent<Damagable>();
            }
            else
            {
                Debug.Log("No MiniBoss found");
            }
        }
        else
        {
            miniBossDmg = miniBoss.GetComponent<Damagable>();
        }
    }
    
    void Start()
    {
        if (miniBossDmg != null)
        {
            healthSlider.value = CalculateSliderPercentage(miniBossDmg.Health, miniBossDmg.MaxHealth);
            healthBar.text = "HP " + miniBossDmg.Health + " / " + miniBossDmg.MaxHealth;
        }
    } 

    private void OnEnable()
    {
        if (miniBossDmg != null)
        {
            miniBossDmg.healthChanged.AddListener(OnMiniBossHealthChanged);
        }
    }

    private void OnDisable()
    {
        if (miniBossDmg != null)
        {
            miniBossDmg.healthChanged.RemoveListener(OnMiniBossHealthChanged);
        }
    }

    private void OnMiniBossHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);

        if (newHealth < 0)
        {
            healthBar.text = "HP " + 0 + " / " + maxHealth; 
            return;
        }
        
        healthBar.text = "HP " + newHealth + " / " + maxHealth;
    }

    private float CalculateSliderPercentage(float currHealth, float maxHealth)
    {
        return currHealth / maxHealth;
    }
    
    public void ToggleHealthBar(bool isActive)
    {
        healthSlider.gameObject.SetActive(isActive);
        healthBar.gameObject.SetActive(isActive);
    }
}
