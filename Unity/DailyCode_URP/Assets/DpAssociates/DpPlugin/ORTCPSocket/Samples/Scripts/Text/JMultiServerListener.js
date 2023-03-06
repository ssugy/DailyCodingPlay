#pragma strict

function Start() {
}

function Update() {
}

function OnServerConnect(eventParams:ORTCPEventParams) {
	print("Server connected: " + eventParams.clientID);
}
	
function OnServerDisconnect(eventParams:ORTCPEventParams) {		
	print("Server disconnected: " + eventParams.clientID);
}
	
function OnDataReceived(eventParams:ORTCPEventParams) {

	if (eventParams.client.socketType == ORTCPSocketType.Text) {
	
		print("Data Received: " + eventParams.clientID + " -> " + eventParams.message);
		
	} else {
	
		var packet:ORSocketPacket = eventParams.packet;
		
		print("Bytes Received (Length = " + packet.bytesCount + "): " + packet.bytes);
	
	}
	
}
