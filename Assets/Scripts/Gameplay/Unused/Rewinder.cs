using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rewinder : MonoBehaviour{

    GameObject box;
    List<Vector3> positions = new List<Vector3>{};
    List<Quaternion> rotations = new List<Quaternion>{};
    bool record = true;
    bool rewind = false;
    int frame = -1;


    void Start(){
        box = GameObject.Find("Box");
    }



    void Record(){
        positions.Add(box.transform.position);
        rotations.Add(box.transform.rotation);
    }

    public void Rewind(){
        if (frame == -1){
            frame = positions.Count-1;
            record = false;
            rewind = true;
            Time.timeScale = 2f;
        }


        box.transform.position = positions[frame];
        box.transform.rotation = rotations[frame];
        frame--;

        if (frame == 0){
            frame = -1;
            rewind = false;
            record = true;
            Time.timeScale = 1f;
        }

    }

    void Update(){
        if (record){
            Record();
        }


        else if (rewind){
            Rewind();
        }

    }
}
