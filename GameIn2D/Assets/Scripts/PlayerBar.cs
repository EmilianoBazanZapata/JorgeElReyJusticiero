using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType
{
    Healt,
    Mana
}
public class PlayerBar : MonoBehaviour
{
    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private PlayerMana playerMana;

    private Slider slider;
    public BarType Type;

    private void Awake()
    {
        this.slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        switch (Type)
        {
            case BarType.Healt:
                playerHealth.HealthChanged += UpdateValue;
                UpdateValue(playerHealth.CurrentHealth);
                break;
            case BarType.Mana:
                playerMana.ManaChanged += UpdateValue;
                UpdateValue(playerMana.CurrentMana);
                break;
        }
    }

    private void OnDisable()
    {
        switch (Type)
        {
            case BarType.Healt:
                playerHealth.HealthChanged -= UpdateValue;
                break;
            case BarType.Mana:
                playerMana.ManaChanged -= UpdateValue;
                break;
        }
    }

    private void UpdateValue(float value)
    {
        slider.value = value;
    }
}

