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

    public string currentLevel;

    public void MenuMostrar(string areaName)
    {
        if (!worldMapController.GetComponent<WorldMapController>().isAMenuShowing)
        {
            areaMenuSubtitle.GetComponent<Text>().text = areaName;
            areaMenuCanvas.SetActive(true);
            worldMapController.GetComponent<WorldMapController>().isAMenuShowing = true;
        }
    }

    public void MenuCancelar()
    {
        areaMenuCanvas.SetActive(false);
        worldMapController.GetComponent<WorldMapController>().isAMenuShowing = false;
    }

    public void MenuEntrar()
    {
        GameObject.Find("Savedata").GetComponent<Savedata>().currentLevel = currentLevel;
        SceneManager.LoadScene("Battle");
    }

    public void SetLevel(string level)
    {
        currentLevel = level;
    }
}
