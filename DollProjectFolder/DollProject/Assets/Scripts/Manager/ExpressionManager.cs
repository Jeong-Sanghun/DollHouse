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
            // ���� ���丮 ����
            if (dialogManager.mainStroyNum == 8)
            {
                Debug.Log("���� ����");
                yield break;
            }
            else
                dialogManager.MainDialog(dialogManager.mainStroyNum);  
        }
        else
        {
            // ���� ��ȭ ����
            dialogManager.RandDialog(mainSceneManager.exprLevel);
        }
        int dialogTextNum = dialogManager.dialogList.childDialogList.Count + dialogManager.dialogList.parentsDialogList.Count;
        int i = 0;
        while (dialogTextNum > 0)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (dialogManager.dialogList.isParentStart) // ���� ��ȭ ���� ���� i�� ¦���̸� ������ȭ Ȧ���̸� �ڽĴ�ȭ
                {
                    if (i % 2 == 0) // ���� ��ȭ ���
                    {
                        childTextBalloon.SetActive(false);
                        parentTextBalloon.SetActive(true);
                        parentText.GetComponent<Text>().text = dialogManager.dialogList.parentsDialogList[i / 2];
                    }
                    else // �ڽ� ��ȭ ���
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
                    if (i % 2 == 1) // ���� ��ȭ ���
                    {
                        childTextBalloon.SetActive(false);
                        parentTextBalloon.SetActive(true);
                        parentText.GetComponent<Text>().text = dialogManager.dialogList.parentsDialogList[i / 2];

                    }
                    else // �ڽ� ��ȭ ���
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


    //3�ܰ���� ����, ����ǥ���� �θ�� ����, ĳ���͸� �ڴ����� ����ǥ��

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject touchedObject;               //��ġ�� ������Ʈ
            RaycastHit2D hit;                         //��ġ�� ���� raycastHit
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //���콺 ��Ŭ������ ���콺�� ��ġ���� Ray�� ��� ������Ʈ�� ����
            if (hit = Physics2D.Raycast(mousePos, Vector2.zero))
            {
                touchedObject = hit.collider.gameObject;

                //Ray�� ���� �ݶ��̴��� ��ġ�� ������Ʈ�� ����
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