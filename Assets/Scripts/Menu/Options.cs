using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class Options : MonoBehaviour{
    
    void Awake(){
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        if (!PlayerPrefs.HasKey("Unlocked")){
            PlayerPrefs.SetInt("Unlocked", 0);
        }
        if (!PlayerPrefs.HasKey("Deaths")){
            PlayerPrefs.SetInt("Deaths", 0);
        }
    }
}
