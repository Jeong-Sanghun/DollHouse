using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]
    GameObject parentObject;
    [SerializeField]
    Sprite parentAngrySprite;
    [SerializeField]
    GameObject parentAngryPostProcess;
    //플레이어 트랜스폼
    [SerializeField]
    Transform playerTransform;

    //텔레비전에서 바꿔줄 변수.
    bool watchingTV;
    bool isExpressioning;

    GameObject touchedObject;               //터치한 오브젝트
    RaycastHit2D hit;                         //터치를 위한 raycastHit
    [SerializeField]
    Camera cam;                      //레이캐스트를 위한 카메라.

    Vector3 playerOriginPos;
    Vector3 playerMovePos;
    //왼쪽 -0.43 오른쪽 8
    float playerMoveTimer = 2;

    // 활동력 매일 2로 초기화 시켜준다
    public int energyPoint = 2;
    bool isParentAppear = false;
    //부모님이 등장하는 타이머. 초기값 30.
    //float parentAppearTimer = 30;

    bool isGameOver;
    void Start()
    {
        parentObject.SetActive(false);
        watchingTV = false;
        isGameOver = false;
        //StartCoroutine(ParentAppearCoroutine());
    }



    void ParentGetAngry()
    {
        parentAngryPostProcess.SetActive(true);
        parentObject.GetComponent<SpriteRenderer>().sprite = parentAngrySprite;
        GameOver();
    }

    bool ParentChecker()
    {
        if(watchingTV || playerMoveTimer < 1 || isExpressioning)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //IEnumerator ParentAppearCoroutine()
    //{
    //    parentObject.SetActive(false);
    //    while (parentAppearTimer > 0)
    //    {
    //        parentAppearTimer -= Time.deltaTime;
    //        yield return null;
    //    }
    //    if (!isGameOver)
    //    {
    //        parentObject.SetActive(true);
    //        if (ParentChecker())
    //        {
    //            ParentGetAngry();
    //        }
    //    }

    //}
    void TouchMoveSetting(float mousePosX)
    {
        if (mousePosX > 8)
        {
            mousePosX = 8;
        }
        else if (mousePosX < -0.43f)
        {
            mousePosX = 0.43f;
        }
        playerMoveTimer = 0;
        playerOriginPos = playerTransform.position;
        playerMovePos = new Vector3(mousePosX, playerOriginPos.y, playerOriginPos.z);
        if (playerTransform.localEulerAngles.y == 0 && playerMovePos.x > playerOriginPos.x)
        {
            playerTransform.localEulerAngles = new Vector3(0, 180, 0);
        }
        else if (playerTransform.localEulerAngles.y == 180 && playerMovePos.x < playerOriginPos.x)
        {
            playerTransform.localEulerAngles = Vector3.zero;
        }
    }
    


    void Update()
    {
        //암데나 터치했을 때.
        if (Input.GetMouseButtonDown(0))
        {
            if(isExpressioning || isGameOver)
            {
                return;
            }
            if (isParentAppear)
            {
                ParentGetAngry();
            }
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //마우스 좌클릭으로 마우스의 위치에서 Ray를 쏘아 오브젝트를 감지
            if (hit = Physics2D.Raycast(mousePos, Vector2.zero))
            {
                //터치된 오브젝트.
                touchedObject = hit.collider.gameObject; //Ray에 맞은 콜라이더를 터치된 오브젝트로 설정
                if (touchedObject.CompareTag("touchable"))
                {
                    //거리가 1 아래일경우
                    if(Mathf.Abs(touchedObject.transform.position.x - playerTransform.position.x) < 1)
                    {
                        //이게 섹시포인트다.
                        //이러면 touchableObject의 onTouch가 아니라
                        //다이어리 누를땐 Diary.OnTouch가 실행된다. 완전 섹시포인트.
                        touchedObject.GetComponent<TouchableObject>().OnTouch();
                    }
                    else
                    {
                        TouchMoveSetting(mousePos.x);
                    }
                }
            }
            else
            {
                TouchMoveSetting(mousePos.x);
            }

            if (energyPoint <= 0)
            {
                parentObject.SetActive(true);
            }
        }

        //이동
        if(playerMoveTimer < 1)
        {
            playerMoveTimer += Time.deltaTime;
            playerTransform.position = Vector3.Lerp(playerOriginPos, playerMovePos, playerMoveTimer);
        }



    }


    //Television스크립트에서 불러옴.
    public void TurnOnTV()
    {
        watchingTV = !watchingTV;
    }
    public void GameOver()
    {
        isGameOver = true;
    }

    public void ExpressionToggle()
    {
        isExpressioning = !isExpressioning;
    }
}
