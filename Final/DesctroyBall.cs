using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnCollision : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        // 检查碰撞物体的标签是否为 "Destroyer"
        if (collision.gameObject.CompareTag("Destroyer"))
        {
            Destroy(gameObject); // 销毁当前球体的GameObject
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // 销毁当前球体的GameObject
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // 检查碰撞物体的标签是否为 "Destroyer"
        if (other.gameObject.CompareTag("Destroyer"))
        {
            Destroy(gameObject); // 销毁当前球体的GameObject
        }
        if (other.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject); // 销毁当前球体的GameObject
        }
    }
}
