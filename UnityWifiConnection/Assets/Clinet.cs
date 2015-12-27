using UnityEngine;
using System.Collections;

public class Clinet : MonoBehaviour {
	public bool showDetail;
	public bool connect = false;

	void Awake(){
		if (!gameObject.GetComponent<NetworkView> ()) {
			gameObject.AddComponent<NetworkView>();
		}
	}

	void Update () {
		MasterServer.RequestHostList("YourPCRoom");
	}

	void OnGUI(){
		GUILayout.Label ("");
		GUILayout.Label ("");
		GUILayout.Label ("");
		if(GUILayout.Button("showDetail")){
			showDetail = !showDetail;
		}
		HostData[] datas = MasterServer.PollHostList ();

		if (!showDetail)
			return;

		for (int i=0; i<datas.Length; i++) {
			HostData data = datas[i];
			if(data==null)
				continue;

			GUILayout.BeginHorizontal();
			Debug.Log("gameName("+data.gameName+")"
			          +", connectedPlayers("+data.connectedPlayers+"/"+data.playerLimit+")");


			string logInfo = "";
			for(int j=0;j<data.ip.Length;j++){
				string host = data.ip[j];
				logInfo += " [Host("+host+") port("+data.port+")] ";
			}
			Debug.Log(logInfo);

			Debug.Log("comment("+data.comment+")");
			if(GUILayout.Button("Connect to server")){
				Network.Connect(data);
				connect = true;
			}
			GUILayout.EndHorizontal();
		}
		if (connect) {
			if(GUILayout.Button("+")){
				if (GetComponent<NetworkView> ()) {
					GetComponent<NetworkView> ().RPC("Add", RPCMode.All, 1);
				}
			}
			if(GUILayout.Button("+")){
				if (GetComponent<NetworkView> ()) {
					GetComponent<NetworkView> ().RPC("Subtract", RPCMode.All, 1);
				}
			}
		}
	}

}
