using UnityEngine;

namespace GameIn2D.Player.Progression
{

    [DisallowMultipleComponent]
    public sealed class PlayerProgressTracker : MonoBehaviour
    {
        private Vector3 startPosition;

        public float CurrentDistance => Mathf.Abs(transform.position.x - startPosition.x);

        private void Awake()
        {
            startPosition = transform.position;
        }

        private void Start()
        {
            startPosition = transform.position;
        }

        public void ResetProgress()
        {
            transform.position = startPosition;
        }
    }
}
