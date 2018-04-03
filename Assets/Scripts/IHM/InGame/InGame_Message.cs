using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame_Message : MonoBehaviour {

	MC_Exception exception;

	[SerializeField]
	GameObject container;
	[SerializeField]
	Button activateButton;
	[SerializeField]
	Text activateText;
	[SerializeField]
	Image activateImage;
	[SerializeField]
	Button removeButton;

	[SerializeField]
	InGame_MessageItem_Line LinePrefab;

	#region PROPERTIES
	public MC_Exception Exception {
		get {
			return exception;
		}
		set {
			exception = value;
		}
	}
	#endregion

	#region Handle Messages
	public void showMessage(){
		container.SetActive(true);
	}
	public void hideMessage(){
		container.SetActive(false);
	}
	public void activateMessage(){
		InGame_MessageManager.instance.activateMessage (this);
	}
	public void removeMessage(){
		InGame_MessageManager.instance.removeMessage (this);
	}
	#endregion

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init( MC_Exception exception ){
		this.Exception = exception;

		removeButton.onClick.AddListener ( removeMessage );
		activateButton.onClick.AddListener ( activateMessage );

		// fill exception
		activateText.text = exception.Title;
		InGame_MessageItem_Line.instantiate( exception.Message, container.transform, LinePrefab );
	}

	public static InGame_Message instantiate( MC_Exception exception, Transform parent, InGame_Message prefab ){
		InGame_Message result = Instantiate<InGame_Message> (prefab, parent);
		result.Init (exception);

		result.hideMessage ();
		return result;
	}
}
