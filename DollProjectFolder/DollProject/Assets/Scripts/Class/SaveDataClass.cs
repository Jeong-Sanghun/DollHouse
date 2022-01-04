using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataClass
{
    public int active;
    public bool tvPower;
    public int smartLevel;
    public int currentConversationLevel;

    public SaveDataClass() //시작할때 생성자
    {
        active = 2;
        tvPower = false;
        smartLevel = 0;
        currentConversationLevel = 1;
    }

    //감정표현 진행후 하루끝날때 , 활동력변화있을때,  티비 킬때
    
}
