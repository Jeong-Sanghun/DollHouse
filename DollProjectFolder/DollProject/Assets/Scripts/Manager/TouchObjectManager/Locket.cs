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
        //���̾�� ���������� ���ְ�, ���������� ���ش�.
        locketCanvas.SetActive(true);
        Debug.Log("��������");
        mainSceneManager.energyPoint--;
    }
}