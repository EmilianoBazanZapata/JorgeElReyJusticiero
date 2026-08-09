using UnityEngine;
using GameIn2D.Player.Health;

namespace GameIn2D.Enemies.Combat
{

    [DisallowMultipleComponent]
    public sealed class EnemyContactDamage : MonoBehaviour
    {
        [SerializeField] private float damage = 20f;

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.CompareTag("King"))
            {
                otherCollider.GetComponent<PlayerHealth>().ReceiveDamage(damage);
            }
        }
    }
}
