using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchableObject : MonoBehaviour
{

    //���ξ� �޴����� ��Ҹ� �ٲ��ٰŴϱ� �޾ƿ������.
    [SerializeField]
    protected MainSceneManager mainSceneManager;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //��ġ �� �������� �Ͼ���� �Լ�.
    public virtual void OnTouch()
    {
        //���볻���� ������ ����� ���ش�. ���尡 �� �ϰ� Ƥ�ٴ��� �ϴ°�.
        mainSceneManager.energyPoint--;
    }
}
