using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class LeaderBoardSystem : IGameSystem
{
    
    [System.Serializable]
    private class HighScores{
        public List<HighScoreEntry> highScoreEntrys;
    }

	// public MainGameUI _mainGameUI;
    // public Dictionary <string , List<HighScoreEntry>> dicHighScoreEntrys;
    public string stageName;
    public List<HighScoreEntry> nowHighScoreEntrys;
    public List<HighScoreEntry> oldHighScoreEntrys;
    private GoogleSheetManager googleSheetManager; // 連線存檔

    public LeaderBoardSystem(GameMediator meditor):base(meditor)
	{
		Initialize();
	}

    public override void Initialize()
    {
        googleSheetManager  = GameObject.Find("GoogleSheetManager").GetComponent<GoogleSheetManager>();
        // LoadData();
        // dicHighScoreEntrys = new Dictionary <string , List<HighScoreEntry>>();
		// nowHighScoreEntrys = new List<HighScoreEntry>();
        // oldHighScoreEntrys = new List<HighScoreEntry>();
    }

    public override void Update(){
        // PrintLeaderBoard();
    }


    public void AddScore(string id, HighScoreEntry scoreEntry){
        SetNowHighScoreEntrys(id);
        
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

        // dicHighScoreEntrys[id] = nowHighScoreEntrys;
        SaveData(id);
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
        // 按照分數排列
        nowHighScoreEntrys.Sort((x, y) => -x.score.CompareTo(y.score));
    }

    public List<HighScoreEntry> GetHighScoreEntrys(){
        return nowHighScoreEntrys;
    }

    private void SetNowHighScoreEntrys(string id){
        LoadData(id);
        // if(dicHighScoreEntrys.ContainsKey(id))
        //     nowHighScoreEntrys = dicHighScoreEntrys[id];
        // else
        // {
        //     Debug.Log($"新增{id}的排行榜資料");
        //     dicHighScoreEntrys.Add(id, new List<HighScoreEntry>());
        //     nowHighScoreEntrys = dicHighScoreEntrys[id];
        // }
        
    }

    public void LoadData(string id){
        // string json = PlayerPrefs.GetString("Dic_HighScoreTable");
        // HighScores highScore  = JsonUtility.FromJson(json);
        stageName = id;
        string json = PlayerPrefs.GetString(id);
        HighScores highScores  = JsonUtility.FromJson<HighScores>(json);
        if(highScores == null){
            Debug.Log("Load:創造新資料");
            // dicHighScoreEntrys = new Dictionary <string , List<HighScoreEntry>>();
            nowHighScoreEntrys = new List<HighScoreEntry>();
        }
        else{
            Debug.Log("Load:"+ PlayerPrefs.GetString(id));
            // dicHighScoreEntrys =  highScores.dicHighScoreEntrys;
            nowHighScoreEntrys = highScores.highScoreEntrys;
        }
        // dicHighScoreEntrys = new Dictionary <string , List<HighScoreEntry>>();

    }

    public void SaveData(string id){
        HighScores highScores = new HighScores{highScoreEntrys = nowHighScoreEntrys};
        // Debug.Log($"{id}:" + highScores);

        string json = JsonUtility.ToJson(highScores);
        PlayerPrefs.SetString(id , json);
        PlayerPrefs.Save();
        Debug.Log($"Save{id}"+ PlayerPrefs.GetString(id));
        
        googleSheetManager.SaveData(id, json);
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