#pip install websockets
import asyncio, pathlib, ssl, websockets

async def LAMP(websocket, path):
    print("hello")
    data = await websocket.recv()
    print(f"< {data}")
    await websocket.send(f"{data})")

ssl_context = ssl.SSLContext(ssl.PROTOCOL_TLS_SERVER)
cert_pem = pathlib.Path(__file__).with_name("cert.pem")
privkey_pem = pathlib.Path(__file__).with_name("privkey.pem")
ssl_context.load_cert_chain(cert_pem, keyfile = privkey_pem)

server = websockets.serve(LAMP, "0.0.0.0", 8765, ssl = ssl_context)
asyncio.get_event_loop().run_until_complete(server)
print("Server Running at wss://0.0.0.0:8765")
asyncio.get_event_loop().run_forever()
