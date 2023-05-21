using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BarType
{
    Healt,
    Mana
}
public class PlayerBar : MonoBehaviour
{ 
    private Slider slider;
    public BarType Type;
    // Start is called before the first frame update
    void Start()
    {
        this.slider = GetComponent<Slider>();
    }

    // de esta forma se mostrara en el slider la cantidad de vida y mana que el usuario tiene en tiempo real
    void Update()
    {
        switch(Type)
        {
            case BarType.Healt:
                slider.value = PlayerController.sharedInstance.GetHealth();
                break;
            case BarType.Mana:
                slider.value = PlayerController.sharedInstance.GetMana();
                break;
        }
    }
}

