using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFolllow : MonoBehaviour
{
    // Start is called before the first frame update
    
    
         public Transform target; // 角色的Transform
    public Vector3 offset; // 摄像机相对于角色的偏移
    public float smoothSpeed = 0.125f; // 摄像机跟随的平滑速度
    public bool lookAtTarget = false; // 是否让摄像机始终朝向角色
    void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        if (lookAtTarget)
        {
            transform.LookAt(target);
        }
    }
}
