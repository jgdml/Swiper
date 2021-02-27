using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HudManager : MonoBehaviour{


    public GameObject deaths;

    GameObject arrowImage;

    Text comboTxt;
    Animator comboAnim;
    LevelManager levelManager;

    Text levelTxt;

    Animator resAnim;
    Text[] resTxts;

    Text scoreTxt;

    Animator overlayAnim;

    
    int maxCombo;
    int combo;
    int score;
    int misses;

    

    void Start(){

        int lvl = PlayerPrefs.GetInt("Diff")+1;

        arrowImage = GameObject.Find("Arrow");

        comboTxt = GameObject.Find("Combo").GetComponent<Text>();
        comboAnim = GameObject.Find("Combo").GetComponent<Animator>();
        levelManager = GameObject.Find("Scripts").GetComponent<LevelManager>();
        levelTxt = GameObject.Find("LevelName").GetComponent<Text>();

        resAnim = GameObject.Find("Results").GetComponent<Animator>();
        resTxts = GameObject.Find("Results").GetComponentsInChildren<Text>();

        scoreTxt = GameObject.Find("Score").GetComponent<Text>();

        overlayAnim = GameObject.Find("Overlay").GetComponent<Animator>();

        
        levelTxt.text = "Nivel "+lvl;

        if (lvl % 5 == 0){
            levelTxt.color = Color.red;
        }

    }


    /////////////////


    public void addCombo(){

        combo++;
        if (combo > maxCombo){
            maxCombo = combo;
        }
        score+=250*(combo+1);

        updateScore();

        comboTxt.text = combo+"x";
        comboAnim.SetTrigger("AddCombo");
    }

    public void resetCombo(){
        if (combo > 0){
            comboAnim.SetTrigger("ResetCombo");
        }
        
        misses++;
        combo = 0;
        comboTxt.text = "0x";
    }

    public string getResults(){
        return maxCombo+"/"+levelManager.getMaxCombo();
    }


    //////////////////


    void updateScore(){
        scoreTxt.text = ""+score;
    }


    //////////////////


    public void showDeaths(){
        PlayerPrefs.SetInt("Deaths", PlayerPrefs.GetInt("Deaths")+1);
        PlayerPrefs.Save();
        Instantiate(deaths, transform);
    }


    //////////////////


    IEnumerator destroyArrow(GameObject arrow){
        yield return new WaitForSeconds(1.5f);

        Destroy(arrow);
    }

    public void pointArrowAt(string dir){

        var thisArrow = Instantiate(arrowImage);

        thisArrow.transform.SetParent(transform);

        thisArrow.transform.localScale = new Vector2(2, 2);

        var thisAnim = thisArrow.GetComponent<Animator>();


        float rotation = 0f;

        if (dir == "w"){
            rotation = 90f;
            thisAnim.SetTrigger("arrowUp");
        }
        if (dir == "a"){
            rotation = 180f;
            thisAnim.SetTrigger("arrowLeft");
        }
        if (dir == "s"){
            thisAnim.SetTrigger("arrowDown");
            rotation = 270f;
        }
        if (dir == "d"){
            thisAnim.SetTrigger("arrowRight");
        }

        thisArrow.transform.rotation = Quaternion.Euler(0f, 0f, rotation);

        StartCoroutine(destroyArrow(thisArrow));   
    }


    //////////////////


    IEnumerator resEnter(){
        yield return new WaitForSecondsRealtime(1f);
        resAnim.SetTrigger("resultsEnter");
    }


    public void showResults(){
        resTxts[0].text += score;
        resTxts[1].text += getResults();
        resTxts[2].text += misses;

        StartCoroutine("resEnter");
    }


    IEnumerator buttonDelay(string scene){
        resAnim.SetTrigger("resultsExit");
        yield return new WaitForSecondsRealtime(1f);

        overlayAnim.SetTrigger("doSlide");
        yield return new WaitForSecondsRealtime(1f);
    
        SceneManager.LoadScene(scene);
    }


    public void nextLevel(){
        PlayerPrefs.SetInt("Diff", PlayerPrefs.GetInt("Diff")+1);
        PlayerPrefs.Save();

        StartCoroutine(buttonDelay("Gameplay"));    
    }

    public void restartLevel(){
        StartCoroutine(buttonDelay("Gameplay"));
    }

    public void menu(){
        StartCoroutine(buttonDelay("Menu"));
    }
}
