using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionTest : MonoBehaviour

{
   public GameplayController gameController;
   private void Awake()
   {
     
      Application.targetFrameRate = 60;
      gameController = FindObjectOfType<GameplayController>();
   }

   [SerializeField]private TMP_Text answerText;
   
   private void OnTriggerEnter(Collider other)

   {
       gameController.OnAnswer(other);
   }
   
   

  
}
