using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionManager : MonoBehaviour
{
    [SerializeField]
    MainSceneManager mainSceneManager;

    //��ư �ΰ����ִ� �з�Ʈ
    //�÷��̾� ��������Ʈ.
    [SerializeField]
    SpriteRenderer playerSpriteRenderer;
    //���Ŀ� �ִϸ��̼����� �ٲ� �����ε� �ڷ�ƾ���� �ص� �ǰ�, ������Ͻø� �˴ϴ�.
    [SerializeField]
    Sprite playerOriginalSprite;
    [SerializeField]
    Sprite[] cryingSprite;
    

    int nowCryLevel;
    int nowCriedNumber;
    const int cryLevelBound = 3;
    const int endCryLevel = 4;
    
    void Start()
    {
        nowCryLevel = 0;
        nowCriedNumber = 0;
    }

    public void OnCryButton()
    {
        nowCriedNumber++;
        if(nowCriedNumber > cryLevelBound)
        {
            nowCriedNumber = 0;
            nowCryLevel++;
        }
        StartCoroutine(CryCoroutine());
    }
    public void OnButtonParentActive(bool active)
    {

    }

    

    IEnumerator CryCoroutine()
    {
        mainSceneManager.ExpressionToggle();
        playerSpriteRenderer.sprite = cryingSprite[nowCryLevel];
        yield return new WaitForSeconds(1f);
        playerSpriteRenderer.sprite = playerOriginalSprite;
        if(nowCryLevel > endCryLevel)
        {
            mainSceneManager.GameOver();
        }
        else
        {
            mainSceneManager.ExpressionToggle();
        }
        
    }
}
