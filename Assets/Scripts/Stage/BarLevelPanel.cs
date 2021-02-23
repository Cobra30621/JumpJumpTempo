using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarLevelPanel : MonoBehaviour
{
    [SerializeField] private List<BarLevelCeil> barLevelCeils;
    [SerializeField] private IStageData stageData;
    private Color[] colors;

    [SerializeField] GameObject barLevelCeilPrefab;
    [SerializeField] Transform bar_pos;

    public void Init(IStageData data){
        stageData = data;
        CreateAllBarLevelCeil();
    }

    public void CloseLevelBar(){
        foreach (var bar in barLevelCeils)
        {
            bar.SetLight(false);
            bar.SetMask(true);
        }
    }

    // 設置LevelBar亮起與否
    public void SetLevelBar(int id){
        if(id >= colors.Length )
            id = 0;
        else
            id = colors.Length - id - 1;
        
        foreach (var bar in barLevelCeils)
        {
            bar.SetLight(false);
            bar.SetMask(true);
        }

        for (int i = id ; i < colors.Length ; i++)
        {
            barLevelCeils[i].SetMask(false);
        }

        // barLevelCeils[id].SetLight(true);
    }

    /// <summary>
    /// 製作所有的LeverBar
    /// </summary>
    public void CreateAllBarLevelCeil(){
        colors = stageData.Colors;
        barLevelCeils = new List<BarLevelCeil>();

        for (int i = colors.Length -1 ; i >= 0; i --)
        {
            CreateLeverBar(colors[i]);
        }
    }

    /// <summary>
    /// 製作一個LeverBar
    /// </summary>
    public void CreateLeverBar(Color barColor)
    {
        var g = Instantiate(barLevelCeilPrefab, bar_pos);

        var bar = g.GetComponent<BarLevelCeil>();
        bar.Init(barColor); 
        bar.SetMask(true);
        bar.SetLight(false);

        barLevelCeils.Add( bar);
    }
}
