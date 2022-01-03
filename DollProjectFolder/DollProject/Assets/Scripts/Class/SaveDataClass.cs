using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataClass
{
    int active;
    bool tvPower;
    int smartLevel;
    int currentConversationLevel;

    public SaveDataClass() //시작할때 생성자
    {
        active = 2;
        tvPower = false;
        smartLevel = 0;
        currentConversationLevel = 0;
    }

    //감정표현 진행후 하루끝날때 , 활동력변화있을때,  티비 킬때
    
}
