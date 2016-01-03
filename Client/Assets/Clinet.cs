using UnityEngine;
using System.Collections;

public class Clinet : MonoBehaviour {
	public bool showDetail;
	public bool connect = false;
	public int serverAmount = -1;

	public float GUILayoutHeight = 40;
	
	void Awake(){
		if (!gameObject.GetComponent<NetworkView> ()) {
			gameObject.AddComponent<NetworkView>();
		}
	}
	
	void Update () {
		MasterServer.RequestHostList("YourPCRoom");
	}
	
	void OnGUI(){
		if(GUILayout.Button("顯示server資訊", GUILayout.Height(GUILayoutHeight))){
			showDetail = !showDetail;
		}
		HostData[] datas = MasterServer.PollHostList ();
		
		if (!showDetail)
			return;
		
		for (int i=0; i<datas.Length; i++) {
			if(i%2 ==0)
				GUI.color = Color.blue;
			else
				GUI.color = Color.cyan;
			HostData data = datas[i];
			if(data==null)
				continue;
			
			GUILayout.BeginHorizontal();
			Debug.Log("gameName("+data.gameName+")"
			          +", connectedPlayers("+data.connectedPlayers+"/"+data.playerLimit+")");
			GUILayout.Label("gameName("+data.gameName+")"
			                +", connectedPlayers("+data.connectedPlayers+"/"+data.playerLimit+")"
			                , GUILayout.Height(GUILayoutHeight));
			
			string logInfo = "";
			for(int j=0;j<data.ip.Length;j++){
				string host = data.ip[j];
				logInfo += " [Host("+host+") port("+data.port+")] ";
			}
			Debug.Log(logInfo);
			GUILayout.Label(logInfo);
			
			Debug.Log("comment("+data.comment+")");
			GUILayout.Label("comment("+data.comment+")", GUILayout.Height(GUILayoutHeight));
			if(!connect){
				if(GUILayout.Button("連線", GUILayout.Height(GUILayoutHeight))){
					Network.Connect(data);
					connect = true;
				}
			}
			GUILayout.EndHorizontal();
			GUILayout.Label("<color=yellow>serverAmount("+serverAmount+")</color>"
			                , GUILayout.Height(GUILayoutHeight));
		}
		GUI.color = Color.white;
		if (connect) {
			if(GUILayout.Button("+", GUILayout.Height(GUILayoutHeight))){
				if (GetComponent<NetworkView> ()) {
					GetComponent<NetworkView> ().RPC("Add", RPCMode.Server, 1);
				}
			}
			if(GUILayout.Button("-", GUILayout.Height(GUILayoutHeight))){
				if (GetComponent<NetworkView> ()) {
					GetComponent<NetworkView> ().RPC("Subtract", RPCMode.Server, 1);
				}
			}
		}
	}
	
	[RPC]
	void UpdateServerAmount(int amount){
		Debug.Log ("Do UpdateServerAmount");
		serverAmount = amount;
	}

	[RPC]
	void Add(int add){

	}
	
	[RPC]
	void Subtract(int subtract){

	}
}
