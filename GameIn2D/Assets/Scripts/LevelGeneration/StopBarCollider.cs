using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace GameIn2D.LevelGeneration
{

    public class StopBarCollider : MonoBehaviour
    {
        [FormerlySerializedAs("collider2D")]
        [SerializeField] private GameObject stopBarrier;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag == "King")
            {
                stopBarrier.SetActive(true);
            }
        }
    }
}
