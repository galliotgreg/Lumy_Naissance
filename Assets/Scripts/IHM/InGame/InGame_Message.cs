using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InGame_Message : MonoBehaviour {

	System.Exception exception;
	string title;
	string message;

	[SerializeField]
	Button activateButton;
	[SerializeField]
	Text activateText;
	[SerializeField]
	Image activateImage;
	[SerializeField]
	Button removeButton;

	[SerializeField]
	Color selectedColor;
	Color originalColor;

	[SerializeField]
	InGame_MessageItem_Line LinePrefab;

	#region PROPERTIES
	public System.Exception Exception {
		get {
			return exception;
		}
		set {
			exception = value;
		}
	}
	#endregion

	#region Handle Messages
	public List<InGame_MessageItem_Line> showMessage( Transform container ){
		activateButton.GetComponent<Image> ().color = selectedColor;
		return new List<InGame_MessageItem_Line> (){ InGame_MessageItem_Line.instantiate (message, container, LinePrefab) };
	}
	public void hideMessage(){
		activateButton.GetComponent<Image> ().color = originalColor;
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
		originalColor = activateButton.GetComponent<Image> ().color;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Init( System.Exception exception, string title, string message ){
		this.Exception = exception;

		removeButton.onClick.AddListener ( removeMessage );
		activateButton.onClick.AddListener ( activateMessage );

		// fill exception
		this.title = title;
		this.message = message;
		activateText.text = title;
	}

	public static InGame_Message instantiate( System.Exception exception, string title, string message, Transform parent, InGame_Message prefab ){
		InGame_Message result = Instantiate<InGame_Message> (prefab, parent);
		result.Init (exception, title, message);

		result.hideMessage ();
		return result;
	}
}
