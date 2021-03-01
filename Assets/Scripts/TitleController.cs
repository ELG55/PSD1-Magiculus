using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleController : MonoBehaviour
{
    public GameObject musicController;
    public ControllerAudioMusic controllerAudioMusic;

    public Slider sliderMusic;
    public Slider sliderSound;

    public Toggle toggleCompleta;
    public Toggle toggleVentana;

    // Start is called before the first frame update
    void Start()
    {
        controllerAudioMusic = musicController.GetComponent<ControllerAudioMusic>();
        controllerAudioMusic.PlaySong(controllerAudioMusic.bgmTitle);

        sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume", 0.2f);
        sliderSound.value = PlayerPrefs.GetFloat("SoundVolume", 0.4f);

        if (PlayerPrefs.GetInt("Fullscreen", 1) == 1)
        {
            toggleCompleta.isOn = false;
            toggleVentana.isOn = true;
            SetFullscreen(true);
        }
        else
        {
            toggleCompleta.isOn = true;
            toggleVentana.isOn = false;
            SetFullscreen(false);
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetFloat("MusicVolume", 0.2f) != sliderMusic.value)
        {
            controllerAudioMusic.MusicSrc.volume = sliderMusic.value;
            PlayerPrefs.SetFloat("MusicVolumen", sliderMusic.value);
            PlayerPrefs.Save();
        }
        if (PlayerPrefs.GetFloat("SoundVolume", 0.4f) != sliderSound.value)
        {
            PlayerPrefs.SetFloat("SoundVolume", sliderSound.value);
            PlayerPrefs.Save();
        }
    }

    void Awake()
    {
        musicController = GameObject.Find("MusicController");
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.SetResolution(1920, 1080, fullscreen);
    }

    public void TogglePantalla()
    {
        if (toggleCompleta.isOn)
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
            SetFullscreen(true);
        }
        else
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
            SetFullscreen(false);
        }
        
    }
}
