/*using UnityEngine;
using Firebase.Auth;
using System.Collections.Generic;

public sealed class MyFireBaseAuth : MonoBehaviour {

	public static MyFireBaseAuth instance = null;

	private FirebaseAuth auth;
	private FirebaseUser user;

	void Awake(){
		if (instance == null)
			instance = this;
		else
			return;
	}

	void Start(){
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
	}

	public string RemoveSpecialCharacters(string str)
	{
		System.Text.StringBuilder sb = new System.Text.StringBuilder ();
		for (int i = 0; i < str.Length; i++)
		{
			if ((str[i] >= '0' && str[i] <= '9')
				|| (str[i] >= 'A' && str[i] <= 'z'|| str[i] == '_'))
			{
				sb.Append(str[i]);
			}
		}
		//Debug.Log ("return string :" + sb.ToString ());
		return sb.ToString();
	}

	public void CreatePasswordBasedAccount(string email, string password){
		auth.CreateUserWithEmailAndPasswordAsync (email, password).ContinueWith (task => {
			if (task.IsCanceled) {
				//Debug.LogWarning ("Create user canceled");
				return;
			}
			if (task.IsFaulted) {
				//Debug.LogWarning ("create user canceled");
				return;
			}
			Debug.Log("creating user email: " +email + " password : " + password);
			FirebaseUser newUser = task.Result;
			user = newUser;
			MyFirebaseStack.instance.email = email;
			MyFirebaseStack.instance.password = password;
			MyFirebaseStack.instance.uId = RemoveSpecialCharacters(newUser.UserId);
			MyFirebaseStack.instance.displayName = newUser.DisplayName;

			//EnterExistingUsers (email, password);
		});
	}

	public void EnterExistingUsers(string email, string password){
		auth.SignInWithEmailAndPasswordAsync (email, password).ContinueWith (task =>{
			if (task.IsCanceled) {
				//Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
				return;
			}
			if (task.IsFaulted) {
				//Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
				return;
			}

			FirebaseUser newUser = task.Result;
			//Debug.LogFormat("Exisist User signed in successfully: {0} ({1})",
			//	newUser.DisplayName, newUser.UserId);
			MyFirebaseStack.instance.uId = RemoveSpecialCharacters(newUser.UserId);
			MyFirebaseStack.instance.displayName = newUser.DisplayName;
			InitializeFirebase ();
			GetCredential (email, password);
		});
	}

	void GetCredential(string email, string password){
		Credential credential = EmailAuthProvider.GetCredential(email, password);
		auth.SignInWithCredentialAsync(credential).ContinueWith(task => {
			if (task.IsCanceled) {
				//Debug.LogError("SignInWithCredentialAsync was canceled.");
				return;
			}
			if (task.IsFaulted) {
				//Debug.LogError("SignInWithCredentialAsync encountered an error: " + task.Exception);
				return;
			}

			FirebaseUser newUser = task.Result;
			//Debug.LogFormat("Credential User signed in successfully: {0} ({1})",
			//	newUser.DisplayName, newUser.UserId);
		});
	}

	void InitializeFirebase(){
		auth = FirebaseAuth.DefaultInstance;
		auth.StateChanged += Auth_StateChanged;
		Auth_StateChanged (this, null);
	}

	void Auth_StateChanged (object sender, System.EventArgs e)
	{
		if (auth.CurrentUser != user) {
			bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
			if (!signedIn && user != null) {
				//Debug.Log("Signed out " + user.UserId);
			}
			user = auth.CurrentUser;
			if (signedIn) {
				//Debug.Log("Signed in " + user.UserId);
			}
		}
	}

	public class CurrentUser{
		public string userName;
		public string email;
		System.Uri photo_url;

		public CurrentUser(string userName, string email, System.Uri url){
			this.userName = userName;
			this.email = email;
			this.photo_url = url;
		}
	}
}
*/