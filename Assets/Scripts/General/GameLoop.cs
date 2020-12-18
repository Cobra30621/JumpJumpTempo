using UnityEngine;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameLoop : MonoBehaviour
{
    public StageData stageData;
    static GameLoop instance;
    void Awake () {
        if (instance==null)
        {
            instance = this;
        }
        else if (this!=instance)
        {
            Destroy(gameObject);
        }      
		GameMediator.Instance.Initinal();
        
        GameMediator.Instance.SetStage(stageData);
        GameMediator.Instance.StartGame();
	}

    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    void Update()
    {
        GameMediator.Instance.Update();
    }


    



}
