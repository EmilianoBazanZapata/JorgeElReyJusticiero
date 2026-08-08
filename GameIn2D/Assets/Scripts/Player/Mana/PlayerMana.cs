using UnityEngine;

[DisallowMultipleComponent]
public sealed class PlayerMana : MonoBehaviour
{
    [SerializeField] private float initialMana = 90f;
    [SerializeField] private float maximumMana = 100f;

    private float currentMana;

    public float CurrentMana => currentMana;
    public float MaximumMana => maximumMana;
    public bool HasMana => currentMana > 0f;

    public void ResetMana()
    {
        currentMana = initialMana;
    }

    public void Consume(float points)
    {
        currentMana -= points;
    }

    public void Recover(float points)
    {
        currentMana += points;

        if (currentMana >= maximumMana)
        {
            currentMana = maximumMana;
        }
    }
}
