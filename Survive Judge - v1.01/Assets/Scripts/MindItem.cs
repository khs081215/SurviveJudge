using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

/**
 * 마인드아이템에 일정속도 이상으로 충돌 상호작용시 CG와 다이어로그를 표시합니다.
 */
public class MindItem : MonoBehaviour
{
    public OVRInput.Button nextButton;
    public OVRInput.Controller OVRController;
    
    [SerializeField] 
    private GameObject mindCGCanvas;
    private GameObject mindCG;
    [SerializeField] 
    private string dialogID;
    private bool isDisplayingCG = false;
    private bool isDisplayingDialog = false;
    private bool hasPlayedSound = false;
    [SerializeField] 
    private KeyCode pcDebugKey;

    
    void Start()
    {
        mindCG=mindCGCanvas.transform.GetChild(0).gameObject;
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            float collisionSqrSpeed;
            collisionSqrSpeed = collision.gameObject.GetComponent<CalculateSpeed>().GetSpeed();

            if (collisionSqrSpeed  > 1.0f)
            {
                isDisplayingCG = true;
            }
        }
    }

    void Update()
    {
        if (isDisplayingDialog)
        {
            DisplayDialog();
            isDisplayingDialog = false;
        }
        if (isDisplayingCG)
        {
            if (!hasPlayedSound)
            {
                SoundManager.Instance.PlaySound("DDring",gameObject,0.7f);
                hasPlayedSound = true;
            }
            if (DialogManager.Instance.isDisplaying == true)
            {
                isDisplayingCG = false;
            }
            else
            {
                mindCG.SetActive(true);
                if (Input.GetKeyDown(KeyCode.Space) || OVRInput.GetDown(nextButton, OVRController))
                {
                    isDisplayingCG = false;
                    mindCG.SetActive(false);
                    isDisplayingDialog = true;
                }
            }
        }
        if (Input.GetKeyDown(pcDebugKey))
        {
            isDisplayingCG = true;
            Debug.Log(pcDebugKey);
        }
    }

    private void DisplayDialog()
    {
        DialogManager.Instance.dialogID = dialogID;
        Destroy(gameObject);
    }
}
