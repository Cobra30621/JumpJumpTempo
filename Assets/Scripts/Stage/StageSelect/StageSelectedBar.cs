using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectedBar : MonoBehaviour
{
    [SerializeField] private Text lab_stageName;
    [SerializeField] private Text lab_title;
    
    [SerializeField] private Button butt_SelectedStage;
    

    public IStageData stageData;
    private StageSelectedPanel stageSelectedPanel;
    [SerializeField] private BarLevelPanel barLevelPanel;


    public void Init (StageSelectedPanel panel){
        stageSelectedPanel = panel;
        barLevelPanel.Init(stageData);
        barLevelPanel.ReverseBarOrder();
        RefreshInfo();
    }

    public void Init(IStageData stageData, StageSelectedPanel panel){
        this.stageData = stageData;
        stageSelectedPanel = panel;
        barLevelPanel.Init(stageData);
        barLevelPanel.ReverseBarOrder();
        RefreshInfo();

    }

    public void RefreshInfo(){
        lab_stageName.text = stageData.stageName;
        SetTitleInfo();
    }

    public void OnEnterStage(){
        stageSelectedPanel.EnterStage(stageData);
    }

    public void SetStageInfo(){
        stageSelectedPanel.SetStageInfo(stageData);
    }

    // 取得每一關的最高等級
    public void SetTitleInfo(){
        int id = GameMediator.Instance.GetStageTitleID(stageData.stageName);
        if(id == -1){
            lab_title.text = "";
        }
        else{
            lab_title.text = stageData.upgradeTexts[id];
            lab_title.color = stageData.Colors[id];
            barLevelPanel.SetLevelBar(id);
        }
    }
}
