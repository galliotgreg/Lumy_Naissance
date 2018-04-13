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
	Button closeMessageButton;
	[SerializeField]
	GameObject Logger;
	[SerializeField]
	ScrollRect ErrorList;
	[SerializeField]
	ScrollRect SelectedErrorPrompt;

	[SerializeField]
	GameObject[] amountErrors;

	[SerializeField]
	InGame_Message messagePrefab;

	#region Handle Messages
	public void addMessage( System.Exception newException ){
		string exceptionHash = Md5Sum( ExceptionToString( newException ) );
		if( ! currentMessages.ContainsKey( exceptionHash ) ){
			string title = "Error";
			string message = "Message";
			// Defining Title
			if (newException is MC_Exception) {
				MC_Exception mcException = (MC_Exception)newException;
				title = mcException.Title;
				message = mcException.getMessage ();
			} else {
				title = newException.Message;
				message = newException.Message;
			}

			currentMessages.Add ( exceptionHash, InGame_Message.instantiate (newException, title, message, ErrorList.content.transform, messagePrefab) );

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
		// If the exception is not Selected, select it
		if (exception != selectedMessage) {
			// Hide previous
			hideMessage (selectedMessage);

			// Show current
			showMessage( exception );

			selectedMessage = exception;
		
		// If the exception is Selected, hide it
		} else {
			hideMessage ( selectedMessage );
			selectedMessage = null;
		}
	}
	public void deactivateAll(){
		activateMessage (null);
	}
	public void showMessage( InGame_Message exception ){
		if (exception != null) {
			SelectedErrorPrompt.gameObject.SetActive (true);

			// Clear container and fill with the new ones
			// Clear
			for(int i=0; i<SelectedErrorPrompt.content.childCount; i++){
				Destroy (SelectedErrorPrompt.content.GetChild (i).gameObject);
			}
			// Fill
			exception.showMessage ( SelectedErrorPrompt.content.transform );
		}
	}
	public void hideMessage( InGame_Message exception ){
		SelectedErrorPrompt.gameObject.SetActive (false);
		if (exception != null) {
			exception.hideMessage();
		}
	}
	#endregion

	#region Activate/Deactivate
	public void show(){
        if(Logger.active == false)
        {
            Logger.SetActive(true);

            //activateButton.gameObject.SetActive (false);

            // close all messages
            deactivateAll();

            UpdateAmount();

        }
        else
        {
            Logger.SetActive(false);
           // activateButton.gameObject.SetActive(true);

            UpdateAmount();
        }


    }
	
	#endregion

	// Use this for initialization
	void Start () {
		activateButton.onClick.AddListener (show);
		//deactivateButton.onClick.AddListener (hide);
		closeMessageButton.onClick.AddListener (deactivateAll);

        Logger.SetActive(false);
        // activateButton.gameObject.SetActive(true);
        UpdateAmount();
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
	public static string ExceptionToString( System.Exception exception ){
		if (exception is MC_Exception) {
			MC_Exception mcException = (MC_Exception)exception;
			return "[ Cast = " + mcException.Cast + " ]" + mcException.getMessage ();
		}
		return exception.Message;
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
