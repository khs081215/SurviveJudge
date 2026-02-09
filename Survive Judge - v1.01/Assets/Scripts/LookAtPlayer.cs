using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/**
 * 게임오브젝트가 플레이어를 바라보게 만듭니다.
 */
public class LookAtPlayer : MonoBehaviour
{
    [SerializeField] 
    private Transform player;
    
    // Update is called once per frame
    void Update()
    {
        this.transform.LookAt(player);
    }
}
