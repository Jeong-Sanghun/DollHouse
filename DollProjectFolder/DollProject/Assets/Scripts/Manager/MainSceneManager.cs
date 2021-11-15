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
    bool watchingTV;
    bool isExpressioning;

    GameObject touchedObject;               //��ġ�� ������Ʈ
    RaycastHit2D hit;                         //��ġ�� ���� raycastHit
    [SerializeField]
    Camera cam;                      //����ĳ��Ʈ�� ���� ī�޶�.

    Vector3 playerOriginPos;
    Vector3 playerMovePos;
    //���� -0.43 ������ 8
    float playerMoveTimer = 2;

    // Ȱ���� ���� 2�� �ʱ�ȭ �����ش�
    public int energyPoint = 2;
    bool isParentAppear = false;
    //�θ���� �����ϴ� Ÿ�̸�. �ʱⰪ 30.
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
        //�ϵ��� ��ġ���� ��.
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
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //���콺 ��Ŭ������ ���콺�� ��ġ���� Ray�� ��� ������Ʈ�� ����
            if (hit = Physics2D.Raycast(mousePos, Vector2.zero))
            {
                //��ġ�� ������Ʈ.
                touchedObject = hit.collider.gameObject; //Ray�� ���� �ݶ��̴��� ��ġ�� ������Ʈ�� ����
                if (touchedObject.CompareTag("touchable"))
                {
                    //�Ÿ��� 1 �Ʒ��ϰ��
                    if(Mathf.Abs(touchedObject.transform.position.x - playerTransform.position.x) < 1)
                    {
                        //�̰� ��������Ʈ��.
                        //�̷��� touchableObject�� onTouch�� �ƴ϶�
                        //���̾ ������ Diary.OnTouch�� ����ȴ�. ���� ��������Ʈ.
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

        //�̵�
        if(playerMoveTimer < 1)
        {
            playerMoveTimer += Time.deltaTime;
            playerTransform.position = Vector3.Lerp(playerOriginPos, playerMovePos, playerMoveTimer);
        }



    }


    //Television��ũ��Ʈ���� �ҷ���.
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
