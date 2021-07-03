#pip install websockets
import asyncio, pathlib, ssl, websockets

async def LAMP(websocket, path):
    print(websocket)
    #print(websocket.remote_address)
    data = await websocket.recv()
    print(f"< {data}")
    await websocket.send(f"{data})")
    #websocket.close()

server = websockets.serve(LAMP, "0.0.0.0", 8765)
asyncio.get_event_loop().run_until_complete(server)
print("Server Running at ws://0.0.0.0:8765")
asyncio.get_event_loop().run_forever()
