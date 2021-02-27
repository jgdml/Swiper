using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PredefinedDiff : MonoBehaviour{

    public struct Diff{
        public int speed;
        public int lines;
        public int speedUps;
    }

    int[,] diffArr = new int[8, 3] {
        {6, 14, 1},
        {8, 12, 2},
        {8, 12, 3},
        {10, 10, 2},
        {12, 10, 2},
        {14, 10, 0},
        {15, 8, 1},
        {16, 8, 2}
    };

    Diff[] diffs = new Diff[8];

    void Start(){
        
        for (int i = 0; i < diffs.Length; i++){
            diffs[i].speed = diffArr[i, 0];
            diffs[i].lines = diffArr[i, 1];
            diffs[i].speedUps = diffArr[i, 2];
        }
    }

    public Diff getDiff(){
        return diffs[PlayerPrefs.GetInt("Diff")];
    }

}
