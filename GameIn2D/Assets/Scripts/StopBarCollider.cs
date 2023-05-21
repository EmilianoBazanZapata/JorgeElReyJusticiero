using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopBarCollider : MonoBehaviour
{
    public GameObject collider2D;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "King")
        {
            collider2D.SetActive(true);
        }
    }
}