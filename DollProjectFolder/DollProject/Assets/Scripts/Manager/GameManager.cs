using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager singleTon;
    public SaveDataClass saveData;
    public MainSceneManager mainSceneManager;
    public DialogManager dialogManager;
    public ModuleManager moduleManager;
    [SerializeField]
    GameObject gameOverCanvas;
    [SerializeField]
    Image fadeImage;
    [SerializeField]
    Text gameOverText;

    public bool isGameEnd;
    // Start is called before the first frame update
    void Awake()
    {
        if (singleTon == null)
        {
            singleTon = this;
            DontDestroyOnLoad(gameObject);//이거만 존재하게 만듬
        }
        else
        {
            Destroy(gameObject);//두번째로 들어어오는거 삭제
        }

    }

    private void Start()
    {
        saveData = JsonManager.LoadSaveData();
        isGameEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void Save()
    {

        saveData.active = mainSceneManager.energyPoint;
        saveData.tvPower = mainSceneManager.watchingTV;
        saveData.smartLevel = mainSceneManager.exprLevel;
        saveData.currentConversationLevel = dialogManager.mainStroyNum;

        JsonManager.SaveJson(saveData);
    }
    void Load()
    {
        SaveDataClass loadData = JsonManager.LoadSaveData();
        mainSceneManager.energyPoint = loadData.active;
        mainSceneManager.watchingTV = loadData.tvPower;
        mainSceneManager.exprLevel = loadData.smartLevel;
        dialogManager.mainStroyNum = loadData.currentConversationLevel;
    }

    public void GameClear(LocketObject locketObj)
    {
        string clearString = null;
        switch (locketObj)
        {
            case LocketObject.Key:
                clearString = "됐다. 나간다. 나는 이제 나간다. 나는 나간다. 잘 있어라. 고생했다. 나는 나간다.";
                break;
            case LocketObject.Phone:
                clearString = "전화다. 이제 산거야. 이제 살았어. 이제 날 도와주러 올거야. 희망은 있다.";
                break;
            case LocketObject.Rope:
                clearString = "끝났어...끝난거야..이제 힘들지 않아도 돼...다 끝났어.. 잘있어라....";
                break;
            case LocketObject.Die:
                clearString = "이제 아무도 오지 않는구나....끝났어....";
                break;
        }
        gameOverCanvas.SetActive(true);
        fadeImage.color = new Color(0, 0, 0, 0);
        StartCoroutine(moduleManager.FadeModule_Image(fadeImage, 0, 1, 1));
        StartCoroutine(moduleManager.AfterRunCoroutine(1,moduleManager.LoadTextOneByOne(clearString, gameOverText,0.3f,false)));
        StartCoroutine(BackToStartScene());
    }

    IEnumerator BackToStartScene()
    {
        yield return new WaitForSeconds(3f);
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isGameEnd = false;
                gameOverText.text = "";
                gameOverCanvas.SetActive(false);
                SceneManager.LoadScene(0);
                break;
            }
            yield return null;
        }
    }

    public void GameStart(bool isLoaded)
    {
        StartCoroutine(SceneStart(isLoaded));
    }

    IEnumerator SceneStart(bool isLoaded)
    {
        if(isLoaded == false)
        {
            saveData = new SaveDataClass();
            JsonManager.SaveJson(saveData);
        }
        SceneManager.LoadScene(1);
        yield return null;
        if (isLoaded)
        {
            Load();
        }

    }

    
}
