using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBarPanel : MonoBehaviour
{
    [SerializeField] GameObject levelBarPrefab;
    [SerializeField] Transform bar_pos;
    [SerializeField] GameObject panel;
    
    private List<LevelBar> levelBars;
    private StageSystem stageSystem;
    public Color[] colors;
    public string[] upgradeTexts;


    public void PlayLevelAnime(int level){ // 0 ~ length - 1
        if(level < 0)
            return;

        RemoveAllLevelBar();
        CreateAllLevelBar();
        
        panel.SetActive(true);
        HideAllLevelBar();
        
        for (int i = 0; i <= level ; i++)
        {
            levelBars[i].gameObject.SetActive(true);
            levelBars[i].PlayShowAnime();

            // // 玩家到達的地方變大
            // if(i == level)
            //     levelBars[i].HightLight();
        }
    }

    public void Hide(){
        panel.SetActive(false);
    }


    public void HideAllLevelBar(){
        foreach (LevelBar item in levelBars)
        {
            // item.gameObject.SetActive(false);
            item.Hide();
        }
    }

    
    /// <summary>
    /// 製作所有的LeverBar
    /// </summary>
    public void CreateAllLevelBar(){
        stageSystem = GameMediator.Instance.GetStageSystem();
        colors = stageSystem._nowStage.Colors;
        upgradeTexts = stageSystem._nowStage.upgradeTexts;
        
        levelBars = new List<LevelBar>();

        for (int i = 0; i < colors.Length; i++)
        {
            CreateLeverBar(upgradeTexts[i], colors[i]);
        }
    }

    /// <summary>
    /// 製作一個LeverBar
    /// </summary>
    public void CreateLeverBar(string title, Color barColor)
    {
        var g = Instantiate(levelBarPrefab, bar_pos);

        var l = g.GetComponent<LevelBar>();
        l.SetInfo(title, barColor); 
        levelBars.Add( l);
    }

    private void RemoveAllLevelBar(){
        if(levelBars == null)
            return;
        
        foreach(LevelBar bar in levelBars){
            if(bar != null)
                Destroy(bar.gameObject);
        }
    }
}
