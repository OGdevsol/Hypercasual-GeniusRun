using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Transform playerTransform;

    [SerializeField] private float limitValue;
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            MovePlayer();
        }
    }

    private void MovePlayer()
    {
        float halfScreen = Screen.width / 2;
        float xPos = (Input.mousePosition.x-halfScreen)/halfScreen;
        float finalXPos = Mathf.Clamp(xPos * limitValue,-limitValue,limitValue) ;
        playerTransform.localPosition = Vector3.Lerp(playerTransform.localPosition,new Vector3(finalXPos, 0, 0),0.2f); ;
    }
}
