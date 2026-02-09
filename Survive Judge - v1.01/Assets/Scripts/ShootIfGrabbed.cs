using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * VR로 총을 쥔 상태에서 Trigger 입력시 총알을 발사합니다.
 */
public class ShootIfGrabbed : MonoBehaviour
{
    private GunShoot gunShoot;
    private OVRGrabbable ovrGrabbable;
    public OVRInput.Button shootingButton;

    // Start is called before the first frame update
    void Start()
    {
        gunShoot = gameObject.GetComponent<GunShoot>();
        ovrGrabbable = gameObject.GetComponent <OVRGrabbable>();
    }

    // Update is called once per frame
    void Update()
    {
       if(ovrGrabbable.isGrabbed&&OVRInput.GetDown(shootingButton,
            ovrGrabbable.grabbedBy.GetController()))
        {
            gunShoot.TriggerShoot();
        }
       
    }
}
