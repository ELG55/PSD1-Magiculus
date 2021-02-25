﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FunctionsController : MonoBehaviour
{
    //public GameObject cuadraticaCanvas;
    //public GameObject cubicaCanvas;
    //public GameObject trigonometricaCanvas;
    //public GameObject exponencialCanvas;
    //public GameObject linealCanvas;

    //Elementos de funcion cuadrática
    public GameObject cuadraticaEspacio01;
    public GameObject cuadraticaEspacio02;
    public GameObject cuadraticaEspacio03;
    public GameObject cuadraticaVariable;

    //Elementos de funcion cúbica
    public GameObject cubicaEspacio01;
    public GameObject cubicaEspacio02;
    public GameObject cubicaEspacio03;
    public GameObject cubicaVariable;

    //Elementos de funcion trigonométrica
    public GameObject trigonometricaEspacio01;
    public GameObject trigonometricaEspacio02;
    public GameObject trigonometricaEspacio03;
    public GameObject trigonometricaVariable;
    public GameObject trigonometricaSenCos;

    //Elementos de función trinomial
    public GameObject trinomialEspacio01;
    public GameObject trinomialEspacio02;
    public GameObject trinomialEspacio03;
    public GameObject trinomialEspacio04;
    public GameObject trinomialVariable;

    //Elementos de funcion lineal
    public GameObject linealEspacio01;
    public GameObject linealEspacio02;
    public GameObject linealVariable;

    //CANCELADO
    /*Elementos de funcion exponencial
    public GameObject exponencialEspacio01;
    public GameObject exponencialEspacio02;
    public GameObject exponencialEspacio03;
    public GameObject exponencialVariable;
    //public GameObject exponencialEulerLn;*/

    //TO DO NEXT

    //ASSIGN THE METHODS TO THE ELEMENTS

    public enum FunctionType { Cuadratica, Cubica, Trigonometrica, Trinomial, Lineal}

    public List<GameObject> canvases;
    public decimal[] variablesEspacio = new decimal[4] { 1, 1, 1, 1 };
    public bool isXYswitched = false;
    public bool isSenCosSwitched = false;

    public FunctionType currentFunctionType = FunctionType.Cuadratica;

    public void AddSmallVariableEspacio(int variableEspacio)
    {
        variablesEspacio[variableEspacio] += 0.1m;
        //Sound.play(snd_cursor_up);
    }

    public void SubtractSmallVariableEspacio(int variableEspacio)
    {
        variablesEspacio[variableEspacio] -= 0.1m;
    }

    public void AddMediumVariableEspacio(int variableEspacio)
    {
        variablesEspacio[variableEspacio] += 1.0m;
    }

    public void SubtractMediumVariableEspacio(int variableEspacio)
    {
        variablesEspacio[variableEspacio] -= 1.0m;
    }

    public void AddBigVariableEspacio(int variableEspacio)
    {
        variablesEspacio[variableEspacio] += 10.0m;
    }

    public void SubtractBigVariableEspacio(int variableEspacio)
    {
        variablesEspacio[variableEspacio] -= 10.0m;
    }

    public void SwitchXY()
    {
        isXYswitched = !isXYswitched;
    }

    public void SwitchSenCos()
    {
        isSenCosSwitched = !isSenCosSwitched;
    }

    public void RefreshAllTexts()
    {
        //Cuadrática
        cuadraticaEspacio01.GetComponent<Text>().text = variablesEspacio[0].ToString();
        cuadraticaEspacio02.GetComponent<Text>().text = variablesEspacio[1].ToString();
        cuadraticaEspacio03.GetComponent<Text>().text = variablesEspacio[2].ToString();
        if (isXYswitched)
        {
            cuadraticaVariable.GetComponent<Text>().text = "Y";
        }
        else
        {
            cuadraticaVariable.GetComponent<Text>().text = "X";
        }/*
        //Cúbica
        cubicaEspacio01.GetComponent<Text>().text = variablesEspacio[0].ToString();
        cubicaEspacio02.GetComponent<Text>().text = variablesEspacio[1].ToString();
        cubicaEspacio03.GetComponent<Text>().text = variablesEspacio[2].ToString();
        if (isXYswitched)
        {
            cubicaVariable.GetComponent<Text>().text = "Y";
        }
        else
        {
            cubicaVariable.GetComponent<Text>().text = "X";
        }
        //Trigonométrica
        trigonometricaEspacio01.GetComponent<Text>().text = variablesEspacio[0].ToString();
        trigonometricaEspacio02.GetComponent<Text>().text = variablesEspacio[1].ToString();
        trigonometricaEspacio03.GetComponent<Text>().text = variablesEspacio[2].ToString();
        if (isXYswitched)
        {
            trigonometricaVariable.GetComponent<Text>().text = "Y";
        }
        else
        {
            trigonometricaVariable.GetComponent<Text>().text = "X";
        }
        if (isSenCosSwitched)
        {
            trigonometricaSenCos.GetComponent<Text>().text = "Cos";
        }
        else
        {
            trigonometricaSenCos.GetComponent<Text>().text = "Sen";
        }
        //Trinomial
        trinomialEspacio01.GetComponent<Text>().text = variablesEspacio[0].ToString();
        trinomialEspacio02.GetComponent<Text>().text = variablesEspacio[1].ToString();
        trinomialEspacio03.GetComponent<Text>().text = variablesEspacio[2].ToString();
        trinomialEspacio04.GetComponent<Text>().text = variablesEspacio[2].ToString();
        if (isXYswitched)
        {
            trinomialVariable.GetComponent<Text>().text = "Y";
        }
        else
        {
            trinomialVariable.GetComponent<Text>().text = "X";
        }
        //Lineal
        linealEspacio01.GetComponent<Text>().text = variablesEspacio[0].ToString();
        linealEspacio01.GetComponent<Text>().text = variablesEspacio[1].ToString();
        if (isXYswitched)
        {
            linealVariable.GetComponent<Text>().text = "Y";
        }
        else
        {
            linealVariable.GetComponent<Text>().text = "X";
        }
        */
    }

    public void ChangeEspacioTextToEspacio0(GameObject textObject)
    {
        textObject.GetComponent<Text>().text = variablesEspacio[0].ToString();
    }

    public void ChangeEspacioTextToEspacio1(GameObject textObject)
    {
        textObject.GetComponent<Text>().text = variablesEspacio[1].ToString();
    }

    public void ChangeEspacioTextToEspacio2(GameObject textObject)
    {
        textObject.GetComponent<Text>().text = variablesEspacio[2].ToString();
    }

    public void ChangeEspacioTextToEspacio3(GameObject textObject)
    {
        textObject.GetComponent<Text>().text = variablesEspacio[3].ToString();
    }

    public void ChangeVariableTextToX(GameObject textObject)
    {
        textObject.GetComponent<Text>().text = "X";
    }

    public void ChangeVariableTextToY(GameObject textObject)
    {
        textObject.GetComponent<Text>().text = "Y";
    }

    public void ChangeTrigonometricaVariableTextToSen(GameObject textObject)
    {
        textObject.GetComponent<Text>().text = "Sen";
    }

    public void ChangeTrigonometricaVariableTextToCos(GameObject textObject)
    {
        textObject.GetComponent<Text>().text = "Cos";
    }

    public void SetCurrentFunctionType(int type)
    {
        switch (type)
        {
            case 0:
                this.currentFunctionType = FunctionType.Cuadratica;
                break;
            case 1:
                this.currentFunctionType = FunctionType.Cubica;
                break;
            case 2:
                this.currentFunctionType = FunctionType.Trigonometrica;
                break;
            case 3:
                this.currentFunctionType = FunctionType.Trinomial;
                break;
            case 4:
                this.currentFunctionType = FunctionType.Lineal;
                break;
        }
    }

    public void HideAllCanvases()
    {
        for (int i = 0; i < canvases.Count; i++)
        {
            canvases[i].SetActive(false);
        }
    }

    public void HideCanvas(GameObject canvas)
    {
        canvas.SetActive(false);
    }

    public void ShowCanvas(GameObject canvas)
    {
        canvas.SetActive(true);
    }


    /*OLD
    public void ChangeSmallVariableEspacio(int variableEspacio, bool add)
    {
        if (add)
        {
            variablesEspacio[variableEspacio] += 0.1f;
        }
        else
        {
            variablesEspacio[variableEspacio] -= 0.1f;
        }
    }

    public void ChangeMediumVariableEspacio(int variableEspacio, bool add)
    {
        if (add)
        {
            variablesEspacio[variableEspacio] += 1.0f;
        }
        else
        {
            variablesEspacio[variableEspacio] -= 1.0f;
        }
    }

    public void ChangeBigVariableEspacio(int variableEspacio, bool add)
    {
        if (add)
        {
            variablesEspacio[variableEspacio] += 10.0f;
        }
        else
        {
            variablesEspacio[variableEspacio] -= 10.0f;
        }
    }

    public void ChangeEspacioText(GameObject textObject, int variableEspacioNumber)
    {
        textObject.GetComponent<Text>().text = variablesEspacio[variableEspacioNumber].ToString();
    }

    public void ChangeVariableText(GameObject textObject, bool XYTrueSenCosFalse)
    {
        if (XYTrueSenCosFalse)
        {
            if (isXYswitched)
            {
                textObject.GetComponent<Text>().text = "Y";
            }
            else
            {
                textObject.GetComponent<Text>().text = "X";
            }
        }
        else
        {
            if (isSenCosSwitched)
            {
                textObject.GetComponent<Text>().text = "Cos";
            }
            else
            {
                textObject.GetComponent<Text>().text = "Sen";
            }
        }
    }
    */
}