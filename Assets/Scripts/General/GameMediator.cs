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

	// 遊戲系統
	private StageSystem _stageSystem;

	private GameMediator(){}

	public void Initinal(){
		_stageSystem = new StageSystem(this);
		
	}

	public void Update(){
		_stageSystem.Update();
	}

	// ===============StageSystem================

	public StageSystem GetStageSystem(){
		return _stageSystem;
	}

	public void SetStage(StageData stageData){
		_stageSystem.SetStage(stageData);
	}

	public void StartGame(){
		_stageSystem.StartGame();
	}

	
}