# main.py
import M5
from M5 import *
from hardware import *
import time
import math

# 全局变量来存储上次触发时间
last_trigger_time = 0
# 设置最小触发间隔为500毫秒
trigger_interval = 500

# 用于晃动检测的变量
last_accel = 0
small_shake_start_time = 0
small_shake_detected = False  # 标志变量，用于指示是否已经检测到小幅度晃动
small_shake_duration = 800  # 小幅度晃动确认时间（1秒）
small_shake_threshold = 0.4  # 小幅度晃动阈值

def setup():
    M5.begin()
    print("IMU initialized")

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

if __name__ == '__main__':
    try:
        setup()
        while True:
            loop()
    except (Exception, KeyboardInterrupt) as e:
        print(e)




