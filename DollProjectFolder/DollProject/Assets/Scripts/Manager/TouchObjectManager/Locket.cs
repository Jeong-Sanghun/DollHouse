using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locket : TouchableObject
{
    [SerializeField]
    GameObject locketCanvas;
    public override void OnTouch()
    {
        base.OnTouch();
        //다이어리가 켜져있으면 꺼주고, 꺼져있으면 켜준다.
        locketCanvas.SetActive(true);
        Debug.Log("ㅁㄴㅇㄹ");
        mainSceneManager.energyPoint--;
    }
}