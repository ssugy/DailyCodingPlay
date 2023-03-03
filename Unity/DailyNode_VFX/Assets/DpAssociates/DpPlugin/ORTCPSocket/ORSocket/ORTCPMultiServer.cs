using UnityEngine;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;
using System.Collections.Generic;

public class ORTCPMultiServer : MonoBehaviour {
	
	private class NewConnection {
		
		public TcpClient tcpClient;
		
		public NewConnection(TcpClient tcpClient) {
			this.tcpClient = tcpClient;
		}
		
	}
	
	public static string DefaultName			 		= "ORTCPMultiServer";

	public static ORTCPMultiServer CreateInstance() {
		return CreateInstance(DefaultName);
	}
	
	public static ORTCPMultiServer CreateInstance(string name) {
		
		GameObject go = new GameObject(name);
		ORTCPMultiServer multi = go.AddComponent<ORTCPMultiServer>();
		
		return multi;
		
	}
	
	public ORTCPServerStartListen listenOn				= ORTCPServerStartListen.Start;
	public bool autoListenOnDisconnect					= ORTCPServer.DefaultAotListenOnDisconnect;
	public int port										= ORTCPServer.DefaultPort;
	public GameObject[] listeners						= null;
	public string onConnectMessage						= ORTCPServer.DefaultOnConnectMessage;
	public string onDisconnectMessage					= ORTCPServer.DefaultOnDisconnectMessage;
	public string onDataReceivedMessage					= ORTCPServer.DefaultOnDataReceivedMessage;
	
	private int ClientID					= 0;
	
	private Dictionary<int, ORTCPClient> _clients;
	private TcpListener _tcpListener;
	private Queue<NewConnection> _newConnections;
	private bool _listenning;
	
	public int clientsCount {
		get { return _clients.Count; }
	}
	
	public bool listenning {
		get { return _listenning; }
	}
	
	private void Awake() {
		
		_listenning = false;
		
		_newConnections = new Queue<NewConnection>();
		
		_clients = new Dictionary<int, ORTCPClient>();
		
		if (listenOn == ORTCPServerStartListen.Awake)
			StartListening();
		
	}
	
	private void Start() {

		if (listenOn == ORTCPServerStartListen.Start)
			StartListening();
		
	}
	
	private void Update() {
		
		while (_newConnections.Count > 0) {
			
			NewConnection newConnection = _newConnections.Dequeue();

			ORTCPClient client = ORTCPClient.CreateInstance("ORMultiServerClient", newConnection.tcpClient);
			client.listeners = new GameObject[]{ gameObject };
			
			int clientID = SaveClient(client);
			
			ORTCPEventParams eventParams = new ORTCPEventParams();
			eventParams.eventType = ORTCPEventType.Connected;
			eventParams.client = client;
			eventParams.clientID = clientID;
			eventParams.socket = newConnection.tcpClient;
			
			Notify(onConnectMessage, eventParams);
			
		}
		
	}
	
	private void OnDestroy() {
		
		DisconnectAll();

		StopListening();
		
	}
	
	private void OnApplicationQuit() {
		
		DisconnectAll();

		StopListening();
		
	}
	
	// Private
	
	private int SaveClient(ORTCPClient client) {
		
		int currentClientID = ClientID;
		
		_clients.Add(currentClientID, client);

		ClientID++;
		
		return currentClientID;
		
	}
	
	private int RemoveClient(int clientID) {
		
		ORTCPClient client = GetClient(clientID);
		
		if (client == null)
			return clientID;
		
		client.Disconnect();
		
		_clients.Remove(clientID);
		
		Destroy(client.gameObject);
		
		return clientID;
		
	}
	
	private int RemoveClient(ORTCPClient client) {
		
		int clientID = GetClientID(client);
		
		if (clientID < 0) {
			
			Destroy(client.gameObject);
			
			return -1;
			
		}
		
		return RemoveClient(clientID);
		
	}
	
	private void Notify(string message, ORTCPEventParams eventParams) {
		
		foreach (GameObject aListener in listeners)
			aListener.SendMessage(message, eventParams, SendMessageOptions.DontRequireReceiver);

	}
	
	private TcpClient GetTcpClient(int clientID) {

		ORTCPClient client = null;
		
		if (!_clients.TryGetValue(clientID, out client))
			return null;
		
		return client.tcpClient;
		
	}
	
	private ORTCPClient GetClient(int clientID) {
		
		ORTCPClient client = null;
		
		if (_clients.TryGetValue(clientID, out client))
		    return client;
		
		return null;
		
	}
	
	private int GetClientID(ORTCPClient client) {
		
		foreach (KeyValuePair<int, ORTCPClient> entry in _clients)
			if (entry.Value == client)
				return entry.Key;
		
		return -1;
		
	}
	
	private int GetClientID(TcpClient tcpClient) {
		
		foreach (KeyValuePair<int, ORTCPClient> entry in _clients)
			if (entry.Value.tcpClient == tcpClient)
				return entry.Key;
		
		return -1;

	}
	
	private void AcceptClient() {
		_tcpListener.BeginAcceptTcpClient(new AsyncCallback(AcceptTcpClientCallback), _tcpListener);
	}

	private void AcceptTcpClientCallback(IAsyncResult ar) {
		
	    TcpListener tcpListener = (TcpListener)ar.AsyncState;
		TcpClient tcpClient = tcpListener.EndAcceptTcpClient(ar);
		
		if (tcpListener != null && tcpClient.Connected) {
		
			_newConnections.Enqueue(new NewConnection(tcpClient));
		
			AcceptClient();
			
		}
		
	}
	
	// Events

	public void OnServerConnect(ORTCPEventParams eventParams) {

	}
	
	public void OnClientDisconnect(ORTCPEventParams eventParams) {
		
		eventParams.clientID = GetClientID(eventParams.client);
		
		Notify(onDisconnectMessage, eventParams);
		
		RemoveClient(eventParams.client);

	}
	
	public void OnDataReceived(ORTCPEventParams eventParams) {
		
		eventParams.clientID = GetClientID(eventParams.client);
		
		Notify(onDataReceivedMessage, eventParams);

	}
	
	// Public
	
	public void StartListening() {
		StartListening(port, listeners);
	}
	
	public void StartListening(int port) {
		StartListening(port, listeners);
	}
	
	public void StartListening(int port, GameObject[] listeners) {

		if (_listenning)
			return;

		this.port = port;
		this.listeners = listeners;
		
		_listenning = true;
		
		_newConnections.Clear();
		
		_tcpListener = new TcpListener(IPAddress.Any, port);
		_tcpListener.Start();
		
		AcceptClient();
		
	}

	public void StopListening() {
		
		_listenning = false;
		
		if (_tcpListener == null)
			return;
		
		_tcpListener.Stop();
		_tcpListener = null;
		
	}
	
	public void DisconnectAll() {
		
		foreach (KeyValuePair<int, ORTCPClient> entry in _clients)
			entry.Value.Disconnect();
		
		_clients.Clear();
		
	}

	public void SendAll(string message) {
		
		foreach (KeyValuePair<int, ORTCPClient> entry in _clients)
			entry.Value.Send(message);
		
	}
	
	public void Disconnect(int clientID) {
		
		ORTCPClient client = GetClient(clientID);
		
		if (client == null)
			return;
		
		client.Disconnect();
		
	}
	
	public void Send(int clientID, string message) {
		
		ORTCPClient client = GetClient(clientID);
		
		if (client == null)
			return;

		client.Send(message);
		
	}
	
}
