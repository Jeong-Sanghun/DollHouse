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
    //�÷��̾� Ʈ������
    [SerializeField]
    Transform playerTransform;

    //�ڷ��������� �ٲ��� ����.
    public bool watchingTV;
    public bool isExpressioning;

    //���̾���� �ٲ��� ����.����
    public bool openDiary;
    public bool isOpenDiary;

    public bool openEmptyDiary;
    public bool isOpenEmptyDiary;

    GameObject touchedObject;               //��ġ�� ������Ʈ
    RaycastHit2D hit;                         //��ġ�� ���� raycastHit
    [SerializeField]
    Camera cam;                      //����ĳ��Ʈ�� ���� ī�޶�.

    Vector3 startPosition;
    Vector3 laterPosition;
    bool isMovingRight;
    bool isMoving;
    //���� -0.43 ������ 8
    float playerMoveTimer = 2;

    // Ȱ���� ���� 2�� �ʱ�ȭ �����ش�
    public int energyPoint = 2;
    public int exprLevel = 0;
    bool isParentAppear = false;
    //�θ���� �����ϴ� Ÿ�̸�. �ʱⰪ 30.
    //float parentAppearTimer = 30;

    bool isGameOver;

    [SerializeField]
    GameObject dialogManagerObj;


    void Start()
    {
        parentObject.SetActive(false);
        watchingTV = false;
        isGameOver = false;
        //����
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
        //�ϵ��� ��ġ���� ��.
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

            if (energyPoint <= 0)
            {
                parentObject.SetActive(true);
            }
        }

        CharacterMove();



    }

    //Television��ũ��Ʈ���� �ҷ���.
    public void TurnOnTV()
    {
        watchingTV = !watchingTV;
    }

    //����
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