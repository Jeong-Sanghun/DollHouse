using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class DialogManager : MonoBehaviour
{

    public DialogParsing dialogParser;
    public DialogList dialogList;
    public List<char> symbols;
    public int mainStroyNum;

    // Start is called before the first frame update
    void Start()
    {
        dialogParser = new DialogParsing();
        mainStroyNum = GameManager.singleTon.saveData.currentConversationLevel;
        GameManager.singleTon.dialogManager = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RandDialog(int exprLevel)
    {
        int randNum = Random.Range(1, 4);
        dialogList = dialogParser.StoryParse("RandDialog" + exprLevel + "-" + randNum);
        switch (exprLevel)
        {   
            case 0:
                // 대화습득단계 0
                // 전부 특수문자로 바꿔
                for (int i = 0; i < dialogList.parentsDialogList.Count; i++)
                {
                    StringBuilder builder = new StringBuilder();
                    string randSymbols;
                    string symbols = "!@#$%&";

                    for (int j = 0; j < dialogList.parentsDialogList[i].Length; j++)
                    {
                        builder.Append(symbols[Random.Range(0, symbols.Length)]);
                    }
                    randSymbols = builder.ToString();
                    dialogList.parentsDialogList[i] = randSymbols;
                }
                break;
            case 1:
                // 대화습득 1단계
                // 30%만 바꿔

                for (int i = 0; i < dialogList.parentsDialogList.Count; i++)
                {
                    List<int> randNumList = new List<int>();
                    int curStringCharNum = 0;
                    string changedString;
                    for (int j = 0; j < dialogList.parentsDialogList[i].Length; j++)
                    {
                        curStringCharNum++;
                    }
                    int changedNum = curStringCharNum * 2 / 3;

                    while (randNumList.Count < changedNum)
                    {
                        int randSymbolNum = Random.Range(0, curStringCharNum);
                        if (!randNumList.Contains(randSymbolNum))
                        {
                            randNumList.Add(randSymbolNum);
                        }
                    }

                    StringBuilder builder = new StringBuilder();
                    string symbols = "!@#$%&";
                    for (int k = 0; k < dialogList.parentsDialogList[i].Length; k++)
                    {
                        if (randNumList.Contains(k))
                        {
                            builder.Append(symbols[Random.Range(0, symbols.Length)]);
                        }
                        else
                        {
                            builder.Append(dialogList.parentsDialogList[i][k]);
                        }
                    }
                    changedString = builder.ToString();
                    dialogList.parentsDialogList[i] = changedString;
                    
                }
                break;
            case 2:
                // 그대로 출력되니까 바꿔줄 필요 없다.
                break;
            case 3:
                // 3단계는 랜덤 대화가 아니다
                Debug.Log("3단계는 랜덤 대화가 아닌데 이게 뜨면 망한거임 ㅅㄱ");
                break;
        }
    }
    public void MainDialog(int dialogNo)
    {
        dialogList = dialogParser.StoryParse("MainDialog" + dialogNo);
        mainStroyNum++;
        GameManager.singleTon.saveData.currentConversationLevel = mainStroyNum;
    }
}
