using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame_MessageManager : MonoBehaviour {

	#region SINGLETON
	/// <summary>
	/// The static instance of the Singleton for external access
	/// </summary>
	public static InGame_MessageManager instance = null;

	/// <summary>
	/// Enforce Singleton properties
	/// </summary>
	void Awake()
	{
		//Check if instance already exists and set it to this if not
		if (instance == null)
		{
			instance = this;
		}

		//Enforce the unicity of the Singleton
		else if (instance != this)
		{
			Destroy(gameObject);
		}
	}
	#endregion

	Dictionary<string, InGame_Message> currentMessages = new Dictionary<string, InGame_Message> ();
	InGame_Message selectedMessage = null;

	[SerializeField]
	Button activateButton;
	[SerializeField]
	Button deactivateButton;
	[SerializeField]
	GameObject logger;
	[SerializeField]
	Transform loggerContainer;

	[SerializeField]
	GameObject[] amountErrors;

	[SerializeField]
	InGame_Message messagePrefab;

	bool first = true;
	#region Handle Messages
	public void addMessage( MC_Exception newException ){
		string exceptionHash = Md5Sum( ExceptionToString( newException ) );
		if( ! currentMessages.ContainsKey( exceptionHash ) ){
			currentMessages.Add ( exceptionHash, InGame_Message.instantiate (newException, loggerContainer, messagePrefab) );

			UpdateAmount ();
		}
	}
	public void removeMessage( InGame_Message exception ){
		string exceptionHash = Md5Sum( ExceptionToString( exception.Exception ) );

		//currentMessages.Remove ( exceptionHash );
		currentMessages[ exceptionHash ] = null;
		UpdateAmount ();

		Destroy (exception.gameObject);
	}
	public void activateMessage( InGame_Message exception ){
		if (exception != selectedMessage) {
			// Hide previous
			if (selectedMessage != null) {
				hideMessage (selectedMessage);
			}

			// Show current
			showMessage( exception );
			selectedMessage = exception;
		} else {
			hideMessage ( exception );
			selectedMessage = null;
		}
	}
	public void showMessage( InGame_Message exception ){
		exception.showMessage ();
	}
	public void hideMessage( InGame_Message exception ){
		exception.hideMessage ();
	}
	#endregion

	#region Activate/Deactivate
	public void show(){
		logger.SetActive (true);
		activateButton.gameObject.SetActive (false);
		UpdateAmount ();
	}
	public void hide(){
		logger.SetActive (false);
		activateButton.gameObject.SetActive (true);
		UpdateAmount ();
	}
	#endregion

	// Use this for initialization
	void Start () {
		activateButton.onClick.AddListener (show);
		deactivateButton.onClick.AddListener (hide);

		hide ();
	}
	
	// Update is called once per frame
	void Update () {
	}

	void UpdateAmount(){
		int amount = 0;
		foreach (InGame_Message mes in currentMessages.Values) {
			if (mes != null) {
				amount++;
			}
		}
		foreach (GameObject amountContainer in amountErrors) {
			if (amount > 0) {
				amountContainer.SetActive (true);
				amountContainer.GetComponentInChildren<Text> ().text = amount.ToString ();
			} else {
				amountContainer.SetActive (false);
			}
		}
	}

	#region MD5
	public static string ExceptionToString( MC_Exception exception ){
		return "[ Cast = "+exception.Cast+" ]"+exception.Message;
	}

	public static string Md5Sum(string strToEncrypt)
	{
		System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
		byte[] bytes = ue.GetBytes(strToEncrypt);

		// encrypt bytes
		System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
		byte[] hashBytes = md5.ComputeHash(bytes);

		// Convert the encrypted bytes back to a string (base 16)
		string hashString = "";

		for (int i = 0; i < hashBytes.Length; i++)
		{
			hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
		}

		return hashString.PadLeft(32, '0');
	}
	#endregion
}
