using System;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float initialHealth = 90f;
    [SerializeField] private float maximumHealth = 100f;

    private float currentHealth;

    public event Action Died;
    public event Action<float> HealthChanged;

    public float CurrentHealth => currentHealth;
    public float MaximumHealth => maximumHealth;
    public bool IsDead { get; private set; }

    public void ResetHealth()
    {
        float previousHealth = currentHealth;
        currentHealth = initialHealth;
        IsDead = false;

        if (currentHealth != previousHealth)
        {
            HealthChanged?.Invoke(currentHealth);
        }
    }

    public void ReceiveDamage(float points)
    {
        float previousHealth = currentHealth;
        currentHealth -= points;

        if (currentHealth != previousHealth)
        {
            HealthChanged?.Invoke(currentHealth);
        }

        if (currentHealth <= 0f && !IsDead)
        {
            IsDead = true;
            Died?.Invoke();
        }
    }

    public void Recover(float points)
    {
        float previousHealth = currentHealth;
        currentHealth = currentHealth + points > maximumHealth
            ? maximumHealth
            : currentHealth + points;

        if (currentHealth != previousHealth)
        {
            HealthChanged?.Invoke(currentHealth);
        }
    }
}
