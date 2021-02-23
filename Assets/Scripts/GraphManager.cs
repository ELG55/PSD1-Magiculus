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
        //(bool x, float multCuad,float sumaX, bool signoCuad, float adicion, float[] valores )
        float[] xd = new float[] { -10, -9, -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        Vector2[] listaCua = CrearListaCuadratica(true, 1, 0, 0, xd);
        UnirPuntos(listaCua);


        //Vector2[] listaCub = CrearListaCubica(true, 1, 5, 10,xd);
        //UnirPuntos(listaCub);
        //Vector2[] listaTri = CrearListaTrigonometrica(true,true, 10, 5, 10,xd);
        //UnirPuntos(listaTri);
        //Vector2[] listaLin = CrearListaLineal(true, 10, 0,xd);
        //UnirPuntos(listaLin);
        //float[] xd2 = new float[] { RNG(0, 200, 10)-10, RNG(0, 200, 10) - 10, RNG(0, 200, 10) - 10};
        //Vector2[] listaRNG = CrearListaCuadratica((Random.value > 0.5f), RNG(0, 200, 10) - 10, RNG(0, 200, 10) - 10, RNG(0, 200, 10) - 10, xd2);
        //MostrarPuntos(listaRNG);
        //int[] hitP = CompareFunctionsCuad(true, 1, 0, 0, xd2,listaRNG);


    }

    private int[] CompareFunctionsCuad(bool x, float multCuad, float sumaX, float adicion, float[] valores, Vector2[] points)
    {
        int[] hitP = new int[] {0,0};
        Vector2[] comparables = CrearListaCuadratica(x, multCuad, sumaX, adicion, valores);
        MostrarPuntos(comparables);
        for (int i = 0; i < valores.Length; i++)
        {
            if (Vector2.Distance(points[i],comparables[i])<10)
            {
                hitP[0]++;
            }
            else
            {
                hitP[1]++;
            }
            Debug.Log("x1:" + points[i].x + " y1:" + points[i].y + " y x2:" + comparables[i].x + " y2:" + comparables[i].y);
            Debug.Log("distancia:" + Vector2.Distance(points[i], comparables[i]));
        }
        Debug.Log("HitP: " + hitP[0] + "aciertos , " + hitP[1] + " fallos");
        return hitP;
    }
    private int[] CompareFunctionsCub(bool x, float multCuad, float sumaX, float adicion, float[] valores, Vector2[] points)
    {
        int[] hitP = new int[] { 0, 0 };
        Vector2[] comparables = CrearListaCubica(x, multCuad, sumaX, adicion, valores);
        MostrarPuntos(comparables);
        for (int i = 0; i < valores.Length; i++)
        {
            if (Vector2.Distance(points[i], comparables[i]) < 10)
            {
                hitP[0]++;
            }
            else
            {
                hitP[1]++;
            }
            Debug.Log("x1:" + points[i].x + " y1:" + points[i].y + " y x2:" + comparables[i].x + " y2:" + comparables[i].y);
            Debug.Log("distancia:" + Vector2.Distance(points[i], comparables[i]));
        }
        Debug.Log("HitP: " + hitP[0] + "aciertos , " + hitP[1] + " fallos");
        return hitP;

    }
    private int[] CompareFunctionsTri(bool x, bool seno, float multCuad, float sumaX, float adicion, float[] valores, Vector2[] points)
    {
        int[] hitP = new int[] { 0, 0 };
        Vector2[] comparables = CrearListaTrigonometrica(x,seno, multCuad, sumaX, adicion, valores);
        MostrarPuntos(comparables);
        for (int i = 0; i < valores.Length; i++)
        {
            if (Vector2.Distance(points[i], comparables[i]) < 10)
            {
                hitP[0]++;
            }
            else
            {
                hitP[1]++;
            }
            Debug.Log("x1:" + points[i].x + " y1:" + points[i].y + " y x2:" + comparables[i].x + " y2:" + comparables[i].y);
            Debug.Log("distancia:" + Vector2.Distance(points[i], comparables[i]));
        }
        Debug.Log("HitP: " + hitP[0] + "aciertos , " + hitP[1] + " fallos");
        return hitP;

    }
    private int[] CompareFunctionsLin(bool x, float multCuad, float adicion, float[] valores, Vector2[] points)
    {
        int[] hitP = new int[] { 0, 0 };
        Vector2[] comparables = CrearListaLineal(x, multCuad, adicion, valores);
        MostrarPuntos(comparables);
        for (int i = 0; i < valores.Length; i++)
        {
            if (Vector2.Distance(points[i], comparables[i]) < 10)
            {
                hitP[0]++;
            }
            else
            {
                hitP[1]++;
            }
            Debug.Log("x1:" + points[i].x + " y1:" + points[i].y + " y x2:" + comparables[i].x + " y2:" + comparables[i].y);
            Debug.Log("distancia:" + Vector2.Distance(points[i], comparables[i]));
        }
        Debug.Log("HitP: " + hitP[0] + "aciertos , " + hitP[1] + " fallos");
        return hitP;
    }

    private int RNG(int a, int b)
    {
        return Random.Range(a, b);
    }
    private float RNG(int a, int b, int c)
    {
        return Random.Range(a, b) / c;
    }

    private void MostrarPuntos(Vector2[] lista)
    {
        for (int i = 0; i < lista.Length; i++)
        {
            GameObject Circulo = CrearCirculo(lista[i]);
        }
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
        float Altura = container.sizeDelta.y;
        float Ancho = container.sizeDelta.x;
        if (((puntoA.x >= 0 && puntoA.x <= Ancho) && (puntoA.y >= 0 && puntoA.y <= Altura)) || ((puntoB.x >= 0 && puntoB.x <= Ancho) && (puntoB.y >= 0 && puntoB.y <= Altura)))
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

    }
    private GameObject CrearCirculo(Vector2 posicion)
    {
        float Altura = container.sizeDelta.y;
        float Ancho = container.sizeDelta.x;
        GameObject objeto = new GameObject("circle", typeof(Image));
        objeto.transform.SetParent(container, false);
        RectTransform rectTrans = objeto.GetComponent<RectTransform>();
        rectTrans.anchoredPosition = posicion;
        rectTrans.sizeDelta = new Vector2(11, 11);
        rectTrans.anchorMin = new Vector2(0, 0);
        rectTrans.anchorMax = new Vector2(0, 0);
        if ((posicion.x <= Ancho && posicion.x >= 0) && (posicion.y <= Altura && posicion.y >= 0))
        {
            objeto.GetComponent<Image>().sprite = circleSprite;
        }
        else
        {
            objeto.GetComponent<Image>().sprite = null;
            objeto.GetComponent<Image>().color = new Color(1, 1, 1, 0f);
        }
        return objeto;
    }

    private Vector2[] CrearListaCuadratica(bool x, float multCuad, float sumaX, float adicion, float[] valores)
    {
        float Altura = container.sizeDelta.y;
        float Ancho = container.sizeDelta.x;
        float xSize = 35f;
        Vector2[] lista = new Vector2[valores.Length];
        if (x)
        {
            for (int i = 0; i < valores.Length; i++)
            {       //Aqui se decide la posicion de los puntos en x, y
                float xPos = valores[i] * xSize + Ancho / 2;
                float yPos;
                yPos = ((multCuad * (sumaX + valores[i]) * (sumaX + valores[i])) + adicion) * xSize / 10 + Altura / 2;
                lista[i] = new Vector2(xPos, yPos);
            }
        }
        else
        {
            for (int i = 0; i < valores.Length; i++)
            {       //Aqui se decide la posicion de los puntos en x, y
                float yPos = valores[i] * xSize + Altura / 2;
                float xPos;
                xPos = ((multCuad * (sumaX + valores[i]) * (sumaX + valores[i])) + adicion) * xSize / 10 + Ancho / 2;
                lista[i] = new Vector2(xPos, yPos);
            }
        }
        return lista;
    }

    private Vector2[] CrearListaCubica(bool x, float multCuad, float sumaX, float adicion, float[] valores)
    {
        float Altura = container.sizeDelta.y;
        float Ancho = container.sizeDelta.x;
        float xSize = 35f;
        Vector2[] lista = new Vector2[valores.Length];
        if (x)
        {
            for (int i = 0; i < valores.Length; i++)
            {       //Aqui se decide la posicion de los puntos en x, y
                float xPos = valores[i] * xSize + Ancho / 2;
                float yPos;
                yPos = ((multCuad * (sumaX + valores[i]) * (sumaX + valores[i]) * (sumaX + valores[i])) + adicion) * xSize / 10 + Altura / 2;
                lista[i] = new Vector2(xPos, yPos);
            }
        }
        else
        {
            for (int i = 0; i < valores.Length; i++)
            {       //Aqui se decide la posicion de los puntos en x, y
                float yPos = valores[i] * xSize + Altura / 2;
                float xPos;
                xPos = ((multCuad * (sumaX + valores[i]) * (sumaX + valores[i]) * (sumaX + valores[i])) + adicion) * xSize / 10 + Ancho / 2;
                lista[i] = new Vector2(xPos, yPos);
            }
        }
        return lista;
    }

    private Vector2[] CrearListaTrigonometrica(bool x, bool seno, float multCuad, float sumaX, float adicion, float[] valores)
    {
        float Altura = container.sizeDelta.y;
        float Ancho = container.sizeDelta.x;
        float xSize = 35f;
        Vector2[] lista = new Vector2[valores.Length];
        if (x)
        {
            for (int i = 0; i < valores.Length; i++)
            {       //Aqui se decide la posicion de los puntos en x, y
                float xPos = valores[i] * xSize + Ancho / 2;
                float yPos;
                    if (seno)
                    {
                        yPos = ((multCuad * (Mathf.Sin(valores[i] + sumaX))) + adicion) * xSize / 10 + Altura / 2;
                    }
                    else
                    {
                        yPos = ((multCuad * (Mathf.Cos(valores[i] + sumaX))) + adicion) * xSize / 10 + Altura / 2;
                    }
                lista[i] = new Vector2(xPos, yPos);
            }
        }
        else
        {
            for (int i = 0; i < valores.Length; i++)
            {       //Aqui se decide la posicion de los puntos en x, y
                float yPos = valores[i] * xSize + Altura / 2;
                float xPos;
                    if (seno)
                    {
                        xPos = ((multCuad * (Mathf.Sin(valores[i] + sumaX))) + adicion) * xSize / 10 + Ancho / 2;
                    }
                    else
                    {
                        xPos = ((multCuad * (Mathf.Cos(valores[i] + sumaX))) + adicion) * xSize / 10 + Ancho / 2;
                    }
                lista[i] = new Vector2(xPos, yPos);
            }
        }
        return lista;
    }

    private Vector2[] CrearListaLineal(bool x, float multCuad, float adicion, float[] valores)
    {
        float Altura = container.sizeDelta.y;
        float Ancho = container.sizeDelta.x;
        float xSize = 35f;
        Vector2[] lista = new Vector2[valores.Length];
        if (x)
        {
            for (int i = 0; i < valores.Length; i++)
            {       //Aqui se decide la posicion de los puntos en x, y
                float xPos = valores[i] * xSize + Ancho / 2;
                float yPos;
                yPos = ((multCuad * valores[i]) + adicion) * xSize / 10 + Altura / 2;
                lista[i] = new Vector2(xPos, yPos);
            }
        }
        else
        {
            for (int i = 0; i < valores.Length; i++)
            {       //Aqui se decide la posicion de los puntos en x, y
                float yPos = valores[i] * xSize + Altura / 2;
                float xPos;
                xPos = ((multCuad * valores[i]) + adicion) * xSize / 10 + Ancho / 2;
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
