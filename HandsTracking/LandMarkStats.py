import numpy as np


def land_mark_stats(hand_data):
    x_list = np.array(hand_data[0::3])
    y_list = np.array(hand_data[1::3])
    z_list = np.array(hand_data[2::3])
    print("==========================================")
    print(f"x mean: {x_list.mean() : .2f}, x min: {x_list.min() : .2f}, x max: {x_list.max(): .2f}")
    print(f"y mean: {y_list.mean() : .2f}, y min: {y_list.min(): .2f}, y max: {y_list.max() : .2f}")
    print(f"z mean: {z_list.mean() : .2f}, z min: {z_list.min() : .2f}, z max: {z_list.max() : .2f}")
