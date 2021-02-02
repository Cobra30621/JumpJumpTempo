using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMediator
{
    //------------------------------------------------------------------------
	// Singleton模版
	private static GameMediator _instance;
	public static GameMediator Instance
	{
		get
		{
			if (_instance == null)			
				_instance = new GameMediator();
			return _instance;
		}
	}

	// 重要參數
	private bool usingUI;

	// 遊戲系統
	private StageSystem _stageSystem;
	private LeaderBoardSystem _leaderBoardSystem;

	private GameMediator(){}

	public void Initinal(){
		_leaderBoardSystem = new LeaderBoardSystem(this);
		_stageSystem = new StageSystem(this);
		
	}

	public void Update(){
		_leaderBoardSystem.Update();
		_stageSystem.Update();
		
	}

	// ===============StageSystem================

	public StageSystem GetStageSystem(){
		return _stageSystem;
	}

	public void SetStage(IStageData stageData){
		_stageSystem.SetStage(stageData);
	}

	public void StartGame(){
		_stageSystem.StartGame();
	}

	public void SetUsingUI(bool bo){
		usingUI = bo;
	}

	public void SetPlayerName(string name){
		_stageSystem.SetPlayerName(name);
	}

	// 將資料加入排行榜 ，會放在結束時出現的按鈕那邊
	public void AddScoreToLeaderBoard(){
		_stageSystem.AddScoreToLeaderBoard();
	} 

	//暫停遊戲
	public void Pause(){
		_stageSystem.Pause();
	}

	public void EndPause(){
		_stageSystem.EndPause();
	}

	// ===============LeaderBoardSystem================

	public void AddScore(string id , HighScoreEntry score){
		_leaderBoardSystem.AddScore(id, score);
	}

	public LeaderBoardSystem GetLeaderBoardSystem(){
		return _leaderBoardSystem;
	}

	public void LoadData(string id){
		_leaderBoardSystem.LoadData(id);
	}

	public void DeleteAll()
    {
        _leaderBoardSystem.DeleteAll();   
    }
}
