using UnityEngine;
using System.Collections;

public class ServerListener : MonoBehaviour {
	
	public Color connectedColor		= Color.green;
	public Color disconnectedColor	= Color.red;
	public GameObject target		= null;
	
	public void Awake() {
		//target.GetComponent<Renderer>().sharedMaterial.color = disconnectedColor;
	}
	
	public void Update() {
		
		for (int i = 0; i < 10; i++)
			if (Input.GetKeyDown(i.ToString()))
				ChangeScaleTo(i);
		
	}
	
	public void ChangeScaleTo(float scale) {
		
		//iTween.ScaleTo(target,
		//               iTween.Hash("scale", new Vector3(scale, scale, scale),
		//                           "time", 1f,
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
		
		float scale = 0;
		
		if (!float.TryParse(eventParams.message, out scale))
			return;
		
		print("Data received: " + eventParams.message);
		
		ChangeScaleTo(scale);
		
	}
	
}
