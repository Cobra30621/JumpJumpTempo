using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;


    public void Open(){
        if (panel.activeSelf)
        {
            GameMediator.Instance.SetUsingUI(false);
            panel.SetActive(false);
        }
        else if (!panel.activeSelf)
        {
            GameMediator.Instance.SetUsingUI(true);
            panel.SetActive(true);
        }
        Pause();
    }

    public void Close(){
        GameMediator.Instance.SetUsingUI(false);
        panel.SetActive(false);
        EndPause();
    }

    // ButtonFunction

    // 暫停遊戲
    public void Pause(){
        GameMediator.Instance.Pause();
    }

    // 停止暫停遊戲
    public void EndPause(){
        GameMediator.Instance.EndPause();
    }


}
