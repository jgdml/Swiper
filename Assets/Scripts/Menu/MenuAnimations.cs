using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuAnimations : MonoBehaviour
{
    
    Animator textAnim;
    Animator canvasAnim;
    Animator titleAnim;


    float startTime = 0f;


    void Start(){
        canvasAnim = GameObject.Find("CanvasParent").GetComponent<Animator>();
        titleAnim = GameObject.Find("Title").GetComponent<Animator>();
        textAnim = GameObject.Find("Start").GetComponent<Animator>();

        StartCoroutine("blinkStart");

    }

    void showMenu(){
        textAnim.SetTrigger("endBlink");
        titleAnim.SetTrigger("titleShrink");
        canvasAnim.SetTrigger("buttonEnter");
    }


    public void continueGame(){
        StartCoroutine("startGameDelay");
    }


    public void newGame(){
        PlayerPrefs.SetInt("Diff", 0);
        PlayerPrefs.SetInt("Unlocked", 0);
        
        PlayerPrefs.Save();

        StartCoroutine("startGameDelay");
    }


    public void quitGame(){
        Application.Quit();
    }


    IEnumerator blinkStart(){
        yield return new WaitForSeconds(2);
        textAnim.SetTrigger("startBlink");
    }

    IEnumerator startGameDelay(){
        canvasAnim.SetTrigger("zoomIn");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("Gameplay");
    }

    void Update(){
        if (Time.timeSinceLevelLoad > 2 && Input.GetMouseButtonUp(0)){
            showMenu();
        }
    }
}

