using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;

namespace GameIn2D.Settings
{


    public class ScreenManager : MonoBehaviour
    {
        [SerializeField] private GameSettingsController settingsController;

        public Toggle toggle;
        //referencia al Cbo de las resoluciones
        public TMP_Dropdown ResolutionCbo;
        //arreglo para almacenar todas las resoluciones disponibles del pc en donde se ejecute el juego
        Resolution[] resolutions;

        private void Start()
        {
            if (Screen.fullScreen)
            {
                toggle.isOn = true;
            }
            else
            {
                toggle.isOn = false;
            }

            ChangeResolution();
        }

        public void ActualizarPantallaCompleta(bool pantallaCompleta)
        {
            settingsController.ApplyFullscreen(pantallaCompleta);
        }

        public void ChangeResolution()
        {
            resolutions = Screen.resolutions;
            //limpiamos las opciones predefinidas del cbo
            ResolutionCbo.ClearOptions();
            //resolucion actual
            int ActualResolution = 0;
            //creo una lista de las resoluciones para agregar los valores en el cbo
            List<string> options = new List<string>();
            options = options.Distinct().ToList();
            //detecto la cantidad de resolciones disponibles
            for (int i = 0; i < resolutions.Length; i++)
            {
                //tomo el alto y ancho de la imagen y lo agrego en las opciones para mostrar el valor en el cbo
                string option = resolutions[i].width + " x " + resolutions[i].height;
                if (!options.Contains(option)) {
                    options.Add(option);
                }
                //revisa si la opcion guardada es la resolucion actual
                if (Screen.fullScreen && resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                {
                    ActualResolution = i;
                }
            }
            //limpiar las opciones predefinidas antes de agregar nuevas opciones
            ResolutionCbo.ClearOptions();
            ResolutionCbo.AddOptions(options);
            ResolutionCbo.value = ActualResolution;
            ResolutionCbo.RefreshShownValue();
            //resolucion por defecto
            ResolutionCbo.value = settingsController.LoadResolutionIndex();
        }

        public void ChangeResolution(int IndexResolution)
        {
            Resolution resolution = resolutions[IndexResolution];
            settingsController.ApplyResolution(resolution, ResolutionCbo.value);
        }
    }
}
