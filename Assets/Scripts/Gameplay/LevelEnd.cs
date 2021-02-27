using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEnd : MonoBehaviour
{

    Box box;

    HudManager hm;


    void Start(){
        box = GameObject.Find("Box").GetComponent<Box>();
        hm = GameObject.Find("Hud").GetComponent<HudManager>();
    }

    void OnTriggerExit2D(Collider2D c){
        hm.showResults();
        box.gameEnd("good");

        int unlocked = PlayerPrefs.GetInt("Unlocked");
        int current = PlayerPrefs.GetInt("Diff");
        int high = PlayerPrefs.GetInt("Highscore");

        if (current+1 > unlocked){
            PlayerPrefs.SetInt("Unlocked", current+1);
        }

        if (current > high){
            PlayerPrefs.SetInt("Highscore", current);
        }
        
        PlayerPrefs.Save();
    }
}
