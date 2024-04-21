

# IXD-256-SP24
Adv proto
------
# Project 1 - Golf Goal Installation using RGB
<img width="1232" alt="flow chart" src="https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/0900ecb7-d014-4ba8-847e-5994ff3b8269">

https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/a5ec98b3-8e68-4d41-b9a7-7d6449c3e6cf

# Project 2 - Monitor Light Bar using IMU
<img width="1026" alt="flow" src="https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/b82bbfd4-b785-4112-9764-d1716b9968a6">
Demo Video:https://vimeo.com/918383484?share=copy


Project 3 - Option Selector "Don't know what for Lunch?"
## Idea
![Jeff-Idea2](https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/43b3ed86-c4d4-45e7-b03c-95adc4eec9c0)

Flow Chart:
<img width="1680" alt="flow chart" src="https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/c235ecd0-aad7-4d1b-9d2f-8b00e699cdab">
<img width="752" alt="states" src="https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/5e7546a2-d887-4a92-ae4d-4afa9b617de2">
![states](https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/afc006c3-f711-4466-b0ab-557e876c2b10)

## Code
Control the servo to rotate 45 degrees to ensure that the servo angle does not exceed 180 degrees, and then pause briefly to complete the servo movement.
```python
def rotate_45_degrees():
    """每次旋转45度"""
    global last_angle
    next_angle = (last_angle + 45) % 180  # 计算下一个角度，确保不超过舵机范围
    servo.move(next_angle)
    last_angle = next_angle  # 更新最后的角度
    time.sleep_ms(100)  # 短暂暂停，以便完成移动
```

Continuously monitor the ADC readings, control the servo to start rotating based on the readings and stop rotating after a specified time, and then update the displayed ADC value.
```python
def loop():
    global is_rotating, last_move_time, rotation_duration
    current_time = time.time()
    
    adc_val = adc1.read()
    if adc_val > 3400 and not is_rotating:
        # 开始旋转
        is_rotating = True
        rotation_duration = random.randint(2, 4)  # 随机旋转时间
        last_move_time = current_time

    if is_rotating:
        if (current_time - last_move_time) <= rotation_duration:
            rotate_45_degrees()  # 每次旋转45度
        else:
            servo.move(90)  # 停止，回到90度位置
            is_rotating = False
    label0.setText(str(adc_val))
```






# Project 4(Final) - Unity Game "Catch Ball"
<img width="945" alt="Game Example" src="https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/2525cbc7-d73e-4e74-b004-6dc90a1962d1">
## Game Video


https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/cbef6ce8-338c-45ae-a328-b3d33cd73f15



## Code in Main.py
This code defines a set of variables used to detect and manage small shaking events on the M5Stack device. The variable last_accel is used to store the last acceleration value for comparison with the current value. small_shake_start_time records the time when a small shake begins, and small_shake_detected is a Boolean flag used to indicate whether the system has detected a small shake. In addition, small_shake_duration is set to 800 milliseconds, which defines the minimum duration required to confirm that the shake is valid, while small_shake_threshold sets the acceleration threshold that triggers shake detection, here is 0.4. These variables work together to help the system accurately identify and respond to small shaking events.
```python
last_accel = 0
small_shake_start_time = 0
small_shake_detected = False 
small_shake_duration = 800  
small_shake_threshold = 0.4 
```

This function is mainly used to monitor and record acceleration changes for subsequent data analysis or shaking detection logic. However, the current code snippet does not contain code that uses this acceleration data for further logical processing, such as shake detection and event triggering.
```python
def loop():
    global last_trigger_time, trigger_interval
    global last_accel, small_shake_start_time, small_shake_detected

    M5.update()

    # 获取加速度计的值
    imu_val = Imu.getAccel()
    imu_x_val = imu_val[0]
    imu_y_val = imu_val[1]
    imu_z_val = imu_val[2]
    
    print(imu_x_val, "|", imu_y_val, "|", imu_z_val)
    time.sleep(0.01)
```

# Code In Unity
## Serial Communicate
```csharp
public class arduinoCtrl : MonoBehaviour {
    // replace with your board's COM port
    SerialPort stream = new SerialPort("/dev/tty.usbmodemUiFlow2_1", 115200);

    public Transform t;

    void Start()
    {
        stream.Open();
    }

    void Update()
    {
        //Vector3 lastData = Vector3.zero;
        string lastData = "";

        string UnSplitData = stream.ReadLine();
        Debug.Log(UnSplitData);
        lastData = UnSplitData;
    }
```

## Serial Controller Code(ShipController)
This function is mainly used to monitor and record acceleration changes for subsequent data analysis or shaking detection logic. However, the current code snippet does not contain code that uses this acceleration data for further logical processing, such as shake detection and event triggering.
```csharp
void Start()
    {
        //rb = GetComponent<Rigidbody>(); // Make sure there's a Rigidbody component attached to this GameObject
        Debug.Log("Serial opened");
        stream.Open();
        stream.DtrEnable = true;
        stream.RtsEnable = true;
        readThread = new Thread(ReadSerialPort)
        {
            IsBackground = true
        };
        readThread.Start();
        accel = new Vector3(0, 0, 0);
    }
```


