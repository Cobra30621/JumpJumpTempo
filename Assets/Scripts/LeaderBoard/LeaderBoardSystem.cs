using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class LeaderBoardSystem : IGameSystem
{
    private class HighScores{
        public List<HighScoreEntry> highScoreEntryList;
    }

	// public MainGameUI _mainGameUI;
    public Dictionary <string , List<HighScoreEntry>> dicHighScoreEntrys;
    public List<HighScoreEntry> nowHighScoreEntrys;
    public List<HighScoreEntry> oldHighScoreEntrys;

    public LeaderBoardSystem(GameMediator meditor):base(meditor)
	{
		Initialize();
	}

    public override void Initialize()
    {
        LoadData();
        dicHighScoreEntrys = new Dictionary <string , List<HighScoreEntry>>();
		// nowHighScoreEntrys = new List<HighScoreEntry>();
        oldHighScoreEntrys = new List<HighScoreEntry>();
    }

    public override void Update(){
        // PrintLeaderBoard();
    }

    public void AddScore(string id, HighScoreEntry scoreEntry){
        SetNowHighScoreEntrys(id);
        AddScore(scoreEntry);
    }

    public void AddScore(HighScoreEntry scoreEntry){
        oldHighScoreEntrys = nowHighScoreEntrys;

        if(CheckWhetherHad(scoreEntry)) // 如果已有玩家資料
        {
            AddHadScore(scoreEntry); // 將玩家資料修改
            Debug.Log($"更改資料{scoreEntry}");
        }
        else{
            nowHighScoreEntrys.Add(scoreEntry);  // 新增玩家資料
            Debug.Log($"新增資料{scoreEntry}");
        }
            
        
        SortNowScores();
        SaveData();
        PrintLeaderBoard();
    }

    private void AddHadScore(HighScoreEntry scoreEntry){
        foreach (HighScoreEntry score in nowHighScoreEntrys)
        {
            if(score.name == scoreEntry.name){
                score.score = scoreEntry.score;
                score.title = scoreEntry.title;
            }
                
        }
    }

    private bool CheckWhetherHad(HighScoreEntry scoreEntry){
        foreach (HighScoreEntry score in nowHighScoreEntrys)
        {
            if(score.name == scoreEntry.name)
                return true;
        }
        return false;
    }

    private void SortNowScores(){

    }

    public List<HighScoreEntry> GetHighScoreEntrys(){
        return nowHighScoreEntrys;
    }

    private void SetNowHighScoreEntrys(string id){
        if(dicHighScoreEntrys.ContainsKey(id))
            nowHighScoreEntrys = dicHighScoreEntrys[id];
        else
        {
            Debug.Log($"新增{id}的排行榜資料");
            dicHighScoreEntrys.Add(id, new List<HighScoreEntry>());
        }
    }

    public void LoadData(){
        string json = PlayerPrefs.GetString("HighScoreTable");
        HighScores highScores =  JsonUtility.FromJson<HighScores>(json);
        if(highScores == null){
            nowHighScoreEntrys = new List<HighScoreEntry>();
        }
        else{
            nowHighScoreEntrys =  highScores.highScoreEntryList;
        }
        

    }

    public void SaveData(){
        HighScores highScores = new HighScores{highScoreEntryList = nowHighScoreEntrys};

        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString("HighScoreTable", json);
        PlayerPrefs.Save();
        Debug.Log("Save"+ PlayerPrefs.GetString("HighScoreTable"));
    }

    public void PrintLeaderBoard(){
        string scores = "";
        foreach (HighScoreEntry score in nowHighScoreEntrys)
        {
            scores += score +"\n";
        }
        Debug.Log(scores);
    }

    
}