using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GraphManager : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    [SerializeField] private Sprite objectiveSprite;
    private RectTransform container;
    public int circleSize;
    public int hitDistance;
    public float lineSize;
    public float r;
    public float g;
    public float b;
    private float[] xd = new float[201];
    private bool Rx; private bool Rseno; private float RmultCuad; private float RsumaX; private float RsumaX2; private float RsumaX3; private float Radicion; private float[] RxValues;

    Vector2[] RNGPoints;
    Vector2[] listaUsuario;
    private void Awake()
    {
        for (int i = 0; i < xd.Length; i++)
        {
            xd[i] = (i - 100)/10f;
        }
        container = transform.Find("container").GetComponent<RectTransform>();
        //(bool x, float multCuad,float sumaX, bool signoCuad, float adicion, float[] valores )
        //float[] xd = new float[] { -10, -9, -8, -7, -6, -5, -4, -3, -2, -1, 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
        /*RandomizeFunction();
        RandomValidFloatsCub(Rx, RmultCuad, RsumaX, Radicion, 5);
        Vector2[] RNGPoints = CrearListaCubica(Rx, RmultCuad, RsumaX, Radicion, RxValues);
        MostrarPuntos(RNGPoints, 1);*/

        //Vector2[] listaCua = CrearListaCuadratica(false, 1, 0, 0, xd);
        //UnirPuntos(listaCua);
        /*Vector2[] listaCub = CrearListaCubica(false, 1, 5, 10, xd);
        UnirPuntos(listaCub);*/
        //Vector2[] listaTri = CrearListaTrigonometrica(true,true, 10, 5, 10,xd);
        //UnirPuntos(listaTri);
        //Vector2[] listaLin = CrearListaLineal(true, 10, 0, xd);
        //UnirPuntos(listaLin);
        //int[] listaRNG = CompareFunctionsCub(false, 1, 5, 10, RNGPoints);
        ///compareHard(RNGPoints, listaCub);


    }

    public int[] compareHard()
    {
        if (listaUsuario == null || listaUsuario.Length == 0)
        {
            return new int[] { 0, RNGPoints.Length };
        }
        int hitP = 0;
        for (int i = 0; i < RNGPoints.Length; i++)
        {
            for (int j = 0; j < listaUsuario.Length; j++)
            {
                if (Vector2.Distance(RNGPoints[i], listaUsuario[j])<hitDistance)
                {
                    hitP++;
                    break;
                }
            }
        }
        return new int[] {hitP, RNGPoints.Length-hitP};
    }
    private void RandomizeFunction()
    {
        Rx = (Random.value > 0.5f); Rseno = (Random.value > 0.5f); RmultCuad = RNG(10, 190, 10) - 10; RsumaX = RNG(10, 190, 10) - 10; RsumaX2 = RNG(10, 190, 10) - 10; RsumaX3 = RNG(10, 190, 10) - 10; Radicion = RNG(10, 190, 10) - 10;
    }

    private void RandomValidFloatsCuad(bool x, float multCuad, float sumaX, float adicion, int n)
    {
        float Altura = container.sizeDelta.y;
        float Ancho = container.sizeDelta.x;
        Vector2[] points = new Vector2[n];
        float[] deck = new float[200];
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i] = i;
        }
        int l = deck.Length;
        while (l > 1)
        {
            l--;
            int k = Random.Range(0, deck.Length);
            float value = deck[k];
            deck[k] = deck[l];
            deck[l] = value;
        }
        float[] values = new float[n];
        int q = 0;
        for (int i = 0; i < points.Length; i++)
        {
            values[i] = (deck[q] - 100) / 10;
            q++;
            points[i] = CrearListaCuadratica(x, multCuad, sumaX, adicion, new float[] { values[i] })[0];
            while (points[i].y >= Altura || points[i].y <= 0 || points[i].x >= Ancho || points[i].x <= 0)
            {
                values[i] = (deck[q] - 100) / 10;
                q++;
                points[i] = CrearListaCuadratica(x, multCuad, sumaX, adicion, new float[] { values[i] })[0];
            }
        }
        RxValues = values;
    }
    private void RandomValidFloatsCub(bool x, float multCuad, float sumaX, float adicion, int n)
    {
        float Altura = container.sizeDelta.y;
        float Ancho = container.sizeDelta.x;
        Vector2[] points = new Vector2[n];
        float[] deck = new float[200];
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i] = i;
        }
        int l = deck.Length;
        while (l > 1)
        {
            l--;
            int k = Random.Range(0, deck.Length);
            float value = deck[k];
            deck[k] = deck[l];
            deck[l] = value;
        }
        float[] values = new float[n];
        int q = 0;
        for (int i = 0; i < points.Length; i++)
        {
            values[i] = (deck[q] - 100) / 10;
            q++;
            points[i] = CrearListaCubica(x, multCuad, sumaX, adicion, new float[] { values[i] })[0];
            while (points[i].y >= Altura || points[i].y <= 0 || points[i].x >= Ancho || points[i].x <= 0)
            {
                values[i] = (deck[q] - 100) / 10;
                q++;
                points[i] = CrearListaCubica(x, multCuad, sumaX, adicion, new float[] { values[i] })[0];
            }
        }
        RxValues = values;
    }
    private void RandomValidFloatsTri(bool x, bool seno, float multCuad, float sumaX, float adicion, int n)
    {
        float Altura = container.sizeDelta.y;
        float Ancho = container.sizeDelta.x;
        Vector2[] points = new Vector2[n];
        float[] deck = new float[200];
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i] = i;
        }
        int l = deck.Length;
        while (l > 1)
        {
            l--;
            int k = Random.Range(0, deck.Length);
            float value = deck[k];
            deck[k] = deck[l];
            deck[l] = value;
        }
        float[] values = new float[n];
        int q = 0;
        for (int i = 0; i < points.Length; i++)
        {
            values[i] = (deck[q] - 100) / 10;
            q++;
            points[i] = CrearListaTrigonometrica(x, seno, multCuad, sumaX, adicion, new float[] { values[i] })[0];
            while (points[i].y >= Altura || points[i].y <= 0 || points[i].x >= Ancho || points[i].x <= 0)
            {
                values[i] = (deck[q] - 100) / 10;
                q++;
                points[i] = CrearListaTrigonometrica(x, seno, multCuad, sumaX, adicion, new float[] { values[i] })[0];
            }
        }
        RxValues = values;
    }

    private void RandomValidFloatsTrino(bool x, float multCuad, float sumaX1, float sumaX2, float sumaX3, float adicion, int n)
    {
        float Altura = container.sizeDelta.y;
        float Ancho = container.sizeDelta.x;
        Vector2[] points = new Vector2[n];
        float[] deck = new float[200];
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i] = i;
        }
        int l = deck.Length;
        while (l > 1)
        {
            l--;
            int k = Random.Range(0, deck.Length);
            float value = deck[k];
            deck[k] = deck[l];
            deck[l] = value;
        }
        float[] values = new float[n];
        int q = 0;
        for (int i = 0; i < points.Length; i++)
        {
            values[i] = (deck[q] - 100) / 10;
            q++;
            points[i] = CrearListaTrinomial(x, multCuad, sumaX1, sumaX2, sumaX3, adicion, new float[] { values[i] })[0];
            while (points[i].y >= Altura || points[i].y <= 0 || points[i].x >= Ancho || points[i].x <= 0)
            {
                values[i] = (deck[q] - 100) / 10;
                q++;
                points[i] = CrearListaTrinomial(x, multCuad, sumaX1, sumaX2, sumaX3, adicion, new float[] { values[i] })[0];
            }
        }
        RxValues = values;
    }

    private void RandomValidFloatsLin(bool x, float multCuad, float adicion, int n)
    {
        float Altura = container.sizeDelta.y;
        float Ancho = container.sizeDelta.x;
        Vector2[] points = new Vector2[n];
        float[] deck = new float[200];
        for (int i = 0; i < deck.Length; i++)
        {
            deck[i] = i;
        }
        int l = deck.Length;
        while (l > 1)
        {
            l--;
            int k = Random.Range(0, deck.Length);
            float value = deck[k];
            deck[k] = deck[l];
            deck[l] = value;
        }
        float[] values = new float[n];
        int q = 0;
        for (int i = 0; i < points.Length; i++)
        {
            values[i] = (deck[q] - 100) / 10;
            q++;
            points[i] = CrearListaLineal(x, multCuad, adicion, new float[] { values[i] })[0];
            while (points[i].y >= Altura || points[i].y <= 0 || points[i].x >= Ancho || points[i].x <= 0)
            {
                values[i] = (deck[q] - 100) / 10;
                q++;
                points[i] = CrearListaLineal(x, multCuad, adicion, new float[] { values[i] })[0];
            }
        }
        RxValues = values;
    }
    //previous Comparing
    /*
    private int[] CompareFunctionsCuad(bool x, float multCuad, float sumaX, float adicion, Vector2[] points)
    {
        float Ancho = container.sizeDelta.x;
        float Altura = container.sizeDelta.y;
        int[] hitP = new int[] { 0, 0 };
        float[] values = new float[points.Length * 5];
        if (x)
        {
            for (int i = 0; i < points.Length; i++)
            {
                values[(i * 5) + 0] = ((points[i].x - (Ancho / 2)) / 35f) - .2f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) - .2f));
                values[(i * 5) + 1] = ((points[i].x - (Ancho / 2)) / 35f) - .1f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) - .1f));
                values[(i * 5) + 2] = (points[i].x - (Ancho / 2)) / 35f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f)));
                values[(i * 5) + 3] = ((points[i].x - (Ancho / 2)) / 35f) + .1f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) + .1f));
                values[(i * 5) + 4] = ((points[i].x - (Ancho / 2)) / 35f) + .2f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) + .2f));
            }
        }
        else
        {
            for (int i = 0; i < points.Length; i++)
            {
                values[(i * 5) + 0] = ((points[i].y - (Altura / 2)) / 35f) - .2f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) - .2f));
                values[(i * 5) + 1] = ((points[i].y - (Altura / 2)) / 35f) - .1f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) - .1f));
                values[(i * 5) + 2] = (points[i].y - (Altura / 2)) / 35f;
                values[(i * 5) + 3] = ((points[i].y - (Altura / 2)) / 35f) + .1f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) + .1f));
                values[(i * 5) + 4] = ((points[i].y - (Altura / 2)) / 35f) + .2f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) + .2f));
            }
        }
        Vector2[] comparables = CrearListaCuadratica(x, multCuad, sumaX, adicion, values);
        MostrarPuntos(comparables, 3);
        for (int i = 0; i < values.Length; i++)
        {
            if (Vector2.Distance(comparables[(i * 5) + 0], points[i]) < hitDistance || Vector2.Distance(comparables[(i * 5) + 1], points[i]) < hitDistance || Vector2.Distance(comparables[(i * 5) + 2], points[i]) < hitDistance || Vector2.Distance(comparables[(i * 5) + 3], points[i]) < hitDistance || Vector2.Distance(comparables[(i * 5) + 4], points[i]) < hitDistance)
            {
                hitP[0]++;
            }
            else
            {
                hitP[1]++;
            }
        }
        Debug.Log("HitP: " + hitP[0] + "aciertos , " + hitP[1] + " fallos");
        return hitP;
    }
    private int[] CompareFunctionsCub(bool x, float multCuad, float sumaX, float adicion, Vector2[] points)
    {
        float Ancho = container.sizeDelta.x;
        float Altura = container.sizeDelta.y;
        int[] hitP = new int[] { 0, 0 };
        float[] values = new float[points.Length * 5];
        if (x)
        {
            for (int i = 0; i < points.Length; i++)
            {
                values[(i * 5) + 0] = ((points[i].x - (Ancho / 2)) / 35f) - .2f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) - .2f));
                values[(i * 5) + 1] = ((points[i].x - (Ancho / 2)) / 35f) - .1f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) - .1f));
                values[(i * 5) + 2] = (points[i].x - (Ancho / 2)) / 35f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f)));
                values[(i * 5) + 3] = ((points[i].x - (Ancho / 2)) / 35f) + .1f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) + .1f));
                values[(i * 5) + 4] = ((points[i].x - (Ancho / 2)) / 35f) + .2f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) + .2f));
            }
        }
        else
        {
            for (int i = 0; i < points.Length; i++)
            {
                values[(i * 5) + 0] = ((points[i].y - (Altura / 2)) / 35f) - .2f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) - .2f));
                values[(i * 5) + 1] = ((points[i].y - (Altura / 2)) / 35f) - .1f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) - .1f));
                values[(i * 5) + 2] = (points[i].y - (Altura / 2)) / 35f;
                values[(i * 5) + 3] = ((points[i].y - (Altura / 2)) / 35f) + .1f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) + .1f));
                values[(i * 5) + 4] = ((points[i].y - (Altura / 2)) / 35f) + .2f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) + .2f));
            }
        }
        Vector2[] comparables = CrearListaCubica(x, multCuad, sumaX, adicion, values);
        MostrarPuntos(comparables, 3); Debug.Log("" + comparables.Length);
        for (int i = 0; i < points.Length; i++)
        {
            if (Vector2.Distance(comparables[(i * 5) + 0], points[i]) < hitDistance || Vector2.Distance(comparables[(i * 5) + 1], points[i]) < hitDistance || Vector2.Distance(comparables[(i * 5) + 2], points[i]) < hitDistance || Vector2.Distance(comparables[(i * 5) + 3], points[i]) < hitDistance || Vector2.Distance(comparables[(i * 5) + 4], points[i]) < hitDistance)
            {
                hitP[0]++;
            }
            else
            {
                hitP[1]++;
            }
        }
        Debug.Log("HitP: " + hitP[0] + "aciertos , " + hitP[1] + " fallos");
        return hitP;
    }
    private int[] CompareFunctionsTri(bool x, bool seno, float multCuad, float sumaX, float adicion, Vector2[] points)
    {
        float Ancho = container.sizeDelta.x;
        float Altura = container.sizeDelta.y;
        int[] hitP = new int[] { 0, 0 };
        float[] values = new float[points.Length * 5];
        if (x)
        {
            for (int i = 0; i < points.Length; i++)
            {
                values[(i * 5) + 0] = ((points[i].x - (Ancho / 2)) / 35f) - .2f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) - .2f));
                values[(i * 5) + 1] = ((points[i].x - (Ancho / 2)) / 35f) - .1f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) - .1f));
                values[(i * 5) + 2] = (points[i].x - (Ancho / 2)) / 35f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f)));
                values[(i * 5) + 3] = ((points[i].x - (Ancho / 2)) / 35f) + .1f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) + .1f));
                values[(i * 5) + 4] = ((points[i].x - (Ancho / 2)) / 35f) + .2f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) + .2f));
            }
        }
        else
        {
            for (int i = 0; i < points.Length; i++)
            {
                values[(i * 5) + 0] = ((points[i].y - (Altura / 2)) / 35f) - .2f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) - .2f));
                values[(i * 5) + 1] = ((points[i].y - (Altura / 2)) / 35f) - .1f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) - .1f));
                values[(i * 5) + 2] = (points[i].y - (Altura / 2)) / 35f;
                values[(i * 5) + 3] = ((points[i].y - (Altura / 2)) / 35f) + .1f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) + .1f));
                values[(i * 5) + 4] = ((points[i].y - (Altura / 2)) / 35f) + .2f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) + .2f));
            }
        }
        Vector2[] comparables = CrearListaTrigonometrica(x, seno, multCuad, sumaX, adicion, values);
        MostrarPuntos(comparables, 3);
        for (int i = 0; i < values.Length; i++)
        {
            if (Vector2.Distance(comparables[(i * 5) + 0], points[i]) < hitDistance || Vector2.Distance(comparables[(i * 5) + 1], points[i]) < hitDistance || Vector2.Distance(comparables[(i * 5) + 2], points[i]) < hitDistance || Vector2.Distance(comparables[(i * 5) + 3], points[i]) < hitDistance || Vector2.Distance(comparables[(i * 5) + 4], points[i]) < hitDistance)
            {
                hitP[0]++;
            }
            else
            {
                hitP[1]++;
            }
        }
        Debug.Log("HitP: " + hitP[0] + "aciertos , " + hitP[1] + " fallos");
        return hitP;
    }
    private int[] CompareFunctionsLin(bool x, float multCuad, float adicion, Vector2[] points)
    {
        float Ancho = container.sizeDelta.x;
        float Altura = container.sizeDelta.y;
        int[] hitP = new int[] { 0, 0 };
        float[] values = new float[points.Length * 5];
        if (x)
        {
            for (int i = 0; i < points.Length; i++)
            {
                values[(i * 5) + 0] = ((points[i].x - (Ancho / 2)) / 35f) - .2f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) - .2f));
                values[(i * 5) + 1] = ((points[i].x - (Ancho / 2)) / 35f) - .1f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) - .1f));
                values[(i * 5) + 2] = (points[i].x - (Ancho / 2)) / 35f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f)));
                values[(i * 5) + 3] = ((points[i].x - (Ancho / 2)) / 35f) + .1f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) + .1f));
                values[(i * 5) + 4] = ((points[i].x - (Ancho / 2)) / 35f) + .2f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) + .2f));
            }
        }
        else
        {
            for (int i = 0; i < points.Length; i++)
            {
                values[(i * 5) + 0] = ((points[i].y - (Altura / 2)) / 35f) - .2f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) - .2f));
                values[(i * 5) + 1] = ((points[i].y - (Altura / 2)) / 35f) - .1f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) - .1f));
                values[(i * 5) + 2] = (points[i].y - (Altura / 2)) / 35f;
                values[(i * 5) + 3] = ((points[i].y - (Altura / 2)) / 35f) + .1f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) + .1f));
                values[(i * 5) + 4] = ((points[i].y - (Altura / 2)) / 35f) + .2f; Debug.Log("" + (((points[i].x - (Ancho / 2)) / 35f) + .2f));
            }
        }
        Vector2[] comparables = CrearListaLineal(x, multCuad, adicion, values);
        MostrarPuntos(comparables, 3);
        for (int i = 0; i < values.Length; i++)
        {
            if (Vector2.Distance(comparables[(i * 5) + 0], points[i]) < hitDistance || Vector2.Distance(comparables[(i * 5) + 1], points[i]) < hitDistance || Vector2.Distance(comparables[(i * 5) + 2], points[i]) < hitDistance || Vector2.Distance(comparables[(i * 5) + 3], points[i]) < hitDistance || Vector2.Distance(comparables[(i * 5) + 4], points[i]) < hitDistance)
            {
                hitP[0]++;
            }
            else
            {
                hitP[1]++;
            }
        }
        Debug.Log("HitP: " + hitP[0] + "aciertos , " + hitP[1] + " fallos");
        return hitP;
    }
    */
    private float RNG(int a, int b, int c)
    {
        return Random.Range(a, b) / c;
    }

    private void MostrarPuntos(Vector2[] lista, int type)
    {
        for (int i = 0; i < lista.Length; i++)
        {
            GameObject Circulo = CrearCirculo(lista[i], type);
        }
    }
    private void UnirPuntos(Vector2[] lista)
    {

        GameObject lastCircle = null;
        for (int i = 0; i < lista.Length; i++)
        {
            GameObject Circulo = CrearCirculo(lista[i], 2);
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
            objeto1.gameObject.tag = "UserCircle";
            objeto1.transform.SetParent(container, false);
            objeto1.GetComponent<Image>().color = new Color(r, g, b, .5f);
            RectTransform transformarRect = objeto1.GetComponent<RectTransform>();
            Vector2 dir = (puntoB - puntoA).normalized;
            float distancia = Vector2.Distance(puntoA, puntoB);
            transformarRect.anchorMin = new Vector2(0, 0);
            transformarRect.anchorMax = new Vector2(0, 0);
            transformarRect.sizeDelta = new Vector2(distancia, lineSize);
            transformarRect.anchoredPosition = puntoA + dir * distancia * .5f;
            transformarRect.localEulerAngles = new Vector3(0, 0, GetAngleFromVectorFloat(dir));
        }

    }
    private GameObject CrearCirculo(Vector2 posicion, int type)
    {
        float Altura = container.sizeDelta.y;
        float Ancho = container.sizeDelta.x;
        GameObject objeto = new GameObject("circle", typeof(Image));
        objeto.transform.SetParent(container, false);
        RectTransform rectTrans = objeto.GetComponent<RectTransform>();
        rectTrans.anchoredPosition = posicion;
        rectTrans.sizeDelta = new Vector2(circleSize, circleSize);
        rectTrans.anchorMin = new Vector2(0, 0);
        rectTrans.anchorMax = new Vector2(0, 0);
        if ((posicion.x <= Ancho && posicion.x >= 0) && (posicion.y <= Altura && posicion.y >= 0))
        {
            switch (type)
            {
                case 0:
                    objeto.GetComponent<Image>().sprite = circleSprite;
                    objeto.gameObject.tag = "UserCircle";
                    break;
                case 1:
                    objeto.GetComponent<Image>().sprite = objectiveSprite;
                    objeto.gameObject.tag = "TargetCircle";
                    break;
                default:
                    objeto.GetComponent<Image>().sprite = null;
                    objeto.GetComponent<Image>().color = new Color(1, 1, 1, 0f);
                    objeto.gameObject.tag = "UserCircle";
                    break;
            }
        }
        else
        {
            objeto.GetComponent<Image>().sprite = null;
            objeto.GetComponent<Image>().color = new Color(1, 1, 1, 0f);
            objeto.gameObject.tag = "UserCircle";
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

    private Vector2[] CrearListaTrinomial(bool x, float multCuad, float sumaX1, float sumaX2, float sumaX3, float adicion, float[] valores)
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
                yPos = ((multCuad * (sumaX1 + valores[i]) * (sumaX2 + valores[i]) * (sumaX3 + valores[i])) + adicion) * xSize / 10 + Altura / 2;
                lista[i] = new Vector2(xPos, yPos);
            }
        }
        else
        {
            for (int i = 0; i < valores.Length; i++)
            {       //Aqui se decide la posicion de los puntos en x, y
                float yPos = valores[i] * xSize + Altura / 2;
                float xPos;
                xPos = ((multCuad * (sumaX1 + valores[i]) * (sumaX2 + valores[i]) * (sumaX3 + valores[i])) + adicion) * xSize / 10 + Ancho / 2;
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

    public void GenerateRandomTargets(int type)
    {
        RandomizeFunction();
        switch (type)
        {
            case 0:
                RandomValidFloatsCuad(Rx, RmultCuad, RsumaX, Radicion, 5);
                RNGPoints = CrearListaCuadratica(Rx, RmultCuad, RsumaX, Radicion, RxValues);
                break;
            case 1:
                RandomValidFloatsCub(Rx, RmultCuad, RsumaX, Radicion, 5);
                RNGPoints = CrearListaCubica(Rx, RmultCuad, RsumaX, Radicion, RxValues);
                break;
            case 2:
                RandomValidFloatsTri(Rx, Rseno, RmultCuad, RsumaX, Radicion, 5);
                RNGPoints = CrearListaTrigonometrica(Rx, Rseno, RmultCuad, RsumaX, Radicion, RxValues);
                break;
            case 3:
                RandomValidFloatsTrino(Rx, RmultCuad, RsumaX, RsumaX2, RsumaX3, Radicion, 5);
                RNGPoints = CrearListaTrinomial(Rx, RmultCuad, RsumaX, RsumaX2, RsumaX3, Radicion, RxValues);
                break;
            case 4:
                RandomValidFloatsLin(Rx, RmultCuad, Radicion, 5);
                RNGPoints = CrearListaLineal(Rx, RmultCuad, Radicion, RxValues);
                break;
            default:
                Debug.Log("Algo pudo salir mal al generar los targets aleatorios.");
                break;
        }
        MostrarPuntos(RNGPoints, 1);
    }

    public void GenerateUserGraphCuadratica(bool x, float multCuad, float sumaX, float adicion)
    {
        listaUsuario = CrearListaCuadratica(x, multCuad, sumaX, adicion, xd);
        UnirPuntos(listaUsuario);
    }

    public void GenerateUserGraphCubica(bool x, float multCuad, float sumaX, float adicion)
    {
        listaUsuario = CrearListaCubica(x, multCuad, sumaX, adicion, xd);
        UnirPuntos(listaUsuario);
    }

    public void GenerateUserGraphTrigonometrica(bool x, bool seno, float multCuad, float sumaX, float adicion)
    {
        listaUsuario = CrearListaTrigonometrica(x, seno, multCuad, sumaX, adicion, xd);
        UnirPuntos(listaUsuario);
    }

    public void GenerateUserGraphTrinomial(bool x, float multCuad, float sumaX, float sumaX2, float sumaX3, float adicion)
    {
        listaUsuario = CrearListaTrinomial(x, multCuad, sumaX, sumaX2, sumaX3, adicion, xd);
        UnirPuntos(listaUsuario);
    }

    public void GenerateUserGraphLineal(bool x, float multCuad, float adicion)
    {
        listaUsuario = CrearListaLineal(x, multCuad, adicion, xd);
        UnirPuntos(listaUsuario);
    }

    public void DeleteUserCircles()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("UserCircle");

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }

    public void DeleteTargetCircles()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("TargetCircle");

        for (var i = 0; i < gameObjects.Length; i++)
        {
            Destroy(gameObjects[i]);
        }
    }
}
