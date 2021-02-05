using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelectedBar : MonoBehaviour
{
    [SerializeField] private Text lab_stageName;
    [SerializeField] private Text lab_title;
    
    [SerializeField] private Button butt_SelectedStage;
    

    public IStageData _stageData;
    private StageSelectedPanel stageSelectedPanel;


    public void Init (StageSelectedPanel panel){
        stageSelectedPanel = panel;
        RefreshInfo();
    }

    public void Init(IStageData stageData, StageSelectedPanel panel){
        _stageData = stageData;
        stageSelectedPanel = panel;
        RefreshInfo();

    }

    public void RefreshInfo(){
        lab_stageName.text = _stageData.stageName;
        SetTitleInfo();
    }

    public void OnEnterStage(){
        stageSelectedPanel.EnterStage(_stageData);
    }

    public void SetStageInfo(){
        stageSelectedPanel.SetStageInfo(_stageData);
    }

    // 取得每一關的最高等級
    public void SetTitleInfo(){
        int id = GameMediator.Instance.GetStageTitleID(_stageData.stageName);
        if(id == -1){
            lab_title.text = "";
        }
        else{
            lab_title.text = _stageData.upgradeTexts[id];
            lab_title.color = _stageData.Colors[id];
        }
    }
}
