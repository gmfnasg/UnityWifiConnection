using UnityEngine;
using System.Collections;

public class Server : MonoBehaviour {
	public Rect serverConnectionsRect = new Rect();
	public int amount = 0;
	
	void Awake(){
		if (!GetComponent<NetworkView> ()) {
			gameObject.AddComponent<NetworkView>();
		}

	}

	void OnGUI(){
		if (GUILayout.Button ("StartServer")) {
			Network.InitializeServer(99, 9998, !Network.HavePublicAddress()); 
			MasterServer.RegisterHost("YourPCRoom", "PhantasyNan", "alpha 1.0");
			GUI.Label(serverConnectionsRect, "Connections:"+Network.connections.Length);
		}
		GUILayout.Label ("amount:"+amount.ToString());
	}

	[RPC]
	void Add(int add){
		Debug.Log ("Do Add");
		amount += add;
	}

	[RPC]
	void Subtract(int subtract){
		Debug.Log ("Do Subtract");
		amount -= subtract;
	}
}
