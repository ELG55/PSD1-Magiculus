using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseMenuController : MonoBehaviour
{
    public GameObject baseMenuCanvas;
    public GameObject worldMapController;

    public GameObject soundManager;
    public ControllerAudio audioController;

    void Awake()
    {
        soundManager = GameObject.Find("SoundManager");
    }

    private void Start()
    {
        audioController = soundManager.GetComponent<ControllerAudio>();
    }

    public void menuMostrar()
    {
        if (audioController == null)
        {
            if (soundManager == null)
            {
                soundManager = GameObject.Find("SoundManager");
            }
            audioController = soundManager.GetComponent<ControllerAudio>();
        }
        audioController.PlaySound(audioController.sndWindow);
        if (!worldMapController.GetComponent<WorldMapController>().isAMenuShowing)
        {
            baseMenuCanvas.SetActive(true);
            worldMapController.GetComponent<WorldMapController>().isAMenuShowing = true;
        }
    }

    public void menuCancelar()
    {
        audioController.PlaySound(audioController.sndWindow);
        baseMenuCanvas.SetActive(false);
        worldMapController.GetComponent<WorldMapController>().isAMenuShowing = false;
    }

    public void menuSalir()
    {
        audioController.PlaySound(audioController.sndClick);
        SceneManager.LoadScene("Title");
    }
}
