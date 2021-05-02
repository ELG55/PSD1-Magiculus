using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Savedata : MonoBehaviour
{

    //Save variables
    public int slot;
    public string mageName;
    public int level;
    public string mageClass;
    public string date;
    public string progress;
    public float dmgDone;
    public float dmgReceived;
    public float hitP;
    public float percentSum;
    public float percentTimes;

    public string currentLevel;

    private static Savedata saveInstance;

    public GameObject controller;


    public void SalvarDatos()
    {
        mageName = controller.GetComponent<CargarController>().getInputName();
        mageClass = controller.GetComponent<CargarController>().getInputClase();
        if (((mageName == "" && mageClass == "")||((mageName == "" || mageClass == "")))||((mageClass.Length>=11)||(mageName.Length>=11)))
        {
            controller.GetComponent<CargarController>().showErrorMsg();

        }
        else
        {
            SaveFile sv = new SaveFile(slot, mageName, 1, mageClass, System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), "A0B0C0D0", 0, 0, 0, 0, 0);
            SaveManager.Salvar(sv);
            Debug.Log("Has de cuenta que ya corrio el juego");
            UpdateSavedata(sv);
            controller.GetComponent<CargarController>().changeScene();
        }

    }
    public void UpdateSavedata(SaveFile sv)
    {
        slot = sv.slot;
        mageName = sv.mageName;
        level = sv.level;
        mageClass = sv.mageClass;
        date = sv.date;
        progress = sv.progress;
        dmgDone = sv.dmgDone;
        dmgReceived = sv.dmgReceived;
        hitP = sv.hitP;
        percentSum = sv.percentSum;
        percentTimes = sv.percentTimes;
    }

    public void CargarDatos(int casilla)
    {
        slot = casilla;
        SaveFile data = SaveManager.Cargar(casilla);
        if (data == null)
        {
            controller.GetComponent<CargarController>().OcultarDatos();
            controller.GetComponent<CargarController>().MostrarInputs();
        }
        else
        {
            controller.GetComponent<CargarController>().OcultarInputs();
            slot = data.slot;
            mageName = data.mageName;
            level = data.level;
            mageClass = data.mageClass;
            date = data.date;
            progress = data.progress;
            dmgDone = data.dmgDone;
            dmgReceived = data.dmgReceived;
            hitP = data.hitP;
            percentSum = data.percentSum;
            percentTimes = data.percentTimes;
            controller.GetComponent<CargarController>().RefreshData(date, mageName, level.ToString(), mageClass, progress, dmgDone.ToString(), dmgReceived.ToString(), hitP.ToString());
            controller.GetComponent<CargarController>().MostrarDatos();
        }
    }
    public void BorrarDatos()
    {
        SaveManager.Borrar(slot);
        slot = 0;
        mageName = "";
        level = 0;
        mageClass = "";
        date = "";
        progress = "";
        dmgDone = 0;
        dmgReceived = 0;
        hitP = 0;
        controller.GetComponent<CargarController>().OcultarDatos();
        controller.GetComponent<CargarController>().OcultarInputs();

    }
    public static void Yell()
    {
        Debug.Log("Brgaaaaaaaaaaaaaaa Comienza");
    }

    public void SalvarProgreso(int selectSlot)
    {
        if (selectSlot==slot)
        {        }
        else
        {
            slot = selectSlot;
        }
        if (percentTimes == 0)
        {
            hitP = 0;
        }
        else
        {
            hitP = Mathf.Round(percentSum / percentTimes);
        }
        SaveFile sv = new SaveFile(slot, mageName, level, mageClass, System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), progress, dmgDone, dmgReceived, hitP, percentSum, percentTimes);
        SaveManager.Salvar(sv);
        UpdateSavedata(sv);
    }

    void Awake()
    {
        DontDestroyOnLoad(this);

        if (saveInstance == null)
        {
            saveInstance = this;
        }
        else
        {
            Destroy(gameObject); // Used Destroy instead of DestroyObject
        }
    }

    private void OnLevelWasLoaded(int level)
    {
        controller = GameObject.Find("CargarController");
    }
}