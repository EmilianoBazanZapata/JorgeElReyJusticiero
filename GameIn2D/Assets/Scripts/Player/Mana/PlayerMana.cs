using System;
using UnityEngine;

[DisallowMultipleComponent]
public sealed class PlayerMana : MonoBehaviour
{
    [SerializeField] private float initialMana = 90f;
    [SerializeField] private float maximumMana = 100f;

    private float currentMana;

    public event Action<float> ManaChanged;

    public float CurrentMana => currentMana;
    public float MaximumMana => maximumMana;
    public bool HasMana => currentMana > 0f;

    public void ResetMana()
    {
        float previousMana = currentMana;
        currentMana = initialMana;

        if (currentMana != previousMana)
        {
            ManaChanged?.Invoke(currentMana);
        }
    }

    public void Consume(float points)
    {
        float previousMana = currentMana;
        currentMana -= points;

        if (currentMana != previousMana)
        {
            ManaChanged?.Invoke(currentMana);
        }
    }

    public void Recover(float points)
    {
        float previousMana = currentMana;
        currentMana += points;

        if (currentMana >= maximumMana)
        {
            currentMana = maximumMana;
        }

        if (currentMana != previousMana)
        {
            ManaChanged?.Invoke(currentMana);
        }
    }
}
