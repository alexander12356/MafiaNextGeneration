/*using Firebase.Unity.Editor;
using Firebase.Database;
//using Firebase.Auth;
//using Firebase;
using UnityEngine;
using System.Collections.Generic;

public class MyFirebaseDatabase : MonoBehaviour {
	public static MyFirebaseDatabase instance = null;
	public DatabaseReference dataBaseRefence;

	void Awake(){
		if (instance == null)
			instance = this;
		else
			return;
	}
	// Use this for initialization
	void Start () {
		//FirebaseApp.DefaultInstance.SetEditorDatabaseUrl ("https://teleport-tap.firebaseio.com/");
		//dataBaseRefence = FirebaseDatabase.DefaultInstance.RootReference;
		//FirebaseApp.DefaultInstance.SetEditorP12FileName("Teleport Tap-1a33e26cf347");
		//FirebaseApp.DefaultInstance.SetEditorServiceAccountEmail("firebase-adminsdk-tf2mz@teleport-tap.iam.gserviceaccount.com");
		//FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");
		//WriteNewUser ("1", "test", 0, 0);
	}

	public void WriteNewUser(string userID, string name, float time, int money){
		User user = new User (name, time, money);
		string json = JsonUtility.ToJson (user);
		print ("userID :" + userID);
		//dataBaseRefence.Child ("users").Child (userID).Push ();
		dataBaseRefence.Child ("users").Child (userID).SetRawJsonValueAsync (json);
	}

	private void WriteNewScore(string userId, int score) {
		// Create new entry at /user-scores/$userid/$scoreid and at
		// /leaderboard/$scoreid simultaneously
		string key = dataBaseRefence.Child("scores").Push().Key;
		LeaderboardEntry entry = new LeaderboardEntry(userId, score);
		Dictionary<string, System.Object> entryValues = entry.ToDictionary();

		Dictionary<string, System.Object> childUpdates = new Dictionary<string, System.Object>();
		childUpdates["/scores/" + key] = entryValues;
		childUpdates["/user-scores/" + userId + "/" + key] = entryValues;

		dataBaseRefence.UpdateChildrenAsync(childUpdates);
	}
	
	public class User{
		public string username;
		public float time;
		public int money;
		public User(){}

		public User(string username, float time, int money){
			this.username = username;
			this.time = time;
			this.money = money;
		}
	}
}
*/