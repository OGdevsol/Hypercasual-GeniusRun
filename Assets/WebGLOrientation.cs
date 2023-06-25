using System.Collections;
using System.Collections.Generic;
using MarksAssets.ScreenOrientationWebGL;
using UnityEngine;

public class WebGLOrientation : MonoBehaviour
{
    
    void Start()
    {
        ScreenOrientationWebGL.orientation = ScreenOrientationWebGL.ScreenOrientation.LandscapeLeft;
    }

   
}
