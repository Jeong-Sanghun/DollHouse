using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

//다이어리랑 티비는 터쳐블오브젝트 상속이다
//왜그런지는 메인씬 메니저에서 설명.
//ㅎㅎ
public class Diary : TouchableObject
{
    [SerializeField]
    GameObject diaryObject;
    [SerializeField]
    ModuleManager moduleManager;
    [SerializeField]
    Text diaryTextComponent;

    [SerializeField]
    TextAsset diaryOriginText;
    string[] diaryTextList;
    string wholeDiaryString;
    bool isOpenedInThisScene;
    protected override void Start()
    {
        isOpenedInThisScene = false;
        diaryTextList = diaryOriginText.ToString().Split('\n');
        int rand = Random.Range(5, 10);
        StringBuilder builder = new StringBuilder();
        for(int i = 0; i < rand; i++)
        {
            builder.Append(diaryTextList[Random.Range(0, diaryTextList.Length)]);
        }
        wholeDiaryString = builder.ToString();
    }

    public override void OnTouch()
    {
        base.OnTouch();
        //다이어리가 켜져있으면 꺼주고, 꺼져있으면 켜준다.
        diaryObject.SetActive(true);
        mainSceneManager.OpenTheDiary();

        if (mainSceneManager.exprLevel == 3)
        {
            if (mainSceneManager.openDiary)
            {
                Debug.Log("적혀있는 일기장 열었다");
                diaryObject.SetActive(true);
                if (isOpenedInThisScene==false)
                {
                    SoundManager.singleTon.PencilSoundPlay();
                    StartCoroutine(moduleManager.LoadTextOneByOne(wholeDiaryString,
                        diaryTextComponent, 0.05f, false));
                }
                
            }
        }

        isOpenedInThisScene = true;
    }

    public void DiaryExit()
    {
        diaryObject.SetActive(false);
        mainSceneManager.OpenTheDiary();
        mainSceneManager.energyPoint--; //활동력 -1
        mainSceneManager.Equalize();
    }
}