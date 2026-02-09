using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScenebyClick: MonoBehaviour
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

    public void OnClickButton()
    {
        Application.LoadLevel(nextSceneName);
    }
}
