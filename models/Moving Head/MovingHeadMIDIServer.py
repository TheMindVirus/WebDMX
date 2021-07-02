import pygame.midi as MIDI
import threading, socket

MIDI.init()
Device = 1
Input = MIDI.Input(Device)

s = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
s.connect(("127.0.0.1", 8051))

def MIDIevent():
    while(1):
        if(Input.poll()):
            event = Input.read(1)[0]
            msg = bytes(str(event), "UTF-8")
            s.send(msg)
            print(msg)

worker = threading.Thread(target = MIDIevent)
worker.start()
