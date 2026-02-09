using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/**
 * 게임오브젝트의 속도의 제곱을 계산해 반환합니다.
 */
public class CalculateSpeed : MonoBehaviour
{
    private Vector3 lastPosition; 
    [SerializeField]
    private float sqrSpeed;

    void Start()
    {
        lastPosition = transform.position;
    }
    
    void Update()
    {
        sqrSpeed = GetSpeed();
    }

    public float GetSpeed()
    {
        if (Time.deltaTime != 0)
        {
            float speed = (((transform.position - lastPosition).sqrMagnitude) / Time.deltaTime);
            lastPosition = transform.position;
            return speed;
        }
        return 0.0f;
    }
}
