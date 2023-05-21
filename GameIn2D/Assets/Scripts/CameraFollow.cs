using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Target;

    public Vector3 Offset = new Vector3(-0.48f, 5.31f,-10.0f);

    public float DampTime = 0.3f;

    public Vector3 Velocity = Vector3.zero;

    public static CameraFollow SharedInstance;

    private void Awake()
    {
        Application.targetFrameRate = 60;
        SharedInstance = this;
    }
    private void Update()
    {
        //la camara seguira al jugador
        Vector3 Point = GetComponent<Camera>().WorldToViewportPoint(Target.position);
        //Debug.Log(Point);
        //cantidad de  movimiento de la camara
        Vector3 Delta = Target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3 (Offset.x, Offset.y,Point.z));

        Vector3 Destination = Point + Delta;
        //mover la camara solamente en x
        Destination = new Vector3(Target.position.x, Offset.y, Offset.z);

        this.transform.position =Vector3.SmoothDamp(this.transform.position,Destination,ref Velocity,DampTime);
    }
    public void ResetCameraPosition() 
    {
        //la camara seguira al jugador
        Vector3 Point = GetComponent<Camera>().WorldToViewportPoint(Target.position);
        //Debug.Log(Point);
        //cantidad de  movimiento de la camara
        Vector3 Delta = Target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(Offset.x, Offset.y, Point.z));

        Vector3 Destination = Point + Delta;
        //mover la camara solamente en x
        Destination = new Vector3(Target.position.x, Offset.y, Offset.z);

        this.transform.position = Destination;
    }

}
