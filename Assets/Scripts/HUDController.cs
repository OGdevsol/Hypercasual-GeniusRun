using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUDController : MonoBehaviour
{
    public GameObject winPanel;
    public GameObject losePanel;
    public GameObject pausePanel;
    public GameObject pauseButton;
    public GameObject inGameloadingPanel;
   
    public TMP_Text correctAnswersValue;
    public TMP_Text wrongAnswersValue;

    public virtual void Pause(GameObject pausePanelParam)
    {
        Time.timeScale = 0;
        pausePanelParam.SetActive(true);
        
    }
    
    public virtual void Resume(GameObject pausePanelParam)
    {
        Time.timeScale = 1;
        pausePanelParam.SetActive(false);
        
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    
    public void LoadCurrentScene()
    {
        SceneManager.LoadScene(1);
    }





}
