using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CreditsController : MonoBehaviour
{
    public enum CreditsStage { EntranceAnimation, CreditsRollout, ExitAnimation }
    private CreditsStage currentStage = CreditsStage.EntranceAnimation;

    public GameObject entranceBlackRectangleLeft;
    public GameObject entranceBlackRectangleRight;

    public GameObject creditsBackground;
    public GameObject creditsBackground01;

    public GameObject creditCards;
    public GameObject creditCard01;
    public float totalCreditCards;
    public float spaceBetweenCreditCards;
    public float firstCreditCardBuffer;
    public float lastCreditCardBuffer;

    private float creditCardsAnimationSpeed;
    public float creditCardsAnimationSpeedNormal;
    public float creditCardsAnimationSpeedFast;
    private bool isFastSpeedEnabled;

    public GameObject buttonExit;
    public GameObject buttonSpeed;

    public GameObject musicController;
    public ControllerAudioMusic controllerAudioMusic;

    public GameObject soundManagerObject;
    public ControllerAudio soundManager;

    public Savedata savedata;

    private float startingVolume;
    public float volumeFadeSpeed;

    private float animationPause = 8;

    // Start is called before the first frame update

    private void Awake()
    {
        musicController = GameObject.Find("MusicController");
        savedata = GameObject.Find("Savedata").GetComponent<Savedata>();
    }
    void Start()
    {
        //Initialize music
        controllerAudioMusic = musicController.GetComponent<ControllerAudioMusic>();
        controllerAudioMusic.PlaySong(controllerAudioMusic.bgmCredits);
        startingVolume = controllerAudioMusic.MusicSrc.volume;
        controllerAudioMusic.MusicSrc.volume = 0f;

        soundManager = soundManagerObject.GetComponent<ControllerAudio>();

        DisableButtons();

        creditCardsAnimationSpeed = creditCardsAnimationSpeedNormal;
    }

    // Update is called once per frame
    void Update()
    {
        CreditsBackgroundUpdate();
        if (CreditsAnimationUpdate())
        {
            DisableButtons();
            currentStage = CreditsStage.ExitAnimation;
        }
        switch (currentStage)
        {
            case CreditsStage.EntranceAnimation:
                if (EntranceAnimationUpdate())
                {
                    EnableButtons();
                    currentStage = CreditsStage.CreditsRollout;
                }
                break;
            case CreditsStage.CreditsRollout:
                break;
            case CreditsStage.ExitAnimation:
                if (ExitAnimationUpdate())
                {
                    SceneManager.LoadScene(savedata.afterCreditsScene);
                }
                break;
            default:
                break;
        }
    }

    private bool EntranceAnimationUpdate()
    {
        if (entranceBlackRectangleLeft.GetComponent<Transform>().position.x > -14)
        {
            Vector3 tempMove = new Vector3(5.0f, 0, 0);
            entranceBlackRectangleLeft.GetComponent<Transform>().position -= tempMove * Time.deltaTime;
            entranceBlackRectangleRight.GetComponent<Transform>().position += tempMove * Time.deltaTime;
            if (controllerAudioMusic.MusicSrc.volume < startingVolume)
            {
                controllerAudioMusic.MusicSrc.volume += startingVolume * (volumeFadeSpeed/100);
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool ExitAnimationUpdate()
    {
        if (entranceBlackRectangleLeft.GetComponent<Transform>().position.x < -5)
        {
            Vector3 tempMove = new Vector3(3.0f, 0, 0);
            entranceBlackRectangleLeft.GetComponent<Transform>().position += tempMove * Time.deltaTime;
            entranceBlackRectangleRight.GetComponent<Transform>().position -= tempMove * Time.deltaTime;
            if (controllerAudioMusic.MusicSrc.volume > 0f)
            {
                controllerAudioMusic.MusicSrc.volume -= startingVolume * (volumeFadeSpeed / 100) * 0.6f;
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool CreditsAnimationUpdate()
    {
        if (creditCard01.GetComponent<Transform>().position.x > -(firstCreditCardBuffer + ((totalCreditCards-1) * spaceBetweenCreditCards) + lastCreditCardBuffer))
        {
            if ((Mathf.Round(creditCard01.GetComponent<Transform>().position.x + 15.5f) % 15 == 0) && (animationPause > 0) && (creditCard01.GetComponent<Transform>().position.x < 10))
            {
                animationPause -= creditCardsAnimationSpeed*Time.deltaTime;
            }
            else
            {
                if ((Mathf.Round(creditCard01.GetComponent<Transform>().position.x + 16.5f) % 15 == 0))
                {
                    animationPause = 5;
                }
                Vector3 tempMove = new Vector3(creditCardsAnimationSpeed, 0, 0);
                creditCards.GetComponent<Transform>().position -= tempMove * Time.deltaTime;
            }
            return false;
        }
        else
        {
            return true;
        }
    }

    private bool CreditsBackgroundUpdate()
    {
        if (creditsBackground01.GetComponent<Transform>().position.x > -38.2)
        {

            Vector3 tempMove = new Vector3(0.2f, 0, 0);
            creditsBackground.GetComponent<Transform>().position -= tempMove * Time.deltaTime;
            return false;
        }
        else
        {
            return true;
        }
    }

    private void DisableButtons()
    {
        buttonExit.GetComponent<Button>().interactable = false;
        buttonSpeed.GetComponent<Button>().interactable = false;
    }

    private void EnableButtons()
    {
        buttonExit.GetComponent<Button>().interactable = true;
        buttonSpeed.GetComponent<Button>().interactable = true;
    }
    
    public void SwitchAnimationSpeed()
    {
        if (isFastSpeedEnabled)
        {
            creditCardsAnimationSpeed = creditCardsAnimationSpeedNormal;
            isFastSpeedEnabled = false;
            buttonSpeed.transform.GetChild(0).GetComponent<Text>().text = "Normal";
        }
        else
        {
            creditCardsAnimationSpeed = creditCardsAnimationSpeedFast;
            isFastSpeedEnabled = true;
            buttonSpeed.transform.GetChild(0).GetComponent<Text>().text = "Rápido";
        }
    }

    public void ExitCredits()
    {
        soundManager.PlaySound(soundManager.sndClick);
        currentStage = CreditsStage.ExitAnimation;
        DisableButtons();
    }
}
