using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerabound : MonoBehaviour
{
    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;
    private Vector3 targetposition;

    // Use this for initialization
    void Start () {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    // Update is called once per frame
    void Update(){
       // Debug.Log(MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z)));
        Vector3 viewPos = transform.position;
       
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);

        transform.position = viewPos;
        /*viewPos.y = Mathf.Lerp(screenBounds.y - 1, screenBounds.y, 3); 
        targetposition = viewPos;
        */

        //Debug.Log(viewPos);
        /*transform.position = Vector3.Lerp(new Vector3(screenBounds.x - 1, screenBounds.y - 1, 0),
            new Vector3(screenBounds.x, screenBounds.y, 0), 3 * Time.deltaTime);
        transform.position = viewPos;*/
    }
}
