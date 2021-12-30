using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//다이어리랑 티비는 터쳐블오브젝트 상속이다
//왜그런지는 메인씬 메니저에서 설명.
//ㅎㅎ
public class Diary : TouchableObject
{
    [SerializeField]
    GameObject diaryObject;


    public override void OnTouch()
    {
        base.OnTouch();
        //다이어리가 켜져있으면 꺼주고, 꺼져있으면 켜준다.
        diaryObject.SetActive(!diaryObject.activeSelf);
        mainSceneManager.OpenTheDiary();

        if (mainSceneManager.exprLevel == 3)
        {
            if (mainSceneManager.openDiary)
            {
                Debug.Log("적혀있는 일기장 열었다");
            }
            else
            {
                Debug.Log("적혀있는 일기장 닫았다");
                mainSceneManager.energyPoint--; //활동력 -1
                Debug.Log("활동력이 1줄었다");
            }
        }
    }
}