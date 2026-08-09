using UnityEngine;

namespace GameIn2D.LevelGeneration
{

    public class LeaveZone : MonoBehaviour
    {
        private LevelBlock levelBlock;

        private void Awake()
        {
            levelBlock = GetComponentInParent<LevelBlock>();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "King")
            {
                levelBlock.PlayerReachedExit();
            }
        }
    }
}
