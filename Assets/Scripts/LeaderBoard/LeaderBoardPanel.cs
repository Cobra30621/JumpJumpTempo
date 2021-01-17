using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LeaderBoardPanel : MonoBehaviour
{
    public GameObject scoreCellPrefab;
    public string id;
    public HighScoreEntry score;
    private LeaderBoardSystem leaderBoardSystem;

    /// <summary>
    /// UI
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

    public void AddScore(){
        GameMediator.Instance.AddScore(id, score);
    }

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
