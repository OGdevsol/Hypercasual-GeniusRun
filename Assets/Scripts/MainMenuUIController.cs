using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
    [SerializeField] private List<GameObject> references;

    [SerializeField] private Button[] levelButtons;

    [SerializeField] private GameObject mainMenuVolButton;
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private Sprite unLockLevelBackground;
    [SerializeField] private Sprite Mute;
    [SerializeField] private Sprite Unmute;
    [SerializeField] private AudioSource music;
    [SerializeField] private AudioSource clickSound;


    private void Awake()
    {
        CheckVolumeSettings();
       
       
        Debug.LogError(GetMaxLevelReached());
    }

    private void OnEnable()
    {
        CheckVolumeSettings();
        UnlockLevels();
        AdsController.instance.ShowInterStitialAdmob();
    }

    public void PrivacyPolicy()
    {
        clickSound.Play();
        Application.OpenURL("https://lucidtecstudio.blogspot.com/2023/05/privacy-policy.html");
    }

    public void MoreGames()
    {
        clickSound.Play();
        Application.OpenURL("https://play.google.com/store/apps/developer?id=Lucidtec+Studio");
    }

    public void RateUs()
    {
        clickSound.Play();
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.ls.Scattergories.runnergames.quizgames");
    }


    public void ActivateUIReference(int index)
    {
       
        int i;
        int referencesLength = references.Count;

        for (i = 0; i < referencesLength; i++)
        {
            references[i].SetActive(false);
        }

        references[index].SetActive(true);
        clickSound.Play();
    }

    void UnlockLevels()
    {
        for (int i = 0; i <= GetMaxLevelReached(); i++)
        {
            levelButtons[i].interactable = true;
            levelButtons[i].transform.GetChild(1).transform.gameObject.SetActive(false);
            levelButtons[i].GetComponent<Image>().sprite = unLockLevelBackground;
        }
    }

    IEnumerator Loading(float loadingTime)
    {
        loadingScreen.SetActive(true);
        yield return new WaitForSecondsRealtime(loadingTime);
        SceneManager.LoadScene(2);
    }

    public void LoadLevel(int currentLevel)
    {
        SetCurrentLevel(currentLevel);
        StartCoroutine(Loading(6f));
    }

    public void SetCurrentLevel(int currentLevel)
    {
        PlayerPrefs.SetInt("CurrentLevel", currentLevel);
    }

    public int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt("CurrentLevel");
    }

    int GetMaxLevelReached()
    {
        return PlayerPrefs.GetInt("MaxLevel");
    }

    public void OnCLickPlay()
    {
        clickSound.Play();
        LoadLevel(GetMaxLevelReached());
    }

    public void volumebuttonclick()
    {
        
        Debug.Log(PlayerPrefs.GetInt("Volume"));
        
       
        
            if (AudioListener.volume==0)
            {
                AudioListener.volume = 1;
                PlayerPrefs.SetInt("Volume",1);
                mainMenuVolButton.GetComponent<Image>().sprite = Unmute;


            }
            else if (AudioListener.volume == 1)
            {
                AudioListener.volume = 0;
                PlayerPrefs.SetInt("Volume",0);
                mainMenuVolButton.GetComponent<Image>().sprite = Mute;
               
            }
        
        

    }

    public void CheckVolumeSettings()
    {
       
        
        if (!PlayerPrefs.HasKey("Volume"))
        {
            AudioListener.volume = 1;
            PlayerPrefs.SetInt("Volume",1);
            mainMenuVolButton.GetComponent<Image>().sprite = Unmute;
        }
        else
        {
            if (PlayerPrefs.GetInt("Volume")==1)
            {
                AudioListener.volume = 1;
                PlayerPrefs.SetInt("Volume",1);
                mainMenuVolButton.GetComponent<Image>().sprite = Unmute;
            }
            if (PlayerPrefs.GetInt("Volume")==0)
            {
                AudioListener.volume = 0;
                PlayerPrefs.SetInt("Volume",0);
                mainMenuVolButton.GetComponent<Image>().sprite = Mute;
            }
        }


    }

    public void QuitGame()
    {
        Application.Quit();
    }
}