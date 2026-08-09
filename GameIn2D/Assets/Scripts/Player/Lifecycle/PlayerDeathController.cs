using System;
using UnityEngine;
using GameIn2D.Player.Health;
using GameIn2D.Player.Progression;

namespace GameIn2D.Player.Lifecycle
{

    [DisallowMultipleComponent]
    [RequireComponent(typeof(PlayerHealth), typeof(PlayerProgressTracker), typeof(BoxCollider2D))]
    public sealed class PlayerDeathController : MonoBehaviour
    {
        private PlayerHealth playerHealth;
        private PlayerProgressTracker playerProgressTracker;
        private BoxCollider2D boxCollider2d;

        public event Action Died;

        public bool IsDead { get; private set; }

        private void Awake()
        {
            playerHealth = GetComponent<PlayerHealth>();
            playerProgressTracker = GetComponent<PlayerProgressTracker>();
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
            if (currentMaxDistance < playerProgressTracker.CurrentDistance)
            {
                PlayerPrefs.SetFloat("MaxDistance", playerProgressTracker.CurrentDistance);
            }

            boxCollider2d.isTrigger = false;
        }

        public void ResetDeathState()
        {
            IsDead = false;
            boxCollider2d.isTrigger = false;
        }
    }
}
