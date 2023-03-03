using UnityEngine;
using System.Collections;

public class ClientListener : MonoBehaviour {
	
	public ORTCPClient client;
	public Color connectedColor		= Color.green;
	public Color disconnectedColor	= Color.red;
	//public GameObject target;
	
	public void Awake() {
		//target.GetComponent<Renderer>().sharedMaterial.color = disconnectedColor;
	}
	
	public void Update() {
		
		for (int i = 0; i < 10; i++)
			if (Input.GetKeyDown(i.ToString()))
				client.Send(i.ToString());
		
	}
	
	public void OnClientConnect(ORTCPEventParams eventParams) {
		
		print("Client connected: " + eventParams.client.gameObject);
		
		//iTween.ColorTo(target, connectedColor, .5f);
		
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
		
		print("Data received: " + eventParams.message);
		
		//iTween.PunchScale(target, Vector3.one * 1f, 1);
		
	}
	
}
