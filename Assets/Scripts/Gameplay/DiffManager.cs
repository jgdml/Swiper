using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiffManager : MonoBehaviour{

    public struct Diff{
        public float speed;
        public int lines;
        public int speedUps;
    }

    int diffNum;

    Diff diff;

    void Start(){

        diffNum = PlayerPrefs.GetInt("Diff");        
        
        diff.speed = 6 * (1+diffNum*0.1f);  
        diff.lines = Mathf.Clamp( (int)(16 - diffNum*0.2), 6, 16);
        diff.speedUps = 2;

        if ((diffNum+1) % 5 == 0){
            diff.speed += 1.2f;
        }
        print("speed: "+diff.speed);
    }

    public Diff getDiff(){

        return diff;
    }

}
