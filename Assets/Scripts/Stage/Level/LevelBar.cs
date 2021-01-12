using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelBar : MonoBehaviour
{
    [SerializeField] private Text lab_title;
    [SerializeField] private Image img_bar;
    [SerializeField] private GameObject img_mask;

    private string title;
    // Start is called before the first frame update
    
    public void SetInfo(string title, Color barColor){
        this.title = title;
        lab_title.text = title;
        img_bar.color = barColor;
    }

    public void PlayShowAnime(){
        OnStart();
    }

    public void OnStart(){
        lab_title.text = title;
        img_mask.SetActive(false);
        transform.localScale = Vector3.one;
    }

    public void Hide(){
        img_mask.SetActive(true);
        lab_title.text = "???";
    }

    public void HightLight(){
        transform.localScale = transform.localScale * 1.2f;
    }
}
