using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator SharedInstance;
    public List<LevelBlock> AllTheLevelBlocks = new List<LevelBlock>();
    public Transform LevelStartPoint;
    public List<LevelBlock> CurrentBlocks = new List<LevelBlock>();

    private void Awake()
    {
        SharedInstance = this;
    }
    private void Start()
    {
       GenerateInitialBlock();
    }
    public void GenerateInitialBlock()
    {
        for (int i = 0; i < 4; i++)
        {
            AddLevelBlock();
        }
    }
    public void AddLevelBlock()
    {
        //valor aleatorio entre 0 y la cantidad de niveles creados
        int RandomIndex = Random.Range(0, AllTheLevelBlocks.Count);
        //instanciamos un bloque del nivel desde la carpeta assets hasta la escena
        LevelBlock CurrentBlock = (LevelBlock)Instantiate(AllTheLevelBlocks[RandomIndex]);
        //pone el bloque del nivel como hijo de lLevelGenerator
        CurrentBlock.transform.SetParent(this.transform, false);
        
        Vector3 SpawPosition = Vector3.zero;
        if (CurrentBlocks.Count == 0)
        {
            SpawPosition = LevelStartPoint.position;
        }
        else
        {
            SpawPosition = CurrentBlocks[CurrentBlocks.Count - 1].ExitPoint.position;
        }
        //corregimos la posicion del bloque restando las posiciones
        Vector3 Correction = new Vector3(SpawPosition.x - CurrentBlock.StartPoint.position.x,
                                         SpawPosition.y - CurrentBlock.StartPoint.position.y, 0);
        
        CurrentBlock.transform.position = Correction;
        CurrentBlocks.Add(CurrentBlock);

    }
    public void RemoveLevelBlock()
    {
        //elimino el Bloque mas Viejo
        LevelBlock OldestBlock = CurrentBlocks[0];
        CurrentBlocks.Remove(OldestBlock);
        Destroy(OldestBlock.gameObject);
    }
    public void RemoveAllTheBlock()
    {
        while (CurrentBlocks.Count > 0)
        {
            RemoveLevelBlock();
        }
    }
}
