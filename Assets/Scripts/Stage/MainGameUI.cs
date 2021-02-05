
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening; // 添加這個DOTween所在的名字空間

public class MainGameUI: MonoBehaviour
{
    private static MainGameUI instance;
    public StageSystem stageSystem;

    [SerializeField] private Text lab_title;
    [SerializeField] private Text lab_question;
    [SerializeField] private Text lab_time;
    [SerializeField] private Text lab_grade;
    [SerializeField] private Text lab_addGrade;
    // [SerializeField] private Text lab_combo;
    [SerializeField] private Text lab_fever;
    [SerializeField] private GameObject panel_fever;

    // 問題
    [SerializeField] private GameObject[] GO_questions;
    private Question[] questions;
    private Text[] lab_questions;

    [SerializeField] private Image img_progressBG;
    [SerializeField] private Image img_progressFG;
    [SerializeField] private Image img_timeBar;
    [SerializeField] private Image img_feverBar;
    [SerializeField] private BarAnime feverBarAnime;

    [SerializeField] private Button butt_A;
    [SerializeField] private Button butt_B;
    [SerializeField] private Text lab_A;
    [SerializeField] private Text lab_B;
    [SerializeField] private Text lab_showUpgrade;
    [SerializeField] private ShowUpgradeAnime showUpgradeAnime;
    [SerializeField] private TextInfoAnime feverInfoAnime;
    [SerializeField] private TextInfoAnime timeInfoAnime;


    [Header("結算畫面")]
    [SerializeField] private GameObject EndPanel;
    [SerializeField] private Text lab_Endgrade;
    [SerializeField] private Text lab_bestGrade;
    [SerializeField] private Text lab_correctCount;
    [SerializeField] private Text lab_errorCount;
    [SerializeField] private Text lab_level;
    [SerializeField] private Text lab_feverAvg;
    // [SerializeField] private Button butt_Retry;

    [Header("顯示文字畫面")]
    [SerializeField] private GameObject InfoPanel;
    [SerializeField] private Text lab_info;

    public int hadCreatedQuestionCount;
    public float questionInterval = 0.3f;

    // ProgressBar動畫
    [Header("ProgressBar動畫")]
    public float progressBarAddTime = 0.4f;
    private Sequence ProgressBarSequence; 
    private bool hadChangeColor;

