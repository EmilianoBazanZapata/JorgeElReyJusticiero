using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "King")
        {
            LevelGenerator.SharedInstance.AddLevelBlock();
            LevelGenerator.SharedInstance.RemoveLevelBlock();
        }
    }
}