The FixedUpdate method in this Unity script processes character movement based on new data received from an input source. If newDataReceived is true, the script checks conditions for directional movement and jumping. It triggers appropriate animations for walking left or right through an animator object, and adjusts the player object's position accordingly using moveSpeed and Time.deltaTime for smooth frame-independent movement. The script also has the capability to trigger a jump animation with an "UppercutTrigger", although the actual movement code for the jump has been commented out. After processing the movements, it resets the movement flags and the newDataReceived flag to ensure readiness for the next set of input data.
```csharp
void FixedUpdate()
    {
        if (newDataReceived)
        {
            if (walkleft)
            {
                animator.SetTrigger("WalkLeft");
                if (player != null)
                {
                    player.transform.position += Vector3.left * moveSpeed * Time.deltaTime;
                }
                
                // Move the character left
                walkleft = false;
            }
            if (walkright)
            {
                animator.SetTrigger("WalkRight");
                // Move the character left
                //rb.MovePosition(rb.position + Vector3.right * moveSpeed * Time.fixedDeltaTime);
                if (player != null)
                {
                    player.transform.position += Vector3.right * moveSpeed * Time.deltaTime;
                }
                walkright = false;
            }
            if (jump)
            {
                animator.SetTrigger("UppercutTrigger");
                // Move the character left
                //rb.MovePosition(rb.position + Vector3.up * moveSpeed * Time.fixedDeltaTime);
                //jump = false;
            }
            newDataReceived = false;
        }
    }
```

The ReadSerialPort method handles serial communication within a Unity environment, managing the reading of data from a stream and processing it to control character movement. It sets a read timeout of 500 milliseconds to avoid hanging if no data is available. Within a loop that continues until shouldTerminate is false, the method reads a line of data from the stream if open, splits it by '|', and processes it if the length is sufficient. It extracts acceleration values (AccX, AccY, AccZ) from the split data to control character actions. Depending on the difference between the current and previous Y-axis values and a defined threshold, the method sets flags (walkleft or walkright) that initiate movement. These flags are then used elsewhere to animate and move the character in the game world. The method is robust against exceptions, handling I/O issues, timeouts, and other exceptions to maintain stable execution. It ends by updating a global flag (newDataReceived) and stores the current accelerometer values for comparison in the next cycle, facilitating continuous and responsive game character control based on serial input.

```csharp
void ReadSerialPort()
    {
        stream.ReadTimeout = 500; // Set timeout for reading to 500 milliseconds
        while (!shouldTerminate)
        {
            try
            {
                if (stream.IsOpen)
                {
                    string unSplitData = stream.ReadLine();
                    string[] SplitData = unSplitData.Split('|');

                    if (SplitData.Length >= 3)
                    {
                        float AccX = float.Parse(SplitData[0]);
                        float AccY = float.Parse(SplitData[1]);
                        float AccZ = float.Parse(SplitData[2]);
                        //AccZ -= 1.0f;
                        float controlForce = AccY;
                        accel = new Vector3(AccX, AccY, AccZ);

                        if (controlForce - lastData[0] < threshold)
                        {
                            //Debug.Log("click button");
                            if (animator != null)
                            {
                                walkleft = true;
                            }
                            else
                            {
                                walkleft = false;
                            }

                        }

                        if (controlForce - lastData[0] > threshold * -1)
                        {
                            //Debug.Log("click button");
                            if (animator != null)
                            {
                                walkright = true;
                            }
                            else
                            {
                                walkright = false;
                            }

                        }

                        //if (AccX - lastData[0] > 4.5)
                        //{
                        //    Debug.Log("click button");
                        //    if (animator != null)
                        //    {
                        //        jump = true;
                        //    }
                        //    else
                        //    {
                        //        jump = false;
                        //    }

                        //}


                        lastData = new Vector3(AccX, AccY, AccZ);
                        newDataReceived = true;
                    }

                    //lastData = unSplitData;
                    //newDataReceived = true;
                    //Debug.Log(newDataReceived);
                }
            }
            catch (System.IO.IOException ex)
            {
                Debug.LogWarning("I/O error: " + ex.Message);
            }
            catch (System.TimeoutException)
            {
                Debug.LogWarning("Time out");
                // Timeout is expected, just catch it and continue the loop.
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Error reading from serial port: " + ex.Message);
                break; // Break the loop on unexpected exceptions
            }
        }
    }
```

The DisableSphereRendering method is a coroutine used to disable the rendering of a specified GameObject, referred to as sphere, after a designated delay. This coroutine leverages WaitForSeconds to pause execution for the number of seconds specified by delay, ensuring the rendering change doesn't occur immediately but rather after the pause. Once the delay elapses, it accesses the Renderer component of the sphere GameObject and disables it by setting enabled to false. This method is useful for temporarily hiding objects in a scene dynamically.

