using System.Collections.Generic;
using UnityEngine;

namespace GameIn2D.LevelGeneration
{

    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject firstBlock;

        public List<LevelBlock> AllTheLevelBlocks = new List<LevelBlock>();
        public Transform LevelStartPoint;
        public List<LevelBlock> CurrentBlocks = new List<LevelBlock>();

        private GameObject currentFirstBlock;

        private void Start()
        {
            GenerateInitialBlocks();
        }

        public void BeginRun(Vector3 firstBlockPosition, Quaternion firstBlockRotation)
        {
            currentFirstBlock = Instantiate(firstBlock, firstBlockPosition, firstBlockRotation);
        }

        public void StartGeneration()
        {
            RemoveAllBlocks();
            GenerateInitialBlocks();
        }

        public void RestartRun()
        {
            GenerateInitialBlocks();
        }

        public void EndRun()
        {
            RemoveAllBlocks();
            Destroy(currentFirstBlock);
        }

        public void AdvanceGeneration()
        {
            GenerateNextBlock();
            RemoveOldestBlock();
        }

        private void GenerateInitialBlocks()
        {
            for (int i = 0; i < 4; i++)
            {
                GenerateNextBlock();
            }
        }

        private void GenerateNextBlock()
        {
            int randomIndex = Random.Range(0, AllTheLevelBlocks.Count);
            LevelBlock currentBlock = Instantiate(AllTheLevelBlocks[randomIndex]);
            currentBlock.transform.SetParent(transform, false);

            Vector3 spawnPosition = Vector3.zero;
            if (CurrentBlocks.Count == 0)
            {
                spawnPosition = LevelStartPoint.position;
            }
            else
            {
                spawnPosition = CurrentBlocks[CurrentBlocks.Count - 1].ExitPoint.position;
            }

            Vector3 correction = new Vector3(spawnPosition.x - currentBlock.StartPoint.position.x,
                                             spawnPosition.y - currentBlock.StartPoint.position.y, 0);

            currentBlock.transform.position = correction;
            currentBlock.Initialize(this);
            CurrentBlocks.Add(currentBlock);
        }

        private void RemoveOldestBlock()
        {
            LevelBlock oldestBlock = CurrentBlocks[0];
            CurrentBlocks.Remove(oldestBlock);
            Destroy(oldestBlock.gameObject);
        }

        private void RemoveAllBlocks()
        {
            while (CurrentBlocks.Count > 0)
            {
                RemoveOldestBlock();
            }
        }
    }
}
