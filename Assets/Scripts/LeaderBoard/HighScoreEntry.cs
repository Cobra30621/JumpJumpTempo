using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HighScoreEntry 
{
    public float score;
    public string name;
    public string title;

    public HighScoreEntry(string name, float score, string title){
        this.score = score;
        if(name == "")
            this.name = "None";
        else
            this.name = name;
        
        this.title = title;
    }

    public override string ToString(){
        return $"name:{name}, score:{score}, title:{title}";
    }
}
