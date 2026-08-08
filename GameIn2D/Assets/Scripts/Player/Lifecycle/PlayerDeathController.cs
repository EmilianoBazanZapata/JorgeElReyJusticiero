using System;
using UnityEngine;

[DisallowMultipleComponent]
[RequireComponent(typeof(PlayerHealth), typeof(PlayerController), typeof(BoxCollider2D))]
public sealed class PlayerDeathController : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private PlayerController playerController;
    private BoxCollider2D boxCollider2d;

    public event Action Died;

    public bool IsDead { get; private set; }

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();
        playerController = GetComponent<PlayerController>();
        boxCollider2d = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        playerHealth.Died += Die;
    }

    private void OnDisable()
    {
        playerHealth.Died -= Die;
    }

    public void Die()
    {
        if (IsDead)
        {
            return;
        }

        IsDead = true;
        Died?.Invoke();

        float currentMaxDistance = PlayerPrefs.GetFloat("MaxDistance", 0);
        if (currentMaxDistance < playerController.GetDistance())
        {
            PlayerPrefs.SetFloat("MaxDistance", playerController.GetDistance());
        }

        boxCollider2d.isTrigger = false;
    }

    public void ResetDeathState()
    {
        IsDead = false;
        boxCollider2d.isTrigger = false;
    }
}
