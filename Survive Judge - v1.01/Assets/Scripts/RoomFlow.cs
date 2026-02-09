using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomFlow : MonoBehaviour
{
    [SerializeField]
    private string roomDialogID;
    // Start is called before the first frame update
    void Start()
    {
        DialogManager.Instance.dialogID = roomDialogID;
    }


}
