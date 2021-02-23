using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarLevelCeil : MonoBehaviour
{
    [SerializeField] private Image img_bar;
    [SerializeField] private GameObject GO_mask;
    [SerializeField] private GameObject GO_light;


    public void Init(Color color){
        img_bar.color = color;

    }

    public void SetMask(bool bo){
        GO_mask.SetActive(bo);
    }

    public void SetLight(bool bo){
        GO_light.SetActive(bo);
    }


}
