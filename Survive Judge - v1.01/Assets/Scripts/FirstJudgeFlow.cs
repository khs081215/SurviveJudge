using System.Collections;
using System.Collections.Generic;
using Oculus.Platform;
using UnityEngine;
using UnityEngine.Serialization;

public class FirstJudgeFlow : MonoBehaviour
{
    public OVRInput.Button nextButton;
    public OVRInput.Controller OVRController;

    #region private member
    
    private int judgeCnt = 0;
    private int hintCnt = 0;
    private int messageCnt = 0;
    private GameObject programmerHintCG;
    private GameObject doctorHintCG;
    private bool isDisplayingHint = false;
    private bool isDisplayingDialog = false;
    private bool hasPlayedSound = false;
    private bool isDisplayingHintDialog = false;
    private List<string> hintNPCNameList = new List<string> { "silverwolf", "boothill", "silverwolf", "" };
   
    [SerializeField] private int flowCnt = 0;
    [SerializeField] private GameObject ovgcamera;
    [SerializeField] private List<string> judgeIDList = new List<string>(6);
    [SerializeField] private List<string> hintIDList = new List<string>(5);
    [SerializeField] private List<GameObject> messageList = new List<GameObject>(4);
    [SerializeField] private GameObject hintCGCanvas;
    [SerializeField] private GameObject criminal;
    [SerializeField] private GameObject CGCanvas;
    [SerializeField] private GameObject BGMManager;

    #endregion private member


    void Awake()
    {
        for (int i = 1; i < 7; i++)
        {
            string buffer = "JudgeA0" + i.ToString();
            judgeIDList.Add(buffer);
        }

        for (int i = 1; i < 6; i++)
        {
            string buffer = "HintA0" + i.ToString();
            hintIDList.Add(buffer);
        }

        for (int i = 1; i < 5; i++)
        {
            string buffer = "message" + i.ToString();
            GameObject message = GameObject.Find(buffer);
            if (message != null)
            {
                messageList.Add(message);
            }
        }

        programmerHintCG = hintCGCanvas.transform.GetChild(0).gameObject;
        doctorHintCG = hintCGCanvas.transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (flowCnt < 18)
        {
            switch (flowCnt % 4)
            {
                case 0:
                    ViewDialog();
                    break;
                case 1:
                    CheckDialogEnd();
                    break;
                case 2:
                    MessageMove(messageList[messageCnt], hintNPCNameList[hintCnt]);
                    break;
                case 3:
                    MessageInitialize(messageList[messageCnt]);
                    break;
            }
        }
        else if (flowCnt == 18)
        {
            BoxCollider boxCollider = criminal.GetComponent<BoxCollider>();
            if (boxCollider != null)
            {
                boxCollider.enabled = true;
                flowCnt++;
            }
        }
        else if (flowCnt == 19)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                flowCnt++;
            }
        }
        else if (flowCnt == 20)
        {
            PlayBGM playBGM=BGMManager.GetComponent<PlayBGM>();
            playBGM.ChangeBGM("Complete",0.7f);
            CGCanvas.transform.GetChild(1).gameObject.SetActive(true);
            flowCnt++;
        }
    }

    public void MessageBreakSucceed()
    {
        flowCnt++;
    }


    void MessageMove(GameObject message, string hintNPCName)
    {
        if (message.transform.position.y < -5.0f)
            message.transform.position = new Vector3(message.transform.position.x, -1f, message.transform.position.z);
        if (message.transform.position.z < 4.5f)
            message.transform.position = new Vector3(message.transform.position.x, message.transform.position.y, 9.38f);
        message.transform.Translate(0f, -0.8f * Time.deltaTime, 0);
        DisplayHintMessage(hintNPCName);
    }

    void MessageInitialize(GameObject message)
    {
        if (!hasPlayedSound)
        {
            SoundManager.Instance.PlaySound("Break",gameObject,0.7f);
            hasPlayedSound = true;
        }
        CGCanvas.transform.GetChild(0).gameObject.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(nextButton, OVRController))
        {
            hasPlayedSound = false;
            CGCanvas.transform.GetChild(0).gameObject.SetActive(false);
            message.transform.position = new Vector3(message.transform.position.x, -7f, message.transform.position.z);
            messageCnt++;
            hintCnt++;
            flowCnt++;
        }
    }

    void DisplayHintMessage(string hintNPCName)
    {
        RaycastHit hitInfo;
        if (Physics.Raycast(ovgcamera.transform.position, ovgcamera.transform.forward, out hitInfo,
                1 << LayerMask.NameToLayer("npc"))
            || isDisplayingHint)
        {
            if (Equals(hitInfo.transform.name, hintNPCName) || isDisplayingHint)
            {
                if (Equals("silverwolf", hintNPCName))
                {
                    programmerHintCG.SetActive(true);
                }

                if (Equals("boothill", hintNPCName))
                {
                    doctorHintCG.SetActive(true);
                }

                if (isDisplayingHintDialog)
                {
                    DialogManager.Instance.dialogID = hintIDList[hintCnt];
                    isDisplayingHint = false;
                    isDisplayingHintDialog = false;
                }
                if (OVRInput.GetDown(nextButton, OVRController) || Input.GetKeyDown(KeyCode.Space))
                {
                    isDisplayingHintDialog = true;
                }
            }
            else
            {
                programmerHintCG.SetActive(false);
                doctorHintCG.SetActive(false);
            }
        }
        else
        {
            programmerHintCG.SetActive(false);
            doctorHintCG.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            if (isDisplayingHint == false)
            {
                isDisplayingHint = true;
            }
            else
            {
                isDisplayingHint = false;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            flowCnt++;
        }
    }

    void ViewDialog()
    {
        DialogManager.Instance.dialogID = judgeIDList[judgeCnt++];
        flowCnt++;
    }

    void CheckDialogEnd()
    {
        if (!isDisplayingDialog && DialogManager.Instance.isDisplaying == true)
        {
            isDisplayingDialog = true;
        }
        if (DialogManager.Instance.isDisplaying == false&&isDisplayingDialog)
        {
            isDisplayingDialog = false;
            flowCnt++;
        }
    }
}