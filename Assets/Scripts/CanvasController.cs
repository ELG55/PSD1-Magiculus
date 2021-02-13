using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public List<GameObject> hideCanvases;

    private void Awake()
    {
        for (int i = 0; i < hideCanvases.Count; i++)
        {
            hideCanvases[i].SetActive(false);
        }
    }
    public void hideCanvas(GameObject canvas)
    {
        canvas.SetActive(false);
    }

    public void showCanvas(GameObject canvas)
    {
        canvas.SetActive(true);
    }
}
