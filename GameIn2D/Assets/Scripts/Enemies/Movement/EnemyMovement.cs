using UnityEngine;
using GameIn2D.Enemies;
using GameIn2D.GameFlow;

namespace GameIn2D.Enemies.Movement
{

    [DisallowMultipleComponent]
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float runningSpeed = 2f;

        private Rigidbody2D rigidBody;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            float currentRunningSpeed = runningSpeed;

            if (Enemy.turnAround)
            {
                currentRunningSpeed = runningSpeed;
                transform.eulerAngles = new Vector3(0, 0, 0);
            }
            else
            {
                currentRunningSpeed = -runningSpeed;
                transform.eulerAngles = new Vector3(0, 180f, 0);
            }

            if (GameManager.SharedInstance.CurrentGameState == GameState.InGame)
            {
                rigidBody.linearVelocity = new Vector2(currentRunningSpeed, rigidBody.linearVelocity.y);
            }
        }
    }
}
