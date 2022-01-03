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
                }
            }
        }
    }
    

}