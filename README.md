

# IXD-256-SP24
Adv proto
------
Project 1 - Golf Goal Installation using RGB
<img width="1232" alt="flow chart" src="https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/0900ecb7-d014-4ba8-847e-5994ff3b8269">

https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/a5ec98b3-8e68-4d41-b9a7-7d6449c3e6cf

Project 2 - Monitor Light Bar using IMU
<img width="1026" alt="flow" src="https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/b82bbfd4-b785-4112-9764-d1716b9968a6">
Demo Video:https://vimeo.com/918383484?share=copy


Project 3 - Option Selector "Don't know what for Lunch?"
Idea:
![Jeff-Idea2](https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/43b3ed86-c4d4-45e7-b03c-95adc4eec9c0)

Flow Chart:
<img width="1680" alt="flow chart" src="https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/c235ecd0-aad7-4d1b-9d2f-8b00e699cdab">
<img width="752" alt="states" src="https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/5e7546a2-d887-4a92-ae4d-4afa9b617de2">
![states](https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/afc006c3-f711-4466-b0ab-557e876c2b10)

Code:
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






Project 4(Final) - Unity Game "Catch Ball"
<img width="945" alt="Game Example" src="https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/2525cbc7-d73e-4e74-b004-6dc90a1962d1">

Code in Main.py
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






