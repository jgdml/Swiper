using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGenerator : MonoBehaviour
{

    public Color[] colors = new Color[4];
    List<Color> colorList = new List<Color>{};

    void Start(){

        var hexColors = "#55efc4 #81ecec #74b9ff #a29bfe #ffeaa7 #ff7675 #fd79a8".Split(' ');

        
        for (int i = 0; i < hexColors.Length; i++){

            Color currentColor;
            ColorUtility.TryParseHtmlString(hexColors[i], out currentColor);
            colorList.Add(currentColor);
        }


        for (int i = 0; i < 4; i++){
            int value = Random.Range(0, colorList.Count);

            colors[i] = colorList[value];
            colorList.RemoveAt(value);
        }   
    }    
}
