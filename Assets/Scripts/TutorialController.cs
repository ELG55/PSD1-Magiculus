using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialController : MonoBehaviour
{
    public List<TutorialGuide> guideList;

    public int currentGuide { get; set; } = 0;
    public int currentSlide { get; set; } = 0;

    public GameObject imageHolder;
    public GameObject textHolder;
    public GameObject nextButton;
    public GameObject previousButton;

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

    public void ShowInitialSlide()
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
        currentSlide = 0;
        imageHolder.GetComponent<Image>().overrideSprite = guideList[currentGuide].spriteList[currentSlide];
        textHolder.GetComponent<Text>().text = guideList[currentGuide].stringList[currentSlide];
        updateNextAndPreviousButtons();
    }

    public void NextSlide()
    {
        audioController.PlaySound(audioController.sndCursorUp);
        if (currentSlide + 1 <= guideList[currentGuide].spriteList.Count)
        {
            currentSlide++;
            imageHolder.GetComponent<Image>().overrideSprite = guideList[currentGuide].spriteList[currentSlide];
            if (currentSlide + 1 <= guideList[currentGuide].stringList.Count)
            {
                textHolder.GetComponent<Text>().text = guideList[currentGuide].stringList[currentSlide];
            }
        }
        updateNextAndPreviousButtons();
    }

    public void PreviousSlide()
    {
        audioController.PlaySound(audioController.sndCursorDown);
        if (currentSlide - 1 >= 0)
        {
            currentSlide--;
            imageHolder.GetComponent<Image>().overrideSprite = guideList[currentGuide].spriteList[currentSlide];
            textHolder.GetComponent<Text>().text = guideList[currentGuide].stringList[currentSlide];
        }
        updateNextAndPreviousButtons();
    }

    private void updateNextAndPreviousButtons()
    {
        //Check if there's another slide to keep or hide the next button
        if (currentSlide >= guideList[currentGuide].spriteList.Count - 1)
        {
            nextButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            nextButton.GetComponent<Button>().interactable = true;
        }
        //Check if there's a previous slide to keep or hide the previous button
        if (currentSlide <= 0)
        {
            previousButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            previousButton.GetComponent<Button>().interactable = true;
        }
    }
}
