import bpy, sys, threading, socket, time, math

def print(data, type = "OUTPUT"):
    for window in bpy.context.window_manager.windows:
        screen = window.screen
        for area in screen.areas:
            if area.type == 'CONSOLE':
                override = {'window': window, 'screen': screen, 'area': area}
                bpy.ops.console.scrollback_append(override, text=str(data), type=type)

def MIDI2Intensity(value, max = 2.0):
    return ((float(value) / 127.0) * max)

def MIDI2Shutter(value, max = 100.0):
    return ((float(value) / 127.0) * max)

def MIDI2Pan(value, sweep = 4.0):
    return ((float(value) / 127.0) * (sweep * math.pi)) - ((sweep / 2) * math.pi)

def MIDI2Tilt(value, sweep = 1.2):
    return ((float(value) / 127.0) * (sweep * math.pi)) - ((sweep / 2) * math.pi)

def Netproc(s):
    while(1):
        msg = s.recv(4096)
        data = eval(msg)
        if(data[0][0] == 176):
            if(data[0][1] == 1):
                bpy.data.objects["Cone.001"].active_material.node_tree.nodes["Emission"].inputs[1].default_value = MIDI2Intensity(data[0][2])
                bpy.data.objects["Cone.002"].active_material.node_tree.nodes["Emission"].inputs[1].default_value = MIDI2Intensity(data[0][2])
            if(data[0][1] == 2):
                bpy.data.objects["Cone.001"].scale.x = MIDI2Shutter(data[0][2])
                bpy.data.objects["Cone.001"].scale.y = MIDI2Shutter(data[0][2])
                bpy.data.objects["Cone.002"].scale.x = MIDI2Shutter(data[0][2])
                bpy.data.objects["Cone.002"].scale.y = MIDI2Shutter(data[0][2])
            if(data[0][1] == 3):
                bpy.data.objects["Pan Axis.001"].rotation_euler.z = MIDI2Pan(data[0][2]) * -1.0
                bpy.data.objects["Pan Axis.002"].rotation_euler.z = MIDI2Pan(data[0][2])
            if(data[0][1] == 4):
                bpy.data.objects["Tilt Axis.001"].rotation_euler.y = MIDI2Tilt(data[0][2])
                bpy.data.objects["Tilt Axis.002"].rotation_euler.y = MIDI2Tilt(data[0][2])

try:
    s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
    s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    s.bind(("127.0.0.1", 8051))
    worker = threading.Thread(target = Netproc, args = (s,))
    worker.start()
except Exception as error:
    print(error, "ERROR")