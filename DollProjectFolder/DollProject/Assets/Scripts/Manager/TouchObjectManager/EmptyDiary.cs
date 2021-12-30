using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���̾�� Ƽ��� ���ĺ������Ʈ ����̴�
//�ֱ׷����� ���ξ� �޴������� ����.
//����
public class EmptyDiary : TouchableObject
{
    [SerializeField]
    GameObject emptyDiaryObject;

    public override void OnTouch()
    {
        base.OnTouch();
        //���̾�� ���������� ���ְ�, ���������� ���ش�.
        emptyDiaryObject.SetActive(!emptyDiaryObject.activeSelf);
        mainSceneManager.OpenTheEmptyDiary();

        if (mainSceneManager.exprLevel < 3)
        {
            if (mainSceneManager.openEmptyDiary)
            {
                Debug.Log("����ִ� �ϱ��� ������");
            }
            else
            {
                Debug.Log("����ִ� �ϱ��� �ݾҴ�");
            }
        }
    }
}