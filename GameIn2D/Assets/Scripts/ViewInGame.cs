using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour
{
    //etiqueta para ver la puntuacion actual
    public Text CollectableLabel;

    public Text DistanceLabel;

    public Text MaxDistanceLabel;
    private void Update()
    {
        if (GameManager.SharedInstance.CurrentGameState == GameState.InGame)
        {
            float CurrentObjects = GameManager.SharedInstance.CollectedObjects;
            //mostrare en el canvas la cantidad de monedas recogidas
            //UnityEngine.Debug.Log(CurrentObjects);
            this.CollectableLabel.text = CurrentObjects.ToString();
            //mostrare la distancia recorrida
            float TraveledDistance = PlayerController.sharedInstance.GetDistance();
                                                                        //asi solo mostrare un deciaml
            this.DistanceLabel.text = "Distancia \n" + TraveledDistance.ToString("f1");

            float MaxDistance = PlayerPrefs.GetFloat("MaxDistance",0);
            //actualizo la nueva distancia maxima
            this.MaxDistanceLabel.text = "Distancia Maxima \n" + MaxDistance.ToString("f1");

        }
        else if (GameManager.SharedInstance.CurrentGameState == GameState.GameOver)
        {
            float CurrentObjects = GameManager.SharedInstance.CollectedObjects;
            //mostrare en el canvas la cantidad de monedas recogidas
            this.CollectableLabel.text = CurrentObjects.ToString();
        }
    }
}