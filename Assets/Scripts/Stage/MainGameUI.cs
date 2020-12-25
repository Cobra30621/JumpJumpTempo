
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


    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Start()
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
    }

    public void Initialize(){
        stageSystem = GameMediator.Instance.GetStageSystem();

        butt_A.onClick.AddListener(delegate() {
            AnswerQuetion(Answer.A);
        } );

        butt_B.onClick.AddListener(delegate() {
            AnswerQuetion(Answer.B);
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
    }

    public void RefreshTimeBar(){
        float rate = stageSystem.nowTime / stageSystem.totalTime;
        img_timeBar.gameObject.transform.localScale = new Vector3(rate, 1, 1);
        lab_time.text = Mathf.Ceil(stageSystem.nowTime) + "";
    }

    public void SetNextQuetion(){
        lab_quetion.text = stageSystem.nowQuetion;
        lab_combo.text = $"combo:{stageSystem.combo}";
        RefreshProgressBar();
    }

    public void AnswerQuetion(Answer answer){
        stageSystem.AnswerQuetion(answer);
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