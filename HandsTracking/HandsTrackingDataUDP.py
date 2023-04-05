import cv2
import time
import numpy as np
import mediapipe as mp
import socket
import LandMarkStats

CAM_WIDTH = 1280
CAM_HEIGHT = 720
SERVER_INFO = ("127.0.0.1", 5055)

cam_feed = cv2.VideoCapture(0)
cam_feed.set(3, CAM_WIDTH)
cam_feed.set(4, CAM_HEIGHT)
prev_t = 0

mp_hands = mp.solutions.hands
mp_drawing = mp.solutions.drawing_utils
mp_drawing_styles = mp.solutions.drawing_styles

soc = socket.socket(family=socket.AF_INET, type=socket.SOCK_DGRAM)


with(mp_hands.Hands(static_image_mode=False,
                    max_num_hands=2,
                    min_detection_confidence=.5,
                    min_tracking_confidence=.5)) as hands:

    while cam_feed.isOpened():
        success, img = cam_feed.read()
        if not success:
            print("No input, skipping frame !")
            continue

        output_data = []

        img = cv2.cvtColor(img, cv2.COLOR_BGR2RGB)

        detection = hands.process(img)

        img = cv2.cvtColor(img, cv2.COLOR_RGB2BGR)

        if detection.multi_hand_landmarks:

            for h_marks in detection.multi_hand_landmarks:
                for id_lm, lm in enumerate(h_marks.landmark):
                    output_data.extend([lm.x, 1 - lm.y, lm.z])
                mp_drawing.draw_landmarks(
                    img,
                    h_marks,
                    mp_hands.HAND_CONNECTIONS,
                    mp_drawing_styles.get_default_hand_landmarks_style(),
                    mp_drawing_styles.get_default_hand_connections_style())

            LandMarkStats.land_mark_stats(output_data)
            soc.sendto(str.encode(str(output_data)), SERVER_INFO)

        cv2.imshow("Tracking Feed", cv2.flip(img, 1))

        current_t = time.time()
        fps = 1 / (current_t - prev_t)
        prev_t = current_t
        cv2.putText(img, f"FPS: {int(fps)}", (20, 40), cv2.FONT_HERSHEY_DUPLEX, 1, (210, 210, 210), 1)
        cv2.waitKey(5)

cam_feed.release()