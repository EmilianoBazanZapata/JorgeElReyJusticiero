using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameIn2D.Settings
{

    public class QualityManager : MonoBehaviour
    {
        [SerializeField] private GameSettingsController settingsController;

        //tomo el cbo de la calidad
        public TMP_Dropdown dropdown;
        public int calidad;
        private void Start()
        {
            //el valor por defecto de la calidad es 3
            calidad = settingsController.LoadQuality();
        }

        public void AjustarClidad()
        {
            UnityEngine.Debug.Log(dropdown.value);
            // Cambiar el nivel de calidad
            settingsController.ApplyQuality(dropdown.value);
        }
    }
}
