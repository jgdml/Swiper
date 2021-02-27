using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUp : MonoBehaviour
{
    
    Box box;
    PlaySound playSound;

    float baseSpeed;

    void Start(){
        box = GameObject.Find("Box").GetComponent<Box>();
        playSound = GameObject.Find("Audio").GetComponent<PlaySound>();

        float baseSpeed = box.speed;
    }

    void speedUp(){
        box.speed += 1.3f;

        // playSound.speedUpMusic(0.1f);

    }

    void OnTriggerEnter2D(Collider2D c){
        speedUp();
    }
}
