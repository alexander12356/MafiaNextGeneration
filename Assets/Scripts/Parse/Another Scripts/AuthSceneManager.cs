using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Collections;
using System.Security.Cryptography;

public class AuthSceneManager : MonoBehaviour {


	[SerializeField]
	public Button signIn;
	public Button logIn;
	public Button logOut;
	public InputField email;
	public InputField password;
	public InputField confirm;
	public Text warrningText;
	public GameObject auth;
	public GameObject afterAuth;
	public bool isNewUser;
	bool showText;

	public bool allCorrect {
		get{ 
			return ValidateInputFields;
		}
	}

	void Start () {
		signIn.onClick.AddListener (() => CheckInputFileds ());
		logIn.onClick.AddListener (() => CheckInputFileds ());
		logOut.onClick.AddListener (() => Logout());

		StartCoroutine (InvokeWarrningState ());
		logIn.gameObject.SetActive (false);
		SwitchFormsAfterAuth (ParseControl.instance.CurrentUser ());
	}

	void Update () {
		if (Input.GetKeyUp (KeyCode.P)) {
			ParseControl.instance.Request (Templates.Request.Requests.logOutRoom);
		}
	}

	IEnumerator InvokeWarrningState(){
		float t = 0;
		while (t < 11 && showText) {
			t += Time.deltaTime;
			if (t < 1)
				warrningText.CrossFadeAlpha (1, .5f, false);
			else if (t > 9 && t < 10) {
				warrningText.CrossFadeAlpha (0, .9f, false);
			}
			if (t > 10) {
				showText = false;
				warrningText.text = "";
			}
			yield return null;
		}
	}
	public void CheckInputFileds(){
		if (ValidateInputFields) {
			Debug.Log ("allcorrect");
		} else {
			Debug.Log ("error");
		}
	}

	public void SetSignIn(){
		isNewUser = !isNewUser;
		confirm.gameObject.SetActive ((isNewUser) ? true : false);
		logIn.gameObject.SetActive((isNewUser) ? false : true);
		signIn.gameObject.SetActive((isNewUser) ? true : false);
	}

	public void Logout(){
		ParseControl.instance.LogOut ();
		SwitchFormsAfterAuth (false);
	}

	private bool ValidateInputFields {
		get{
			if (password.text.Length > 6 && password.text.Length < 16) {
				warrningText.text = "";
				MD5CryptoServiceProvider md5s = new MD5CryptoServiceProvider();
				byte[] bytes = Encoding.UTF8.GetBytes (password.text);
				byte[] hash = md5s.ComputeHash (bytes);

				StringBuilder sb = new StringBuilder ();
				for (int i = 0; i < hash.Length; i++) {
					sb.Append (hash [i].ToString ("x2"));
				}

				if (isNewUser) {
					ParseControl.instance.SignUp (email.text, sb.ToString ());
					SwitchFormsAfterAuth (true);
				} else {
					ParseControl.instance.LogIn (email.text, sb.ToString ());
					SwitchFormsAfterAuth (true);
				}
				return true;
			} else {
				warrningText.text = "длина пароля должна быть больше 6, но меньше 16";
				showText = true;
				return false;
			}
		}
	}
	private void OnDestroy(){
		
	}

	private void SwitchFormsAfterAuth(bool state){
		afterAuth.SetActive (state);
		auth.SetActive (!state);
		if (state) {
			ParseControl.instance.Request (Templates.Request.Requests.getRoom);
		}
	}
}
