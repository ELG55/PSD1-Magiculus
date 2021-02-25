using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BattleController : MonoBehaviour
{
    public enum BattleStage { EntranceAnimation, UserInput, DamageAnimation }
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

    public GameObject enemySprite;
    public Sprite sprEnemy0;
    public Sprite sprEnemy1;
    public Sprite sprEnemy2;
    public Sprite sprEnemy3;
    public Sprite sprEnemyBossA;
    public Sprite sprEnemyBossB;
    public Sprite sprEnemyBossC;
    public Sprite sprEnemyBossD;

    public float timerTime = 5.0f;
    public float hitPercentage = 0;
    private float playerMaxHealth = 100;
    private float playerHealth = 100;
    private float currentPlayerHealth = 100;
    private float enemyMaxHealth = 100;
    private float enemyHealth = 100;
    private float currentEnemyHealth = 100;

    private Vector3 playerHealthBarMaskStartPosition;
    public GameObject playerHealthBarMask;
    public GameObject playerHealthBarHeart;
    public Sprite playerHealthBarBrokenHeart;
    private Vector3 enemyHealthBarMaskStartPosition;
    public GameObject enemyHealthBarMask;
    public GameObject enemyHealthBarHeart;
    public Sprite enemyHealthBarBrokenHeart;

    private bool playerWon = false;

    private bool UserInputStartDone = false;
    private bool DamageAnimationStartDone = false;
    private float bigFontCurrentSize = 180;

    public GameObject grapherObject;
    private GraphManager graphManager;
    int hits;
    int misses;
    public GameObject hitPercentageObject;

    //DamageAnimation variables
    private enum DamageAnimationPart { PercentageMessageShow, PercentageMessageHide, RoundMessageShow, RoundMessageHide, RoundMessageWait, EndBattle }
    private DamageAnimationPart damageAnimationPart = DamageAnimationPart.PercentageMessageShow;
    /*bool isPercentageMessageShown = false;
    bool isPercentageMessageHidden = false;
    bool isRoundMessageShown = false;
    bool isRoundMessageHidden = false;*/

    // Start is called before the first frame update
    void Start()
    {
        //Set enemy sprite based on the savedata current level
        PrepareEnemy();
        //Hide all the function canvases at the start
        functionsController = objectFunctionsController.GetComponent<FunctionsController>();
        functionsController.HideAllCanvases();
        DisableFunctionTypeButtons();

        playerHealthBarMaskStartPosition = playerHealthBarMask.GetComponent<Transform>().position;
        enemyHealthBarMaskStartPosition = enemyHealthBarMask.GetComponent<Transform>().position;

        graphManager = grapherObject.GetComponent<GraphManager>();

        timer = new Timer(timerTime, false);
    }

    // Update is called once per frame
    void Update()
    {
        timer.Update();
        switch (currentStage)
        {
            case BattleStage.EntranceAnimation:
                EntranceAnimationUpdate();
                break;
            case BattleStage.UserInput:
                if (!UserInputStartDone)
                {
                    DamageAnimationStartDone = false;
                    UserInputStartDone = true;
                    bigFontCurrentSize = -1;
                    centerMessageText.GetComponent<Text>().fontSize = -1;
                    timer.SetTimerTime(timerTime);
                    timer.ResumeTimer();
                    graphManager.GenerateRandomTargets();
                    GetHitsAndMisses();
                    Debug.Log("hits: " + hits);
                    Debug.Log("misses: " + misses);
                    EnableFunctionTypeButtons();
                }                
                UserInputUpdate();
                break;
            case BattleStage.DamageAnimation:
                if (!DamageAnimationStartDone)
                {
                    UserInputStartDone = false;
                    DamageAnimationStartDone = true;
                    timer.SetTimerTime(1.5f);
                    timer.StopTimer();
                    playerHealth -= 20;
                    enemyHealth -= 400;
                }
                UpdatePlayerHealth();
                UpdateEnemyHealth();
                switch (damageAnimationPart)
                {
                    case DamageAnimationPart.PercentageMessageShow:
                        Debug.Log("PercentageMessageShow");
                        if (ShowMidScreenMessage((Mathf.Round(100.0f - hitPercentage)) + "% / " + (Mathf.Round(hitPercentage))+ "%", 200f, Color.red))
                        {
                            timer.ResumeTimer();
                            if (timer.IsTimerDone())
                            {
                                timer.SetTimerTime(0.5f);
                                damageAnimationPart = DamageAnimationPart.PercentageMessageHide;
                            }
                        }
                        break;
                    case DamageAnimationPart.PercentageMessageHide:
                        Debug.Log("PercentageMessageHide");
                        if (HideMidScreenMessage(200f))
                        {
                            timer.ResumeTimer();
                            if (timer.IsTimerDone())
                            {
                                timer.SetTimerTime(1.5f);
                                damageAnimationPart = DamageAnimationPart.RoundMessageWait;
                            }
                        }
                        break;
                    case DamageAnimationPart.RoundMessageWait:
                        Debug.Log("RoundMessageWait");
                        if ((currentPlayerHealth == playerHealth) && (currentEnemyHealth == enemyHealth))
                        {
                            if ((currentPlayerHealth == 0) && (currentEnemyHealth == 0))
                            {
                                playerWon = false;
                                ShowMidScreenMessage("DOBLE MUERTE", 100f, Color.yellow);
                                if (ExitAnimationUpdate())
                                {
                                    timer.ResumeTimer();
                                    if (timer.IsTimerDone())
                                    {
                                        timer.SetTimerTime(1.5f);
                                        damageAnimationPart = DamageAnimationPart.EndBattle;
                                    }
                                }
                            }
                            else if (currentPlayerHealth == 0)
                            {
                                playerWon = false;
                                ShowMidScreenMessage("DERROTADO", 100f, Color.red);
                                if (ExitAnimationUpdate())
                                {
                                    timer.ResumeTimer();
                                    if (timer.IsTimerDone())
                                    {
                                        timer.SetTimerTime(1.5f);
                                        damageAnimationPart = DamageAnimationPart.EndBattle;
                                    }
                                }
                            }
                            else if (currentEnemyHealth == 0)
                            {
                                playerWon = true;
                                ShowMidScreenMessage("VICTORIA", 100f, Color.green);
                                if (ExitAnimationUpdate())
                                {
                                    timer.ResumeTimer();
                                    if (timer.IsTimerDone())
                                    {
                                        timer.SetTimerTime(1.5f);
                                        damageAnimationPart = DamageAnimationPart.EndBattle;
                                    }
                                }
                            }
                            else
                            {
                                damageAnimationPart = DamageAnimationPart.RoundMessageShow;
                            }   
                        }
                        break;
                    case DamageAnimationPart.RoundMessageShow:
                        Debug.Log("RoundMessageShow");
                        if (ShowMidScreenMessage("NUEVA RONDA", 200f, Color.white))
                        {
                            timer.ResumeTimer();
                            if (timer.IsTimerDone())
                            {
                                timer.SetTimerTime(0.5f);
                                damageAnimationPart = DamageAnimationPart.RoundMessageHide;
                            }
                        }
                        break;
                    case DamageAnimationPart.RoundMessageHide:
                        Debug.Log("RoundMessageHide");
                        if (HideMidScreenMessage(200f))
                        {
                            timer.ResumeTimer();
                            if (timer.IsTimerDone())
                            {
                                timer.SetTimerTime(1.5f);
                                damageAnimationPart = DamageAnimationPart.PercentageMessageShow;
                                currentStage = BattleStage.UserInput;
                            }
                        }
                        break;
                    case DamageAnimationPart.EndBattle:
                        Debug.Log("EndBattle");
                        Debug.Log("Aquí hay que continuar.");
                        if (HideMidScreenMessage(200f))
                        {
                            timer.ResumeTimer();
                            if (timer.IsTimerDone())
                            {
                                SceneManager.LoadScene("WorldMap");
                            }
                        }
                        break;
                    default:
                        Debug.Log("Algo pudo haber salido mal en la parte de animación de mensajes en media partida");
                        break;
                }
                break;
        }
    }

    private void UserInputUpdate()
    {
        if (timer.IsTimerDone())
        {
            timer.SetTimerTime(0);
            RefreshTimerText();
            DisableFunctionTypeButtons();
            functionsController.HideAllCanvases();
            currentStage = BattleStage.DamageAnimation;
        }
        UpdateHitPercentage();
        RefreshTimerText();
    }

    private void UpdateHitPercentage()
    {
        //GetHitsAndMisses();
        Debug.Log("hits: " + hits);
        Debug.Log("misses: " + misses);
        hitPercentage = Mathf.Round(((float)hits / ((float)hits + (float)misses)) * 100f);
        Debug.Log("hitPercentage: " + hitPercentage);
        hitPercentageObject.GetComponent<Text>().text = hitPercentage.ToString();
    }

    private void EntranceAnimationUpdate()
    {
        if (entranceBlackRectangleLeft.GetComponent<Transform>().position.x > -14)
        {
            Vector3 tempMove = new Vector3(5.0f, 0, 0);
            entranceBlackRectangleLeft.GetComponent<Transform>().position -= tempMove * Time.deltaTime;
            entranceBlackRectangleRight.GetComponent<Transform>().position += tempMove * Time.deltaTime;
            HideMidScreenMessage(100f);
            /*if (centerMessageText.GetComponent<Text>().fontSize > 0)
            {
                bigFontCurrentSize -= 100 * Time.deltaTime;
                int bigFontCurrentSizeRounded = (int)Mathf.Round(bigFontCurrentSize);
                centerMessageText.GetComponent<Text>().fontSize = bigFontCurrentSizeRounded;
            }
            else
            {
                bigFontCurrentSize = -1;
                centerMessageText.GetComponent<Text>().fontSize = -1;
            }*/
        }
        else
        {
            currentStage = BattleStage.UserInput;
        }
    }

    private bool ExitAnimationUpdate()
    {
        if (entranceBlackRectangleLeft.GetComponent<Transform>().position.x < -5)
        {
            Vector3 tempMove = new Vector3(5.0f, 0, 0);
            entranceBlackRectangleLeft.GetComponent<Transform>().position += tempMove * Time.deltaTime;
            entranceBlackRectangleRight.GetComponent<Transform>().position -= tempMove * Time.deltaTime;
            return false;
            /*if (centerMessageText.GetComponent<Text>().fontSize > 0)
            {
                bigFontCurrentSize -= 100 * Time.deltaTime;
                int bigFontCurrentSizeRounded = (int)Mathf.Round(bigFontCurrentSize);
                centerMessageText.GetComponent<Text>().fontSize = bigFontCurrentSizeRounded;
            }
            else
            {
                bigFontCurrentSize = -1;
                centerMessageText.GetComponent<Text>().fontSize = -1;
            }*/
        }
        else
        {
            return true;
        }
    }

    private bool ShowMidScreenMessage(string message, float speed, Color color)
    {
        if (centerMessageText.GetComponent<Text>().fontSize < 180)
        {
            centerMessageText.GetComponent<Text>().color = color;
            centerMessageText.GetComponent<Text>().text = message;
            bigFontCurrentSize += speed * Time.deltaTime;
            int bigFontCurrentSizeRounded = (int)Mathf.Round(bigFontCurrentSize);
            centerMessageText.GetComponent<Text>().fontSize = bigFontCurrentSizeRounded;
            return false;
        }
        else
        {
            bigFontCurrentSize = 180;
            centerMessageText.GetComponent<Text>().fontSize = 180;
            return true;
        }
    }

    private bool HideMidScreenMessage(float speed)
    {
        if (centerMessageText.GetComponent<Text>().fontSize > 0)
        {
            bigFontCurrentSize -= speed * Time.deltaTime;
            int bigFontCurrentSizeRounded = (int)Mathf.Round(bigFontCurrentSize);
            centerMessageText.GetComponent<Text>().fontSize = bigFontCurrentSizeRounded;
            return false;
        }
        else
        {
            bigFontCurrentSize = -1;
            centerMessageText.GetComponent<Text>().fontSize = -1;
            return true;
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

    private void EnableFunctionTypeButtons()
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

    private void PrepareEnemy()
    {
        //DEBUG ONLY
        string currentLevel = "C3";
        //string currentLevel = GameObject.Find("Savedata").GetComponent<Savedata>().currentLevel;
        char[] currentLevelChars = currentLevel.ToCharArray();
        char area = currentLevelChars[0];
        int level = (int)System.Char.GetNumericValue(currentLevelChars[1]);
        switch (level)
        {
            case 0:
                enemySprite.GetComponent<SpriteRenderer>().sprite = sprEnemy0;
                enemyMaxHealth = 200;
                break;
            case 1:
                enemySprite.GetComponent<SpriteRenderer>().sprite = sprEnemy1;
                enemyMaxHealth = 300;
                break;
            case 2:
                enemySprite.GetComponent<SpriteRenderer>().sprite = sprEnemy2;
                enemyMaxHealth = 400;
                break;
            case 3:
                enemySprite.GetComponent<SpriteRenderer>().sprite = sprEnemy3;
                enemyMaxHealth = 600;
                break;
            case 4:
                enemyMaxHealth = 1000;
                switch (area)
                {
                    case 'A':
                        enemySprite.GetComponent<SpriteRenderer>().sprite = sprEnemyBossA;
                        break;
                    case 'B':
                        enemySprite.GetComponent<SpriteRenderer>().sprite = sprEnemyBossB;
                        break;
                    case 'C':
                        enemySprite.GetComponent<SpriteRenderer>().sprite = sprEnemyBossC;
                        break;
                    case 'D':
                        enemySprite.GetComponent<SpriteRenderer>().sprite = sprEnemyBossD;
                        break;
                    default:
                        Debug.Log("Algo pudo salir mal al preparar el sprite de jefe.");
                        break;
                }
                break;
            default:
                Debug.Log("Algo pudo salir mal al preparar el sprite del enemigo.");
                break;
        }
        enemyHealth = enemyMaxHealth;
        currentEnemyHealth = enemyHealth;
        Debug.Log("enemy health: " + enemyHealth);
    }

    private void UpdatePlayerHealth()
    {
        //Note: 4.3f is the distance that the player mask has to move to cover the entire health bar
        if (currentPlayerHealth <= 0)
        {
            currentPlayerHealth = 0;
            playerHealth = currentPlayerHealth;
            playerHealthBarHeart.GetComponent<SpriteRenderer>().sprite = playerHealthBarBrokenHeart;
        }
        if (currentPlayerHealth > playerHealth)
        {
            float tempHealthReduction = (playerMaxHealth / 3) * Time.deltaTime;
            currentPlayerHealth -= tempHealthReduction;
            float moveHealth = tempHealthReduction * (4.3f / playerMaxHealth);
            Vector3 tempMove = new Vector3(moveHealth, 0, 0);
            playerHealthBarMask.GetComponent<Transform>().position -= tempMove;
        }
        else
        {
            currentPlayerHealth = playerHealth;
            float moveHealth = ((playerMaxHealth - currentPlayerHealth) / playerMaxHealth) * 4.3f;
            Vector3 tempMove = new Vector3(moveHealth, 0, 0);
            //The player moves the mask to the left
            playerHealthBarMask.GetComponent<Transform>().position = playerHealthBarMaskStartPosition - tempMove;
        }
    }

    private void UpdateEnemyHealth()
    {
        //Note: 5.18f is the distance that the enemy mask has to move to cover the entire health bar
        if (currentEnemyHealth <= 0)
        {
            currentEnemyHealth = 0;
            enemyHealth = currentEnemyHealth;
            enemyHealthBarHeart.GetComponent<SpriteRenderer>().sprite = enemyHealthBarBrokenHeart;
        }
        if (currentEnemyHealth > enemyHealth)
        {
            float tempHealthReduction = (enemyMaxHealth/3) * Time.deltaTime;
            currentEnemyHealth -= tempHealthReduction;
            float moveHealth = tempHealthReduction * (5.18f / enemyMaxHealth);
            Vector3 tempMove = new Vector3(moveHealth, 0, 0);
            enemyHealthBarMask.GetComponent<Transform>().position += tempMove;
        }
        else
        {
            currentEnemyHealth = enemyHealth;
            float moveHealth = ((enemyMaxHealth - currentEnemyHealth) / enemyMaxHealth) * 5.18f;
            Vector3 tempMove = new Vector3(moveHealth, 0, 0);
            //The enemy moves the mask to the right
            enemyHealthBarMask.GetComponent<Transform>().position = enemyHealthBarMaskStartPosition + tempMove;
        }
    }

    public void GetHitsAndMisses()
    {
        int[] hitsAndMisses = graphManager.compareHard();
        hits = hitsAndMisses[0];
        misses = hitsAndMisses[1];
    }
}
