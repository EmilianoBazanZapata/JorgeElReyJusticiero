using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameIn2D.Player.Lifecycle;

namespace GameIn2D.LevelGeneration
{

    public class KillZone : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("King"))
            {
                //metodo que matara al jugador
                collision.GetComponent<PlayerDeathController>().Die();
            }
        }
    }
}
