using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Splash : MonoBehaviour
{ [SerializeField] private GameObject loadingScreen;
    private void Awake()
    {
        StartCoroutine(Loading(5f));
    }
    IEnumerator Loading(float loadingTime)
    {
      
        yield return new WaitForSecondsRealtime(loadingTime);
        SceneManager.LoadScene(1);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
