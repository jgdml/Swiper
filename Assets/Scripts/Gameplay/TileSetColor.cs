using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


public class TileSetColor : MonoBehaviour
{
    
    Tilemap tileFinish;
    Tilemap tileLevel;
    Tilemap tilePath;

    ColorGenerator colorGenerator;

    void Start(){
        tileLevel = GameObject.Find("TileLevel").GetComponent<Tilemap>();
        tileFinish = GameObject.Find("FinishTile").GetComponent<Tilemap>();
        tilePath = GameObject.Find("TilePath").GetComponent<Tilemap>();
        colorGenerator = GetComponent<ColorGenerator>();

        tileLevel.color = colorGenerator.colors[1];
        tileFinish.color = colorGenerator.colors[2];

    }

}
