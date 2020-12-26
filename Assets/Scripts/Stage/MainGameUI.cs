
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainGameUI: MonoBehaviour
{
    private static MainGameUI instance;
    public StageSystem stageSystem;

    [SerializeField] private Text lab_title;
    [SerializeField] private Text lab_quetion;
    [SerializeField] private Text lab_time;
    [SerializeField] private Text lab_grade;
    [SerializeField] private Text lab_addGrade;
    [SerializeField] private Text lab_combo;

    // 問題
    [SerializeField] private GameObject[] GO_questions;
    private Question[] questions;
    private Text[] lab_questions;

    [SerializeField] private Image img_progressBG;
    [SerializeField] private Image img_progressFG;
    [SerializeField] private Image img_timeBar;

    [SerializeField] private Button butt_A;
    [SerializeField] private Button butt_B;
    [SerializeField] private Text lab_A;
    [SerializeField] private Text lab_B;
    [SerializeField] private Text lab_showUpgrade;
    [SerializeField] private ShowUpgradeAnime showUpgradeAnime;

    [Header("結算畫面")]
    [SerializeField] private GameObject EndPanel;
    [SerializeField] private Text lab_Endgrade;
    [SerializeField] private Text lab_bestGrade;
    [SerializeField] private Text lab_correctCount;
    [SerializeField] private Text lab_level;
    // [SerializeField] private Button butt_Retry;

    public int hadCreatedQuestionCount;
    public float questionInterval = 1f;


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
        RefreshProgressBar();
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
    ///  設置關卡資訊
    /// </summary>

    public void SetStageInfo(){
        if(stageSystem == null)
            stageSystem = GameMediator.Instance.GetStageSystem();

        IStageData stageData = stageSystem._nowStage;
        lab_title.text = stageData.questionName;
        
    }

    public void SetLevelInfo(){
        ILevelData levelData = stageSystem._nowLevel;
        IStageData stageData = stageSystem._nowStage;
        lab_A.text = stageData.QA_name;
        lab_B.text = stageData.QB_name;
        lab_addGrade.text = $"x{stageSystem.addGrade}" ;

        img_progressBG.color = stageSystem.GetBGColor();
        img_progressFG.color = stageSystem.GetFGColor();
        
        // 播放生階動畫
        int index = stageSystem.nowLevel - 1;
        if(index <0) 
            return;
            
        if(index >= stageData.upgradeTexts.Length)
            index = stageData.upgradeTexts.Length -1;
        
        lab_showUpgrade.text = stageData.upgradeTexts[index];
        lab_showUpgrade.color = stageSystem.GetBGColor();
        // 設定結算介面等級
        lab_level.text = stageData.upgradeTexts[index];
        lab_level.color = stageSystem.GetBGColor();
        showUpgradeAnime.Scaling(); 
    }

    public void RefreshProgressBar(){
        float rate = stageSystem.correctCount / stageSystem.needCorrectCount;
        if(rate > 1){rate = 1;}

        img_progressFG.gameObject.transform.localScale = new Vector3(rate, 1, 1);
        lab_grade.text = stageSystem.grade + "";
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

    public void RefreshTimeBar(){
        float rate = stageSystem.nowTime / stageSystem.totalTime;
        img_timeBar.gameObject.transform.localScale = new Vector3(rate, 1, 1);
        lab_time.text = Mathf.Ceil(stageSystem.nowTime) + "";
    }

    public void SetNextQuestions(){
        // 設置題目
        for (int i = 0 ; i < stageSystem.questionCount; i++){
            lab_questions[i].text = stageSystem.nowQuestions[i];
        }

        hadCreatedQuestionCount = 0;
        HideAllLabQuestion();
        CreateQuestion();
        // lab_quetion.text = stageSystem.nowQuetion;
        lab_combo.text = $"combo:{stageSystem.combo}";
        
    }

    public void CreateQuestion(){
        if(hadCreatedQuestionCount == stageSystem.questionCount){
            stageSystem.AllQuestionHadCreate();// 已經創造完全部的問題
        }
        else{ // 創造下一個題目
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
        RefreshProgressBar();
    }


    /// <summary>
    ///  設置結算畫面
    /// </summary>
    public void ShowEndPanel(){
        EndPanel.SetActive(true);
        lab_bestGrade.text = $"Best Grade:{stageSystem.bestGrade}";
        lab_Endgrade.text = $"Grade:{stageSystem.grade}";
        lab_correctCount.text = $"Correct Count:{stageSystem.allCorrectCount}";
    }

    public void HideEndPanel(){
        EndPanel.SetActive(false);
    }

    public void Retry(){
        stageSystem.StartGame();
    }

}