using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataClass
{
    public int active;
    public bool tvPower;
    public int smartLevel;
    public int currentConversationLevel;

    public SaveDataClass() //�����Ҷ� ������
    {
        active = 2;
        tvPower = false;
        smartLevel = 0;
        currentConversationLevel = 1;
    }

    //����ǥ�� ������ �Ϸ糡���� , Ȱ���º�ȭ������,  Ƽ�� ų��
    
}
