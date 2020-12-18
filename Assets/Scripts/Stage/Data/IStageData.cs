using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class IStageData : ScriptableObject{

    public string stageName;
    public string questionName;
    public float totalTime;
    public ILevelData [] levelDatas;



}