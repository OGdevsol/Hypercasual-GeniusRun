using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InGameUIController : MonoBehaviour
{
    public GameObject levelCompletePanel;
    public GameObject levelFailPanel;

    public Image[] fillStar;
    public GameObject Mute;
    public GameObject UnMute;
    

    public float leveltimer;
    public float currenttime;
    private bool condition = false;
    private int timecontroller;
    public GameObject levelData;
    
    public TMP_Text assetsTextInGame;
    public TMP_Text liabilitiesTextInGame;
    public TMP_Text netWorthTextInGame;
    public TMP_Text timertext;
    public TMP_Text assetsTextInLevelComplete;
    public TMP_Text assetsTextInLevelFail;
    public TMP_Text liabilitiesTextInLevelComplete;
    public TMP_Text liabilitiesTextInLevelFail;
    public TMP_Text netWorthTextInLevelComplete;
    public TMP_Text netWorthTextInLevelFail;
    public Scene[] levels;
    public GameObject PausePanel;
    

    public void volumebuttonclick()
    {
        if (AudioListener.volume==0)
        {
            AudioListener.volume = 1;
          //  UnMute.SetActive(true);
          //  Mute.SetActive(false);
            
        }
        else if (AudioListener.volume == 1)
        {
            AudioListener.volume = 0;
          //  Mute.SetActive(true);
           // UnMute.SetActive(false);
        }
        {
            
        }
    }

    private void Start()
    {
        currenttime = leveltimer;
    }

    public void EnableStar()
    {
        // if level 1 complete, 1 star, if level 2, 2 stars, if 3, 3 stars.
    }

    public void Quit()
    {
    
        SceneManager.LoadScene("MainMenu");
    }

    public void finish()
    {
        Debug.Log("Condition Working");
        assetsTextInLevelComplete.text = playercontroller.instance.Assets.ToString();
        liabilitiesTextInLevelComplete.text = playercontroller.instance.Liabilities.ToString();
        netWorthTextInLevelComplete.text = playercontroller.instance.netWorth.ToString();
        assetsTextInLevelFail.text = playercontroller.instance.Assets.ToString();
        liabilitiesTextInLevelFail.text = playercontroller.instance.Liabilities.ToString();
        netWorthTextInLevelFail.text = playercontroller.instance.netWorth.ToString();
        if (playercontroller.instance.netWorth>0)
        {
            levelCompletePanel.SetActive(true);
            Time.timeScale = 0;
            levelData.SetActive(false);
            /*FindObjectOfType<EnemyScript>().enabled = false;
        FindObjectOfType<EnemyProjectile>().enabled = false;
            FindObjectOfType<OtherProjectileScript>().enabled = false;
            FindObjectOfType<playercontroller>().enabled = false;*/
           // levelData.SetActive(false);
        }
        else
        {
            levelFailPanel.SetActive(true);
            Time.timeScale = 0;
            playercontroller.instance.lose.Play();
            levelData.SetActive(false);
            /*FindObjectOfType<EnemyScript>().enabled = false;
            FindObjectOfType<EnemyProjectile>().enabled = false;
            FindObjectOfType<OtherProjectileScript>().enabled = false;
            FindObjectOfType<playercontroller>().enabled = false;*/
          //  levelData.SetActive(false);
        }
       
     //   levelData.SetActive(false);

    }

    public void NextButtonClick()
    {
        Debug.Log(PlayerPrefs.GetInt("Level"));
        if (PlayerPrefs.GetInt("Level")==1)
        {
            SceneManager.LoadScene(2);
        }
        if (PlayerPrefs.GetInt("Level")==2)
        {
            SceneManager.LoadScene(3);
        }
        if (PlayerPrefs.GetInt("Level")==3)
        {
            SceneManager.LoadScene(1);
        }
       
      
        
    }

    public GameObject HelpPanel;
    public void OnCLickHelp()
    {
        Time.timeScale = 0;
        HelpPanel.SetActive(true);
    }
    
    

    public void RETRY()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Level"));
    }



    private void Update()
    {
        
        
       currenttime -= Time.deltaTime;

       
            
        if (!condition)
        {
            timertext.text = Convert.ToInt32(currenttime).ToString();
            if (currenttime <= 0)
            {
                condition = true;
                finish();
            }    
        }
        
    }

    public void PauseButtonClick()
    {
        Time.timeScale = 0;
        PausePanel.SetActive(true);
    }
    public void ResumeButtonClick(GameObject panel)
    {
        
        Time.timeScale = 1;
        panel.SetActive(false);
    }
}
