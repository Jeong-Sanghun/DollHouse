using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���̾�� Ƽ��� ���ĺ������Ʈ ����̴�
//�ֱ׷����� ���ξ� �޴������� ����.
//����
public class Diary : TouchableObject
{
    [SerializeField]
    GameObject diaryObject;


    public override void OnTouch()
    {
        base.OnTouch();
        //���̾�� ���������� ���ְ�, ���������� ���ش�.
        diaryObject.SetActive(!diaryObject.activeSelf);
        mainSceneManager.OpenTheDiary();

        if (mainSceneManager.exprLevel == 3)
        {
            if (mainSceneManager.openDiary)
            {
                Debug.Log("�����ִ� �ϱ��� ������");
            }
            else
            {
                Debug.Log("�����ִ� �ϱ��� �ݾҴ�");
                mainSceneManager.energyPoint--; //Ȱ���� -1
                Debug.Log("Ȱ������ 1�پ���");
            }
        }
    }
}