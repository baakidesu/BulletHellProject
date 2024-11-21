using System;
using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public enum GameState
    {
        Gameplay,
        Paused,
        GameOver,
        LevelUp
    }

    public GameState currentState;

    public GameState previousState;

    [Header("Screens")] 
    public GameObject pauseScreen;
    public GameObject resultScreen;
    public GameObject levelUpScreen;
    
    [Header("Current Stat Displays")] 
    public TMP_Text currentHealthDisplay;
    public TMP_Text currentRecoveryDisplay;
    public TMP_Text currentMoveSpeedDisplay;
    public TMP_Text currentMightDisplay;
    public TMP_Text currentProjectileSpeedDisplay;
    public TMP_Text currentMagnetDisplay;
    public TMP_Text timeSurvivedDisplay;
    
    [Header("Results Screen Displays")] 
    public Image chosenCharacterImage;
    public TMP_Text chosenCharacterName;
    public TMP_Text levelReachedDisplay;
    public List<Image> chosenWeaponsUI = new List<Image>(6);
    public List<Image> chosenPassiveItemsUI = new List<Image>(6);

    [Header("Stopwatch")] 
    public float timeLimit;
    private float stopwatch;
    public TMP_Text stopwatchDisplay;
    
    [Header ("Damage Text Settings")]
    public Canvas damageTextCanvas;
    public float textFontSize;
    public TMP_FontAsset textFont;
    public Camera referenceCamera;
    
    [HideInInspector]
    public bool isGameOver = false; //oyun bitti mi kontrolü
    
    [HideInInspector]
    public bool choosingLevel = false; // level ekranı açık mı kontrolü

    public GameObject playerObject; //player game object referansı. inventory sistem send message call'ı için açıldı.
    
    private void Awake()
    {
        if (instance == null)
        {
         instance = this;   
        }
        else
        {
            Debug.LogError("extra " + this + "deleted");
            Destroy(this);
        }
        DisableScreen();
    }

    private void Update()
    {
        switch (currentState)
        {
            case GameState.Gameplay:
                CheckForPauseAndResume();
                UpdateStopwatchTime();
                break;

            case GameState.Paused:
                CheckForPauseAndResume();
                break;

            case GameState.GameOver:
                if (!isGameOver)
                {
                    isGameOver = true;
                    Time.timeScale = 0f; //oyunu durdur
                    Debug.Log("Game Over");
                    DisplayResults();
                }
                break;
            
            case GameState.LevelUp:
                if (!choosingLevel)
                {
                    choosingLevel = true;
                    Time.timeScale = 0f;
                    levelUpScreen.SetActive(true);
                    Debug.Log("Upgrades Shown");
                }
                break;

            default:
                Debug.LogWarning("STATE DOES NOT EXIST");
                break;
        }
    }

    public static void GenerateFloatingText(string text, Transform target, float duration = 1f, float speed = 1f)
    {
        if (!instance.damageTextCanvas)
        {
            return;
        }

        if (!instance.referenceCamera)
        {
            instance.referenceCamera  = Camera.main;
        }
        
        instance.StartCoroutine(instance.GenerateFloatingTextCoroutine(text, target, duration, speed));
    }

    IEnumerator GenerateFloatingTextCoroutine(string text, Transform target, float duration = 1f, float speed = 50f)
    {
        GameObject textObj = new GameObject("Damage Floating Text");
        RectTransform rect = textObj.AddComponent<RectTransform>();
        TextMeshProUGUI tmPro = textObj.AddComponent<TextMeshProUGUI>();
        tmPro.text = text;
        tmPro.horizontalAlignment = HorizontalAlignmentOptions.Center;
        tmPro.verticalAlignment = VerticalAlignmentOptions.Middle;
        tmPro.fontSize = textFontSize;
        if (textFont)
        {
            tmPro.font = textFont;
        }
        rect.position = referenceCamera.WorldToScreenPoint(target.position);
        
        Destroy(textObj, duration);
        
        textObj.transform.SetParent(instance.damageTextCanvas.transform);

        WaitForEndOfFrame w = new WaitForEndOfFrame();
        float t = 0;
        float yOffset = 0;
        while (t < duration)
        {
            yield return w;
            t+= Time.deltaTime;
            
            tmPro.color = new Color(tmPro.color.r, tmPro.color.g, tmPro.color.b, 1-t / duration);
            
            yOffset += speed * Time.deltaTime;
            //rect.position = referenceCamera.WorldToScreenPoint(target.position + new Vector3(0, yOffset));
        }
    }

    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }

    public void PauseGame()
    {
        if (currentState != GameState.Paused)
        {
            previousState = currentState;
            ChangeState(GameState.Paused);
            Time.timeScale = 0f;
            pauseScreen.SetActive(true);
            Debug.Log("Game Paused");
        }

    }

    public void ResumeGame()
    {
        if (currentState == GameState.Paused)
        {
            ChangeState(previousState);
            Time.timeScale = 1f;
            pauseScreen.SetActive(false);
            Debug.Log("Game Resumed");
        }
    }

    public void CheckForPauseAndResume()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentState == GameState.Paused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
        
    }
    public void DisableScreen()
    {
        pauseScreen.SetActive(false);
        resultScreen.SetActive(false);
        levelUpScreen.SetActive(false);
    }

    public void GameOver()
    {
        timeSurvivedDisplay.text = stopwatchDisplay.text;
        ChangeState(GameState.GameOver);
    }

    void DisplayResults()
    {
        stopwatchDisplay.enabled = false;
        resultScreen.SetActive(true);
    }

    public void AssignChosenCharacterUI(CharacterScriptableObject chosenCharacterData)
    {
        chosenCharacterImage.sprite = chosenCharacterData.Icon;
        chosenCharacterName.text = chosenCharacterData.Name;
    }

    public void AssignLevelReachedUI(int levelReachedData)
    {
        levelReachedDisplay.text = levelReachedData.ToString();
    }

    public void AssignChosenWeaponsAndPassiveItemsUI(List<Image> chosenWeaponsData, List<Image> chosenPassiveItemsData)
    {
        if (chosenWeaponsData.Count != chosenWeaponsUI.Count ||
            chosenPassiveItemsData.Count != chosenPassiveItemsUI.Count)
        {
            Debug.Log("Chosen weapons and passive items data lists have different lengths");
        }
    
        //assign chosen weapons data to chosenWeaponsUI
        for (int i = 0; i < chosenWeaponsUI.Count; i++)
        {
            //check that the sprite of the corresponding element in chosenWeaponsData is not null
            if (chosenWeaponsData[i].sprite)
            {
                //enable the corresponding element int chosenWeaponsUI and set its sprite to the corresponding sprite in chosenWeaponData
                chosenWeaponsUI[i].enabled = true;
                chosenWeaponsUI[i].sprite = chosenWeaponsData[i].sprite;
            }
            else
            {
                //if the sprite is null, disable the corresponding element in chosenWeaponsUI
                chosenWeaponsUI[i].enabled = false;
            }
        }
        
        for (int i = 0; i < chosenPassiveItemsUI.Count; i++)
        {
            if (chosenPassiveItemsData[i].sprite)
            {
                chosenPassiveItemsUI[i].enabled = true;
                chosenPassiveItemsUI[i].sprite = chosenPassiveItemsData[i].sprite;
            }
            else
            {
                chosenPassiveItemsUI[i].enabled = false;
            }
        }
        
    }

    public void UpdateStopwatchTime()
    {
        stopwatch += Time.deltaTime;
        
        UpdateStopwatchDisplay();

        if (stopwatch >= timeLimit)
        {
            playerObject.SendMessage("Kill");
        }
    }

    public void UpdateStopwatchDisplay()
    {
        int minutes = Mathf.FloorToInt(stopwatch / 60);
        int seconds = Mathf.FloorToInt(stopwatch % 60);
        
        stopwatchDisplay.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StartLevelUp()
    {
        ChangeState(GameState.LevelUp);
        playerObject.SendMessage("RemoveAndApplyUpgrades");
    }

    public void EndLevelUp()
    {
        choosingLevel = false;
        Time.timeScale = 1f;
        levelUpScreen.SetActive(false);
        ChangeState(GameState.Gameplay);
    }
}
