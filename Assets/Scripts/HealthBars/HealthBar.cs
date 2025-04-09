using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider; 
    public TMP_Text healthBar;
    private Damagable playerDmg;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if (player == null)
        {
            Debug.Log("No Player found");
        }
        
        playerDmg = player.GetComponent<Damagable>();
    }
    
    void Start()
    {
        healthSlider.value = CalculateSliderPercentage(playerDmg.Health, playerDmg.MaxHealth);
        healthBar.text = "HP " + playerDmg.Health + " / " + playerDmg.MaxHealth;
    } 

    private void OnEnable()
    {
        playerDmg.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDmg.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);

        if (newHealth < 0)
        {
            healthBar.text = "HP " + 0 + " / " + maxHealth; 
            return;
        }
        
        healthBar.text = "HP " + newHealth + " / " + maxHealth;
    }

    // Start is called before the first frame update

    private float CalculateSliderPercentage(float currHealth, float maxHealth)
    {
        return currHealth / maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
