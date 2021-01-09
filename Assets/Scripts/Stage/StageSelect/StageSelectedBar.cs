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


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
    }

    public void OnEnterStage(){
        stageSelectedPanel.EnterStage(_stageData);
    }
}
