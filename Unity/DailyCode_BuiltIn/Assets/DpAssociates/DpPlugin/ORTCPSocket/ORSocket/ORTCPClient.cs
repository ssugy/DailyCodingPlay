using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public enum ORTCPClientState {
	Connecting,
	Connected,
	Disconnected
}

public enum ORTCPClientStartConnection {
	DontConnect,
	Awake,
	Start
}

public class ORTCPClient : MonoBehaviour {

	// Static
	
	public static string DefaultORTCPClientName					= "ORTCPClient";
	public static ORTCPSocketType DefaultSocketType				= ORTCPSocketType.Text;
	public static int DefaultBufferSize							= 1024;
	public static string DefaultOnConnectMessage				= "OnClientConnect";
	public static string DefaultOnDisconnectMessage				= "OnClientDisconnect";
	public static string DefaultOnConnectionRefusedMessage		= "OnClientConnectionRefused";
	public static string DefaultOnDataReceivedMessage			= "OnDataReceived";
		
	public static ORTCPClient CreateInstance() {
		return CreateInstance(DefaultORTCPClientName);
	}
	
	public static ORTCPClient CreateInstance(string name) {

		GameObject go = new GameObject(name);
		ORTCPClient client = go.AddComponent<ORTCPClient>();
		
		return client;
		
	}
	
	public static ORTCPClient CreateInstance(string name, TcpClient tcpClient) {
		
		GameObject go = new GameObject(name);
		ORTCPClient client = go.AddComponent<ORTCPClient>();
		
		client.SetTcpClient(tcpClient);
		
		return client;
		
	}
	
	// MonoBehaviour
	
	public ORTCPClientStartConnection connectOn		= ORTCPClientStartConnection.DontConnect;
	public bool autoConnectOnDisconnect				= true;
	public float disconnectTryInterval				= 3;
	public bool autoConnectOnConnectionRefused		= true;
	public float connectionRefusedTryInterval		= 3;
	public string hostname							= "localhost";
	public int port									= 1933;
	public ORTCPSocketType socketType				= DefaultSocketType;
	public int bufferSize							= DefaultBufferSize;
	public GameObject[] listeners					= null;
	public string onConnectMessage					= DefaultOnConnectMessage;
	public string onDisconnectMessage				= DefaultOnDisconnectMessage;
	public string onDataReceivedMessage				= DefaultOnDataReceivedMessage;
	public string onConnectionRefusedMessage		= DefaultOnConnectionRefusedMessage;
	
	private ORTCPClientState _state;
	private NetworkStream _stream;
	private StreamWriter _writer;
	private StreamReader _reader;
	private Thread _readThread;
	private TcpClient _client;
	private Queue<ORTCPEventType> _events;
	private Queue<string> _messages;
	private Queue<ORSocketPacket> _packets;
	
	public bool isConnected {
		get { return _state == ORTCPClientState.Connected; }
	}
	
	public ORTCPClientState state {
		get { return _state; }
	}
	
	public TcpClient client {
		get { return _client; }
	}
	
	public TcpClient tcpClient {
		get { return _client; }
	}

	private void Awake() {
		
		_state = ORTCPClientState.Disconnected;
		_events = new Queue<ORTCPEventType>();
		_messages = new Queue<string>();
		_packets = new Queue<ORSocketPacket>();
		
		if (connectOn == ORTCPClientStartConnection.Awake)
			Connect();
		
	}

	private void Start () {
		
		if (connectOn == ORTCPClientStartConnection.Start)
			Connect();
		
	}
	
	private void Update() {
		
		while (_events.Count > 0) {
			
			ORTCPEventType eventType = _events.Dequeue();
			
			ORTCPEventParams eventParams = new ORTCPEventParams();
			eventParams.eventType = eventType;
			eventParams.client = this;
			eventParams.socket = _client;
			
			if (eventType == ORTCPEventType.Connected) {
				
				foreach (GameObject listener in listeners)
					listener.SendMessage(onConnectMessage, eventParams, SendMessageOptions.DontRequireReceiver);
				
			} else if (eventType == ORTCPEventType.Disconnected) {
				
				foreach (GameObject listener in listeners)
					listener.SendMessage(onDisconnectMessage, eventParams, SendMessageOptions.DontRequireReceiver);
				
				_reader.Close();
				_writer.Close();
				_client.Close();

				if (autoConnectOnDisconnect)
					ORTimer.Execute(gameObject, disconnectTryInterval, "OnDisconnectTimer");
				
			} else if (eventType == ORTCPEventType.DataReceived) {
				
				if (socketType == ORTCPSocketType.Text) {
					
					eventParams.message = _messages.Dequeue();
					
				} else {
					
					eventParams.packet = _packets.Dequeue();
					
				}
				
				foreach (GameObject listener in listeners)
					listener.SendMessage(onDataReceivedMessage, eventParams, SendMessageOptions.DontRequireReceiver);
				
			} else if (eventType == ORTCPEventType.ConnectionRefused) {
				
				foreach (GameObject listener in listeners)
					listener.SendMessage(onConnectionRefusedMessage, eventParams, SendMessageOptions.DontRequireReceiver);
				
				if (autoConnectOnConnectionRefused)
					ORTimer.Execute(gameObject, connectionRefusedTryInterval, "OnConnectionRefusedTimer");
				
			}
			
		}

	}
	
