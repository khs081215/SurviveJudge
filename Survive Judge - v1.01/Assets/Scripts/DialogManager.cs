using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEngine.UI;
using System;

/**
 * 특정 ID를 가진 다이어로그(이름,사진,대화내용)를 표시합니다.
 * DialogManager.instance.dialogID="MindItem01"의 형태로 호출할 수 있습니다.
 *
 * [제약사항]
 * 씬에 UICanvas 태그가 있는 Canvas 가 필요하며
 * -UICanvas(Canvas, Tag: UICanvas)
 * ㄴ UIChild(Canvas, Activate=false)
 * - ㄴ DialogBar(RawImage)
 * - - Name(TMP_Text)
 * - - CharacterCG(RawImage)
 * - - Description(TMP_Text)
 * - - PlayerCG(RawImage)
 * 위와 같이 구성되어 있어야 합니다.
 */
public class DialogManager : GenericSingleton<DialogManager>
{
    public string dialogID { get; set; }
    public bool isDisplaying { get; private set; }
    
    private ScriptableDialog scriptableDialog;
    private TMP_Text uiName;
    private TMP_Text uiDescription;
    private RawImage uiPhoto;
    private RawImage uiPlayerPhoto;
    private GameObject uiCanvas;
    private GameObject uiCanvasChild;
    private OVRInput.Button nextButton;
    private OVRInput.Controller OVRController;
    private Dictionary<string, Texture> photoMapping;
    private int dialogCnt;
    private string pastDialogID;
    
    
    private enum PhotoAssetName
    {
        Boothill,
        Gallagher,
        Player,
        Phelix,
        Silverwolf
    };



    // Update is called once per frame
    void Update()
    {
        if (dialogID != null)
        {
            if (pastDialogID != dialogID && isDisplaying)
            {
                dialogID = pastDialogID;
            }
            if (dialogCnt == -1)
            {
                dialogCnt = 0;
                isDisplaying = true;
                pastDialogID=dialogID;
                ShowDialog();
            }
            if (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(nextButton, OVRController))
            {
                dialogCnt++;
                ShowDialog();
            }

        }
    }

    public void ShowDialog()
    {
        if (dialogCnt == 0)
        {
            uiCanvasChild.SetActive(true);
        }
        if (dialogCnt >= scriptableDialog.dialogDic[dialogID].Count)
        {
            dialogID = null;
            pastDialogID = null;
            uiCanvasChild.SetActive(false);
            isDisplaying = false;
            dialogCnt = -1;
        }
        else
        {
            if (scriptableDialog.dialogDic.TryGetValue(dialogID, out var dialogList))
            {

                uiName.text = dialogList[dialogCnt].name;
                if (Equals(dialogList[dialogCnt].photo, "Player"))
                {
                    uiPhoto.enabled = false;
                    uiPlayerPhoto.enabled = true;
                }
                else
                {
                    uiPhoto.enabled = true;
                    uiPlayerPhoto.enabled = false;
                    uiPhoto.texture = photoMapping[dialogList[dialogCnt].photo];
                }
                uiDescription.text = dialogList[dialogCnt].description;
            }
            else
            {
                Debug.Log("DialogError");
            }
        }

    }


    protected override void InitReference()
    {
        dialogID = null;
        dialogCnt = -1;
        if (photoMapping == null)
        {
            photoMapping = new Dictionary<string, Texture>();
            foreach (string photoName in Enum.GetNames(typeof(PhotoAssetName)))
            {
                photoMapping.Add(photoName, (Resources.Load<Sprite>("2DAssets/" + photoName)).texture);
            }
        }
        if (uiCanvas == null)
        {
            uiCanvas = GameObject.FindGameObjectWithTag("UICanvas");
        }
        if (uiCanvasChild == null)
        {
            uiCanvasChild = uiCanvas.transform.GetChild(0).gameObject;
        }
        if (uiName == null)
        {
            uiName = uiCanvasChild.transform.GetChild(1).GetComponent<TMP_Text>();
        }
        if (uiPhoto == null)
        {
            uiPhoto = uiCanvasChild.transform.GetChild(2).GetComponent<RawImage>();
        }
        if (uiDescription == null)
        {
            uiDescription = uiCanvasChild.transform.GetChild(3).GetComponent<TMP_Text>();
        }
        if (uiPlayerPhoto == null)
        {
            uiPlayerPhoto = uiCanvasChild.transform.GetChild(4).GetComponent<RawImage>();
        }
        if (scriptableDialog == null)
        {
            scriptableDialog = Resources.Load<ScriptableDialog>("Data/DialogData");
        }
    }
}
