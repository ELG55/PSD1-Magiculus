using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public List<GameObject> hideCanvases;

    public GameObject soundManager;
    public ControllerAudio audioController;

    void Awake()
    {
        soundManager = GameObject.Find("SoundManager");
        for (int i = 0; i < hideCanvases.Count; i++)
        {
            hideCanvases[i].SetActive(false);
        }
    }

    private void Start()
    {
        audioController = soundManager.GetComponent<ControllerAudio>();
    }
    public void hideCanvas(GameObject canvas)
    {
        audioController.PlaySound(audioController.sndWindow);
        canvas.SetActive(false);
    }

    public void showCanvas(GameObject canvas)
    {
        audioController.PlaySound(audioController.sndWindow);
        canvas.SetActive(true);
    }
}
