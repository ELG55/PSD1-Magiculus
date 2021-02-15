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

    public void menuMostrar(string areaName)
    {
        if (!worldMapController.GetComponent<WorldMapController>().isAMenuShowing)
        {
            areaMenuSubtitle.GetComponent<Text>().text = areaName;
            areaMenuCanvas.SetActive(true);
            worldMapController.GetComponent<WorldMapController>().isAMenuShowing = true;
        }
    }

    public void menuCancelar()
    {
        areaMenuCanvas.SetActive(false);
        worldMapController.GetComponent<WorldMapController>().isAMenuShowing = false;
    }

    public void menuEntrar()
    {
        SceneManager.LoadScene("Battle");
    }
}
