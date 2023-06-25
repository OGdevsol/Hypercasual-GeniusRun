using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIController : MonoBehaviour
{
 [SerializeField]  private List<GameObject> references;
 [SerializeField] private GameObject loadingScreen;
 [SerializeField] private Button[] levelButtons;

 private void Awake()
 {
     UnlockLevels();
     Debug.LogError(GetMaxLevelReached());
 }

 public void ActivateUIReference(int index)
   {
       int i ;
       int referencesLength = references.Count;
       
       for (i =0; i < referencesLength; i++)
       {
           references[i].SetActive(false);
       }
       references[index].SetActive(true);
   }

   void UnlockLevels()
   {
       for (int i = 0; i <= GetMaxLevelReached(); i++)
       {
           levelButtons[i].interactable = true;
           levelButtons[i].transform.GetChild(1).transform.gameObject.SetActive(false);
       }
   }

   IEnumerator Loading(float loadingTime)
   {
       loadingScreen.SetActive(true);
       yield return new WaitForSecondsRealtime(loadingTime);
       SceneManager.LoadScene(1);

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

   public int GetCurrentLevel(int currentLevel)
   {
       currentLevel = PlayerPrefs.GetInt("CurrentLevel");
       return currentLevel;
   }
   int GetMaxLevelReached()
   {
       return PlayerPrefs.GetInt("MaxLevel");
   }
   
   
}
