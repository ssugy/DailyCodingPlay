using UnityEngine;
using System.Collections;

public class BinaryServerListener : MonoBehaviour {
	
	public Color connectedColor		= Color.green;
	public Color disconnectedColor	= Color.red;
	public GameObject target		= null;
	
	public void Awake() {
		target.GetComponent<Renderer>().sharedMaterial.color = disconnectedColor;
	}
	
	public void Update() {
	}
	
	public void ChangeScaleTo(float scale, float time) {
		
		//iTween.ScaleTo(target,
		//               iTween.Hash("scale", new Vector3(scale, scale, scale),
		//                           "time", time,
		//                           "easetype", iTween.EaseType.easeInElastic));
		
		//iTween.PunchRotation(target, new Vector3(10, 10, 10), 1f);
		
	}
	
	public void OnServerConnect(ORTCPEventParams eventParams) {
		
		print("Server connected: " + eventParams.server.gameObject);
		
		//iTween.ColorTo(target, connectedColor, .5f);
		
	}
	
	public void OnServerDisconnect(ORTCPEventParams eventParams) {
		
		print("Server disconnected: " + eventParams.server.gameObject);
		
		//iTween.ColorTo(target, disconnectedColor, .5f);

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
		
		byte[] response = new byte[bytesCount];
		
		for (int i = 0; i < bytesCount; i++)
			response[i] = bytes[i];
		
		float scale = (float)bytes[0];
		float time = (float)bytes[1];
		
		ChangeScaleTo(scale, time);
		
		/**
		 * Send the response
		 */
		
		eventParams.server.SendBytes(response);
		
	}
	
}
