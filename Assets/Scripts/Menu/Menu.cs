using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{

    Text deaths;

    Text lastLevel;

    void Start(){
        Time.timeScale = 1f;
        
        deaths = GameObject.Find("Deaths").GetComponent<Text>();
        lastLevel = GameObject.Find("LastLevel").GetComponent<Text>();

        deaths.text = deaths.text+PlayerPrefs.GetInt("Deaths");
        lastLevel.text = lastLevel.text+(PlayerPrefs.GetInt("Highscore"));
    
    }
}

