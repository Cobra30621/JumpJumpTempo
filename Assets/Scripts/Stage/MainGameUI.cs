
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

    [SerializeField] private Image img_progressBG;
    [SerializeField] private Image img_progressFG;
    [SerializeField] private Image img_timeBar;

    [SerializeField] private Button butt_A;
    [SerializeField] private Button butt_B;
    [SerializeField] private Text lab_A;
    [SerializeField] private Text lab_B;

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
        lab_A.text = levelData.QA_name;
        lab_B.text = levelData.QB_name;

        img_progressBG.color = stageSystem.GetBGColor();
        img_progressFG.color = stageSystem.GetFGColor();
    }

    public void RefreshProgressBar(){
        float rate = stageSystem.correctCount / stageSystem.needCorrectCount;
        img_progressFG.gameObject.transform.localScale = new Vector3(rate, 1, 1);
    }

    public void RefreshTimeBar(){
        float rate = stageSystem.nowTime / stageSystem.totalTime;
        img_timeBar.gameObject.transform.localScale = new Vector3(rate, 1, 1);
        lab_time.text = Mathf.Ceil(stageSystem.nowTime) + "";
    }

    public void SetNextQuetion(){
        lab_quetion.text = stageSystem.nowQuetion;
        RefreshProgressBar();
    }

    [SerializeField]
    public void AnswerQuetion(Answer answer){
        stageSystem.AnswerQuetion(answer);
    }

}