using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreEntry 
{
    public float score;
    public string name;
    public string title;
    public int titleID; // -1 代表沒資料

    public HighScoreEntry(string name, float score, string title, int titleID){
        Init(name, score, title);
        this.titleID = titleID;
    }

    public HighScoreEntry(string name, float score, string title){
        Init(name, score, title);
    }

    private void Init(string name, float score, string title){
        this.score = score;
        if(name == "")
            this.name = "None";
        else
            this.name = name;
        
        this.title = title;
    }

    public void SetTitleID(int ID){
        titleID = ID;
    }

    public override string ToString(){
        return $"name:{name}, score:{score}, title:{title}";
    }
}
