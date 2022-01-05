using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LocketObject
{
    Rope, Phone, Key, Die
}

public class Locket : TouchableObject
{
    [SerializeField]
    GameObject locketCanvas;
    [SerializeField]
    InputField locketInputField;
    [SerializeField]
    SpriteRenderer locketObjectSpriteRenderer;
    [SerializeField]
    Television television;

    [SerializeField]
    Sprite[] locketObjectSprite;

    bool isOpened;
    LocketObject openedObject;

    protected override void Start()
    {
        base.Start();
        isOpened = false;
    }

    public override void OnTouch()
    {
        base.OnTouch();
        //���̾�� ���������� ���ְ�, ���������� ���ش�.
        if(isOpened == false)
        {
            locketCanvas.SetActive(true);
        }

    }

    public void LocketActiveFalse()
    {
        Debug.Log("���äŤ�");
        mainSceneManager.energyPoint--;
        mainSceneManager.Equalize();
        locketCanvas.SetActive(false);
    }

    IEnumerator LocketOpenCoroutine()
    {
        locketObjectSpriteRenderer.gameObject.SetActive(true);
        isOpened = true;
        locketCanvas.SetActive(false);
        GameManager.singleTon.isGameEnd = true;

        if (mainSceneManager.watchingTV == true)
        {
            television.OnTouch();
        }
        yield return new WaitForSeconds(2f);
        GameManager.singleTon.GameClear(openedObject);
    }
 

    public void OnValueEnd()
    {
        string locketString = locketInputField.text;

        if (locketString == "������")
        {
            openedObject = LocketObject.Rope;
            locketObjectSpriteRenderer.sprite = locketObjectSprite[(int)LocketObject.Rope];
            StartCoroutine(LocketOpenCoroutine());

        }
        else if (locketString == "������")
        {
            openedObject = LocketObject.Key;
            locketObjectSpriteRenderer.sprite = locketObjectSprite[(int)LocketObject.Key];
            locketObjectSpriteRenderer.gameObject.SetActive(true);
            StartCoroutine(LocketOpenCoroutine());
        }
        else if (locketString == "�������")
        {
            openedObject = LocketObject.Phone;
            locketObjectSpriteRenderer.sprite = locketObjectSprite[(int)LocketObject.Phone];
            locketObjectSpriteRenderer.gameObject.SetActive(true);
            StartCoroutine(LocketOpenCoroutine());
        }
    }
}