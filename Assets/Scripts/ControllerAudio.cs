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
    public AudioClip sndClick;
    public AudioClip sndCursorDown;
    public AudioClip sndCursorUp;
    public AudioClip sndDamage;
    public AudioClip sndDefeat;
    public AudioClip sndDraw;
    public AudioClip sndEnteringBattle;
    public AudioClip sndNewRound;
    public AudioClip sndWin;
    public AudioClip sndWindow;

    //public AudioClip sound2;
    //public AudioClip Coin;
    //public bool checador = false;

    //public Slider SliderSound;


    void Awake()
    {
        audioSrc = GetComponent<AudioSource>(); //Se asigna el audio que se controlara
        InicializarVolumen();
        
        /*DontDestroyOnLoad(this);

        if (controllerAudio == null)
        {
            controllerAudio = this;
        }
        else
        {
            Destroy(this.gameObject); // Used Destroy instead of DestroyObject
        }*/
    }

    public void InicializarVolumen() {
            audioSrc.volume = PlayerPrefs.GetFloat("SoundVolume", 0.4f);
            //SliderSound.value = audioSrc.volume;
        
    }
    /*
    void Update() {
            audioSrc.volume = SliderSound.value;
            PlayerPrefs.SetFloat("SoundVolumen", audioSrc.volume);
            PlayerPrefs.Save();
    }
    */
    /*
    public void PMusic() {
        audioSrc.PlayOneShot(sound1);

    }
    public void PSound() {
        audioSrc.PlayOneShot(sound2);
    }

    public void coin() {
        audioSrc.PlayOneShot(Coin);
    }
    */
    public void PlaySound(AudioClip audioClip)
    {
        InicializarVolumen();
        audioSrc.PlayOneShot(audioClip);
    }

    /*Crear un metodo para reproducir ese sonido especifico
      public void Acierto(){
           audioSrc.clip = SonidoAcierto;
           audioScr.play();
      }
     
    */

}
