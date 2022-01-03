using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class DialogList
{
    public List<string> parentsDialogList;
    public List<string> childDialogList;
    public bool isParentStart;
    public DialogList()
    {
        parentsDialogList = new List<string>();
        childDialogList = new List<string>();
    }
}