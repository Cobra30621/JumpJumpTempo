using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageInfoPanel : MonoBehaviour
{
    [SerializeField] private LeaderBoardPanel leaderBoardPanel;
    [SerializeField] private GameObject infoPanel;

    private IStageData nowStageData;

    // StageInfoPanel
    [SerializeField] private StageSelectedPanel stageSelectedPanel;

    public void Open(){
        if (infoPanel.activeSelf)
        {
            GameMediator.Instance.SetUsingUI(false);
            infoPanel.SetActive(false);
        }
        else if (!infoPanel.activeSelf)
        {
            GameMediator.Instance.SetUsingUI(true);
            infoPanel.SetActive(true);
            RefreshInfo();
        }
    }

    public void Close(){
        GameMediator.Instance.SetUsingUI(false);
        infoPanel.SetActive(false);
    }

    private void RefreshInfo(){
        
        leaderBoardPanel.ShowLeaderBoardPanel();
    }

    public void SetStageInfo(IStageData stageData){
        nowStageData = stageData;
        leaderBoardPanel.SetStageData(stageData.stageName); // 設定排行榜系統的關卡資料
        Debug.Log($"顯示關卡{stageData}資料");
    }

    public void EnterStage(){
        Close(); // 關閉選單介面
        stageSelectedPanel.EnterStage(nowStageData); // 進入關卡
        Debug.Log($"進入關卡{nowStageData}資料");
    }
}
