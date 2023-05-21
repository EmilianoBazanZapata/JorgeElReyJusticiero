using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BrilloManager : MonoBehaviour
{
    //hace referencia al silder en pantalla
    public Slider slider;
    //hace referencia al valor del brillo
    public float sliderValue;
    //hace referencia al canvas , el cual le cambiare el alfa
    public Image panelBrillo;
    public Image ImageOptions ;
    private void Start() {
        //agregare el brillo por defecto
        slider.value = PlayerPrefs.GetFloat("Brillo",0.5f);
        //pasare el valor del alfa gracias al slider
        panelBrillo.color = new Color(panelBrillo.color.r , panelBrillo.color.g , panelBrillo.color.b , slider.value);
        ImageOptions.color = new Color(panelBrillo.color.r , panelBrillo.color.g , panelBrillo.color.b , slider.value);
    }
    public void ChangeSlider(float valor)
    {
        //pasare el valor del alfa gracias al slider
        sliderValue =  valor ;
        //actualizo el brillo
        PlayerPrefs.SetFloat("Brillo",sliderValue);
        //cambio el brillo
        panelBrillo.color = new Color(panelBrillo.color.r , panelBrillo.color.g , panelBrillo.color.b , slider.value); 
        ImageOptions.color = new Color(panelBrillo.color.r , panelBrillo.color.g , panelBrillo.color.b , slider.value); 
    }
}