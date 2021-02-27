using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInput : MonoBehaviour
{

    string[] directions = new string[4] {"w", "a", "s", "d"};

    string swipeDirection = "";

    float deadzone = 50;
    Vector2 firstPos;
    bool firstPosExists = false;
    bool doUpdate = false;
    public bool disabled = false;



    public bool getSwipe(string direction){
        if (disabled){
            return false;
        }
        else{
            return (direction == swipeDirection);
        }
    }


    float distanceOf(float x, float y){
        var dist = x-y;
        return dist;
    }

    float removeSignal(float num){
        if (num < 0){
            return num*-1;
        }

        return num;
    }


    void updateSwipe(){
        var pos = Input.mousePosition;

        if (firstPosExists){
            var x = distanceOf(pos.x, firstPos.x);
            var y = distanceOf(pos.y, firstPos.y);


            if (removeSignal(x) > deadzone || removeSignal(y) > deadzone){

                if (removeSignal(x) > removeSignal(y)){

                    if (x > 0){
                        swipeDirection = "d";
                    }
                    else{
                        swipeDirection = "a";
                    }
                }

                else{
                    if (y > 0){
                        swipeDirection = "w";
                    }
                    else{
                        swipeDirection = "s";
                    }
                }
            }

            if (swipeDirection != ""){
                doUpdate = false;
            }
        }

        else{
            firstPosExists = true;
            firstPos = pos;
        }

    }


    void Update(){
        if (doUpdate){
            updateSwipe();
        }
        else{
            swipeDirection = "";
            firstPosExists = false;
        }
        if (Input.GetMouseButtonDown(0)){
            doUpdate = true;
        }
        if (Input.GetMouseButtonUp(0)){
            doUpdate = false;
        }




    }
}
