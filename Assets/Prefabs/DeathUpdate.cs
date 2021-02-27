using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathUpdate : MonoBehaviour
{

    Text deaths;

    int num;

    void Start()
    {
        deaths = GetComponent<Text>();

        num = PlayerPrefs.GetInt("Deaths")-1;
        deaths.text = deaths.text+num;

        StartCoroutine("changeTxt");
    }

    IEnumerator changeTxt(){
        yield return new WaitForSecondsRealtime(0.7f);
        deaths.text = "Mortes: "+(num+1);
    }
}
