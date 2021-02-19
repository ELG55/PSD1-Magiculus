using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphManager : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    private RectTransform container;

    private void Awake()
    {
        container = transform.Find("container").GetComponent<RectTransform>();

        Vector2[] lista = CrearLista(true, 10, true, 10, 11);
        UnirPuntos(lista);

    }


    private void UnirPuntos(Vector2[] lista)
    {

        GameObject lastCircle = null;
        for (int i = 0; i < lista.Length; i++)
        {
            GameObject Circulo = CrearCirculo(lista[i]);
            if (lastCircle != null)
            {
                Union(lastCircle.GetComponent<RectTransform>().anchoredPosition, Circulo.GetComponent<RectTransform>().anchoredPosition);
            }
            lastCircle = Circulo;
        }

    }
    private void Union(Vector2 puntoA, Vector2 puntoB)
    {
        GameObject objeto1 = new GameObject("dotConnection", typeof(Image));
        objeto1.transform.SetParent(container, false);
        objeto1.GetComponent<Image>().color = new Color(1, 1, 1, .5f);
        RectTransform transformarRect = objeto1.GetComponent<RectTransform>();
        Vector2 dir = (puntoB - puntoA).normalized;
        float distancia = Vector2.Distance(puntoA, puntoB);
        transformarRect.anchorMin = new Vector2(0, 0);
        transformarRect.anchorMax = new Vector2(0, 0);
        transformarRect.sizeDelta = new Vector2(distancia, 3f);
        transformarRect.anchoredPosition = puntoA + dir * distancia * .5f;
        transformarRect.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));

    }
    private GameObject CrearCirculo(Vector2 posicion)
    {
        GameObject objeto = new GameObject("circle", typeof(Image));
        objeto.transform.SetParent(container, false);
        objeto.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTrans = objeto.GetComponent<RectTransform>();
        rectTrans.anchoredPosition = posicion;
        rectTrans.sizeDelta = new Vector2(11, 11);
        rectTrans.anchorMin = new Vector2(0, 0);
        rectTrans.anchorMax = new Vector2(0, 0);
        return objeto;
    }

    private Vector2[] CrearLista(bool x, int multCuad, bool signoCuad, int adicion, int rango)
    {
        float Altura = container.sizeDelta.y;
        float Ancho = container.sizeDelta.x;
        float xSize = 50f;
        Vector2[] lista = new Vector2[rango];
        if (x)
        {   
            for (int i = 0; i < rango; i++)
            {       //Aqui se decide la posicion de los puntos en x, y
                float xPos = (i - rango / 2) *xSize + Ancho / 2;

                float yPos;
                if (signoCuad) {
                    yPos = ((multCuad * (i - rango / 2) * (i - rango / 2)) + adicion) + Altura / 2;
                }
                else
                {
                    yPos = ((-1 * multCuad * (i - rango / 2) * (i - rango / 2)) + adicion) + Altura / 2;
                }
                lista[i] = new Vector2(xPos, yPos);
            }
        }
        else
        {
            for (int i = 0; i < rango; i++)
            {       //Aqui se decide la posicion de los puntos en x, y
                float yPos = (i - rango / 2) * xSize + Altura/2;

                float xPos;
                if (signoCuad)
                {
                    xPos = ((multCuad * (i - rango / 2) * (i - rango / 2)) + adicion) + Ancho / 2;
                }
                else
                {
                    xPos = ((-1 * multCuad * (i - rango / 2) * (i - rango / 2)) + adicion) + Ancho/2;
                }
                lista[i] = new Vector2(xPos, yPos);
            }
        }
        return lista;
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        return n;
    }

}
