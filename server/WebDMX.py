import json, math, time
from datetime import datetime

SERVER_FILE = "\\\\SYNOLOGYDS215J\web\webgl\WebDMX\lights.json"

def LightingServer(path, t):
    #file = open(path, "r")
    #data = file.read()
    #file.close()
    
    #print(data)

    data = '{ "Lamp1": { "SetIntensity": "255.0", "SetRed": "255.0", \
        "SetGreen": "0.0", "SetBlue": "0.0", "SetAlpha": "100.0", \
        "SetPan": "90.0", "SetTilt": "45.0", "SetPanMult": "1.0", \
        "SetTiltMult": "1.0", "SetAperture": "30.0", "SetRange": "100.0" }, \
        "Lamp2": { "SetIntensity": "255.0", "SetRed": "255.0", \
        "SetGreen": "0.0", "SetBlue": "0.0", "SetAlpha": "100.0", \
        "SetPan": "90.0", "SetTilt": "45.0", "SetPanMult": "-1.0", \
        "SetTiltMult": "1.0", "SetAperture": "30.0", "SetRange": "100.0" } }'
    #print(data[490:500])
    info = json.loads(data)

    info["Lamp1"]["SetRed"] = str(0.0)
    info["Lamp2"]["SetRed"] = str(0.0)
    info["Lamp1"]["SetBlue"] = str(255.0)
    info["Lamp2"]["SetBlue"] = str(255.0)
    info["Lamp1"]["SetTilt"] = str(math.sin(t) * 45.0)
    info["Lamp2"]["SetTilt"] = str(math.sin(t) * 45.0)
    
    data = json.dumps(info, indent = 4)
    
    file = open(path, "w")
    file.write(data)
    file.close()

t = 0
while True:
    LightingServer(SERVER_FILE, t)
    time.sleep(0.01)
    t = t + 0.01 if t < 255.0 else 0
