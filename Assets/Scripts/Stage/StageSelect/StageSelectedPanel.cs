using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectedPanel : MonoBehaviour
{
    private StageSystem stageSystem;
    public StageSelectedBar[] stageSelectedBars;
    [SerializeField] private GameObject selectedPanel;

    // StageInfoPanel
    [SerializeField] private StageInfoPanel stageInfoPanel;
    
    
    // Start is called before the first frame update
    void Start()
    {
        stageSystem = GameMediator.Instance.GetStageSystem();
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(){
        foreach (var bar in stageSelectedBars)
        {
            bar.Init(this);
        }
    }

    public void Open(){
        if (selectedPanel.activeSelf)
        {
            GameMediator.Instance.SetUsingUI(false);
            selectedPanel.SetActive(false);
        }
        else if (!selectedPanel.activeSelf)
        {
            GameMediator.Instance.SetUsingUI(true);
            selectedPanel.SetActive(true);
            RefreshInfo();
        }
    }

    public void Close(){
        GameMediator.Instance.SetUsingUI(false);
        selectedPanel.SetActive(false);
    }

    private void RefreshInfo(){
        foreach (var bar in stageSelectedBars)
        {
            bar.RefreshInfo();
        }
    }

    public void EnterStage(IStageData stageData){
        stageSystem.EndPause();
        stageSystem.SwitchStage(stageData);
        Close();
    }

    // 設置關卡資料
    public void SetStageInfo(IStageData stageData){
        stageInfoPanel.SetStageInfo(stageData);
        stageInfoPanel.Open();
    }

    
}
