using UnityEngine;
using System.Collections;

public class BinaryClientListener : MonoBehaviour {
	
	public ORTCPClient client;
	public Color connectedColor		= Color.green;
	public Color disconnectedColor	= Color.red;
	public GameObject target;
	
	private byte[] packet = new byte[]{ 4, 5 };
	
	public void Awake() {
		target.GetComponent<Renderer>().sharedMaterial.color = disconnectedColor;
	}
	
	public void Update() {
	}
	
	public void OnClientConnect(ORTCPEventParams eventParams) {
		
		print("Client connected: " + eventParams.client.gameObject);
		
		//iTween.ColorTo(target, connectedColor, .5f);
		
		client.SendBytes(packet);
		
	}
	
	public void OnClientDisconnect(ORTCPEventParams eventParams) {
		
		print("Client disconnected: " + eventParams.client.gameObject);
	
		//iTween.ColorTo(target, disconnectedColor, .5f);
		
	}
	
	public void OnClientConnectionRefused(ORTCPEventParams eventParams) {
		
		print("Client connection refused: " + eventParams.client.gameObject);

		//iTween.ShakePosition(target, Vector3.one * .1f, 1);
		
	}
	
	public void OnDataReceived(ORTCPEventParams eventParams) {
		
		byte[] bytes = eventParams.packet.bytes;
		int bytesCount = eventParams.packet.bytesCount;
		
		/**
		 * Print the array of bytes
		 */
		
		string raw = "Bytes (Length = " + bytesCount + ") = {";
		
		for (int i = 0; i < 2; i++)
			raw += " " + bytes[i].ToString();
		
		raw += " }";
		
		print (raw);
		
		/**
		 * Animate
		 */
		
		//iTween.PunchScale(target, Vector3.one * 1f, 1);
		
	}
	
}
