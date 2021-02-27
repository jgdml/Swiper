using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAnimation : MonoBehaviour
{
    
    Animator cameraAnim;
    Camera cam;

    void Start(){
        cam = GameObject.Find("Camera").GetComponent<Camera>();
        cameraAnim = GameObject.Find("Camera").GetComponent<Animator>();
    }

    public void doShake(string direction){
        string shakeSelect = "";

        if (direction == "w"){
            shakeSelect = "shakeUp";
        }
        if (direction == "a"){
            shakeSelect = "shakeLeft";
        }
        if (direction == "s"){
            shakeSelect = "shakeDown";
        }
        if (direction == "d"){
            shakeSelect = "shakeRight";
        }

        cameraAnim.SetTrigger(shakeSelect);
    }

    public void doZoomOut(){
        cameraAnim.SetTrigger("zoomOut");
    }
    
}