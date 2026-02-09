using Microsoft.Win32.SafeHandles;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEditor;
using UnityEngine;
using static System.Net.Mime.MediaTypeNames;

/**
 * 다이어로그가 적힌 CSV 파일을 ScriptableObject(이하 SO)로 변환합니다.
 *
 * [제약사항]
 * Tools-Import Dialogue CSV를 통해 에디터에서 SO를 생성하며, CSV 수정시 수동으로 생성해야합니다.
 * CSV 파일은 Assets/DataFiles/ 에 위치해야 합니다.
 *
 * [제약사항-CSV]
 * CSV 파일은 한줄당  DialogID,이름,사진이름,대화내용 으로 구성되어있어야 합니다.
 * 예를 들어 "Minditem01,나,Player,이건 결정적 단서가 되겠어." 와 같은 형태로 구성되어야 합니다.
 * 사진이름의 경우 DialogManager.cs의 enum PhotoAssetName을 참고 바랍니다.
 */
public class DialogImportEditor
{
    [MenuItem("Tools/Import Dialogue CSV")]
    public static void ImportDialog()
    {
        StreamReader sr;
        ScriptableDialog scriptableDialog;
        bool isFinished = false;
        string data;
        string csvFileName = "csvdialog.csv";
        string buffer = UnityEngine.Application.dataPath + "/DataFiles/" + csvFileName;
        
        if (!File.Exists(buffer)) return;
        
        //기존 SO 인스턴스가 존재한다면 로드, 없으면 인스턴스 생성
        scriptableDialog = AssetDatabase.LoadAssetAtPath<ScriptableDialog>("Assets/Data/DialogData.asset");
        if (scriptableDialog == null) scriptableDialog=ScriptableObject.CreateInstance<ScriptableDialog>();
        scriptableDialog.dialogList.Clear();
        sr = new StreamReader(buffer);

        while(!isFinished)
        {
            data=sr.ReadLine();
            if (data == null)
            {
                isFinished = true;
                break;
            }
            string[] splitdata=data.Split(',');
            Dialogclass dialogClass = new Dialogclass
            {
                id= splitdata[0].Trim(),
                name =splitdata[1].Trim(),
                photo=splitdata[2].Trim(),
                description= splitdata[3].Trim()
            };
            scriptableDialog.dialogList.Add(dialogClass);

        }
        
        //asset 파일 저장
        if (!AssetDatabase.Contains(scriptableDialog)) AssetDatabase.CreateAsset(scriptableDialog, "Assets/Resources/Data/DialogData.asset");
        EditorUtility.SetDirty(scriptableDialog);
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();

        UnityEngine.Debug.Log("ScriptableObject Created Complete!");

    }
}

