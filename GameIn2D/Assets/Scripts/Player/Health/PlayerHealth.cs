using System;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float initialHealth = 90f;
    [SerializeField] private float maximumHealth = 100f;

    private float currentHealth;

    public event Action Died;

    public float CurrentHealth => currentHealth;
    public float MaximumHealth => maximumHealth;
    public bool IsDead { get; private set; }

    public void ResetHealth()
    {
        currentHealth = initialHealth;
        IsDead = false;
    }

    public void ReceiveDamage(float points)
    {
        currentHealth -= points;

        if (currentHealth <= 0f && !IsDead)
        {
            IsDead = true;
            Died?.Invoke();
        }
    }

    public void Recover(float points)
    {
        currentHealth = currentHealth + points > maximumHealth
            ? maximumHealth
            : currentHealth + points;
    }
}
