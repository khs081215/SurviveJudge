using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 총알이 오버랩 시 해당 씬으로 전환합니다.
 */
public class MoveScenebyTrigger : MonoBehaviour
{
    [SerializeField]
    private string nextSceneName;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Application.LoadLevel(nextSceneName);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            Application.LoadLevel(nextSceneName);
        }
    }
}
