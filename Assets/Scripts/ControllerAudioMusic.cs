using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerAudioMusic : MonoBehaviour
{

    private AudioSource MusicSrc;
    public AudioClip Music;
    public Slider SliderMusic;
    private static ControllerAudioMusic controladorMusica;
    //Aqui agregan mas si quieren mas musicas
    //public AudioClip Music1;
    //public AudioClip Music2;

    // Start is called before the first frame update
    void Awake()
    {
        MusicSrc = GetComponent<AudioSource>(); //Se asigna el audio que se controlara
        InicializarVolumenMusic();
        MusicSrc.Play();

        DontDestroyOnLoad(this);

        if (controladorMusica == null)
        {
            controladorMusica = this;
        }
        else
        {
            Destroy(this.gameObject); // Used Destroy instead of DestroyObject
        }


    }
    private void InicializarVolumenMusic()
    {
        MusicSrc.volume = PlayerPrefs.GetFloat("MusicVolumen", 1.0f);
        SliderMusic.value = MusicSrc.volume;
    }
    void Update()
    {
        MusicSrc.volume = SliderMusic.value;
        PlayerPrefs.SetFloat("MusicVolumen", MusicSrc.volume);
        PlayerPrefs.Save();
    }


    // Update is called once per frame


}