	private void OnDestroy() {
		Disconnect();
	}

	private void OnApplicationQuit() {
		Disconnect();
	}
	
	// Private
	
	private void ConnectCallback(IAsyncResult ar) {
		
        try {
		
	    	TcpClient tcpClient = (TcpClient)ar.AsyncState;
			tcpClient.EndConnect(ar);
			
			SetTcpClient(tcpClient);

        } catch (Exception e) {
			
			_events.Enqueue(ORTCPEventType.ConnectionRefused);

			Debug.LogWarning("Connect Exception: " + e.Message);
			
        }
		
    }
	
	private void ReadData() {

		bool endOfStream = false;
		
		while (!endOfStream) {
			
			if (socketType == ORTCPSocketType.Text) {
				
				String response = null;
	
				try { response = _reader.ReadLine(); } catch (Exception e) { e.ToString(); }
				
				if (response != null) {
	
					response = response.Replace(Environment.NewLine, "");
		
					_events.Enqueue(ORTCPEventType.DataReceived);
						
					_messages.Enqueue(response);
						
				} else {
					
					endOfStream = true;
					
				}
				
			} else if (socketType == ORTCPSocketType.Binary) {
				
				byte[] bytes = new byte[bufferSize];
				
				int bytesRead = _stream.Read(bytes, 0, bufferSize);
				
				if (bytesRead == 0) {
					
					endOfStream = true;
					
				} else {
					
					_events.Enqueue(ORTCPEventType.DataReceived);
					
					_packets.Enqueue(new ORSocketPacket(bytes, bytesRead));
					
				}

			}
			
		}
		
		_state = ORTCPClientState.Disconnected;
		
		_client.Close();
		
		_events.Enqueue(ORTCPEventType.Disconnected);
		
	}
	
	// Events
	
	private void OnDisconnectTimer(ORTimer timer) {
		Connect();
	}
	
	private void OnConnectionRefusedTimer(ORTimer timer) {
		Connect();
	}
	
	// Public
	
	public void Connect() {
		Connect(hostname, port);
	}
	
	public void Connect(string hostname, int port) {
		
		if (_state == ORTCPClientState.Connected)
			return;
		
		this.hostname = hostname;
		this.port = port;
		
		_state = ORTCPClientState.Connecting;
		
		_messages.Clear();
		_events.Clear();
		
		_client = new TcpClient();
		
		_client.BeginConnect(hostname,
		                     port,
		                     new AsyncCallback(ConnectCallback),
		                     _client);
		
	}
	
	public void Disconnect() {
		
		_state = ORTCPClientState.Disconnected;

		try { if (_reader != null) _reader.Close(); } catch (Exception e) { e.ToString(); }
		try { if (_writer != null) _writer.Close(); } catch (Exception e) { e.ToString(); }
		try { if (_client != null) _client.Close(); } catch (Exception e) { e.ToString(); }
		
	}

	public void Send(string message) {

		if (!isConnected)
			return;
		
		_writer.WriteLine(message);
		_writer.Flush();
		
	}
	
	public void SendBytes(byte[] bytes) {
		SendBytes(bytes, 0, bytes.Length);
	}
	
	public void SendBytes(byte[] bytes, int offset, int size) {
		
		if (!isConnected)
			return;
		
		_stream.Write(bytes, offset, size);
		_stream.Flush();

	}
	
	public void SetTcpClient(TcpClient tcpClient) {
		
		_client = tcpClient;
		
		if (_client.Connected) {

			_stream = _client.GetStream();
			_reader = new StreamReader(_stream);
			_writer = new StreamWriter(_stream);
				
			_state = ORTCPClientState.Connected;
			
			_events.Enqueue(ORTCPEventType.Connected);
		
			_readThread = new Thread(ReadData);
			_readThread.IsBackground = true;
			_readThread.Start();
			
		} else {
			
			_state = ORTCPClientState.Disconnected;
			
		}
		
	}

}
