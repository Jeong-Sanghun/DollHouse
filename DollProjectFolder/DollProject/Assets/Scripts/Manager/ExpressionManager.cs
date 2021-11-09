using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpressionManager : MonoBehaviour
{
    [SerializeField]
    MainSceneManager mainSceneManager;

    //버튼 두개들어가있는 패런트
    //플레이어 스프라이트.
    [SerializeField]
    SpriteRenderer playerSpriteRenderer;
    //추후에 애니메이션으로 바뀔 예정인데 코루틴으로 해도 되고, 맘대로하시면 됩니다.
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
