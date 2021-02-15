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

    public GameObject name1;
    public GameObject nivel;
    public GameObject clase;
    public GameObject fecha;
    public GameObject progreso;
    public GameObject realizado;
    public GameObject recibido;
    public GameObject porcentaje;
    public GameObject GuardarBoton;
    public GameObject Input1;
    public GameObject Input2;
    public GameObject ComenzarBoton;
    public GameObject BorrarBoton;
    public GameObject MagoImagen;


    public void SalvarDatos()
    {
        mageName = Input1.GetComponent<InputField>().text;
        mageClass = Input2.GetComponent<InputField>().text;
        if(mageName == "" || mageClass == "")
        {

        } else{
            SaveFile sv = new SaveFile(slot, mageName, 1, mageClass, System.DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), "A0B0C0D0", 0, 0, 0);
            SaveManager.Salvar(sv);
            Debug.Log("Has de cuenta que ya corrio el juego");
        }
        
    }
    public void CargarDatos(int casilla)
    {
        slot = casilla;
        SaveFile data = SaveManager.Cargar(casilla);
        if (data == null)
        {
            OcultarDatos();
            MostrarInputs();
        }
        else
        {
            OcultarInputs();
            slot = data.slot;
            mageName = data.mageName;
            level = data.level;
            mageClass = data.mageClass;
            date = data.date;
            progress = data.progress;
            dmgDone = data.dmgDone;
            dmgReceived = data.dmgReceived;
            hitP = data.hitP;
            RefreshData();
            MostrarDatos();
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
        OcultarDatos();
        OcultarInputs();

    }

    /*public void NewGame()
    {
        this.slot = 1;
        this.mageName = "0ofM4ster69";
        this.level = 1;
        this.mageClass = "Jiler XD";
        this.date = "Hoy";
        this.progress = ".-.XD";
        this.dmgDone = 0;
        this.dmgReceived = 0;
        this.hitP = 0;
    }*/

    public static void Yell()
    {
        Debug.Log("Brgaaaaaaaaaaaaaaa Comienza");
    }

    public void MostrarInputs()
    {
        GuardarBoton.SetActive(true);
        Input1.SetActive(true);
        Input2.SetActive(true);
    }
    public void MostrarDatos()
    {
        name1.SetActive(true);
        nivel.SetActive(true);
        clase.SetActive(true);
        progreso.SetActive(true);
        realizado.SetActive(true);
        recibido.SetActive(true);
        porcentaje.SetActive(true);
        fecha.SetActive(true);
        ComenzarBoton.SetActive(true);
        BorrarBoton.SetActive(true);
        MagoImagen.SetActive(true);
    }
    public void OcultarInputs()
    {
        GuardarBoton.SetActive(false);
        Input1.SetActive(false);
        Input2.SetActive(false);
    }
    public void OcultarDatos()
    {
        name1.SetActive(false);
        nivel.SetActive(false);
        clase.SetActive(false);
        progreso.SetActive(false);
        realizado.SetActive(false);
        recibido.SetActive(false);
        porcentaje.SetActive(false);
        fecha.SetActive(false);
        ComenzarBoton.SetActive(false);
        BorrarBoton.SetActive(false);
        MagoImagen.SetActive(false);
    }


    public void RefreshData()
    {
        fecha.GetComponent<Text>().text = "Fecha: " + date;
        name1.GetComponent<Text>().text = "Nombre: "+mageName;
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