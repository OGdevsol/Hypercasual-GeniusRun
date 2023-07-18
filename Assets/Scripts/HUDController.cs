using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;

public class HUDController : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject pausePanel;
    public GameObject pauseButton;
    public GameObject inGameloadingPanel;

    public TMP_Text correctAnswersValue;
    public TMP_Text wrongAnswersValue;

    public Button soundButton;
    [SerializeField] public Sprite Mute;
    [SerializeField] public Sprite Unmute;


    public virtual void Pause(GameObject pausePanelParam)
    {
        Time.timeScale = 0;
        GameplayController.instance.runningSound.Stop();
        pausePanelParam.SetActive(true);
        if (AdsController.instance)
        {
            AdsController.instance.HideSmartBanner();
            AdsController.instance.ShowInterStitialAdmob();
        }
       

       
    }

    public virtual void Resume(GameObject pausePanelParam)
    {
        GameplayController.instance.runningSound.Play();
        GameplayController.instance.CheckVolumeSettings();
        pausePanelParam.SetActive(false);
        if (AdsController.instance)
        {
            AdsController.instance.ShowSmartBanner();
        }
      
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(2);
    }
}