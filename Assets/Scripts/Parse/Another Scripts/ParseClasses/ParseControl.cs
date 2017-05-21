using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Parse;

public class ParseControl : MonoBehaviour {

	public static ParseControl instance;
	public string myIds;


	void Awake(){
		if (instance == null) {
			instance = this;
		}	
		else{
			Destroy (gameObject);
			return;
		}
		DontDestroyOnLoad (gameObject);
	}

	void Start () {
	}

	public void SignUp(string name, string password){
		var user = new ParseUser()
		{
			Username = name,
			Password = password
		};

		user.SignUpAsync();
	}

	public void LogIn(string name, string password){
		ParseUser.LogInAsync (name, password).ContinueWith (t => {
			if (t.IsFaulted || t.IsCanceled){
				Debug.Log("fail login");
			}
			else{
				Debug.Log("success login " + t.Result);
			}
		});
	}

	public bool CurrentUser(){
		if (ParseUser.CurrentUser != null) {
			Debug.Log ("have current user " + ParseUser.CurrentUser.ObjectId);
			return true;
		} else {
			Debug.Log ("current user is null");
			return false;
		}
	}

	public void LogOut(){
		ParseUser.LogOutAsync().ContinueWith(t => {
			if (t.IsCompleted){
				Debug.Log("complete");
				var currentUser = ParseUser.CurrentUser;
			}
		});
	}

	public Dictionary<Templates.Request.Requests, ParseObject> lastRequests = new Dictionary<Templates.Request.Requests, ParseObject> (); 

	public void Request(Templates.Request.Requests request, Templates.Mafia.Characteristic characteristics = Templates.Mafia.Characteristic.Joker, string[] research = null){
		switch (request) {
		case Templates.Request.Requests.getRoom:
			if (!lastRequests.ContainsKey (Templates.Request.Requests.getRoom)) {
				Parse.ParseObject parseObj = new Parse.ParseObject (Templates.CustomTables.waitRoom.ToString ());
				parseObj ["uids"] = ParseUser.CurrentUser.ObjectId;
				parseObj.SaveAsync ();
				lastRequests.Add (Templates.Request.Requests.getRoom, parseObj);
			}
			break;
		case Templates.Request.Requests.logOutRoom:
			if (lastRequests.ContainsKey(Templates.Request.Requests.getRoom)){
				var myObject = lastRequests [Templates.Request.Requests.getRoom];
				var taskDelete = myObject.DeleteAsync ();

				myObject.Remove (ParseUser.CurrentUser.ObjectId);
				myObject.SaveAsync ();
			}
			break;
		case Templates.Request.Requests.ready:
			Debug.Log ("ready");
			break;
		case Templates.Request.Requests.go:
			//Templates.Mafia mafia = new Templates.Mafia (characteristics, research);
			Debug.Log ("go");
			break;
		}
	}

	public void SendData(Templates.Mafia mafia, string myId, string targetId){
		
	}
}
