using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectedPanel : MonoBehaviour
{
    private StageSystem stageSystem;
    public StageSelectedBar[] stageSelectedBars;
    [SerializeField] private GameObject _panel;
    
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
        if (_panel.activeSelf)
        {
            GameMediator.Instance.SetUsingUI(false);
            _panel.SetActive(false);
        }
        else if (!_panel.activeSelf)
        {
            GameMediator.Instance.SetUsingUI(true);
            _panel.SetActive(true);
            RefreshInfo();
        }
    }

    public void Close(){
        GameMediator.Instance.SetUsingUI(false);
        _panel.SetActive(false);
    }

    private void RefreshInfo(){
        foreach (var bar in stageSelectedBars)
        {
            bar.RefreshInfo();
        }
    }

    public void EnterStage(IStageData stageData){
        stageSystem.SwitchStage(stageData);
        Close();
    }
}