The CloseThreadAndPort method ensures clean termination of a background thread and closing of a serial port connection. It sets a flag shouldTerminate to true to signal other parts of the program that it should stop running. It checks if the readThread is both non-null and alive; if so, it waits for the thread to complete its execution using Join() to ensure that no threads are left hanging, which could lead to resource leaks or unexpected behavior. Additionally, it checks if the stream object is non-null and open, and if true, it closes the stream to properly release the serial port resources. This method is crucial for preventing resource leaks and ensuring the application closes cleanly without leaving open threads or ports.
```csharp
    IEnumerator DisableSphereRendering(GameObject sphere, float delay)
    {
        yield return new WaitForSeconds(delay);
        sphere.GetComponent<Renderer>().enabled = false;
    }

    private void CloseThreadAndPort()
    {
        shouldTerminate = true;
        if (readThread != null && readThread.IsAlive)
        {
            readThread.Join();
        }
        if (stream != null && stream.IsOpen)
        {
            stream.Close();
        }
    }
```

## GameManager Script
The script functions as a GameManager in a Unity game, managing UI and game state based on player interactions with game objects. It initializes by deactivating a "game over" UI element and then continuously updates in-game UI to reflect the player's remaining lives and score. Lives decrease with collisions with red balls, and the score increases with blue ball collisions. If the player's lives are depleted, it triggers a GameOver method, which logs the event, activates the game over UI, and pauses the game. Additionally, the script allows external updates to the score via a public method, showing its modular design. This setup effectively coordinates gameplay mechanics with visual feedback and game state management.
```csharp
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public DestroyerController destroyerController; // Reference to the DestroyerController
    public PlayerController playerController;       // Reference to the PlayerController
    public TextMeshProUGUI lifetext;                // Text for displaying the life left
    public TextMeshProUGUI blueballtext;            // Text for displaying the score

    public int maxLifeCount = 3;
    private GameObject gameOverUI;

    void Start()
    {
        // Find the GameOverUI GameObject by tag
        gameOverUI = GameObject.FindGameObjectWithTag("Finish");
        if (gameOverUI != null)
            gameOverUI.SetActive(false); // Initially deactivate the GameOverUI
    }

    void Update()
    {
        if (destroyerController == null || playerController == null) return;

        // Update life text for red ball collisions
        int lifeLeft = maxLifeCount - destroyerController.redTriggerCount;
        lifetext.text = "Life left: " + lifeLeft.ToString() + "/" + maxLifeCount.ToString();

        // Update blue ball text for collisions with the Player
        blueballtext.text = "Score: " + playerController.blueTriggerCount.ToString();

        // If the red ball trigger count is equal to or exceeds max life count, end the game
        if (destroyerController.redTriggerCount >= maxLifeCount)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over");
        if (gameOverUI != null)
            gameOverUI.SetActive(true); // Activate the GameOverUI
        Time.timeScale = 0; // Pause the game
    }

    // You can also call this method from the PlayerController if you prefer
    public void UpdateBlueScore()
    {
        blueballtext.text = "Score: " + playerController.blueTriggerCount.ToString();
    }
}

```


## SphereSpawner

This Unity script, SphereSpawner, manages the spawning and movement of sphere objects within the game environment. It utilizes two prefabs, one for blue spheres and another for red spheres, to generate spheres at set intervals that decrease over time until a minimum threshold is reached. This mechanic is implemented to increase the challenge or intensity as the game progresses.

The script defines several parameters including the initial and minimum spawn intervals, the acceleration rate at which the spawn interval decreases, and the movement characteristics of the spawner itself (speed, duration, and range). The spawner not only periodically spawns spheres but also moves within a defined range on the x-axis to vary the spawn locations, enhancing the game's dynamism.

The core functionality in the Update method includes decrementing timers related to spawning and movement. When the spawn timer runs out, a new sphere is instantiated at the spawner's current position, and the timer resets. Concurrently, the spawner's position is progressively updated to smoothly transition towards a randomly set new target position once the movement timer elapses. This systematic approach ensures the gameplay remains engaging and unpredictable due to the changing spawn points and pacing.
```csharp
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

```

## DestroyBall Script

The Unity script DestroyOnCollision is designed to handle collision events for GameObjects with which it is associated, ensuring that they are destroyed when they collide with specific objects identified by tags. It employs two primary Unity collision detection methods: OnCollisionEnter for handling physics-based collisions and OnTriggerEnter for handling trigger-based interactions.

Both methods check the tags of colliding or intersecting objects. If an object is tagged as "Destroyer" or "Player", the script executes the Destroy(gameObject) command, which removes the GameObject to which this script is attached from the scene. This behavior is essential for gameplay elements such as removing projectiles upon impact or cleaning up objects that interact with a player or other designated destroyers within the game environment. The script's simplicity and specificity make it highly reusable and adaptable for various game scenarios involving interactions between objects.
```csharp
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

```




