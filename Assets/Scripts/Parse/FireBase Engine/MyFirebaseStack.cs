/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyFirebaseStack : MonoBehaviour {
	public static MyFirebaseStack instance;

	public string displayName {
		get;
		set;
	}
	public string email {
		get;
		set;
	}
	public string password {
		get;
		set;
	}
	public string uId {
		get;
		set;
	}

	void Awake(){
		DontDestroyOnLoad (gameObject);
		if (instance == null)
			instance = this;
		else
			return;
	}

	private string generatedPassword{
		get
		{
			int minCharAmount = 6;
			int maxCharAmount = 8;
			string newPass = null;

			string glyphs= "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
			int charAmount = Random.Range(minCharAmount, maxCharAmount); //set those to the minimum and maximum length of your string
			for(int i=0; i<charAmount; i++)
			{
				newPass += glyphs[Random.Range(0, glyphs.Length)];
			}
			return newPass;
		}
	}

	public void CreateUser(){
		if (email != null) {
				MyFireBaseAuth.instance.CreatePasswordBasedAccount (email, generatedPassword);
		}
		else
			Debug.LogError ("email empty");
	}

	public void CreateDataBaseUSer(){
		MyFirebaseDatabase.instance.WriteNewUser (uId, displayName, 0, 0);
	}

	public void Login(){
		string l_email = "testNickName@gmail.com";
		string l_password = "testtest123";

		MyFireBaseAuth.instance.EnterExistingUsers (l_email, l_password);
	}

}*/
