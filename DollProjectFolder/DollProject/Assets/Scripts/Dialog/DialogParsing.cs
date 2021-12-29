using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class DialogParsing
{
    public enum DialogState
    {
        Start,
        Parent,
        Child
    }

    public DialogList StoryParse(string dialogName)
    {
        DialogList dialogList = new DialogList();

        string originalDialog;
        TextAsset dialog = Resources.Load<TextAsset>(dialogName);
        originalDialog = dialog.text;
        DialogState nowState = DialogState.Start;

        string curText;
        StringBuilder builder = new StringBuilder();

        for (int i= 0; i < originalDialog.Length; i++)
        {
            switch (originalDialog[i])
            {
                case '$':
                    if (nowState == DialogState.Parent)
                    {
                        // 저장
                        curText = builder.ToString();
                        builder.Clear();
                        dialogList.parentsDialogList.Add(curText);
                    }
                    else if (nowState == DialogState.Child)
                    {
                        // 저장
                        curText = builder.ToString();
                        builder.Clear();
                        dialogList.childDialogList.Add(curText);
                        nowState = DialogState.Parent;
                    }
                    else
                    {
                        nowState = DialogState.Parent;
                    }

                    break;
                case '%':
                    if (nowState == DialogState.Parent)
                    {
                        // 저장
                        curText = builder.ToString();
                        builder.Clear();
                        dialogList.parentsDialogList.Add(curText);
                        nowState = DialogState.Child;
                    }
                    else if (nowState == DialogState.Child)
                    {
                        // 저장
                        curText = builder.ToString();
                        builder.Clear();
                        dialogList.childDialogList.Add(curText);
                    }
                    else
                    {
                        nowState = DialogState.Child;
                    }

                    break;
                default:
                    // 대화에 넣어줘
                    builder.Append(originalDialog[i]);
                    break;
            }
        }

        if (nowState == DialogState.Parent)
        {
            curText = builder.ToString();
            builder.Clear();
            dialogList.parentsDialogList.Add(curText);
        }
        else if (nowState == DialogState.Child)
        {
            curText = builder.ToString();
            builder.Clear();
            dialogList.childDialogList.Add(curText);
        }

        return dialogList;
    }

}
