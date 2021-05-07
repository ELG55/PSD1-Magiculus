using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldMapController : MonoBehaviour
{
    public GameObject baseMenuCanvas;
    public GameObject areaMenuCanvas;
    public bool isAMenuShowing { get; set; } = false;

    public GameObject musicController;
    public ControllerAudioMusic controllerAudioMusic;
    public GameObject soundManager;
    public ControllerAudio audioController;

    public Slider sliderMusic;
    public Slider sliderSound;

    public Toggle toggleCompleta;
    public Toggle toggleVentana;

    public Savedata savedata;
    public GameObject textCampeon;
    public GameObject starAreaA;
    public GameObject starAreaB;
    public GameObject starAreaC;
    public GameObject starAreaD;
    public GameObject finalZoneImage;

    public GameObject fieldConectar;
    public GameObject buttonConectar;
    public GameObject textStatus;

    public DBInterface dbInterface;

    void Awake()
    {
        musicController = GameObject.Find("MusicController");
        soundManager = GameObject.Find("SoundManager");
        savedata = GameObject.Find("Savedata").GetComponent<Savedata>();
        dbInterface = GameObject.Find("DBInterface").GetComponent<DBInterface>();
    }

    void Start()
    {
        controllerAudioMusic = musicController.GetComponent<ControllerAudioMusic>();
        audioController = soundManager.GetComponent<ControllerAudio>();
        sliderMusic.value = PlayerPrefs.GetFloat("MusicVolume", 0.2f);
        sliderSound.value = PlayerPrefs.GetFloat("SoundVolume", 0.4f);
        controllerAudioMusic.PlaySong(controllerAudioMusic.bgmWorldMap);
        fieldConectar.GetComponent<InputField>().text = PlayerPrefs.GetString("ServerIP", "");
        UpdateButtonConnect();
        if (PlayerPrefs.GetInt("Fullscreen", 1) == 1)
        {
            toggleCompleta.isOn = true;
            toggleVentana.isOn = false;
        }
        else
        {
            toggleCompleta.isOn = false;
            toggleVentana.isOn = true;
        }
        string savedataProgress = savedata.progress;
        char[] savedataProgressChars = savedataProgress.ToCharArray();
        //Debug.Log("Showing and hiding buttons");
        for (int i = 0; i < 4; i++)
        {
            int areaActive = (int)System.Char.GetNumericValue(savedataProgressChars[1 + (i * 2)]);
            if (areaActive >= 5)
            {
                GameObject.Find("StarArea" + savedataProgressChars[(i * 2)]).SetActive(true);
                //Debug.Log("areaProgress: " + areaActive);
            }
            else
            {
                GameObject.Find("StarArea" + savedataProgressChars[(i * 2)]).SetActive(false);
            }
            for (int j = 1; j <= 5; j++)
            {
                areaActive = (int)System.Char.GetNumericValue(savedataProgressChars[1 + (i * 2)]);
                if (areaActive < 5)
                {
                    areaActive++;
                    //Debug.Log("areaProgress: " + areaActive);
                }
                //Debug.Log("AreaButtonObject: " + "AreaButton" + savedataProgressChars[(i * 2)] + "" + j);
                if (j <= areaActive)
                {
                    GameObject.Find("AreaButton" + savedataProgressChars[(i * 2)] + "" + j).SetActive(true);
                }
                else
                {
                    GameObject.Find("AreaButton" + savedataProgressChars[(i * 2)] + "" + j).SetActive(false);
                }
            }
        }
        if (savedata.level >= 20)
        {
            finalZoneImage.SetActive(true);
            GameObject.Find("AreaButtonA6").SetActive(true);
        }
        else
        {
            finalZoneImage.SetActive(false);
            GameObject.Find("AreaButtonA6").SetActive(false);
        }
        if (savedata.level >= 21)
        {
            textCampeon.SetActive(true);
        }
        else
        {
            textCampeon.SetActive(false);
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetFloat("MusicVolume", 0.2f) != sliderMusic.value)
        {
            PlayerPrefs.SetFloat("MusicVolume", sliderMusic.value);
            PlayerPrefs.Save();
            controllerAudioMusic.MusicSrc.volume = PlayerPrefs.GetFloat("MusicVolume", 0.2f);
        }
        if (PlayerPrefs.GetFloat("SoundVolume", 0.4f) != sliderSound.value)
        {
            PlayerPrefs.SetFloat("SoundVolume", sliderSound.value);
            PlayerPrefs.Save();
        }
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.SetResolution(1920, 1080, fullscreen);
    }

    public void TogglePantalla()
    {
        if (audioController == null)
        {
            if (soundManager == null)
            {
                soundManager = GameObject.Find("SoundManager");
            }
            audioController = soundManager.GetComponent<ControllerAudio>();
        }
        audioController.PlaySound(audioController.sndClick);
        if (toggleCompleta.isOn)
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
            PlayerPrefs.Save();
            SetFullscreen(true);
        }
        else
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
            PlayerPrefs.Save();
            SetFullscreen(false);
        }
    }

    public void TryConnection()
    {
        dbInterface.Server = fieldConectar.GetComponent<InputField>().text;
        dbInterface.UpdateConnectionString();
        if (dbInterface.TryConnection())
        {
            PlayerPrefs.SetString("ServerIP", dbInterface.Server);
            PlayerPrefs.SetInt("ServerAutoConnect", 1);
        }
        else
        {
            PlayerPrefs.SetString("ServerIP", dbInterface.Server);
            PlayerPrefs.SetInt("ServerAutoConnect", 0);
        }
    }

    public void UpdateButtonConnect()
    {
        if (PlayerPrefs.GetInt("ServerAutoConnect", 0) == 0)
        {
            buttonConectar.transform.GetChild(0).GetComponent<Text>().text = "Conectar";
            textStatus.GetComponent<Text>().text = "Estado: desconectado";
        }
        else
        {
            buttonConectar.transform.GetChild(0).GetComponent<Text>().text = "Desactivar";
            textStatus.GetComponent<Text>().text = "Estado: conectado";
        }
    }

    public void ButtonConnect()
    {
        audioController.PlaySound(audioController.sndClick);
        if (PlayerPrefs.GetInt("ServerAutoConnect", 0) == 0)
        {
            TryConnection();
        }
        else
        {
            PlayerPrefs.SetInt("ServerAutoConnect", 0);
        }
        UpdateButtonConnect();
    }
}
