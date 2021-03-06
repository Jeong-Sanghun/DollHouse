using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExpressionManager : MonoBehaviour
{
    [SerializeField]
    Camera cam;

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
    [SerializeField]
    Television television;
    [SerializeField]
    Animator playerAnim;
    bool isTouched;

    private void Start()
    {
        isTouched = false;
    }

    IEnumerator ExpressionCor()
    {
        yield return new WaitForSeconds(2f);
        if (mainSceneManager.exprLevel == 3)
        {
            // 메인 스토리 진행
            if (dialogManager.mainStroyNum == 8)
            {
                Debug.Log("Game Over");

                GameManager.singleTon.saveData.active = 2;
                GameManager.singleTon.saveData.currentConversationLevel = 1;
                GameManager.singleTon.saveData.smartLevel = 0;
                GameManager.singleTon.saveData.tvPower = false;
                JsonManager.SaveJson(GameManager.singleTon.saveData);

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
            if (Input.GetMouseButtonDown(0) && GameManager.singleTon.isOptionOpen == false)
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
        mainSceneManager.ParentGetAngry();
        mainSceneManager.isBalloonOn = false;
        StartCoroutine(mainSceneManager.NextSceneCoroutine());
    }


    //3단계부터 진행, 감정표현시 부모님 나옴, 캐릭터를 꾹누르면 감정표현

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && mainSceneManager.isParentAppear ==false 
            && GameManager.singleTon.isGameEnd == false
            && GameManager.singleTon.isOptionOpen == false
            && mainSceneManager.isObjectOpen == false)
        {
            GameObject touchedObject;               //터치한 오브젝트
            RaycastHit2D hit;                         //터치를 위한 raycastHit
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //마우스 좌클릭으로 마우스의 위치에서 Ray를 쏘아 오브젝트를 감지
            if (hit = Physics2D.Raycast(mousePos, Vector2.zero))
            {
                touchedObject = hit.collider.gameObject;

                //Ray에 맞은 콜라이더를 터치된 오브젝트로 설정
                if (touchedObject.CompareTag("Player") && isTouched == false)
                {
                    isTouched = true;

                    SoundManager.singleTon.CryPlay(mainSceneManager.exprLevel);
                    playerAnim.SetBool("Cry", true);
                    if (dialogManager.mainStroyNum <= 7)
                    {
                        mainSceneManager.isBalloonOn = true;
                        StartCoroutine(mainSceneManager.ParentAppearCoroutine());
                        StartCoroutine(ExpressionCor());
                    }
                    else
                    {
                        mainSceneManager.isBalloonOn = true;
                        StartCoroutine(GameEndCoroutine());
                    }
                    if(mainSceneManager.watchingTV == true)
                    {
                        television.OnTouch();
                    }
                }
            }
        }
    }
    
    IEnumerator GameEndCoroutine()
    {
        yield return new WaitForSeconds(3f);
        GameManager.singleTon.GameClear(LocketObject.Die);
    }

}