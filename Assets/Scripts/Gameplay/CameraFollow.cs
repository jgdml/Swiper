using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour{
    
    GameObject box;
    GameObject camObj;
    Camera cam;
    Vector3 pos;
    float height;
    float width;


    void Start(){
        box = GameObject.Find("Box");
        camObj = GameObject.Find("Camera");
        cam = Camera.main;

        height = cam.orthographicSize *2f;
        width = cam.aspect * height;
    }



    void staticFollow(){
        pos = box.transform.position;


        if (pos.x > camObj.transform.position.x+(width/5)){
            transform.position = new Vector3(box.transform.position.x, box.transform.position.y, -10);
        }

        else if (pos.x < camObj.transform.position.x-(width/5)){
            transform.position = new Vector3(box.transform.position.x, box.transform.position.y, -10);
        }

        else if (pos.y > camObj.transform.position.y+(height/5)){
            transform.position = new Vector3(box.transform.position.x, box.transform.position.y, -10);
        }

        else if (pos.y < camObj.transform.position.y-(height/5)){
            transform.position = new Vector3(box.transform.position.x, box.transform.position.y, -10);
        }

    }




    void fixedFollow(){
        transform.position = new Vector3(box.transform.position.x, box.transform.position.y+1.5f, -10);

    }


    void followCursor(){
        transform.position += Input.mousePosition/100;
    }

    
    void Update(){
        // staticFollow();
        fixedFollow();

        if (Input.GetMouseButton(1)){
            followCursor();
        }
    }
}
