#pragma strict

function Start() {
}

function Update() {
}

function OnServerConnect(eventParams:ORTCPEventParams) {
	print("Server connected: " + eventParams.server.gameObject);
}
	
function OnServerDisconnect(eventParams:ORTCPEventParams) {
	print("Server disconnected: " + eventParams.server.gameObject);
}
	
function OnDataReceived(eventParams:ORTCPEventParams) {

	if (eventParams.server.socketType == ORTCPSocketType.Text) {
	
		print("Data Received: " + eventParams.message);
		
	} else if (eventParams.server.socketType == ORTCPSocketType.Binary) {
	
		var packet:ORSocketPacket = eventParams.packet;
		
		print("Bytes Received (Length = " + packet.bytesCount + "): " + packet.bytes);
	
	}
	
}
