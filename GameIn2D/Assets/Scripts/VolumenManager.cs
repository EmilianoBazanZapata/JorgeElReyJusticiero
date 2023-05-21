using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumenManager : MonoBehaviour
{
    //hace referencia al slider en pantalla
    public Slider slider;
    //variable para manejar el volumen
    public float sliderValue;

    private void Start() {
        //guardamos el volumen para que se mantengan los cambios
        slider.value = PlayerPrefs.GetFloat("VolumenAudio",5f);
        //el volumen del juego tomara el valor del slider
        AudioListener.volume = slider.value;
    }
    public void ChangeSlider(float valor)
    {
        //pasmos el valor que pasare por el slider
        sliderValue = valor;
        //actualizamos el valor de la musica por defecto
        PlayerPrefs.SetFloat("VolumenAudio",sliderValue);
        //el audio general se vera afectado por el slider
        AudioListener.volume = slider.value;
    }
}
