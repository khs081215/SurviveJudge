using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintCollision : MonoBehaviour
{
    [SerializeField] private string gunName;
    private GameObject judgeFlow;
    private FirstJudgeFlow firstJudgeFlow;

    void Awake()
    {
        judgeFlow = GameObject.FindGameObjectWithTag("JudgeFlow");
        if (judgeFlow != null)
        {
            firstJudgeFlow = judgeFlow.GetComponent<FirstJudgeFlow>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            BulletAttribute bulletAttribute = other.GetComponent<BulletAttribute>();
            if (bulletAttribute != null)
            {
                if (Equals(gunName, bulletAttribute.GunName))
                {
                    if (firstJudgeFlow != null)
                    {
                        firstJudgeFlow.MessageBreakSucceed();
                    }
                }
            }
        }
    }
}