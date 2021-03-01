using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BaseMenuController : MonoBehaviour
{
    public GameObject baseMenuCanvas;
    public GameObject worldMapController;

    public void menuMostrar()
    {
        if (!worldMapController.GetComponent<WorldMapController>().isAMenuShowing)
        {
            baseMenuCanvas.SetActive(true);
            worldMapController.GetComponent<WorldMapController>().isAMenuShowing = true;
        }
    }

    public void menuCancelar()
    {
        baseMenuCanvas.SetActive(false);
        worldMapController.GetComponent<WorldMapController>().isAMenuShowing = false;
    }

    public void menuSalir()
    {
        SceneManager.LoadScene("Title");
    }
}
