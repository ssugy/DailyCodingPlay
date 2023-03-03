using UnityEngine;
using UnityEditor;
using System.Collections;

[ExecuteInEditMode]
public class ORSocketEditorWindow : EditorWindow {
	
	[MenuItem("Window/ORFramework/ORSocket/New TCP Multi Server")]
    public static void CreateORTCPMultiServer() {
		ORTCPMultiServer.CreateInstance();
	}
	
    [MenuItem("Window/ORFramework/ORSocket/New TCP Server")]
    public static void CreateORTCPServer() {
		ORTCPServer.CreateInstance();
	}
	
    [MenuItem("Window/ORFramework/ORSocket/New TCP Client")]
    public static void CreateORTCPClient() {
		ORTCPClient.CreateInstance();
	}

}
