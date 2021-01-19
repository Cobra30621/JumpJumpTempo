using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LeaderBoardPanel : MonoBehaviour
{
    [SerializeField] private GameObject scoreBarPrefab;
    [SerializeField] private GameObject leaderBoardPanel;
    [SerializeField] private Transform bar_pos;
    [SerializeField] private Text lab_stageName;
    private LeaderBoardSystem leaderBoardSystem;

    private List<HighScoreEntry> nowHighScoreEntrys;
    private List<ScoreBar> scoreBars;


    /// <summary>
    /// NameInputUI
    /// </summary>
    [SerializeField] private InputField Input_name;
    [SerializeField] private GameObject nameInputPanel;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        leaderBoardSystem = GameMediator.Instance.GetLeaderBoardSystem();
    }

    /// <summary>
    /// 排行榜相關
    /// </summary>
    public void ShowLeaderBoardPanel(){
        leaderBoardPanel.SetActive(true);
        RemoveAllScoreBar();
        CreateAllLevelBar();
    }

    public void HideLeaderBoardPanel(){
        leaderBoardPanel.SetActive(false);
    }


    /// <summary>
    /// 製作所有的LeverBar
    /// </summary>
    public void CreateAllLevelBar(){
        leaderBoardSystem = GameMediator.Instance.GetLeaderBoardSystem();
        nowHighScoreEntrys = leaderBoardSystem.nowHighScoreEntrys;
        lab_stageName.text = leaderBoardSystem.stageName;

        scoreBars = new List<ScoreBar>();
        for (int i = 0; i < nowHighScoreEntrys.Count; i++)
        {
            CreateLeverBar((i+1)+"", nowHighScoreEntrys[i].score +"", nowHighScoreEntrys[i].name);
        }
    }

    /// <summary>
    /// 製作一個LeverBar
    /// </summary>
    public void CreateLeverBar(string rank, string score, string name )
    {
        var g = Instantiate(scoreBarPrefab, bar_pos);

        var l = g.GetComponent<ScoreBar>();
        l.SetInfo(rank, score, name); 
        scoreBars.Add( l);
    }

    private void RemoveAllScoreBar(){
        if(scoreBars == null)
            return;
        
        foreach(ScoreBar bar in scoreBars){
            if(bar != null)
                Destroy(bar.gameObject);
        }
    }

    /// <summary>
    /// 設定名字相關
    /// </summary>

    public void SetPlayerName(){
        string name = Input_name.text;
        GameMediator.Instance.SetPlayerName(name);
        HideNameInputPanel();
    }

    public void ShowNameInputPanel(){
        nameInputPanel.SetActive(true);
    }

    public void HideNameInputPanel(){
        nameInputPanel.SetActive(false);
    }

}
