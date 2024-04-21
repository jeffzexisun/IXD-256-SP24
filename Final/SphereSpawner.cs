using UnityEngine;

public class SphereSpawner : MonoBehaviour
{
    public GameObject sphereBluePrefab; // 蓝色球体的预制件
    public GameObject sphereRedPrefab;  // 红色球体的预制件

    public float spawnInterval = 1.5f; // 初始生成球体的时间间隔
    public float minSpawnInterval = 0.5f; // 最小生成球体的时间间隔
    public float acceleration = 0.05f; // 生成间隔的加速度

    public float moveSpeed = 3f;       // 移动速度
    public float moveDuration = 2f;    // 每次移动的持续时间
    public float moveRange = 5f;       // 移动的范围

    private float timer;               // 生成球体的计时器
    private float moveTimer;           // 移动计时器
    private Vector3 targetPosition;    // 目标位置

    void Start()
    {
        timer = spawnInterval;
        SetNewTargetPosition();
    }

    void Update()
    {
        // 更新生成球体的计时器
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            SpawnRandomSphere();
            timer = spawnInterval; // 重置计时器
        }

        // 逐渐减少生成间隔，直到达到最小间隔
        if (spawnInterval > minSpawnInterval)
        {
            spawnInterval = Mathf.Max(spawnInterval - acceleration * Time.deltaTime, minSpawnInterval);
        }

        // 更新移动计时器
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0)
        {
            SetNewTargetPosition();
        }

        // 移动发射器
        MoveSpawner();
    }

    void SpawnRandomSphere()
    {
        // 随机选择一个球体预制件来生成
        GameObject sphereToSpawn = (Random.Range(0, 2) == 0) ? sphereBluePrefab : sphereRedPrefab;
        Instantiate(sphereToSpawn, transform.position, transform.rotation);
    }

    void MoveSpawner()
    {
        // 平滑移动到目标位置
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    void SetNewTargetPosition()
    {
        // 随机设置新的目标位置
        float randomX = Random.Range(-moveRange, moveRange);
        targetPosition = new Vector3(randomX, transform.position.y, transform.position.z);

        // 重置移动计时器
        moveTimer = moveDuration;
    }
}
