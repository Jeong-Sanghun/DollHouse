using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���̾�� Ƽ��� ���ĺ������Ʈ ����̴�
//�ֱ׷����� ���ξ� �޴������� ����.
public class Diary : TouchableObject
{
    [SerializeField]
    GameObject diaryObject;

    public override void OnTouch()
    {
        base.OnTouch();
        //���̾�� ���������� ���ְ�, ���������� ���ش�.
        diaryObject.SetActive(!diaryObject.activeSelf);
        mainSceneManager.energyPoint--;
    }
}
