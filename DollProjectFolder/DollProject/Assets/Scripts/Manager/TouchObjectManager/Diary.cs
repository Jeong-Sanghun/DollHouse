using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

//���̾�� Ƽ��� ���ĺ������Ʈ ����̴�
//�ֱ׷����� ���ξ� �޴������� ����.
//����
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
        //���̾�� ���������� ���ְ�, ���������� ���ش�.
        diaryObject.SetActive(true);
        mainSceneManager.OpenTheDiary();

        if (mainSceneManager.exprLevel == 3)
        {
            if (mainSceneManager.openDiary)
            {
                Debug.Log("�����ִ� �ϱ��� ������");
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
        mainSceneManager.energyPoint--; //Ȱ���� -1
        mainSceneManager.Equalize();
    }
}