

# IXD-256-SP24
Adv proto
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
[Uploading adc_servo_lightsenimport os, sys, io
import M5
from M5 import *
from hardware import *
import time
from servo import Servo  # 导入servo.py
import random

# 全局变量定义
title0 = None
label0 = None
servo = None
adc1 = None
is_rotating = False
last_move_time = 0  
rotation_duration = 0
last_angle = 90  # 添加用于跟踪舵机当前角度的变量
# set start time to 1 sec after adc input
stop_duration = 1  

def setup():
    global title0, label0, servo, adc1, last_move_time, last_angle
    M5.begin()
    title0 = Widgets.Title("Press to over 3k", 3, 0x000000, 0xffffff)
    label0 = Widgets.Label("--", 8, 10, 4.0, 0xffffff, 0x000000)
    servo = Servo(pin=38)
    servo.move(90)  # 初始化角度为90度
    adc1 = ADC(Pin(1), atten=ADC.ATTN_11DB)
    last_move_time = time.time()
    last_angle = 90  # 初始化last_angle

def rotate_45_degrees():
    """每次旋转45度"""
    global last_angle
    next_angle = (last_angle + 45) % 180  # 计算下一个角度，确保不超过舵机范围
    servo.move(next_angle)
    last_angle = next_angle  # 更新最后的角度
    time.sleep_ms(100)  # 短暂暂停，以便完成移动

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

if __name__ == '__main__':
    setup()
    while True:
        loop()
sor.py…]()



Project 4(Final) - Unity Game "Catch Ball"
<img width="945" alt="Game Example" src="https://github.com/jeffzexisun/IXD-256-SP24/assets/160269351/2525cbc7-d73e-4e74-b004-6dc90a1962d1">
