using UnityEngine;

namespace GameIn2D.LevelGeneration
{

    public class LevelBlock : MonoBehaviour
    {
        public Transform StartPoint;
        public Transform ExitPoint;

        private LevelGenerator levelGenerator;

        public void Initialize(LevelGenerator generator)
        {
            levelGenerator = generator;
        }

        public void PlayerReachedExit()
        {
            levelGenerator.AdvanceGeneration();
        }
    }
}