    [Header("結算升階動畫")]
    public LevelBarPanel levelBarPanel;


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        instance = this;
        Initialize();
    }

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        RefreshTimeBar();
        // RefreshProgressBar();
        RefreshFeverBarInFeverState();
    }

    public void Initialize(){
        stageSystem = GameMediator.Instance.GetStageSystem();

        int length = GO_questions.Length;
        lab_questions = new Text[3];
        questions = new Question[3];
        for (int i = 0; i < length; i++)
        {
            lab_questions[i] = GO_questions[i].GetComponent<Text>();
            questions[i] = GO_questions[i].GetComponent<Question>();
            questions[i].SetMainGameUI(this); 
        }

        butt_A.onClick.AddListener(delegate() {
            AnswerQuestion(Answer.A);
        } );

        butt_B.onClick.AddListener(delegate() {
            AnswerQuestion(Answer.B);
        } );
    }

    /// <summary>
    ///  暫停相關
    /// </summary>
    public void Pause(){
        DOTween.PauseAll();
    }

    public void EndPause(){
        DOTween.PlayAll();
    }

    public void StopGame(){
        
    }

    /// <summary>
    ///  設置關卡資訊
    /// </summary>
    // 開始遊戲
    public void StartGame(){
        HideAllLabQuestion();
		HideEndPanel();
        SetBarColor();
        RefreshProgressBar();
    }

    public void SetStageInfo(){
        if(stageSystem == null)
            stageSystem = GameMediator.Instance.GetStageSystem();

        IStageData stageData = stageSystem._nowStage;
        lab_question.text = stageData.questionName;
        
    }

    public void SetLevelInfo(){
        ILevelData levelData = stageSystem._nowLevel;
        IStageData stageData = stageSystem._nowStage;
        lab_A.text = stageData.QA_name;
        lab_B.text = stageData.QB_name;
        lab_addGrade.text = $"x{stageSystem.addGrade}" ;
        
        
        int index = stageSystem.nowLevel - 1;
        if(index <0){
            lab_title.text = "";
            return;
        }
            
        // 播放生階動畫
        if(index >= stageData.upgradeTexts.Length)
            index = stageData.upgradeTexts.Length -1;
        
        lab_showUpgrade.text = stageData.upgradeTexts[index];
        lab_showUpgrade.color = stageSystem.GetBGColor();
        
        lab_title.text = stageData.upgradeTexts[index];
        lab_title.color = stageSystem.GetBGColor();

        // 設定結算介面等級
        lab_level.text = stageData.upgradeTexts[index];
        lab_level.color = stageSystem.GetBGColor();
        showUpgradeAnime.Scaling();

    }

    // private void SetLevelUp


    public void RefreshTimeBar(){
        float rate = stageSystem.nowTime / stageSystem.totalTime;
        // img_timeBar.fillAmount = 1f - rate;
        img_timeBar.gameObject.transform.localScale = new Vector3(1, rate, 1);
        // img_timeBar.gameObject.transform.localScale = new Vector3(rate, 1, 1);
        lab_time.text = Mathf.Ceil(stageSystem.nowTime) + "";
    }

    public void PlayAddTimeAnime(string addTime){
        string info = $"過關\nTime + {addTime}";
        timeInfoAnime.PlayAnime(info);
    }

    public void RefreshProgressBar(){
        float rate = 0;
        rate = stageSystem.correctCount / stageSystem.needCorrectCount;
        if(rate > 1){rate = 1;}

        float rateBefore = 0; // 上一個進度
        if(stageSystem.correctCount == 0)
            rateBefore = 0f;
        else
            rateBefore = (stageSystem.correctCount -1)/stageSystem.needCorrectCount;
        // Debug.Log($"rateBefore:{rateBefore}, rate:{rate}, stageSystem.correctCount:{stageSystem.correctCount} ");

        img_progressFG.fillAmount = rateBefore;

        // 以下Code寫得十分爛
        // 播放設定進度條動畫
        if(rate == 1f){ // 要升到下一階
            ProgressBarSequence = DOTween.Sequence();
            hadChangeColor = false;
            ProgressBarSequence.Append(img_progressFG.DOFillAmount(1f, progressBarAddTime))
                .OnComplete((SetBarColor)); 
        }
        else{ // 一般增加
            if(!hadChangeColor){ // 當ProgressBarSequence還沒播完，這邊幫中斷
                SetBarColor();
                ProgressBarSequence.Kill(false);// 終止上一個動畫

                // ProgressBarSequence.OnStart(()=>{img_progressFG.fillAmount = 0f;}) // 將進度條歸零在執行動畫
                // .AppendInterval(0.1f) // 等一下子在執行動畫
                // .OnStart(()=>{img_progressFG.fillAmount = 0f;}) // 將進度條歸零在執行動畫
                // .Append(img_progressFG.DOFillAmount(rate, progressBarAddTime));
                // Debug.Log("OAO");
            }
            ProgressBarSequence.AppendInterval(0.1f) // 等一下子在執行動畫
            .Append(img_progressFG.DOFillAmount(rate, progressBarAddTime));
            
        }
            
        lab_grade.text = stageSystem.grade + "";
    }

    

    private void SetBarColor(){
        img_progressFG.fillAmount = 0f; // ProgressBar歸零
        img_progressBG.color = stageSystem.GetBGColor();
        img_progressFG.color = stageSystem.GetFGColor();
        hadChangeColor = true;
    }


    public void RefreshFeverBarInFeverState(){
        float rate;
        string text;
        if(stageSystem.gamingState == GamingState.Fever){
            rate = stageSystem.nowFeverTime / stageSystem.maxFeverTime;
            text = $"{Mathf.Ceil(stageSystem.nowFeverTime)}";
            if(rate > 1){rate = 1;}
            img_feverBar.gameObject.transform.localScale = new Vector3(1, rate, 1);
            lab_fever.text = text;
        }
        

    }

    public void PlayFeverBarAnime(){
        float rateStart = (stageSystem.feverCount - 1) / stageSystem.maxFeverCount;
        float rateEnd = stageSystem.feverCount / stageSystem.maxFeverCount;

        string text = $"{stageSystem.feverCount}";
        if(rateEnd > 1){rateEnd = 1;}
        if(rateStart < 0){ rateStart = 0;}

        feverBarAnime.PlayAnime(rateStart , rateEnd);
        lab_fever.text = text;

        if(stageSystem.feverCount >= 1)
            feverInfoAnime.PlayAnime(); // 顯示提示文字
    }

    public void PlayFeveringAnime(){
        panel_fever.SetActive(true);
    }

    public void EndFeveringAnime(){
        panel_fever.SetActive(false);
    }


    public void PlayCorrectAnime(){
        lab_grade.GetComponent<ScalingAnime>().Scaling();
        int count = stageSystem.hadAnswerQuestionCount;
        questions[count - 1].ShowOutcome(true);
    }

    public void PlayWrongAnime(){
        int count = stageSystem.hadAnswerQuestionCount;
        questions[count - 1].ShowOutcome(false);
    }


    public void PlayShowingQuestionsAnime(){
        // 設置題目
        for (int i = 0 ; i < stageSystem.questionCount; i++){
            lab_questions[i].text = stageSystem.nowQuestions[i];
        }

        hadCreatedQuestionCount = 0;
        HideAllLabQuestion();
        CreateQuestion();
        // lab_quetion.text = stageSystem.nowQuetion;
        // lab_combo.text = $"combo:{stageSystem.combo}";
        
    }

    public void CreateQuestion(){
        if(hadCreatedQuestionCount == stageSystem.questionCount){
            stageSystem.AllQuestionHadCreate();// 已經創造完全部的問題
        }
        else{ // 創造下一個題目
            Debug.Log($"MainUI等待完{questionInterval}秒後，創造下一題");
            questions[hadCreatedQuestionCount].ShowQuestion(questionInterval);
            hadCreatedQuestionCount ++ ;
        }
    }

    public void HideAllLabQuestion(){
        foreach (Question question in questions)
        {
            question.Hide();
        }
    }

    public void AnswerQuestion(Answer answer){
        stageSystem.AnswerQuestion(answer);
        //RefreshProgressBar();
    }

    public void PlayTurnEndAnime(float interval){
        // Debug.Log($"等待完{interval}秒後，創造下一輪題目");
        Sequence mySequence = DOTween.Sequence();
        mySequence.AppendInterval(interval)
            .OnComplete(stageSystem.CreateNextTurnQuestions);
    }

    


    /// <summary>
    ///  設置結算畫面
    /// </summary>
    public void ShowEndPanel(){
        EndPanel.SetActive(true);
        lab_bestGrade.text = $"Best Grade:{stageSystem.bestGrade}";
        lab_grade.text = $"{stageSystem.grade}";
        lab_correctCount.text = $"O:{stageSystem.allCorrectCount}";
        lab_errorCount.text = $"X:{stageSystem.errorCount}";
        lab_feverAvg.text = $"FeverAvg:{stageSystem.GetFeverAvg()}";

        PlayEndLevelAnime(stageSystem.nowLevel - 1);
    }

    public void HideEndPanel(){
        EndPanel.SetActive(false);
    }

    public void Retry(){
        stageSystem.StartGame();
    }

    public void PlayEndLevelAnime(int level){
        levelBarPanel.PlayLevelAnime(level);
    }

    /// <summary>
    ///  顯示文字畫面
    /// </summary>
    public void PlayTextAnime(string info){
        lab_info.text = info;

        Sequence mySequence = DOTween.Sequence();
        mySequence.OnStart(OnStart)
            .Append(InfoPanel.transform.DOScaleY(1, 0.5f).SetEase(Ease.OutQuint))
            .AppendInterval(1f)
            .Append(InfoPanel.transform.DOScaleY(0, 0.5f).SetEase(Ease.OutQuint))
            .OnComplete(OnComplete);
    }

    private void OnStart(){
        InfoPanel.SetActive(true);
        InfoPanel.transform.localScale = new Vector3(1, 0, 1);
    }

    private void OnComplete(){
        InfoPanel.SetActive(false);
        stageSystem.TextAnimeComplete();
    }

    public void ShowCutDown(){

    }

    public void SetAnswerButtonInterActeracable(bool bo){
        butt_A.interactable = bo;
        butt_B.interactable = bo;
    }

}