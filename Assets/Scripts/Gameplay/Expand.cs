using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expand : MonoBehaviour
{
    Animator anim;
    Renderer renderer;
    ColorGenerator colorGenerator;


    void Start(){
        anim = GetComponent<Animator>();
        renderer = GetComponent<Renderer>();
        colorGenerator = GameObject.Find("Scripts").GetComponent<ColorGenerator>();
        
        renderer.material.color = colorGenerator.colors[0];
    }

    public void ExpandBox(){
        anim.SetTrigger("Expand");
    }
}
