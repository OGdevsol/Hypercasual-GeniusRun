using System;
using System.Collections;
using System.Collections.Generic;
using PathCreation.Examples;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Toggle = UnityEngine.UI.Toggle;

public class GameplayController : MonoBehaviour
{
    public static GameplayController instance;
    [SerializeField] private List<GameObject> LevelGameObjects; // Activate main level game object according to index
    public LevelData[] levelsData; // Main level data containing questions game object and their answers
    public Animator playerAnimator;

    public GameObject onAnswerParticle;

    public GameObject onBikeSitParticle;


    [HideInInspector] public List<Spawnables>
        questionsInLevel; // List for current level questions to perform individual operations and win loss logic

    private HUDController hudController;


    #region AudioSources

    public AudioSource winSound;
    public AudioSource loseSound;
    public AudioSource onAnswerCorrectSound;
    public AudioSource onAnswerWrongSound;
    public AudioSource runningSound;
    public AudioSource explosionSound;

    #endregion


    #region DataFlowControlVariables

    private int currentLevelHolderVar;
    private int correctAnswers;
    private int wrongAnswers;

    #endregion

    #region GameplayLogic

    private void Awake()
    {
        instance = this;
        hudController = FindObjectOfType<HUDController>();

        ResetValues(0); //Reset given correct answers and wrong answers to 0 with level restart/next level

        SetUpLevelQuestions();

        Debug.Log("QuestionsPref" + PlayerPrefs.GetFloat("LevelQuestions"));
        Time.timeScale = 1;
      
    }

    private void Start()
    {
        if (AdsController.instance!=null)
        {
            AdsController.instance.ShowSmartBanner();
        }
       
    }

    private void OnEnable()
    {
        CheckVolumeSettings();
    }

    void SetUpLevelQuestions()
    {
        int i;
        int levelGameObjectsCount = LevelGameObjects.Count;
        for (i = 0; i < levelGameObjectsCount; i++)
        {
            LevelGameObjects[i].SetActive(false);
        }

        LevelGameObjects[GetCurrentLevel()].SetActive(true);

        int currentLevelQuestions = levelsData[GetCurrentLevel()].questions.Length;

        for (i = 0; i < currentLevelQuestions; i++)
        {
            questionsInLevel.Add(levelsData[GetCurrentLevel()].questions[i]);
        }

        SetLevelQuestionsVal(currentLevelQuestions);
    }


    public virtual void
        OnAnswer(Collider other) 
        // Have more control Such as cosmetic effects and animations in other classes for every time answer is given
    {
        if (other.CompareTag("Correct"))
        {
            onAnswerCorrectSound.Play();
            Handheld.Vibrate();
            Debug.Log("CorrectAnswer");
            Instantiate(onAnswerParticle, other.transform.position,
                other.transform.rotation);
            SetCorrectAnswers(GetCorrectAnswers() + 1);
            hudController.correctAnswersValue.text = GetCorrectAnswers().ToString();
            questionsInLevel[0].question.gameObject.SetActive(false);
            CheckRemainingQuestions();
            Debug.LogError(levelsData[GetCurrentLevel()].questions.Length);
        }

        if (other.CompareTag("Wrong"))
        {
            onAnswerWrongSound.Play();
            Handheld.Vibrate();

            Debug.Log("WrongAnswer");
            Instantiate(onAnswerParticle, other.transform.position,
                other.transform.rotation);
            SetWrongAnswers(GetWrongAnswers() + 1);
            hudController.wrongAnswersValue.text = GetWrongAnswers().ToString();
            questionsInLevel[0].question.gameObject.SetActive(false);
            CheckRemainingQuestions();
            Debug.LogError(levelsData[GetCurrentLevel()].questions.Length);
        }

        if (other.CompareTag("Obstacle"))
        {
            LevelFailSequence();
        }
    }

    #endregion

    #region Win/Loss Logic

    private void CheckRemainingQuestions()
    {
        if (questionsInLevel.Count > 0)
        {
            questionsInLevel.RemoveAt(0);
        }

        if (questionsInLevel.Count == 0)
        {
            Debug.Log("LEVELCOMPLETE");
            CheckWinningCondition();
        }
    }

    private void CheckWinningCondition()
    {
        float correctPerc = GetCorrectAnswers() / GetLevelQuestions() * 100;


        switch (correctPerc)
        {
            case >= 50:
                LevelWinSequence();
                Debug.LogError(correctPerc);
                break;
            case < 50:
                LevelFailSequence();
                Debug.LogError(correctPerc);
                break;
        }
    }

    private void LevelWinSequence()
    {
        SetMaxLevelReached(); //Set only if level completed

        Vector3 offSet = new Vector3(0, 0, 3);
        levelsData[GetCurrentLevel()].levelPlayer.SetActive(false);
        levelsData[GetCurrentLevel()].levelPlayer.transform.gameObject.GetComponentInParent<PathFollower>().speed = 25f;
        levelsData[GetCurrentLevel()].levelSittingPlayer.SetActive(true);
        Instantiate(onBikeSitParticle, levelsData[GetCurrentLevel()].levelSittingPlayer.transform.position + offSet,
            levelsData[GetCurrentLevel()].levelSittingPlayer.transform.rotation);
        DisableQuestions();
        runningSound.Stop();
        hudController.pauseButton.SetActive(false);
        StartCoroutine(ActivatePanel(hudController.winPanel, winSound));
       
    }

