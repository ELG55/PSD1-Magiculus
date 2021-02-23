using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public enum BattleStage { EntranceAnimation, UserInput, DamageAnimation}
    private BattleStage currentStage = BattleStage.EntranceAnimation;

    public GameObject entranceBlackRectangleLeft;
    public GameObject entranceBlackRectangleRight;
    public GameObject centerMessageText;

    public GameObject buttonCuadratica;
    public GameObject buttonCubica;
    public GameObject buttonTrigonometrica;
    public GameObject buttonExponencial;
    public GameObject buttonLineal;

    public GameObject objectFunctionsController;
    private FunctionsController functionsController;

    public GameObject objectTimer;
    private Timer timer;

    private bool UserInputStartDone = false;
    private float bigFontCurrentSize = 180;

    // Start is called before the first frame update
    void Start()
    {
        functionsController = objectFunctionsController.GetComponent<FunctionsController>();
        functionsController.HideAllCanvases();
        DisableFunctionTypeButtons();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentStage)
        {
            case BattleStage.EntranceAnimation:
                EntranceAnimationUpdate();
                break;
            case BattleStage.UserInput:
                if (!UserInputStartDone)
                {
                    bigFontCurrentSize = -1;
                    centerMessageText.GetComponent<Text>().fontSize = -1;
                    UserInputStartDone = true;
                    timer = new Timer(10.0f, true);
                    EnableeFunctionTypeButtons();
                }
                UserInputUpdate();
                break;
            case BattleStage.DamageAnimation:
                break;
        }
    }

    private void UserInputUpdate()
    {
        if (timer.isTimerDone())
        {
            DisableFunctionTypeButtons();
            functionsController.HideAllCanvases();
            currentStage = BattleStage.DamageAnimation;
        }
        timer.Update();
        RefreshTimerText();
    }

    private void EntranceAnimationUpdate()
    {
        if (entranceBlackRectangleLeft.GetComponent<Transform>().position.x > -14)
        {
            Vector3 tempMove = new Vector3(5.0f, 0, 0);
            entranceBlackRectangleLeft.GetComponent<Transform>().position -= tempMove * Time.deltaTime;
            entranceBlackRectangleRight.GetComponent<Transform>().position += tempMove * Time.deltaTime;
            if (centerMessageText.GetComponent<Text>().fontSize > 0)
            {
                bigFontCurrentSize -= 100 * Time.deltaTime;
                int bigFontCurrentSizeRounded = (int)Mathf.Round(bigFontCurrentSize);
                centerMessageText.GetComponent<Text>().fontSize = bigFontCurrentSizeRounded;
            }
            else
            {
                bigFontCurrentSize = -1;
                centerMessageText.GetComponent<Text>().fontSize = -1;
            }
        }
        else
        {
            currentStage = BattleStage.UserInput;
        }
    }

    private void DisableFunctionTypeButtons()
    {
        buttonCuadratica.GetComponent<Button>().interactable = false;
        buttonCubica.GetComponent<Button>().interactable = false;
        buttonTrigonometrica.GetComponent<Button>().interactable = false;
        buttonExponencial.GetComponent<Button>().interactable = false;
        buttonLineal.GetComponent<Button>().interactable = false;
    }

    private void EnableeFunctionTypeButtons()
    {
        buttonCuadratica.GetComponent<Button>().interactable = true;
        buttonCubica.GetComponent<Button>().interactable = true;
        buttonTrigonometrica.GetComponent<Button>().interactable = true;
        buttonExponencial.GetComponent<Button>().interactable = true;
        buttonLineal.GetComponent<Button>().interactable = true;
    }

    public void RefreshTimerText()
    {
        string temp = System.Math.Round(timer.timeRemaining, 2).ToString();
        if (temp.Contains("."))
        {
            while (temp.Length < 4)
            {
                temp += "0";
            }
        }
        else
        {
            temp += ".00";
        }
        temp += " s";
        objectTimer.GetComponent<Text>().text = temp;
    }
}
