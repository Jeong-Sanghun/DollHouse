using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;
using UnityEngine.SceneManagement;
using System;

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

    [SerializeField]
    GameObject optionCanvas;
    [SerializeField]
    GameObject creditCanvas;
    [SerializeField]
    GameObject muteButton;
    [SerializeField]
    GameObject unMuteButton;


    public bool isOptionOpen;
    public bool isGameEnd;
    bool isRestartable;
    // Start is called before the first frame update

    private InterstitialAd interstitial;
    string appID;
    private void RequestInterstitial()
    {
        //string adUnitId = "ca-app-pub-3940256099942544/8691691433";
        //string adUnitId = "ca-app-pub-3940256099942544/1033173712";
        string adUnitId = "ca-app-pub-6023793752348178/3214040485"; //이게찐또
        // Initialize an InterstitialAd.
        this.interstitial = new InterstitialAd(adUnitId);
        AdRequest request = new AdRequest.Builder().Build();
        // Load the interstitial with the request.
        this.interstitial.LoadAd(request);
        this.interstitial.OnAdClosed += HandleOnAdClosed;
    }

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
        appID = "ca-app-pub-6023793752348178~3405612175";
       
        isGameEnd = false;
        isOptionOpen = false;
        isRestartable = true;
        Application.targetFrameRate = 60;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isRestartable == true)
        {

            isGameEnd = false;
            gameOverText.text = "";
            gameOverCanvas.SetActive(false);
            SceneManager.LoadScene(0);

            isRestartable = false;
        }
    }

    public void AdRequest()
    {
        MobileAds.Initialize((initStatus) => { });
        RequestInterstitial();
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
        StartCoroutine(moduleManager.AfterRunCoroutine(1,moduleManager.LoadTextOneByOne(clearString, gameOverText,0.1f,false)));
        StartCoroutine(BackToStartScene());
    }

    IEnumerator BackToStartScene()
    {

        saveData = new SaveDataClass();
        saveData.isReplay = true;
        JsonManager.SaveJson(saveData);
        yield return new WaitForSeconds(4f);
        isRestartable = true;
        if(this.interstitial.IsLoaded() == false)
        {
            isRestartable = true;
        }
        else
        {
            isRestartable = false;
            this.interstitial.Show();
        }   
    }
    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        RequestInterstitial();
        isRestartable = true;

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
        else
        {
            saveData = JsonManager.LoadSaveData();
        }
        SceneManager.LoadScene(1);
        yield return null;
        if (isLoaded)
        {
            Load();
        }
        

    }


    public void Quit()
    {
        SoundManager.singleTon.ButtonPlay();
        Application.Quit();
    }

    public void OptionActive(bool active)
    {
        SoundManager.singleTon.ButtonPlay();   
        isOptionOpen = active;
        optionCanvas.SetActive(active);
        if (active == true)
        {
            Time.timeScale = 0;
            if (mainSceneManager != null)
            {
                if(mainSceneManager.watchingTV == true)
                {
                    SoundManager.singleTon.PauseTv();
                }
            }
        }
        else
        {
            Time.timeScale = 1;
            if (mainSceneManager != null)
            {
                if (mainSceneManager.watchingTV == true)
                {
                    SoundManager.singleTon.ResumeTv();
                }
            }
        }
    }

    public void Mute()
    {
        SoundManager.singleTon.ButtonPlay();
        muteButton.SetActive(false);
        unMuteButton.SetActive(true);
        SoundManager.singleTon.Mute();
    }

    public void UnMute()
    {
        SoundManager.singleTon.ButtonPlay();

        muteButton.SetActive(true);
        unMuteButton.SetActive(false);
        SoundManager.singleTon.UnMute();
    }

    public void CreditActive(bool active)
    {
        SoundManager.singleTon.ButtonPlay();
        creditCanvas.SetActive(active);
    }

    
}
