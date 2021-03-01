using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CargarController : MonoBehaviour
{
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

    private int SelectedSlot;

    public GameObject savedata;

    public string getInputName()
    {
        return Input1.GetComponent<InputField>().text;
    }
    public string getInputClase()
    {
        return Input2.GetComponent<InputField>().text;
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

    public void RefreshData(string date, string mageName, string level, string mageClass, string progress, string dmgDone, string dmgReceived, string hitP)
    {
        fecha.GetComponent<Text>().text = "Fecha: " + date;
        name1.GetComponent<Text>().text = "Nombre: "+mageName;
        nivel.GetComponent<Text>().text = "Nivel: "+level;
        clase.GetComponent<Text>().text = "Clase: "+mageClass;
        progreso.GetComponent<Text>().text = "Progreso: \n"+progress;
        realizado.GetComponent<Text>().text = "DMG realizado: \n"+dmgDone;
        recibido.GetComponent<Text>().text = "DMG recibido: \n"+dmgReceived;
        porcentaje.GetComponent<Text>().text = "Hit% total: \n"+hitP;
    }

    public void callCargar(int casilla)
    {
        savedata.GetComponent<Savedata>().CargarDatos(casilla);
    }
    public void callSalvar()
    {
        savedata.GetComponent<Savedata>().SalvarDatos();
    }
    public void callBorrar()
    {
        savedata.GetComponent<Savedata>().BorrarDatos();
    }
    public void callGuardarProgreso()
    {
        savedata.GetComponent<Savedata>().SalvarProgreso(SelectedSlot);
        RefreshData(
        savedata.GetComponent<Savedata>().date,
        savedata.GetComponent<Savedata>().mageName,
        savedata.GetComponent<Savedata>().level.ToString(),
        savedata.GetComponent<Savedata>().mageClass,
        savedata.GetComponent<Savedata>().progress,
        savedata.GetComponent<Savedata>().dmgDone.ToString(),
        savedata.GetComponent<Savedata>().dmgReceived.ToString(),
        savedata.GetComponent<Savedata>().hitP.ToString());
    }
    public void showFileInfo(int slot)
    {
        SaveFile sv = SaveManager.Cargar(slot);
        if (sv==null)
        {
            OcultarDatos();
            SelectedSlot = slot;
        }
        else
        {
            RefreshData(sv.date, sv.mageName, sv.level.ToString(), sv.mageClass, sv.progress, sv.dmgDone.ToString(), sv.dmgReceived.ToString(), sv.hitP.ToString());
            name1.SetActive(true);
            nivel.SetActive(true);
            clase.SetActive(true);
            progreso.SetActive(true);
            realizado.SetActive(true);
            recibido.SetActive(true);
            porcentaje.SetActive(true);
            fecha.SetActive(true);
            GuardarBoton.SetActive(true);
            MagoImagen.SetActive(true);
            SelectedSlot = slot;
        }
    }

    private void Awake()
    {
        savedata = GameObject.Find("Savedata");
    }

}
