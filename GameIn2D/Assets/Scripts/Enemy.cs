using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //fuerza que aplico para caminar
    public float runningSpeed = 2.0f;
    //rigidbody del personaje
    private Rigidbody2D rigidBody;
    //variable para saber si debo girar o no
    public static bool turnAround;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        float currentRunningSpeed = runningSpeed;
        
        if (turnAround == true)
        {
            //aquí la velocidad es positiva...
            currentRunningSpeed = runningSpeed;
            this.transform.eulerAngles = new Vector3(0, 0, 0);
        }
        else
        {
            //aquí la velocidad es negativa
            currentRunningSpeed = -runningSpeed;
            this.transform.eulerAngles = new Vector3(0, 180.0F, 0);
        }
        if (GameManager.SharedInstance.CurrentGameState == GameState.InGame)
        {
            rigidBody.velocity = new Vector2(currentRunningSpeed, rigidBody.velocity.y);
        }


    }
}
