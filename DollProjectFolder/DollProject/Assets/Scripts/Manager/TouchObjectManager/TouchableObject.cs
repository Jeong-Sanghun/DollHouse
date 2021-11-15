using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchableObject : MonoBehaviour
{

    //메인씬 메니저의 요소를 바꿔줄거니까 받아와줘야해.
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

    //터치 시 무슨일이 일어나는지 함수.
    public virtual void OnTouch()
    {
        //공통내용이 있으면 여기다 써준다. 사운드가 통 하고 튄다던지 하는거.
        mainSceneManager.energyPoint--;
    }
}
