using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum LocketObject
{
    Rope, Phone, Key
}

public class LocketManager : MonoBehaviour
{

    [SerializeField]
    GameObject locketCanvas;
    [SerializeField]
    InputField locketInputField;
    [SerializeField]
    Image locketObjectImage;

    [SerializeField]
    Sprite[] locketObjectSprite;

    public void OnValueEnd()
    {
        string locketString = locketInputField.text;

        if (locketString.Contains("힘들"))
        {
            locketObjectImage.sprite = locketObjectSprite[(int)LocketObject.Rope];
            locketCanvas.SetActive(false);
        }
        else if (locketString.Contains("살려"))
        {
            locketObjectImage.sprite = locketObjectSprite[(int)LocketObject.Key];
            locketCanvas.SetActive(false);
        }
        else if (locketString.Contains("도와"))
        {
            locketObjectImage.sprite = locketObjectSprite[(int)LocketObject.Phone];
            locketCanvas.SetActive(false);
        }
    }
}
