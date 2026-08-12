using UnityEngine;

namespace GameIn2D.Enemies
{

    public class TriggerMovement : MonoBehaviour
    {
        public bool movingForward;

        private Enemy enemy;

        private void Awake()
        {
            enemy = GetComponentInParent<Enemy>();
        }

        private void OnTriggerEnter2D(Collider2D otherCollider)
        {
            if (otherCollider.CompareTag("Cellectable"))
            {
                return;
            }

            if (otherCollider.CompareTag("Potion"))
            {
                return;
            }

            if (otherCollider.CompareTag("Limit"))
            {
                enemy.SetTurnAround(movingForward);
                movingForward = !movingForward;
            }
        }
    }
}
