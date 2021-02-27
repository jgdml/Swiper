using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{

    Camera cam;

    void Start(){
        cam = GetComponent<Camera>();
    }


    void Update(){
        float scroll = Input.mouseScrollDelta.y;
        float zoom = cam.orthographicSize;

        if (scroll != 0.0f){
            zoom -= scroll;

            if (zoom > 0){
                if (zoom < 40){

                    cam.orthographicSize = zoom;
                }
            }
        }
    }
}