    private void LevelFailSequence()
    {
        levelsData[GetCurrentLevel()].levelPlayer.GetComponent<Animator>().Play("Sad");
        FindObjectOfType<PlayerMovement>().enabled = false;
        FindObjectOfType<PathFollower>().enabled = false;
        DisableQuestions();
        runningSound.Stop();
        hudController.pauseButton.SetActive(false);
        StartCoroutine(ActivatePanel(hudController.losePanel, loseSound));
        
    }

    private IEnumerator ActivatePanel(GameObject panel, AudioSource audioType)
    {
        yield return new WaitForSeconds(4f);
        audioType.Play();
        panel.SetActive(true);
        if (AdsController.instance!=null)
        {
            AdsController.instance.HideSmartBanner();
            AdsController.instance.ShowInterStitialAdmob();
        }
      
    }

    #endregion

    #region Data Flow Control

   
    void SetCorrectAnswers(float correctAnswers)
    {
        PlayerPrefs.SetFloat("CorrectAnswers", correctAnswers);
    }

    float GetCorrectAnswers()
    {
        return PlayerPrefs.GetFloat("CorrectAnswers");
    }

    void SetWrongAnswers(float WrongAnswers)
    {
        PlayerPrefs.SetFloat("WrongAnswers", WrongAnswers);
    }

    float GetWrongAnswers()
    {
        return PlayerPrefs.GetFloat("WrongAnswers");
    }

    public void SetCurrentLevel(int currentLevel)
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
    }

    public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("CurrentLevel");
    }


    void ResetValues(float resetValue)
    {
        SetCorrectAnswers(resetValue);
        SetWrongAnswers(resetValue);
        SetLevelQuestionsVal(resetValue);
    }

    void SetLevelQuestionsVal(float value)
    {
        PlayerPrefs.SetFloat("LevelQuestions", value);
    }

    float GetLevelQuestions()
    {
        return PlayerPrefs.GetFloat("LevelQuestions");
    }

    void SetMaxLevelReached()
    {
        Debug.Log("Current level is" + GetCurrentLevel() + "MAX Level is" + GetMaxLevelReached());
        if (GetCurrentLevel() < 9)
        {
            PlayerPrefs.SetInt("MaxLevel", GetCurrentLevel() + 1);
        }
        else if (GetCurrentLevel() >= 9)
        {
            PlayerPrefs.SetInt("MaxLevel", 9);
        }
    }

    int GetMaxLevelReached()
    {
        return PlayerPrefs.GetInt("MaxLevel");
    }

    #endregion

    #region Cosmetic Effects

    private void DisableQuestions()
    {
        for (int i = 0; i < levelsData[GetCurrentLevel()].questions.Length; i++)
        {
            levelsData[GetCurrentLevel()].questions[i].question.SetActive(false);
        }
    }

    #endregion

    #region InGameUIMethods

    public void OnNextLevelCLick()
    {
        StartCoroutine(LoadingNextLevel(6f));
    }

    IEnumerator LoadingNextLevel(float loadingTime)
    {
        hudController.inGameloadingPanel.SetActive(true);
        yield return new WaitForSecondsRealtime(loadingTime);
        if (GetCurrentLevel() < 9)
        {
            SetCurrentLevel(GetCurrentLevel() + 1);
        }
        else
        {
            SetCurrentLevel(0);
        }

        SceneManager.LoadScene(2);
    }

    #endregion

    #region SoundManagement

    public void volumebuttonclick()
    {
        Debug.Log(PlayerPrefs.GetInt("Volume"));

        if (AudioListener.volume == 0)
        {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("Volume", 1);
            hudController.soundButton.GetComponent<UnityEngine.UI.Image>().sprite = hudController.Unmute;
        }
        else if (AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
            PlayerPrefs.SetInt("Volume", 0);
            hudController.soundButton.GetComponent<UnityEngine.UI.Image>().sprite = hudController.Mute;
        }
    }

    public void CheckVolumeSettings()
    {
        if (!PlayerPrefs.HasKey("Volume"))
        {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("Volume", 1);
            hudController.soundButton.GetComponent<UnityEngine.UI.Image>().sprite = hudController.Unmute;
        }
        else
        {
            if (PlayerPrefs.GetInt("Volume") == 1)
            {
                AudioListener.volume = 1;
                PlayerPrefs.SetInt("Volume", 1);
                hudController.soundButton.GetComponent<UnityEngine.UI.Image>().sprite = hudController.Unmute;
            }

            if (PlayerPrefs.GetInt("Volume") == 0)
            {
                AudioListener.volume = 0;
                PlayerPrefs.SetInt("Volume", 0);
                hudController.soundButton.GetComponent<UnityEngine.UI.Image>().sprite = hudController.Mute;
            }
        }
    }

    #endregion

    public void LoadLevel1()
    {
        SceneManager.LoadScene(0);
    } // DELETE THIS SHIT ONCE DONE TESTING
}

#region Class Holders and Sub Classes

[Serializable]
public class LevelData
{
    // public GameObject levelObject;
    public Spawnables[] questions;
    public GameObject levelPlayer;
    public GameObject levelSittingPlayer;
}

[Serializable]
public class Spawnables
{
    public GameObject question; // Main Object

    public GameObject
        correctAnswer; // Correct Answer for the Main Object for in game operations on individual correct answer

    public GameObject wrongAnswer; // Wrong Answer for the Main Object for in game operations on individual wrong answer
}

#endregion