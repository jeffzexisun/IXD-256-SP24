import os, sys, io
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
