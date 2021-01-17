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

	// ===============LeaderBoardSystem================

	public void AddScore(string id , HighScoreEntry score){
		_leaderBoardSystem.AddScore(id, score);
	}

	public LeaderBoardSystem GetLeaderBoardSystem(){
		return _leaderBoardSystem;
	}
}
