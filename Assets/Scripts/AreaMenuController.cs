using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AreaMenuController : MonoBehaviour
{
    public GameObject areaMenuCanvas;
    public GameObject areaMenuSubtitle;

    public void menuMostrar(string areaName)
    {
        areaMenuSubtitle.GetComponent<Text>().text = areaName;
        areaMenuCanvas.SetActive(true);
    }

    public void menuCancelar()
    {
        areaMenuCanvas.SetActive(false);
    }

    public void menuEntrar()
    {
        SceneManager.LoadScene("Battle");
    }
}
