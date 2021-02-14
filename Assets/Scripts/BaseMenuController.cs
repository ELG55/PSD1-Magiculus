using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BaseMenuController : MonoBehaviour
{
    public GameObject baseMenuCanvas;

    public void menuMostrar()
    {
        baseMenuCanvas.SetActive(true);
    }

    public void menuCancelar()
    {
        baseMenuCanvas.SetActive(false);
    }

    public void menuSalir()
    {
        SceneManager.LoadScene("Title");
    }

    public void menuOpciones()
    {
        //TO BE DONE

        //Add the canvas from the TitleScene when the options window is ready


        //TO BE DONE
    }

    public void menuGuardar()
    {
        //TO BE DONE

        //Add the canvas from the TitleScene when the save/load script and window are ready


        //TO BE DONE
    }
}
