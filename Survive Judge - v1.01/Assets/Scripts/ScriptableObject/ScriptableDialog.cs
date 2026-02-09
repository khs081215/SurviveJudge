using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Serialization;
using Debug = UnityEngine.Debug;

[System.Serializable]
public struct Dialogclass
{
    public string id;
    public string name;
    public string photo;
    public string description;
}

/**
 * 데이터를 List 형태로 저장하는 Scriptable Object입니다.
 * 활성화시 List를 Dictionary로 변환합니다.
 */
[CreateAssetMenu]
public class ScriptableDialog : ScriptableObject
{
    public List<Dialogclass> dialogList = new List<Dialogclass>();

    [System.NonSerialized] 
    public Dictionary<string, List<Dialogclass>> dialogDic;
    [System.NonSerialized] 
    public int dialogListCnt;
    
    private void OnEnable()
    {
        dialogDic = new Dictionary<string, List<Dialogclass>>();
        dialogListCnt = 0;
        foreach (var listInDialogClass in dialogList)
        {
            if (!dialogDic.ContainsKey(listInDialogClass.id))
            {
                dialogDic.Add(listInDialogClass.id, new List<Dialogclass>());
            }

            dialogDic[listInDialogClass.id].Add(listInDialogClass);
            dialogListCnt++;
        }
    }
}