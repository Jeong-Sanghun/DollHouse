using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpressionManager : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    AudioSource cry;

    [SerializeField]
    AudioClip[] cryLevel;

    [SerializeField]
    GameObject parentObject;

    [SerializeField]
    GameObject parentTextBalloon;
    [SerializeField]
    GameObject parentText;
    [SerializeField]
    GameObject childTextBalloon;
    [SerializeField]
    GameObject childText;
    [SerializeField]
    DialogManager dialogManager;
    [SerializeField]
    MainSceneManager mainSceneManager;

    public void ChangeCryingSound()
    {
        int level = GameManager.singleTon.saveData.smartLevel;
        cry.clip = cryLevel[level];
    }

    private void Start()
    {
        cry = GetComponent<AudioSource>();
    }

    IEnumerator ExpressionCor()
    {
        if (mainSceneManager.exprLevel == 3)
        {
            // 메인 스토리 진행
            if (dialogManager.mainStroyNum == 8)
            {
                Debug.Log("게임 오버");
                yield break;
            }
            else
                dialogManager.MainDialog(dialogManager.mainStroyNum);  
        }
        else
        {
            // 랜덤 대화 진행
            dialogManager.RandDialog(mainSceneManager.exprLevel);
        }
        int dialogTextNum = dialogManager.dialogList.childDialogList.Count + dialogManager.dialogList.parentsDialogList.Count;
        int i = 0;
        while (dialogTextNum > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (dialogManager.dialogList.isParentStart) // 엄마 대화 먼저 시작 i가 짝수이면 엄마대화 홀수이면 자식대화
                {
                    if (i % 2 == 0) // 엄마 대화 출력
                    {
                        childTextBalloon.SetActive(false);
                        parentTextBalloon.SetActive(true);
                        parentText.GetComponent<Text>().text = dialogManager.dialogList.parentsDialogList[i / 2];
                    }
                    else // 자식 대화 출력
                    {
                        parentTextBalloon.SetActive(false);
                        childTextBalloon.SetActive(true);
                        childText.GetComponent<Text>().text = dialogManager.dialogList.childDialogList[i / 2];
                    }
                    i++;
                    dialogTextNum--;
                }
                else
                {
                    if (i % 2 == 1) // 엄마 대화 출력
                    {
                        childTextBalloon.SetActive(false);
                        parentTextBalloon.SetActive(true);
                        parentText.GetComponent<Text>().text = dialogManager.dialogList.parentsDialogList[i / 2];

                    }
                    else // 자식 대화 출력
                    {
                        parentTextBalloon.SetActive(false);
                        childTextBalloon.SetActive(true);
                        childText.GetComponent<Text>().text = dialogManager.dialogList.childDialogList[i / 2];
                    }
                    i++;
                    dialogTextNum--;
                }
            }
            yield return null;
        }
        while (true)
        { 
            if (Input.GetMouseButtonDown(0))
            {
                break;
            }
            yield return null;
        }
        parentTextBalloon.SetActive(false);
        childTextBalloon.SetActive(false);
    }


    //3단계부터 진행, 감정표현시 부모님 나옴, 캐릭터를 꾹누르면 감정표현

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject touchedObject;               //터치한 오브젝트
            RaycastHit2D hit;                         //터치를 위한 raycastHit
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //마우스 좌클릭으로 마우스의 위치에서 Ray를 쏘아 오브젝트를 감지
            if (hit = Physics2D.Raycast(mousePos, Vector2.zero))
            {
                touchedObject = hit.collider.gameObject;

                //Ray에 맞은 콜라이더를 터치된 오브젝트로 설정
                if (touchedObject.CompareTag("Player"))
                {
                    Debug.Log(touchedObject.name);

                    cry.Play();
                    parentObject.SetActive(true);

                    StartCoroutine(ExpressionCor());


                }
            }
        }
    }
    

}