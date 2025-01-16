using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Box : MonoBehaviour{

    Rigidbody2D body;
    Renderer renderer;

    Expand expandBox;
    Rewinder rewinder;
    PlaySound playSound;
    SwipeInput swipeInput;
    HudManager hudManager;
    LevelManager levelManager;  
    ParticleManager particleManager;
    ColorGenerator colorGenerator;
    CameraAnimation cameraAnimation;
    LevelControl levelControl;

    DiffManager dm;

    List<string> inputList = new List<string>();
    string[] directions = new string[4] {"w", "a", "s", "d"};
    float[] torques = new float[2] {700f, -700f};

    float gameOverTime = 0;
    bool gameOver = false;

    public float speed;
    int count = -1;




    void Start(){
        Time.timeScale = 1f;

        body = GetComponent<Rigidbody2D>();
        renderer = GetComponent<Renderer>();
        swipeInput = GetComponent<SwipeInput>();

        playSound = GameObject.Find("Audio").GetComponent<PlaySound>();
        expandBox = GameObject.Find("EffectBox").GetComponent<Expand>();
        hudManager = GameObject.Find("Hud").GetComponent<HudManager>();
        particleManager = GameObject.Find("Burst").GetComponent<ParticleManager>();
        levelControl = GameObject.Find("PauseButton").GetComponent<LevelControl>();

        cameraAnimation = GameObject.Find("Scripts").GetComponent<CameraAnimation>();
        colorGenerator = GameObject.Find("Scripts").GetComponent<ColorGenerator>(); 
        levelManager = GameObject.Find("Scripts").GetComponent<LevelManager>();
        rewinder = GameObject.Find("Scripts").GetComponent<Rewinder>();

        dm = GameObject.Find("Scripts").GetComponent<DiffManager>();
        var diff = dm.getDiff();

        renderer.material.color = colorGenerator.colors[0];
        speed = diff.speed;
    }

    
    public void gameEnd(string end){
        body.constraints = RigidbodyConstraints2D.None;
        body.constraints = RigidbodyConstraints2D.FreezePositionX;
        
        body.AddTorque(torques[Random.Range(0, 2)], ForceMode2D.Force);
        gameOver = true;
        gameOverTime = Time.unscaledTime;

        if (end == "bad"){
            body.gravityScale = 1f;
            hudManager.showDeaths();
            levelControl.showMenu();

            playSound.stopAudio();
        }

        else if (end == "good"){
            playSound.playClip("win");
            particleManager.endBurst();
            hudManager.getResults();
        }        
    }
    

    public void addSpeed(float x, float y){
        body.velocity = new Vector2(x, y);
    }



    void writeDirections(){
        for (int i = 0; i< 4; i++){
            if (Input.GetKeyDown(directions[i])){

                inputList.Add(directions[i]);
                hudManager.pointArrowAt(directions[i]);
                
                expandBox.ExpandBox();
                playSound.playClip("press");
                break;
            }
        }
    }


    void doCurrentMove(string crashSide){

        string currentMove = inputList[count];

        if (currentMove == "w"){
            addSpeed(0, speed);
        }
        if (currentMove == "a"){
            addSpeed(-speed, 0);
        }
        if (currentMove == "s"){
            addSpeed(0, -speed);
        }
        if (currentMove == "d"){
            addSpeed(speed, 0);
        }

        
        if (levelManager.isTurnOk(currentMove)){
            particleManager.sideBurst(crashSide);
            cameraAnimation.doShake(crashSide);
            playSound.playClip("hit");
            hudManager.addCombo();
        }

        else{
            hudManager.resetCombo();
            playSound.playClip("break");
        }
        count++;
    
    }


    string doRecoil(Vector3 point){
        string crashSide = "";
        string dir = "";
        var p = levelManager.getTilePos(transform.position);

        if (inputList.Count > count){
            dir = inputList[count];
        }


        if (dir == "w" || dir == "s"){

            if (point.x > transform.position.x){
                crashSide = "d";
            }
            else if (point.x < transform.position.x){
                crashSide = "a";
            }
        }
        else{
            if(point.y < transform.position.y){
                crashSide = "s";
            }
            else{ 
                crashSide = "w";
            }
        }

        p.x += 0.5f;
        p.y += 0.5f;
        p.z = transform.position.z;
        transform.position = p;

        return crashSide;

    }


    void autoTurn(){
        inputList.Add(levelManager.getNextTurn());
    }


    void crashRoutine(Collision2D box){
        if (!gameOver){
            addSpeed(0, 0);
            
            Vector3 point = box.contacts[0].point;

            string crashSide = doRecoil(point);
            
            if (inputList.Count > count){
                doCurrentMove(crashSide);
            }
            else{
                gameEnd("bad");
            }
        }
    }






    void OnCollisionStay2D(Collision2D box){
        autoTurn();
        crashRoutine(box);
    }


    void Update(){

        if (gameOver){
            // if (gameOverTime+2 < Time.unscaledTime){
            //     if (Input.GetMouseButton(0)){
            //         SceneManager.LoadScene("Gameplay");
            //     }
            // }

            float rate = Time.timeScale - (0.6f/0.99f)*Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(rate, 0.02f, 1f);
        }
        else{
            writeDirections();

            if (count == -1 && Time.timeSinceLevelLoad > 0.4f){
                count++;
                addSpeed(0, speed);
            }
        }
    }
}
