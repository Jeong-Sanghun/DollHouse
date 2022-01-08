using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]
    public GameObject parentObject;
    SpriteRenderer parentSprite;
    [SerializeField]
    ModuleManager moduleManager;
    [SerializeField]
    GameObject backGroundAnim;
    [SerializeField]
    PostProcessVolume effectVolume;
    [SerializeField]
    GameObject parentAngryPostProcess;
    //플레이어 트랜스폼
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    GameObject background;
    [SerializeField]
    Sprite originBackGround;
    [SerializeField]
    Sprite backgroundAfter;
    [SerializeField]
    Sprite openedDoorBackground;
    [SerializeField]
    GameObject black;
    [SerializeField]
    Animator playerAnim;


    //텔레비전에서 바꿔줄 변수.
    public bool watchingTV;
    public bool isExpressioning;

    //다이어리에서 바꿔줄 변수.ㅎㅎ
    public bool isObjectOpen;

    GameObject touchedObject;               //터치한 오브젝트
    RaycastHit2D hit;                         //터치를 위한 raycastHit
    [SerializeField]
    Camera cam;                      //레이캐스트를 위한 카메라.

    Vector3 firstPosition;
    Vector3 startPosition;
    Vector3 laterPosition;
    bool isMovingRight;
    bool isMoving;
    //왼쪽 -0.43 오른쪽 8
    //float playerMoveTimer = 2;

    // 활동력 매일 2로 초기화 시켜준다
    public int energyPoint = 2;
    public int exprLevel;
    //익스프레션 매니저에서 참조
    public bool isParentAppear = false;
    //부모님이 등장하는 타이머. 초기값 30.
    //float parentAppearTimer = 30;
    public bool isBalloonOn;

    bool isGameOver;
    [SerializeField]
    GameObject dialogManagerObj;

    bool parentAngryCoroutineRunning;


    void Start()
    {
        parentAngryPostProcess.SetActive(false);
        parentObject.SetActive(false);
        black.SetActive(false);
        watchingTV = false;
        isGameOver = false;
        //ㅎㅎ
        isObjectOpen = false;
        isMoving = false;
        isMovingRight = false;


        isBalloonOn = false;
        parentAngryCoroutineRunning = false;
        //StartCoroutine(ParentAppearCoroutine());
        //DialogManager dialogManager = dialogManagerObj.GetComponent<DialogManager>();
        //dialogManager.RandDialog(1);
        exprLevel = GameManager.singleTon.saveData.smartLevel;
        energyPoint = GameManager.singleTon.saveData.active;
        watchingTV = GameManager.singleTon.saveData.tvPower;
        firstPosition = playerTransform.position;
        parentSprite = parentObject.GetComponent<SpriteRenderer>();
        GameManager.singleTon.mainSceneManager = this;
        GameManager.singleTon.AdRequest();
    }
    public void ParentGetAngry()
    {
        StartCoroutine(SoundManager.singleTon.BgmPitchDownCoroutine());
        
        StartCoroutine(BackGroundChangeCoroutine());
        //GameOver();
    }
    IEnumerator ParentAngryCoroutine()
    {
        if(parentAngryCoroutineRunning == false)
        {
            parentAngryCoroutineRunning = true;
            yield return new WaitForSeconds(2f);
            ParentGetAngry();
            yield return null;
            StartCoroutine(NextSceneCoroutine());
            parentAngryCoroutineRunning = false;
        }
    }

    IEnumerator BackGroundChangeCoroutine()
    {
        parentAngryPostProcess.SetActive(true);
        StartCoroutine(moduleManager.VolumeModule(effectVolume, true, 1));
        backGroundAnim.SetActive(true);
        yield return new WaitForSeconds(5f);
        //for (int i = 0; i < 5; i++)
        //{
        //    background.GetComponent<SpriteRenderer>().sprite = backgroundAfter;
        //    yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
        //    background.GetComponent<SpriteRenderer>().sprite = originBackGround;
        //    yield return new WaitForSeconds(Random.Range(0.1f, 0.2f));
        //}
        
        background.GetComponent<SpriteRenderer>().sprite = backgroundAfter;
        backGroundAnim.SetActive(false);

    }

    public IEnumerator NextSceneCoroutine()
    {
        GameManager.singleTon.saveData.active = 2;
        JsonManager.SaveJson(GameManager.singleTon.saveData);

        yield return new WaitForSeconds(6f);
        black.SetActive(true);
        yield return new WaitForSeconds(2f);
        SoundManager.singleTon.BgmPitchOne();
        SceneManager.LoadScene(1);

    }

    public IEnumerator ParentAppearCoroutine()
    {
        SoundManager.singleTon.ParentFootPlay();
       if(isParentAppear == false)
        {
            background.GetComponent<SpriteRenderer>().sprite = openedDoorBackground;
            isParentAppear = true;
            parentObject.SetActive(true);
            parentSprite.color = new Color(1, 1, 1, 0);
            float time = 0;
            Vector3 playerPos = playerTransform.position;
            playerAnim.SetBool("Walk", true);
            if (playerPos.x > firstPosition.x)
            {
                isMovingRight = false;
            }
            else
            {
                isMovingRight = true;
            }
            Debug.Log(isMovingRight);
            if (isMovingRight == false)
            {
                playerTransform.localEulerAngles = new Vector3(0, 0, 0);
            }
            if (isMovingRight == true)
            {
                playerTransform.localEulerAngles = new Vector3(0, 180, 0);
            }

            while (time <= 2)
            {
                time += Time.deltaTime;
                            if (isMovingRight == false)
            {
                playerTransform.localEulerAngles = new Vector3(0, 0, 0);
            }
            if (isMovingRight == true)
            {
                playerTransform.localEulerAngles = new Vector3(0, 180, 0);
            }
                parentSprite.color = new Color(1, 1, 1, time / 2);
                playerTransform.position = Vector3.Lerp(playerPos, firstPosition, time/2);
                yield return null;
            }
            background.GetComponent<SpriteRenderer>().sprite = originBackGround;
            playerTransform.localEulerAngles = new Vector3(0, 0, 0);
        }

    }

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
        playerAnim.SetBool("Walk", true);
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
        SoundManager.singleTon.WalkingPlay();
        float speed = 5 * Time.deltaTime;
        Vector3 position;
        position = playerTransform.position;
        if (isMovingRight)
        {
            if (playerTransform.position.x > laterPosition.x)
            {
                playerAnim.SetBool("Walk", false);
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
                playerAnim.SetBool("Walk", false);
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
        if (!isBalloonOn && !isParentAppear && GameManager.singleTon.isGameEnd == false && isObjectOpen == false && GameManager.singleTon.isOptionOpen == false)
        {
            //암데나 터치했을 때.
            if (Input.GetMouseButtonDown(0))
            {
                if (isExpressioning || isGameOver)
                {
                    return;
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
            }
            CharacterMove();
        }
    }

    //Television스크립트에서 불러옴.
    public void TurnOnTV()
    {
        watchingTV = !watchingTV;
    }

    //ㅎㅎ
    public void ObjectActive()
    {
        isObjectOpen = !isObjectOpen;
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
        GameManager.singleTon.saveData.tvPower = watchingTV;
        JsonManager.SaveJson(GameManager.singleTon.saveData);
        if (energyPoint <= 0)
        {
            StartCoroutine(ParentAppearCoroutine());
            StartCoroutine(ParentAngryCoroutine());
        }
    }
}