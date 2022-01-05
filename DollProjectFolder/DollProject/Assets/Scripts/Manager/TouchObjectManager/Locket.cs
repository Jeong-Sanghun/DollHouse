using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

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
    GameObject openedLocket;
    [SerializeField]
    SpriteRenderer tableRenderer;

    [SerializeField]
    Sprite[] locketObjectSprite;

    bool isOpened;
    bool locketChanged;
    LocketObject openedObject;

    protected override void Start()
    {
        base.Start();
        isOpened = false;
    }

    private void Update()
    {
        locketChanged = false;
    }

    public override void OnTouch()
    {
        base.OnTouch();
        //다이어리가 켜져있으면 꺼주고, 꺼져있으면 켜준다.
        if(isOpened == false)
        {
            locketCanvas.SetActive(true);
        }
        mainSceneManager.ObjectActive();

    }

    public void LocketActiveFalse()
    {
        if (GameManager.singleTon.isOptionOpen == true)
        {
            return;
        }
        mainSceneManager.energyPoint--;
        mainSceneManager.ObjectActive();
        if (mainSceneManager.energyPoint == 0 && mainSceneManager.watchingTV == true)
        {
            Debug.Log("왜안돼");
            television.OnTouch();
        }
        mainSceneManager.Equalize();
        locketCanvas.SetActive(false);
    }

    IEnumerator LocketOpenCoroutine()
    {
        SoundManager.singleTon.OpenLocketPlay();
        tableRenderer.sortingOrder = 1;
        openedLocket.SetActive(true);
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
        if(locketChanged == true)
        {
            return;
        }
        string locketString = locketInputField.text;
        SoundManager.singleTon.LocketKeyboardPlay();
        locketChanged = true;
        if(mainSceneManager.exprLevel != 3)
        {
            StringBuilder builder = new StringBuilder();
            string symbols = "!@#$%&";
            for (int i = 0; i < locketString.Length; i++)
            {
                builder.Append(symbols[Random.Range(0, symbols.Length)]);
            }
            locketString = builder.ToString();
            locketInputField.text = builder.ToString();
        }
        if (locketString == "힘들어요")
        {
            openedObject = LocketObject.Rope;
            locketObjectSpriteRenderer.sprite = locketObjectSprite[(int)LocketObject.Rope];
            StartCoroutine(LocketOpenCoroutine());

        }
        else if (locketString == "살려줘요")
        {
            openedObject = LocketObject.Key;
            locketObjectSpriteRenderer.sprite = locketObjectSprite[(int)LocketObject.Key];
            locketObjectSpriteRenderer.gameObject.SetActive(true);
            StartCoroutine(LocketOpenCoroutine());
        }
        else if (locketString == "도와줘요")
        {
            openedObject = LocketObject.Phone;
            locketObjectSpriteRenderer.sprite = locketObjectSprite[(int)LocketObject.Phone];
            locketObjectSpriteRenderer.gameObject.SetActive(true);
            StartCoroutine(LocketOpenCoroutine());
        }
    }
}