using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class LevelControl : MonoBehaviour
{


    EventSystem eventSystem;
    Animator buttonAnim;
    PlaySound playSound;
    SwipeInput swipe;

    bool isPaused = false;


    void Start(){
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        buttonAnim = GameObject.Find("OffScreenButtons").GetComponent<Animator>();
        playSound = GameObject.Find("Audio").GetComponent<PlaySound>();

        swipe = GameObject.Find("Box").GetComponent<SwipeInput>();
    }


    public void showMenu(){
        buttonAnim.SetTrigger("slideIn");
    }
    public void hideMenu(){
        buttonAnim.SetTrigger("slideOut");
    }


    public void togglePause(){
        if (isPaused == false){
            showMenu();
            playSound.changeVolume(-0.3f);

            Time.timeScale = 0f;
            isPaused = true;

            swipe.disabled = true;
        }
        else{
            hideMenu();
            playSound.changeVolume(0.3f);

            eventSystem.SetSelectedGameObject(null);
            Time.timeScale = 1f;
            isPaused = false;

            swipe.disabled = false;
        }
    }

    public void restartGame(){
        SceneManager.LoadScene("Gameplay");
    }

    public void goToMenu(){
        SceneManager.LoadScene("Menu");
    }
}
