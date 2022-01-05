using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainSceneManager : MonoBehaviour
{
    [SerializeField]
    public GameObject parentObject;
    SpriteRenderer parentSprite;
    [SerializeField]
    Sprite parentAngrySprite;
    [SerializeField]
    GameObject parentAngryPostProcess;
    //�÷��̾� Ʈ������
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    GameObject background;
    [SerializeField]
    Sprite backgroundAfter;
    [SerializeField]
    GameObject black;

    //�ڷ��������� �ٲ��� ����.
    public bool watchingTV;
    public bool isExpressioning;

    //���̾���� �ٲ��� ����.����
    bool isObjectOpen;

    GameObject touchedObject;               //��ġ�� ������Ʈ
    RaycastHit2D hit;                         //��ġ�� ���� raycastHit
    [SerializeField]
    Camera cam;                      //����ĳ��Ʈ�� ���� ī�޶�.

    Vector3 startPosition;
    Vector3 laterPosition;
    bool isMovingRight;
    bool isMoving;
    //���� -0.43 ������ 8
    //float playerMoveTimer = 2;

    // Ȱ���� ���� 2�� �ʱ�ȭ �����ش�
    public int energyPoint = 2;
    public int exprLevel;
    //�ͽ������� �Ŵ������� ����
    public bool isParentAppear = false;
    //�θ���� �����ϴ� Ÿ�̸�. �ʱⰪ 30.
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
        //����
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
        
        parentSprite = parentObject.GetComponent<SpriteRenderer>();
        GameManager.singleTon.mainSceneManager = this;
    }
    public void ParentGetAngry()
    {
        StartCoroutine(SoundManager.singleTon.BgmPitchDownCoroutine());
        parentAngryPostProcess.SetActive(true);
        parentObject.GetComponent<SpriteRenderer>().sprite = parentAngrySprite;
        background.GetComponent<SpriteRenderer>().sprite = backgroundAfter;
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

    public IEnumerator NextSceneCoroutine()
    {
        GameManager.singleTon.saveData.active = 2;
        JsonManager.SaveJson(GameManager.singleTon.saveData);

        yield return new WaitForSeconds(2f);
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
            isParentAppear = true;
            parentObject.SetActive(true);
            parentSprite.color = new Color(1, 1, 1, 0);
            float time = 0;

            while (time <= 2)
            {
                time += Time.deltaTime;
                parentSprite.color = new Color(1, 1, 1, time / 2);
                yield return null;
            }
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
        if (!isBalloonOn && !isParentAppear && GameManager.singleTon.isGameEnd == false && isObjectOpen == false && GameManager.singleTon.isOptionOpen == false)
        {
            //�ϵ��� ��ġ���� ��.
            if (Input.GetMouseButtonDown(0))
            {
                if (isExpressioning || isGameOver)
                {
                    return;
                }
                Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //���콺 ��Ŭ������ ���콺�� ��ġ���� Ray�� ��� ������Ʈ�� ����
                if (hit = Physics2D.Raycast(mousePos, Vector2.zero))
                {
                    //��ġ�� ������Ʈ
                    touchedObject = hit.collider.gameObject; //Ray�� ���� �ݶ��̴��� ��ġ�� ������Ʈ�� ����
                    if (touchedObject.CompareTag("touchable"))
                    {
                        //�Ÿ��� 1 �Ʒ��ϰ��
                        if (Mathf.Abs(touchedObject.transform.position.x - playerTransform.position.x) < 1)
                        {
                            //�̰� ��������Ʈ��.
                            //�̷��� touchableObject�� onTouch�� �ƴ϶�
                            //���̾ ������ Diary.OnTouch�� ����ȴ�. ���� ��������Ʈ.
                            touchedObject.GetComponent<TouchableObject>().OnTouch();
                        }
                        else
                        {
                            CharacterPosition(mousePos.x); //x��ǥ
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

    //Television��ũ��Ʈ���� �ҷ���.
    public void TurnOnTV()
    {
        watchingTV = !watchingTV;
    }

    //����
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
        Debug.Log("����������");
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