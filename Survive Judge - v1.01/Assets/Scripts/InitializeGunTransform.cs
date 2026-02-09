using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * VR에서 특정 버튼 입력시 총의 위치를 초기 위치로 초기화합니다.
 */
public class InitializeGunTransform : MonoBehaviour
{
    public OVRInput.Button nextButton;
    public OVRInput.Controller OVRController;
    private Vector3 startLocation;
    private Quaternion startRotation;

    void Start()
    {
        startLocation = transform.localPosition;
        startRotation = transform.localRotation;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(nextButton, OVRController))
        {
            transform.localPosition = startLocation;
            transform.localRotation = startRotation;
        }
    }
}
