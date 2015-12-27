using UnityEngine;
using System.Collections;

public class Server : MonoBehaviour {
	public int amount = 0;
	public bool serverStart = false;
	
	void Awake(){
		if (!GetComponent<NetworkView> ()) {
			gameObject.AddComponent<NetworkView>();
		}
	}

	void Update(){
		if (serverStart && GetComponent<NetworkView> () && Network.connections.Length>0) {
			GetComponent<NetworkView> ().RPC("UpdateServerAmount", RPCMode.All, amount);
		}
	}

	void OnGUI(){
		GUILayout.Label("StartServer:"+serverStart);
		if (GUILayout.Button ("StartServer")) {
			Network.InitializeServer (99, 9998, !Network.HavePublicAddress ()); 
			MasterServer.RegisterHost ("YourPCRoom", "PhantasyNan", "alpha 1.0");
			serverStart = true;
		}
		GUILayout.Label("Connections:"+Network.connections.Length);
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

	[RPC]
	void UpdateServerAmount(int amount){
	}
}
