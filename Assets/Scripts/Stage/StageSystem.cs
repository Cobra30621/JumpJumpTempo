using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public enum Answer{A = 0,B = 1};
public enum GameState{WaitStart , Gaming};

public class StageSystem : IGameSystem
{
	public float time;
	public float totalTime;
	public float nowTime{
		get{
			if(time < 0)
				return 0;
			else
				return time;
		}
	}

	public int grade;
	public int addGrade;
	public int[] addGrades = {1,2,4,6,8,10,12,15,18,21,24,30, 45, 60 };
	public int bestGrade;

	public float correctCount;
	public float needCorrectCount;
	public int allCorrectCount;
	public int errorCount;
	public float correctRate;
	public int combo;

	public ILevelData _nowLevel;
	public IStageData _nowStage;
	public ILevelData[] _levelDatas;
	public int nowLevel;
	public int maxLevel;

	public string nowQuetion;
	public Answer nowAnswer;
	public GameState gameState;

	public MainGameUI _mainGameUI;
    public StageSystem(GameMediator meditor):base(meditor)
	{
		Initialize();
		OnSceneLoad();
	}

    public override void Initialize()
    {
        gameState = GameState.WaitStart;
    }

	public override void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
				StartGame();
		if(gameState == GameState.Gaming){
			if(Input.GetKeyDown(KeyCode.LeftArrow))
			AnswerQuetion(Answer.A);
			if(Input.GetKeyDown(KeyCode.RightArrow))
				AnswerQuetion(Answer.B);
		}
		
		GameProcess();
	}

    /// <summary>
    /// 場景的切換
    /// </summary>
    

	/// <summary>
    /// 場景的切換
    /// </summary>
	public void OnSceneLoad(){
		_mainGameUI = GameObject.Find("MainGameUI").GetComponent<MainGameUI>();
	}

    /// <summary>
    /// 關卡的流程
    /// </summary>

    public void GameProcess()
    {
        switch(gameState){
			case GameState.WaitStart:
				break;
			case GameState.Gaming:
				GamingProcess();
				break;
		}
    }

	public void StartGame(){
		ResetStage();
		SetLevel();
		CreateNextQuetion();

		gameState = GameState.Gaming;
		_mainGameUI.HideEndPanel();
	}

	public void GamingProcess(){
		time -= Time.deltaTime;
		if(time < 0){
			EndGame();
			
			Debug.Log("遊戲結束");
		}
	}

	public void EndGame(){
		SetBestGrade();
		_mainGameUI.ShowEndPanel();
		gameState = GameState.WaitStart;
	}

	public void SetBestGrade(){
		if(grade > bestGrade)
			bestGrade = grade;
	}


	/// <summary>
    /// 關卡的切換
    /// </summary>

	public void SetStage(IStageData stageData){
		_nowStage = stageData;
		_levelDatas = stageData.GetLevelDatas();
		_nowLevel = _levelDatas[0];
		maxLevel = _levelDatas.Length - 1;
		totalTime = stageData.totalTime;

		_mainGameUI.SetStageInfo(); // 設置關卡的UI
	}

	public void ResetStage(){
		time = _nowStage.totalTime;

		grade = 0;
		addGrade = 1;
		combo = 0;

		correctCount = 0;
		allCorrectCount = 0;
		errorCount = 0;

		nowLevel = 0;
		_nowLevel = _levelDatas[nowLevel];
		
	}

	public void UpgradeLevel(){
		if(nowLevel < maxLevel){
			nowLevel ++;
			_nowLevel = _levelDatas[nowLevel];
			time += _nowLevel.addTime;
			addGrade = addGrades[nowLevel];
			SetLevel();
			Debug.Log($"進入第{nowLevel}關");
			_mainGameUI.SetLevelInfo(); // 設置下一關的UI
		}
		else{
			Debug.Log("破關了");
		}
	}

	public void SetLevel(){
		correctCount = 0;
		needCorrectCount = _nowLevel.needCorrectCount;
		_mainGameUI.SetLevelInfo();
	}

	/// <summary>
    /// 關卡內部運作
    /// </summary>
	public void AnswerQuetion(Answer answer){
		if (answer == nowAnswer){ // 答對
			correctCount++;
			allCorrectCount++;
			combo++;
			grade += addGrade;
			
			_mainGameUI.PlayCorrectAnime();
			Debug.Log($"答對,答對題數為{correctCount}");
			if(correctCount >= needCorrectCount){
				UpgradeLevel(); // 到下一關
			}
		}
		else{ // 答錯
			errorCount ++;
			combo = 0;
			time -= _nowLevel.subTime;
			Debug.Log("答錯");
		}
		CreateNextQuetion();
	}

	public void CreateNextQuetion(){
		int r = Random.Range(0,2);
		nowQuetion = _nowLevel.GetQuetion(r);
		nowAnswer = (Answer)r;

		_mainGameUI.SetNextQuetion();
	}

	public Color GetBGColor(){
		if(nowLevel == 0)
			return Color.gray;
		else
			return _levelDatas[nowLevel -1].barColor;
	}

	public Color GetFGColor(){
		return _levelDatas[nowLevel].barColor;
	}


}