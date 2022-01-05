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
                // ��ȭ����ܰ� 0
                // ���� Ư�����ڷ� �ٲ�
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
                // ��ȭ���� 1�ܰ�
                // 30%�� �ٲ�

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
                // �״�� ��µǴϱ� �ٲ��� �ʿ� ����.
                break;
            case 3:
                // 3�ܰ�� ���� ��ȭ�� �ƴϴ�
                Debug.Log("3�ܰ�� ���� ��ȭ�� �ƴѵ� �̰� �߸� ���Ѱ��� ����");
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
