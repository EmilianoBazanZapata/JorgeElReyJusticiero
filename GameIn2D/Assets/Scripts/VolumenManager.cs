using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumenManager : MonoBehaviour
{
    [SerializeField] private GameSettingsController settingsController;

    //hace referencia al slider en pantalla
    public Slider slider;
    //variable para manejar el volumen
    public float sliderValue;

    private void Start() {
        //guardamos el volumen para que se mantengan los cambios
        slider.value = settingsController.LoadVolume();
        //el volumen del juego tomara el valor del slider
        settingsController.ApplyVolume(slider.value);
    }
    public void ChangeSlider(float valor)
    {
        //pasmos el valor que pasare por el slider
        sliderValue = valor;
        //actualizamos el valor de la musica por defecto
        settingsController.SaveVolume(sliderValue);
        //el audio general se vera afectado por el slider
        settingsController.ApplyVolume(slider.value);
    }
}
