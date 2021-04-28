using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelManager : MonoBehaviour{
    
    public int levelSize;
    public int maxStraightLine;
    public int maxSpeedUp;

    int turnCount = 0;


    string[] dirs = new string[4] {"w", "a", "d", "s"};
    
    List<string> turns = new List<string>{};

    public Tile tile;
    public Tile tileHit;
    public Tile tileFinish;
    public Tile tileSpeed;
    public Tilemap tilemapPath;
    public Tilemap tilemapLevel;
    public Tilemap tilemapGoal;
    public Tilemap tilemapSpeed;

    Vector3Int pos;
    Vector3Int posHit;

    DiffManager dm;

    void Start(){
        dm = GetComponent<DiffManager>();

        newLevel();
    }


    void newLevel(){
        var diff = dm.getDiff();
        
        maxStraightLine = diff.lines;
        maxSpeedUp = diff.speedUps;
        
        pos = tilemapPath.WorldToCell(Vector3.zero);
        spawnTiles();
    }


    string opositeOf(string dir){
        string oposite = "";
        if (dir == "w"){
            oposite = "s";
        }
        else if (dir == "a"){
            oposite = "d";
        }
        else if (dir == "s"){
            oposite = "w";
        }
        else if (dir == "d"){
            oposite = "a";
        }

        return oposite;
    }
    

    string[] makeLevel(){

        string last = "";
        string current = "";    
        List<string> level = new List<string>{};

        while(level.Count != levelSize){
            current = dirs[Random.Range(0, 3)];


            if (level.Count == 0){
                level.Add("block");
            }

            else if (level.Count == levelSize-2){
                level.Add("w");
                turns.Add("w");
                level.Add("block");
            }

            else if (current != last){

                if (last != opositeOf(current)){

                    if (turns.Count > 4){
                        
                        if (current == "a"){

                            if (turns[turns.Count-2] == "a" && turns[turns.Count-4] == "a"){
                                current = "d";
                            }
                        }
                        else if (current == "d"){

                            if (turns[turns.Count-2] == "d" && turns[turns.Count-4] == "d"){
                                current = "a";
                            }
                        }
                    }


                    level.Add(current);
                    turns.Add(current);

                    last = current;

                    for (int y = 0; y < Random.Range(1, maxStraightLine); y++){

                        if (level.Count != levelSize-2){

                            level.Add("block");
                        }
                    }
                }
            }
        }
        if (turns[0] == "w"){
            turns.RemoveAt(0);
        }


        ///// Speed blocks adder
        for (int i = 0; i < maxSpeedUp; i++){
            int calc = ((int)((level.Count) / (maxSpeedUp+1))) * (i+1);

            if (level[calc] == "block"){
                level[calc] = "speed";
            }
            else{
                level[calc+2] = "speed";
            }
        }

        
        return level.ToArray();
    }

    void setSpawnTiles(){
        tilemapLevel.SetTile(pos, tileHit);
        pos.y++;
        pos.x--;
        tilemapLevel.SetTile(pos, tileHit);
        pos.y--;
        tilemapLevel.SetTile(pos, tileHit);
        pos.x+=2;
        tilemapLevel.SetTile(pos, tileHit);
        pos.y++;
        tilemapLevel.SetTile(pos, tileHit);
        pos.x--;
    }

    void spawnTiles(){
        var level = makeLevel();
        string currentDir = "";
        int[] posMaxSide = new int[2] {0, 0};

        pos.y -= 21;

        setSpawnTiles();

        while (pos.y != 0){
            tilemapPath.SetTile(pos, tile);
            pos.y++;
        }

        for (int i = 0; i < level.Length; i++){

            if (level[i] != "block" && level[i] != "speed"){
                currentDir = level[i];
            }

            if (currentDir == "w"){
                pos.y++;
            }
            else if (currentDir == "a"){
                pos.x--;
            }
            else if (currentDir == "s"){
                pos.y--;
            }
            else if (currentDir == "d"){
                pos.x++;
            }

            
            tilemapPath.SetTile(pos, tile);

            if (level[i] == "speed"){
                tilemapSpeed.SetTile(pos, tileSpeed);
            }
            


            if (i == 0){
                posMaxSide[0] = pos.x;
                posMaxSide[1] = pos.x;
            }

            if (pos.x < posMaxSide[0]){
                posMaxSide[0] = pos.x;
            }

            if (pos.x > posMaxSide[1]){
                posMaxSide[1] = pos.x;
            }
        }
        pos.y++;
        tilemapGoal.SetTile(pos, tileFinish);
        

        Vector3Int posHit = tilemapLevel.WorldToCell(new Vector3(posMaxSide[0]-1, -1, 0));

        for (int y = 0; y < pos.y+1; y++){

            for (int x = posMaxSide[0]; x < posMaxSide[1]+3; x++){

                bool isTile = tilemapPath.GetTile(new Vector3Int(posHit.x, posHit.y, 0));

                if (!isTile){
                    tilemapLevel.SetTile(posHit, tileHit);
                }
                posHit.x++;

            }
            posHit.x = posMaxSide[0]-1;
            posHit.y++;
        }
    }


    public string getNextTurn(){
        return turns[turnCount];
    }


    public bool isTurnOk(string direction){
        if (direction == turns[turnCount]){
            turnCount++;
            return true;
        }

        else{
            return false;
        }
    }


    public int getMaxCombo(){
        print("firstTurn: "+turns[0]);
        return turns.Count;
    }


    public Vector3 getTilePos(Vector3 pos){
        var tilePos = tilemapPath.WorldToCell(pos);

        return tilemapPath.CellToWorld(tilePos);

    }
}
