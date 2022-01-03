using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionManager : MonoBehaviour
{
    [SerializeField]
    Camera cam;
    AudioSource cry;

    [SerializeField]
    AudioClip[] cryLevel;

    [SerializeField]
    GameObject parentObject;

    public void ChangeCryingSound()
    {
        int level = GameManager.singleTon.saveData.smartLevel;
        cry.clip = cryLevel[level];
    }

    private void Start()
    {
        cry = GetComponent<AudioSource>();
    }

    //3단계부터 진행, 감정표현시 부모님 나옴, 캐릭터를 꾹누르면 감정표현

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject touchedObject;               //터치한 오브젝트
            RaycastHit2D hit;                         //터치를 위한 raycastHit
            Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition); //마우스 좌클릭으로 마우스의 위치에서 Ray를 쏘아 오브젝트를 감지
            if (hit = Physics2D.Raycast(mousePos, Vector2.zero))
            {
                touchedObject = hit.collider.gameObject;

                //Ray에 맞은 콜라이더를 터치된 오브젝트로 설정
                if (touchedObject.CompareTag("Player"))
                {
                    Debug.Log(touchedObject.name);
                    cry.Play();
                    parentObject.SetActive(true);
                }
            }
        }
    }
    

}