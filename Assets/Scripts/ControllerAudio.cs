using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerAudio : MonoBehaviour
{
    private AudioSource audioSrc;
    private static ControllerAudio controllerAudio;
    //Aqui poner los audios de los distintos botones u ataques
    /*Ejemplo
     * public AudioClip SonidoAcierto;
     */
    public AudioClip sound1;
    public AudioClip sound2;
    public AudioClip Coin;
    public bool checador = false;

    public Slider SliderSound;


    void Awake()
    {
        audioSrc = GetComponent<AudioSource>(); //Se asigna el audio que se controlara
        InicializarVolumen();
        DontDestroyOnLoad(this);

        if (controllerAudio == null)
        {
            controllerAudio = this;
        }
        else
        {
            Destroy(this.gameObject); // Used Destroy instead of DestroyObject
        }
    }

    private void InicializarVolumen() {
            audioSrc.volume = PlayerPrefs.GetFloat("SoundVolumen", 1.0f);
            SliderSound.value = audioSrc.volume;
        
    }
    void Update() {
            audioSrc.volume = SliderSound.value;
            PlayerPrefs.SetFloat("SoundVolumen", audioSrc.volume);
            PlayerPrefs.Save();
    }

    public void PMusic() {
        audioSrc.PlayOneShot(sound1);

    }
    public void PSound() {
        audioSrc.PlayOneShot(sound2);
    }

    public void coin() {
        audioSrc.PlayOneShot(Coin);
    }

    /*Crear un metodo para reproducir ese sonido especifico
      public void Acierto(){
           audioSrc.clip = SonidoAcierto;
           audioScr.play();
      }
     
    */

}
