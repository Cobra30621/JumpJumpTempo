using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public enum Answer{A = 0,B = 1};
public enum GameState{WaitStart , Gaming};
public enum GamingState {Starting, Questioning, Answering, Texting, Fever};
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
	public float nowFeverTime;
	public float maxFeverTime = 3f;

	// 出題目相關
	public float questioningInterval = 0.5f; // 出題頻率
	public float nextTurnInterval = 0.3f; // 下一輪題目的頻率


	// 分數相關
	public int grade;
	public int addGrade;
	public int[] addGrades = {1,2,4,6,8,10,12,15,18,21,24,30, 45, 60 };
	public int bestGrade;

	public float correctCount;
	public float needCorrectCount;
	public float addCorrectCount;
	public int allCorrectCount;
	public int errorCount;
	public float correctRate;
	public int combo;

	public int correctCountInThisTurn;
	public float maxFeverCount = 5;
	public float feverCount;

	public ILevelData _nowLevel;
	public IStageData _nowStage;
	public ILevelData[] _levelDatas;
	public int nowLevel;
	public int maxLevel;

	public int questionCount = 3;
	public string nowQuetion;
	public string[] nowQuestions;
	public Answer nowAnswer;
	public Answer[] nowAnswers;
	public int hadAnswerQuestionCount;

	public GameState gameState;
	public GamingState gamingState;
	public GamingState animeCompleteState; // 動畫播完後，切換的遊戲狀態

	public MainGameUI _mainGameUI;
    public StageSystem(GameMediator meditor):base(meditor)
	{
		OnSceneLoad();
		Initialize();
	}

    public override void Initialize()
    {
		nowAnswers = new Answer[questionCount];
		nowQuestions = new string[questionCount];
		SetGameState(GameState.WaitStart);
    }

	public override void Update()
	{
		if(Input.GetKeyDown(KeyCode.R))
			StartGame();
		GameProcess();
	}
    

	/// <summary>
    /// 場景的切換
    /// </summary>
	public void OnSceneLoad(){
		_mainGameUI = GameObject.Find("MainGameUI").GetComponent<MainGameUI>();
	}

    /// <summary>
    /// 關卡的流程
    /// </summary>
	public void StartGame(){
		ResetStage();
		SetLevel();
		SetGameState(GameState.Gaming);
		
		PlayTextAnime("成為Master", GamingState.Starting);
		_mainGameUI.StartGame();
	}

	public void EndGame(){
		SetBestGrade();
		_mainGameUI.ShowEndPanel();
		SetGameState(GameState.WaitStart);
	}

    public void GameProcess()
    {
        switch(gameState){
			case GameState.WaitStart:
				break;
			case GameState.Gaming:
				GamingProcess();
				break;
			default:
				Debug.LogError("GameState為錯誤狀態:" + Enum.GetName(typeof(GameState), gameState ));
				break;
		}
    }	

	public void GamingProcess(){
		switch(gamingState){
			case GamingState.Starting:
				CreateNextTurnQuestions(); 
				break;
			case GamingState.Questioning:
				UpdateTime();
				break;
			case GamingState.Answering:
				UpdateTime();
				InputProcess();
				break;
			case GamingState.Texting:
				break;
			case GamingState.Fever:
				UpdateFeverTime();
				InputProcess();
				break;
			default:
				Debug.LogError("GamingState為錯誤狀態:" + Enum.GetName(typeof(GamingState), gamingState ));
				break;
		}
	}

	public void SetGameState(GameState state){
		gameState = state;

		switch(gameState){
			case GameState.WaitStart:
				_mainGameUI.SetAnswerButtonInterActeracable(false);
				break;
			case GameState.Gaming:
				break;
			default:
				Debug.LogError("GameState為錯誤狀態:" + Enum.GetName(typeof(GameState), gameState ));
				break;
		}
	}

	public void SetGamingState(GamingState state){
		gamingState = state;

		switch(gamingState){
			case GamingState.Starting:
				_mainGameUI.SetAnswerButtonInterActeracable(false);
				break;
			case GamingState.Questioning:
				_mainGameUI.SetAnswerButtonInterActeracable(false);
				addCorrectCount = 1f;
				break;
			case GamingState.Answering:
				_mainGameUI.SetAnswerButtonInterActeracable(true);
				break;
			case GamingState.Texting:
				_mainGameUI.SetAnswerButtonInterActeracable(false);
				break;
			case GamingState.Fever:
				_mainGameUI.SetAnswerButtonInterActeracable(true);
				addCorrectCount = 0.5f;
				break;
			default:
				Debug.LogError("GamingState為錯誤狀態:" + Enum.GetName(typeof(GamingState), gamingState ));
				break;
		}
	}

	private void UpdateTime(){
		time -= Time.deltaTime;
		if(time < 0){
			EndGame();
			Debug.Log("遊戲結束");
		}
	}

	private void UpdateFeverTime(){
		nowFeverTime -= Time.deltaTime;
		if(nowFeverTime < 0){
			feverCount = 0;
			CreateNextTurnQuestions();
			Debug.Log("Fever結束");
		}
	}

	private void InputProcess(){
		if(Input.GetKeyDown(KeyCode.LeftArrow))
			AnswerQuestion(Answer.A);
		if(Input.GetKeyDown(KeyCode.RightArrow))
			AnswerQuestion(Answer.B);
	}

	/// <summary>
    /// 關卡的切換
    /// </summary>
	// 切換關卡
	public void SwitchStage(IStageData stageData){
		SetStage(stageData);
		StartGame();
	}

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
		feverCount = 0;

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
		else if(nowLevel == maxLevel){
			nowLevel ++;
			Debug.Log("破關了");
			time += _nowLevel.addTime;
			addGrade = addGrades[nowLevel];
			_mainGameUI.SetLevelInfo(); // 設置下一關的UI
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
	public void AnswerQuestion(Answer answer){
		// Fever狀態判定
		if(gamingState == GamingState.Fever){
			AddCorrectCount();
			
			allCorrectCount++;
			combo++;
			grade += addGrade;

			AudioSourceController.PlaySnd("correct"); // 播放音效
			if(correctCount >= needCorrectCount){
				UpgradeLevel(); // 到下一關
			}
			return;
		}

		// 設置問題
		nowAnswer = nowAnswers[hadAnswerQuestionCount];
		hadAnswerQuestionCount++;

		if (answer == nowAnswer){ // 答對
			AddCorrectCount(); // 增加答對題數

			allCorrectCount++;
			correctCountInThisTurn ++;
			combo++;
			grade += addGrade;

			AudioSourceController.PlaySnd("correct"); // 播放音效
			_mainGameUI.PlayCorrectAnime();
			Debug.Log($"答對,答對題數為{correctCount}");
			if(correctCount >= needCorrectCount){
				UpgradeLevel(); // 到下一關
			}
		}
		else{ // 答錯
			errorCount ++;
			combo = 0;
			time -= _nowStage.subTime;

			AudioSourceController.PlaySnd("wrong"); // 播放音效
			_mainGameUI.PlayWrongAnime();
			Debug.Log("答錯");
		}

		if(hadAnswerQuestionCount == questionCount)
		{
			Debug.Log("這一輪的問題答完了");
			// 本輪全對，fever +1
			if(correctCountInThisTurn >= questionCount)
				feverCount ++;

			_mainGameUI.PlayTurnEndAnime(nextTurnInterval);
		}
		
	}

	public void AddCorrectCount(){
		correctCount += addCorrectCount;
		
		// 更新進度條
		_mainGameUI.RefreshProgressBar();
	}

	public void CreateNextTurnQuestions(){
		if(feverCount == maxFeverCount){ 
			// Fever數達標後，播放Fever動畫
			_mainGameUI.HideAllLabQuestion();
			ShowFeverAnime();
			return;
		}

		SetGamingState(GamingState.Questioning);
		for(int i = 0; i < questionCount; i++){
			int r = UnityEngine.Random.Range(0,2);
			nowQuestions[i] = _nowLevel.GetQuetion(r);
			nowAnswers[i] = (Answer)r;
		}

		correctCountInThisTurn = 0;

		// nowQuetion = _nowLevel.GetQuetion(r);
		// nowAnswer = (Answer)r;

		_mainGameUI.PlayShowingQuestionsAnime();
	}

	public void AllQuestionHadCreate(){
		hadAnswerQuestionCount = 0;
		SetGamingState(GamingState.Answering);
	}

	/// <summary>
    /// Fever
    /// </summary>
	public void ShowFeverAnime(){
		PlayTextAnime("Fever",  GamingState.Fever);
		nowFeverTime = maxFeverTime;
	}

	
	/// <summary>
    /// Text
    /// </summary>
	public void PlayTextAnime(string info, GamingState state){
		SetGamingState(GamingState.Texting);
		animeCompleteState = state;
		_mainGameUI.PlayTextAnime(info);
	}

	public void TextAnimeComplete(){
		SetGamingState(animeCompleteState);
	}

	public Color GetBGColor(){
		if(nowLevel == 0)
			return Color.gray;
		else if(nowLevel >= _levelDatas.Length){
			return _levelDatas[_levelDatas.Length -1].barColor;
		}
		else
			return _levelDatas[nowLevel -1].barColor;
	}

	public Color GetFGColor(){
		if(nowLevel >= _levelDatas.Length)
			return _levelDatas[_levelDatas.Length -1].barColor;
		else
			return _levelDatas[nowLevel].barColor;	
	}


	public void SetBestGrade(){
		if(grade > bestGrade)
			bestGrade = grade;
	}






}