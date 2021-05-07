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
    public GameObject buttonTrinomial;
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
    public Sprite sprEnemyBossFinal;

    //public GameObject enemyAttackIcon;
    public GameObject attackIcon;
    private Sprite currentEnemyAttackIcon;
    public Sprite sprEnemyAttackIcon;
    public Sprite sprBossAttackIcon;
    public Sprite sprPlayerFireIcon;
    public Sprite sprPlayerIceIcon;
    public Sprite sprPlayerWaterIcon;
    public Sprite sprPlayerEarthIcon;
    public Sprite sprPlayerWindIcon;

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
    private bool EndBattleStartDone = false;
    private float bigFontCurrentSize = 180;

    public GameObject grapherObject;
    private GraphManager graphManager;
    int hits;
    int misses;
    public GameObject hitPercentageObject;
    public GameObject okButton;

    public GameObject soundManagerObject;
    public ControllerAudio soundManager;

    public GameObject musicController;
    public ControllerAudioMusic controllerAudioMusic;

    public DBInterface dbInterface;
    public Savedata savedata;
    private char area;
    private int level;

    //Animation objects
    public GameObject playerAnimationObject;
    public GameObject enemyAnimationObject;

    //DamageAnimation variables
    private enum DamageAnimationPart { PercentageMessageShow, PercentageMessageHide, RoundMessageShow, RoundMessageHide, RoundMessageWait, EndBattle }
    private DamageAnimationPart damageAnimationPart = DamageAnimationPart.PercentageMessageShow;
    private bool DamageAnimationPartRoundMessageWaitDone = true;
    /*bool isPercentageMessageShown = false;
    bool isPercentageMessageHidden = false;
    bool isRoundMessageShown = false;
    bool isRoundMessageHidden = false;*/

    private bool isCuadraticaActive = true;
    private bool isCubicaActive = true;
    private bool isTrigonometricaActive = true;
    private bool isTrinomialActive = true;
    private bool isLinealActive = true;
    private List<int> availableFunctionsNumbers = new List<int>();

    private bool isOKButtonPressed = false;
    private double matchTime = 0f;
    private double matchPercentSum = 0f;
    private double matchPercentTimes = 0f;
    private double matchHitP = 0f;

    void Awake()
    {
        musicController = GameObject.Find("MusicController");
        savedata = GameObject.Find("Savedata").GetComponent<Savedata>();
        dbInterface = GameObject.Find("DBInterface").GetComponent<DBInterface>();
    }

    // Start is called before the first frame update
    void Start()
    {
        //Initialize music
        controllerAudioMusic = musicController.GetComponent<ControllerAudioMusic>();
        //Set enemy sprite based on the savedata current level
        PrepareEnemy();
        //Hide all the function canvases at the start
        functionsController = objectFunctionsController.GetComponent<FunctionsController>();
        functionsController.HideAllCanvases();
        DisableFunctionTypeButtons();

        //Set default attack icons
        Color tempColor;
        attackIcon.GetComponent<SpriteRenderer>().sprite = sprPlayerFireIcon;
        tempColor = attackIcon.GetComponent<SpriteRenderer>().color;
        tempColor.a = 0f;
        attackIcon.GetComponent<SpriteRenderer>().color = tempColor;

        /*tempColor = enemyAttackIcon.GetComponent<SpriteRenderer>().color;
        tempColor.a = 0f;
        enemyAttackIcon.GetComponent<SpriteRenderer>().color = tempColor;*/

        soundManager = soundManagerObject.GetComponent<ControllerAudio>();

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
                    okButton.GetComponent<Button>().interactable = true;
                    functionsController.SetAllVariablesEspacioToOne();
                    graphManager.DeleteTargetCircles();
                    if (level == 6)
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            SetFunctionEnabledOrDisabled(i, true);
                        }
                        SetFunctionEnabledOrDisabled(Random.Range(0, 5), false);
                        SetFunctionEnabledOrDisabled(Random.Range(0, 5), false);
                    }
                    UpdateBlockedAndEnabledFunctions();
                    graphManager.GenerateRandomTargets(availableFunctionsNumbers[Random.Range(0, availableFunctionsNumbers.Count)]);
                    GetHitsAndMisses();
                    hits = 0; //This was cancelled to avoid percentage when beginning (when there is no function)
                    EnableFunctionTypeButtons();
                    isOKButtonPressed = false;
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
                    okButton.GetComponent<Button>().interactable = false;
                    graphManager.DeleteUserCircles();
                    graphManager.DeleteTargetCircles();
                    float playerHealthDifference = Mathf.Round(100.0f - hitPercentage);
                    playerHealth -= playerHealthDifference;
                    savedata.dmgReceived += playerHealthDifference;
                    float enemyHealthDifference = Mathf.Round(hitPercentage);
                    enemyHealth -= enemyHealthDifference;
                    savedata.dmgDone += enemyHealthDifference;
                    savedata.percentSum += Mathf.Round(hitPercentage);
                    savedata.percentTimes++;
                    savedata.hitP = Mathf.Round(savedata.percentSum / savedata.percentTimes);
                    matchPercentSum += Mathf.Round(hitPercentage);
                    matchPercentTimes++;
                    matchHitP = Mathf.Round((float)matchPercentSum / (float)matchPercentTimes);
                    soundManager.PlaySound(soundManager.sndDamage);
                    if (Mathf.Round(hitPercentage) == 0)
                    {
                        attackIcon.GetComponent<SpriteRenderer>().sprite = currentEnemyAttackIcon;
                    }
                }
                UpdatePlayerHealth();
                UpdateEnemyHealth();
                switch (damageAnimationPart)
                {
                    case DamageAnimationPart.PercentageMessageShow:
                        ShowMidScreenAttackIcons(2f);
                        if (ShowMidScreenMessage("-" + (Mathf.Round(100.0f - hitPercentage)) + "        -" + (Mathf.Round(hitPercentage)), 200f, Color.red))
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
                        HideMidScreenAttackIcons(2f);
                        if (HideMidScreenMessage(200f))
                        {
                            timer.ResumeTimer();
                            if (timer.IsTimerDone())
                            {
                                if (currentPlayerHealth == 0)
                                {
                                    timer.SetTimerTime(2.5f);
                                }
                                else
                                {
                                    timer.SetTimerTime(1.5f);
                                }
                                DamageAnimationPartRoundMessageWaitDone = false;
                                damageAnimationPart = DamageAnimationPart.RoundMessageWait;
                            }
                        }
                        break;
                    case DamageAnimationPart.RoundMessageWait:
                        if ((currentPlayerHealth == playerHealth) && (currentEnemyHealth == enemyHealth))
                        {
                            if (!DamageAnimationPartRoundMessageWaitDone)
                            {
                                DamageAnimationPartRoundMessageWaitDone = true;
                                if ((currentPlayerHealth == 0) && (currentEnemyHealth == 0))
                                {
                                    controllerAudioMusic.StopSong();
                                    soundManager.PlaySound(soundManager.sndDraw);
                                }
                                else if (currentPlayerHealth == 0)
                                {
                                    controllerAudioMusic.StopSong();
                                    soundManager.PlaySound(soundManager.sndDefeat);
                                }
                                else if (currentEnemyHealth == 0)
                                {
                                    controllerAudioMusic.StopSong();
                                    soundManager.PlaySound(soundManager.sndWin);
                                }
                            }
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
                        if (ShowMidScreenMessage("NUEVA RONDA", 200f, Color.white))
                        {
                            timer.ResumeTimer();
                            if (timer.IsTimerDone())
                            {
                                timer.SetTimerTime(0.5f);
                                soundManager.PlaySound(soundManager.sndNewRound);
                                damageAnimationPart = DamageAnimationPart.RoundMessageHide;
                            }
                        }
                        break;
                    case DamageAnimationPart.RoundMessageHide:
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
                        if (!EndBattleStartDone)
                        {
                            EndBattleStartDone = true;
                            if (playerWon)
                            {
                                SaveProgress();
                                Debug.Log("Score: " + System.Math.Round(matchHitP, 2));
                                Debug.Log("Time: " + System.Math.Round(matchTime, 2));
                                UploadScore(System.Math.Round(matchTime, 2), System.Math.Round(matchHitP, 2));
                            }
                        }
                        if (HideMidScreenMessage(200f))
                        {
                            timer.ResumeTimer();
                            if (timer.IsTimerDone())
                            {
                                if ((level == 6) && (playerWon))
                                {
                                    savedata.SetAfterCreditsScene("WorldMap");
                                    SceneManager.LoadScene("Credits");
                                }
                                else
                                {
                                    SceneManager.LoadScene("WorldMap");
                                }
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
            if (!isOKButtonPressed)
            {
                matchTime += timerTime;
            }
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
        hitPercentage = Mathf.Round(((float)hits / ((float)hits + (float)misses)) * 100f);
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

    private void ShowMidScreenAttackIcons(float speed)
    {
        Color tempColor;
        tempColor = attackIcon.GetComponent<SpriteRenderer>().color;
        if (tempColor.a < 1f)
        {
            tempColor.a += speed * Time.deltaTime;
        }
        attackIcon.GetComponent<SpriteRenderer>().color = tempColor;

        /*tempColor = enemyAttackIcon.GetComponent<SpriteRenderer>().color;
        if (tempColor.a < 1f)
        {
            tempColor.a += speed * Time.deltaTime;
        }
        enemyAttackIcon.GetComponent<SpriteRenderer>().color = tempColor;*/
    }

    private void HideMidScreenAttackIcons(float speed)
    {
        Color tempColor;
        tempColor = attackIcon.GetComponent<SpriteRenderer>().color;
        if (tempColor.a > 0f)
        {
            tempColor.a -= speed * Time.deltaTime;
        }
        attackIcon.GetComponent<SpriteRenderer>().color = tempColor;

        /*tempColor = enemyAttackIcon.GetComponent<SpriteRenderer>().color;
        if (tempColor.a > 0f)
        {
            tempColor.a -= speed * Time.deltaTime;
        }
        enemyAttackIcon.GetComponent<SpriteRenderer>().color = tempColor;*/
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
        buttonTrinomial.GetComponent<Button>().interactable = false;
        buttonLineal.GetComponent<Button>().interactable = false;
    }

    /*public void HideFunctionType(int functionNumber)
    {
        switch (functionNumber)
        {
            case 0:
                buttonCuadratica.SetActive(false);
                break;
            case 1:
                buttonCubica.SetActive(false);
                break;
            case 2:
                buttonTrigonometrica.SetActive(false);
                break;
            case 3:
                buttonTrinomial.SetActive(false);
                break;
            case 4:
                buttonLineal.SetActive(false);
                break;
            default:
                break;
        }
    }*/

    public void UpdateBlockedAndEnabledFunctions()
    {
        availableFunctionsNumbers = new List<int>();
        if (isCuadraticaActive)
        {
            availableFunctionsNumbers.Add(0);
            buttonCuadratica.SetActive(true);
        }
        else
        {
            buttonCuadratica.SetActive(false);
        }
        if (isCubicaActive)
        {
            availableFunctionsNumbers.Add(1);
            buttonCubica.SetActive(true);
        }
        else
        {
            buttonCubica.SetActive(false);
        }
        if (isTrigonometricaActive)
        {
            availableFunctionsNumbers.Add(2);
            buttonTrigonometrica.SetActive(true);
        }
        else
        {
            buttonTrigonometrica.SetActive(false);
        }
        if (isTrinomialActive)
        {
            availableFunctionsNumbers.Add(3);
            buttonTrinomial.SetActive(true);
        }
        else
        {
            buttonTrinomial.SetActive(false);
        }
        if (isLinealActive)
        {
            availableFunctionsNumbers.Add(4);
            buttonLineal.SetActive(true);
        }
        else
        {
            buttonLineal.SetActive(false);
        }
    }

    private void EnableFunctionTypeButtons()
    {
        buttonCuadratica.GetComponent<Button>().interactable = true;
        buttonCubica.GetComponent<Button>().interactable = true;
        buttonTrigonometrica.GetComponent<Button>().interactable = true;
        buttonTrinomial.GetComponent<Button>().interactable = true;
        buttonLineal.GetComponent<Button>().interactable = true;
    }

    private void SetFunctionEnabledOrDisabled(int functionNumberType, bool isEnabled)
    {
        switch (functionNumberType)
        {
            case 0:
                isCuadraticaActive = isEnabled ? true : false;
                break;
            case 1:
                isCubicaActive = isEnabled ? true : false;
                break;
            case 2:
                isTrigonometricaActive = isEnabled ? true : false;
                break;
            case 3:
                isTrinomialActive = isEnabled ? true : false;
                break;
            case 4:
                isLinealActive = isEnabled ? true : false;
                break;
            default:
                break;
        }
    }

    public void RefreshTimerText()
    {
        string temp = System.Math.Round(timer.timeRemaining, 2).ToString();
        if (System.Math.Round(timer.timeRemaining, 2) < 10f)
        {
            temp = "0" + temp;
        }
        if (temp.Contains("."))
        {
            while (temp.Length < 5)
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
        //string currentLevel = "A4";
        string currentLevel = savedata.currentLevel;
        //Debug.Log("Current level: " + currentLevel);
        char[] currentLevelChars = currentLevel.ToCharArray();
        area = currentLevelChars[0];
        level = (int)System.Char.GetNumericValue(currentLevelChars[1]);
        //enemyAttackIcon.GetComponent<SpriteRenderer>().sprite = sprEnemyAttackIcon;
        currentEnemyAttackIcon = sprEnemyAttackIcon;
        if (level == 6)
        {
            controllerAudioMusic.PlaySong(controllerAudioMusic.bgmAreaBoss);
            currentEnemyAttackIcon = sprBossAttackIcon;
        }
        else if (level == 5)
        {
            controllerAudioMusic.PlaySong(controllerAudioMusic.bgmAreaBoss);
            //enemyAttackIcon.GetComponent<SpriteRenderer>().sprite = sprBossAttackIcon;
            currentEnemyAttackIcon = sprBossAttackIcon;

            switch (area)
            {
                case 'A':
                    SetFunctionEnabledOrDisabled(0, false);
                    SetFunctionEnabledOrDisabled(2, false);
                    break;
                case 'B':
                    SetFunctionEnabledOrDisabled(1, false);
                    SetFunctionEnabledOrDisabled(3, false);
                    break;
                case 'C':
                    SetFunctionEnabledOrDisabled(2, false);
                    SetFunctionEnabledOrDisabled(4, false);
                    break;
                case 'D':
                    SetFunctionEnabledOrDisabled(0, false);
                    SetFunctionEnabledOrDisabled(4, false);
                    break;
                default:
                    break;
            }
        }
        else
        {
            switch (area)
            {
                case 'A':
                    controllerAudioMusic.PlaySong(controllerAudioMusic.bgmAreaA);
                    break;
                case 'B':
                    controllerAudioMusic.PlaySong(controllerAudioMusic.bgmAreaB);
                    break;
                case 'C':
                    controllerAudioMusic.PlaySong(controllerAudioMusic.bgmAreaC);
                    break;
                case 'D':
                    controllerAudioMusic.PlaySong(controllerAudioMusic.bgmAreaD);
                    break;
                default:
                    Debug.Log("Algo pudo salir mal al preparar el sprite de jefe.");
                    break;
            }
        }
        UpdateBlockedAndEnabledFunctions();
        switch (level)
        {
            case 1:
                enemySprite.GetComponent<SpriteRenderer>().sprite = sprEnemy0;
                enemyMaxHealth = 200;
                //enemyMaxHealth = 20;
                timerTime = 40.0f;
                break;
            case 2:
                enemySprite.GetComponent<SpriteRenderer>().sprite = sprEnemy1;
                enemyMaxHealth = 250;
                //enemyMaxHealth = 25;
                timerTime = 38.0f;
                break;
            case 3:
                enemySprite.GetComponent<SpriteRenderer>().sprite = sprEnemy2;
                enemyMaxHealth = 300;
                //enemyMaxHealth = 30;
                timerTime = 34.0f;
                break;
            case 4:
                enemySprite.GetComponent<SpriteRenderer>().sprite = sprEnemy3;
                enemyMaxHealth = 350;
                //enemyMaxHealth = 35;
                timerTime = 30.0f;
                break;
            case 5:
                enemyMaxHealth = 350;
                //enemyMaxHealth = 35;
                timerTime = 28.0f;
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
            case 6:
                enemySprite.GetComponent<SpriteRenderer>().sprite = sprEnemyBossFinal;
                enemyMaxHealth = 400;
                //enemyMaxHealth = 40;
                timerTime = 25.0f;
                break;
            default:
                Debug.Log("Algo pudo salir mal al preparar el sprite del enemigo.");
                break;
        }
        enemyHealth = enemyMaxHealth;
        currentEnemyHealth = enemyHealth;
        //Debug.Log("enemy health: " + enemyHealth);
    }

    private void UpdatePlayerHealth()
    {
        //Note: 4.3f is the distance that the player mask has to move to cover the entire health bar
        if (currentPlayerHealth <= 0)
        {
            currentPlayerHealth = 0;
            playerHealth = currentPlayerHealth;
            playerHealthBarHeart.GetComponent<SpriteRenderer>().sprite = playerHealthBarBrokenHeart;
            Animator playerAnimator = playerAnimationObject.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("isPlayerDefeated", true);
            }
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
            Animator enemyAnimator = enemyAnimationObject.GetComponent<Animator>();
            if (enemyAnimator != null)
            {
                enemyAnimator.SetBool("isEnemyDefeated", true);
            }
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

    public void EndTimer()
    {
        isOKButtonPressed = true;
        matchTime += timerTime - timer.timeRemaining;
        timer.timeRemaining = 0;
    }

    public void SaveProgress()
    {
        string savedataProgress = savedata.progress;
        char[] savedataProgressChars = savedataProgress.ToCharArray();
        int saveCharNumber = 1;
        switch (area)
        {
            case 'A':
                saveCharNumber = 1;
                break;
            case 'B':
                saveCharNumber = 3;
                break;
            case 'C':
                saveCharNumber = 5;
                break;
            case 'D':
                saveCharNumber = 7;
                break;
            default:
                Debug.Log("Algo pudo salir mal al preparar el sprite de jefe.");
                break;
        }
        int progressAreaLevel = (int)System.Char.GetNumericValue(savedataProgressChars[saveCharNumber]);
        if (level > progressAreaLevel)
        {
            savedataProgressChars[saveCharNumber] = level.ToString().ToCharArray()[0];
            savedata.progress = new string(savedataProgressChars);
            //Debug.Log("New progress string: " + savedata.progress);
        }
        int totalLevel = 0;
        totalLevel += (int)System.Char.GetNumericValue(savedataProgressChars[1]);
        totalLevel += (int)System.Char.GetNumericValue(savedataProgressChars[3]);
        totalLevel += (int)System.Char.GetNumericValue(savedataProgressChars[5]);
        totalLevel += (int)System.Char.GetNumericValue(savedataProgressChars[7]);
        if (totalLevel > savedata.level)
        {
            savedata.level = totalLevel;
        }
    }

    public void SetPlayerElementIcon(int elementNumber)
    {
        switch (elementNumber)
        {
            case 0:
                attackIcon.GetComponent<SpriteRenderer>().sprite = sprPlayerFireIcon;
                break;
            case 1:
                attackIcon.GetComponent<SpriteRenderer>().sprite = sprPlayerIceIcon;
                break;
            case 2:
                attackIcon.GetComponent<SpriteRenderer>().sprite = sprPlayerWaterIcon;
                break;
            case 3:
                attackIcon.GetComponent<SpriteRenderer>().sprite = sprPlayerEarthIcon;
                break;
            case 4:
                attackIcon.GetComponent<SpriteRenderer>().sprite = sprPlayerWindIcon;
                break;
            default:
                break;
        }
    }

    public void AccomodateFunctionsNumbersList()
    {

        availableFunctionsNumbers.Add(0);
    }

    public void UploadScore(double time, double score)
    {
        dbInterface.InsertHighscore(savedata.mageName, savedata.mageClass, "" + area + level, score, time);
    }
}
