#pragma strict

function Start() {
}

function Update() {
}

function OnClientConnect(eventParams:ORTCPEventParams) {
	print("Client connected: " + eventParams);	
}
	
function OnClientDisconnect(eventParams:ORTCPEventParams) {
	print("Client disconnected: " + eventParams);
}
	
function OnClientConnectionRefused(eventParams:ORTCPEventParams) {
	print("Client connection refused: " + eventParams);
}
	
function OnDataReceived(eventParams:ORTCPEventParams) {

	if (eventParams.client.socketType == ORTCPSocketType.Text) {
	
		print("Data received: " + eventParams);
		
	} else if (eventParams.client.socketType == ORTCPSocketType.Binary) {
	
		var packet:ORSocketPacket = eventParams.packet;
		
		print("Bytes Received (Length = " + packet.bytesCount + "): " + packet.bytes);
	
	}
	
}
