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

	public void SetStage(IStageData stageData){
		_stageSystem.SetStage(stageData);
	}

	public void StartGame(){
		_stageSystem.StartGame();
	}

	public void SetUsingUI(bool bo){
		usingUI = bo;
	}
}