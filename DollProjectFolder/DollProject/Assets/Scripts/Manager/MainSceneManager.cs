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
    public bool watchingTV;
    public bool isExpressioning;

    //다이어리에서 바꿔줄 변수.ㅎㅎ
    public bool openDiary;
    public bool isOpenDiary;

    public bool openEmptyDiary;
    public bool isOpenEmptyDiary;

    GameObject touchedObject;               //터치한 오브젝트
    RaycastHit2D hit;                         //터치를 위한 raycastHit
    [SerializeField]
    Camera cam;                      //레이캐스트를 위한 카메라.

    Vector3 startPosition;
    Vector3 laterPosition;
    bool isMovingRight;
    bool isMoving;
    //왼쪽 -0.43 오른쪽 8
    float playerMoveTimer = 2;

    // 활동력 매일 2로 초기화 시켜준다
    public int energyPoint = 2;
    public int exprLevel = 0;
    bool isParentAppear = false;
    //부모님이 등장하는 타이머. 초기값 30.
    //float parentAppearTimer = 30;

    bool isGameOver;

    [SerializeField]
    GameObject dialogManagerObj;


    void Start()
    {
        parentObject.SetActive(false);
        watchingTV = false;
        isGameOver = false;
        //ㅎㅎ
        openDiary = false;
        isOpenDiary = false;
        isMoving = false;
        isMovingRight = false;

        openEmptyDiary = false;
        isOpenEmptyDiary = false;
        //StartCoroutine(ParentAppearCoroutine());

        //DialogManager dialogManager = dialogManagerObj.GetComponent<DialogManager>();
        //dialogManager.RandDialog(1);
    }

    void ParentGetAngry()
    {
        parentAngryPostProcess.SetActive(true);
        parentObject.GetComponent<SpriteRenderer>().sprite = parentAngrySprite;
        GameOver();
    }

    bool ParentChecker()
    {
        if (watchingTV || playerMoveTimer < 1 || isExpressioning)
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
    void CharacterPosition(float x)
    {
        startPosition = playerTransform.position;
        laterPosition = new Vector3(x, startPosition.y, startPosition.z);
        isMoving = true;
        if (startPosition.x > laterPosition.x)
        {
            isMovingRight = false;
        }
        else
        {
            isMovingRight = true;
        }

        if (playerTransform.localEulerAngles.y == 180 && isMovingRight == false)
        {
            playerTransform.localEulerAngles = new Vector3(0, 0, 0);
        }
        if (playerTransform.localEulerAngles.y == 0 && isMovingRight == true)
        {
            playerTransform.localEulerAngles = new Vector3(0, 180, 0);
        }


    }

    void CharacterMove()
    {
        if (isMoving == false)
        {
            return;
        }

        float speed = 5 * Time.deltaTime;
        Vector3 position;
        position = playerTransform.position;
        if (isMovingRight)
        {
            if (playerTransform.position.x > laterPosition.x)
            {
                isMoving = false;
                playerTransform.position = laterPosition;
            }
            else
            {
                playerTransform.transform.position = new Vector3(position.x + speed, position.y, position.z);
            }
        }
        else
        {
            if (playerTransform.position.x < laterPosition.x)
            {
                isMoving = false;
                playerTransform.position = laterPosition;
            }
            else
            {
                playerTransform.transform.position = new Vector3(position.x - speed, position.y, position.z);
            }
        }
    }

    void Update()
    {
        //암데나 터치했을 때.
        if (Input.GetMouseButtonDown(0))
        {
            if (isExpressioning || isGameOver)
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
                //터치된 오브젝트
                touchedObject = hit.collider.gameObject; //Ray에 맞은 콜라이더를 터치된 오브젝트로 설정
                if (touchedObject.CompareTag("touchable"))
                {
                    //거리가 1 아래일경우
                    if (Mathf.Abs(touchedObject.transform.position.x - playerTransform.position.x) < 1)
                    {
                        //이게 섹시포인트다.
                        //이러면 touchableObject의 onTouch가 아니라
                        //다이어리 누를땐 Diary.OnTouch가 실행된다. 완전 섹시포인트.
                        touchedObject.GetComponent<TouchableObject>().OnTouch();
                    }
                    else
                    {
                        CharacterPosition(mousePos.x); //x좌표
                    }
                }
            }
            else
            {
                CharacterPosition(mousePos.x);
            }

            if (energyPoint <= 0)
            {
                parentObject.SetActive(true);
            }
        }

        CharacterMove();



    }

    //Television스크립트에서 불러옴.
    public void TurnOnTV()
    {
        watchingTV = !watchingTV;
    }

    //ㅎㅎ
    public void OpenTheDiary()
    {
        openDiary = !openDiary;
    }

    public void OpenTheEmptyDiary()
    {
        openEmptyDiary = !openEmptyDiary;
    }

    public void GameOver()
    {
        isGameOver = true;
    }

    public void ExpressionToggle()
    {
        isExpressioning = !isExpressioning;
    }

    public void Equalize()
    {
        GameManager.singleTon.saveData.active = energyPoint;
        GameManager.singleTon.saveData.smartLevel = exprLevel;
    }


}