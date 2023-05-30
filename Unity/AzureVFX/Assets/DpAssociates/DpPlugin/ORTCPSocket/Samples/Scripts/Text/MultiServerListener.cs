using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MultiServerListener : MonoBehaviour {
	
	public float radius = 10;
	public GameObject Cube;
	
	private Dictionary<int, GameObject> _clients;
	
	public void Awake() {
		_clients = new Dictionary<int, GameObject>();
	}
	
	private void ChangeScaleTo(GameObject cube, float scale) {
		
		//iTween.ScaleTo(cube,
		//               iTween.Hash("scale", new Vector3(scale, scale, scale),
		//                           "time", 1f,
		//                           "easetype", iTween.EaseType.easeInElastic));
		
		//iTween.PunchRotation(cube, new Vector3(10, 10, 10), 1f);
		
	}
	
	private GameObject GetCube(int clientID) {
		
		GameObject cube = null;
		
		if (_clients.ContainsKey(clientID) && _clients.TryGetValue(clientID, out cube))
			return cube;
		
		return null;
		
	}
	
	public void OnServerConnect(ORTCPEventParams eventParams) {

		print("Server connected: " + eventParams.clientID);
		
		GameObject cube = (GameObject)Instantiate(Cube, Random.insideUnitSphere * radius, Random.rotation);
		
		_clients.Add(eventParams.clientID, cube);
		
	}
	
	public void OnServerDisconnect(ORTCPEventParams eventParams) {
		
		print("Server disconnected: " + eventParams.clientID);
		
		GameObject cube = GetCube(eventParams.clientID);
		
		if (cube == null)
			return;
			
		_clients.Remove(eventParams.clientID);
			
		Destroy(cube);

	}
	
	public void OnDataReceived(ORTCPEventParams eventParams) {
		
		float scale = 0;
		
		if (!float.TryParse(eventParams.message, out scale))
			return;
		
		print("Data received: " + eventParams.message);
		
		GameObject cube = GetCube(eventParams.clientID);
		
		if (cube == null)
			return;
		
		cube.transform.localScale = Vector3.zero;

		ChangeScaleTo(cube, scale);
		
	}
	
}
