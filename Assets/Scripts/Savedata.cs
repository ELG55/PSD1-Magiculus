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

    private Savedata saveInstance;

    public GameObject name;
    public GameObject nivel;
    public GameObject clase;
    public GameObject fecha;
    public GameObject progreso;
    public GameObject realizado;
    public GameObject recibido;
    public GameObject porcentaje;


    public void SalvarDatos()
    {
        SaveManager.Salvar(this);
    }
    public void CargarDatos()
    {
        Savedata data = SaveManager.Cargar();

        slot = data.slot;
        mageName = data.mageName;
        level = data.level;
        mageClass = data.mageClass;
        date = data.date;
        progress = data.progress;
        dmgDone = data.dmgDone;
        dmgReceived = data.dmgReceived;
        hitP = data.hitP;
    }
    public void NewGame()
    {
        slot = 1;
        mageName = "0ofM4ster69";
        level = 1;
        mageClass = "Jiler XD";
        date = "Hoy";
        progress = ".-.XD";
        dmgDone = 0;
        dmgReceived = 0;
        hitP = 0;

        RefreshData();
    }
    public void LevelUp()
    {
        level++;
    }
    public void RefreshData()
    {
        name.GetComponent<Text>().text = "Nombre: "+mageName;
        nivel.GetComponent<Text>().text = "Nivel: "+level.ToString();
        clase.GetComponent<Text>().text = "Clase: "+mageClass;
        progreso.GetComponent<Text>().text = "Progreso: \n"+progress;
        realizado.GetComponent<Text>().text = "DMG realizado: \n"+dmgDone.ToString();
        recibido.GetComponent<Text>().text = "DMG recibido: \n"+dmgReceived.ToString();
        porcentaje.GetComponent<Text>().text = "Hit% total: \n"+hitP.ToString();
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
}