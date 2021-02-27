using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    GameObject burstObj;
    ColorGenerator colorGenerator;
    ParticleSystem.MainModule main;
    

    void Start(){
        burstObj = GameObject.Find("Burst");
        main = burstObj.GetComponent<ParticleSystem>().main;
        colorGenerator = GameObject.Find("Scripts").GetComponent<ColorGenerator>();

        main.startColor = colorGenerator.colors[0];
    }
    

    ParticleSystem getNew(){
        return Instantiate(burstObj).GetComponent<ParticleSystem>();
    }

    public void sideBurst(string side){
        int degree = 0;

        if (side == "w"){
            degree = 45;
        }
        if (side == "a"){
            degree = 130;
        }
        if (side == "s"){
            degree = 225;
        }
        if (side == "d"){
            degree = 320;
        }


        Vector2 pos = transform.position;
        ParticleSystem current = getNew();

        current.transform.rotation = Quaternion.Euler(Vector3.forward * degree);
        current.transform.position = new Vector3(pos.x, pos.y, -10);
        current.Play();
    }


    public void endBurst(){
        Vector2 pos = transform.position;
        ParticleSystem b1 = getNew();
        ParticleSystem b2 = getNew();

        b1.transform.rotation = Quaternion.Euler(Vector3.forward * 45);
        b2.transform.rotation = Quaternion.Euler(Vector3.forward * 45);

        b1.transform.position = new Vector3(pos.x-3, pos.y, -10);
        b2.transform.position = new Vector3(pos.x+3, pos.y, -10);

        b1.Play();
        b2.Play();


    }

}
