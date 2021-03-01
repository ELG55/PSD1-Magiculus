using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AreaMenuController : MonoBehaviour
{
    public GameObject areaMenuCanvas;
    public GameObject areaMenuSubtitle;
    public GameObject worldMapController;

    public GameObject soundManager;
    public ControllerAudio audioController;

    public string currentLevel;

    private bool isEntranceAnimationRunning = false;
    public GameObject entranceBlackRectangleLeft;
    public GameObject entranceBlackRectangleRight;
    private Timer timer;
    public GameObject buttonEntrar;
    public GameObject buttonCancelar;

    void Awake()
    {
        soundManager = GameObject.Find("SoundManager");
    }

    void Start()
    {
        audioController = soundManager.GetComponent<ControllerAudio>();
        //Animation's end wait time
        timer = new Timer(0.5f, false);
    }

    void Update()
    {
        timer.Update();
        if (isEntranceAnimationRunning)
        {
            EntranceAnimationUpdate();
        }
        if (timer.IsTimerDone())
        {
            //Animation ended, go to next scene
            SceneManager.LoadScene("Battle");
        }
    }

    private void EntranceAnimationUpdate()
    {
        if (entranceBlackRectangleLeft.GetComponent<Transform>().position.y > 0)
        {
            Vector3 tempMove = new Vector3(0, 4.5f, 0);
            entranceBlackRectangleLeft.GetComponent<Transform>().position -= tempMove * Time.deltaTime;
            entranceBlackRectangleRight.GetComponent<Transform>().position += tempMove * Time.deltaTime;
        }
        else
        {
            timer.ResumeTimer();
        }
    }

    public void MenuMostrar(string areaName)
    {
        if (!worldMapController.GetComponent<WorldMapController>().isAMenuShowing)
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
            areaMenuSubtitle.GetComponent<Text>().text = areaName;
            areaMenuCanvas.SetActive(true);
            worldMapController.GetComponent<WorldMapController>().isAMenuShowing = true;
        }
    }

    public void MenuCancelar()
    {
        audioController.PlaySound(audioController.sndWindow);
        areaMenuCanvas.SetActive(false);
        worldMapController.GetComponent<WorldMapController>().isAMenuShowing = false;
    }

    public void MenuEntrar()
    {
        worldMapController.GetComponent<WorldMapController>().isAMenuShowing = true;
        buttonEntrar.GetComponent<Button>().interactable = false;
        buttonCancelar.GetComponent<Button>().interactable = false;
        //Stop music and try to play 2 sounds; the battle entrance sound is more important
        worldMapController.GetComponent<WorldMapController>().controllerAudioMusic.StopSong();
        audioController.PlaySound(audioController.sndClick);
        audioController.PlaySound(audioController.sndEnteringBattle);
        GameObject.Find("Savedata").GetComponent<Savedata>().currentLevel = currentLevel;
        isEntranceAnimationRunning = true;
    }

    public void SetLevel(string level)
    {
        currentLevel = level;
    }
}
