using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayRandomSound : MonoBehaviour
{
    public AudioSource _As;
    //arreglo para agregar los temas
    public AudioClip[] AudioClipArray;

    private int TemaActual = 14;

    private void Start()
    {
        //_As.clip = AudioClipArray[Random.Range(0,AudioClipArray.Length)];
        _As = this.GetComponent<AudioSource>();
        _As.clip = AudioClipArray[14];
        _As.Play();
        _As.loop = true;
    }
    public void TemaSiguiente()
    {
        _As.Pause();
        if (TemaActual == 16)
        {
            TemaActual = 0;
            _As.clip = AudioClipArray[TemaActual];
            _As.Play();
            _As.loop = true;
        }
        else
        {
            TemaActual++;
            _As.clip = AudioClipArray[TemaActual];
            _As.Play();
            _As.loop = true;
        }
        //Debug.Log(TemaActual);
    }
    public void TemaAnterior()
    {
        _As.Pause();
        if (TemaActual == 0)
        {
            TemaActual = 16;
            _As.clip = AudioClipArray[TemaActual];
            _As.Play();
            _As.loop = true;
        }
        else
        {
            TemaActual--;
            _As.clip = AudioClipArray[TemaActual];
            _As.Play();
            _As.loop = true;
        }
    }
}
