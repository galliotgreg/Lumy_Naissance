using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debugger_Node : MonoBehaviour {

	[SerializeField]
	UnityEngine.UI.Text text;

	public enum NodeType{
		State, Action, Operator, Param
	};

	NodeType itemType;

	ABState state;

	/// <summary>
	/// The image used to show the node
	/// </summary>
	[SerializeField]
	UnityEngine.UI.Image NodeImage;

	/// <summary>
	/// The sprite loaded for a State Image
	/// </summary>
	[SerializeField]
	protected Sprite StateImage;
	/// <summary>
	/// The sprite loaded for a Action Image
	/// </summary>
	[SerializeField]
	protected Sprite ActionImage;
	/// <summary>
	/// The sprite loaded for a Operator Image
	/// </summary>
	[SerializeField]
	protected Sprite OperatorImage;
	/// <summary>
	/// The sprite loaded for a Param Image
	/// </summary>
	[SerializeField]
	protected Sprite ParamImage;

	public NodeType ItemType {
		get {
			return itemType;
		}
		set {
			itemType = value;
			setImage ();
		}
	}

	public ABState State {
		get {
			return state;
		}
		set {
			state = value;
			text.text = value.Name;

			if (state.Action == null) {
				// State
				ItemType = NodeType.State;
			} else {
				// State
				ItemType = NodeType.Action;
			}
		}
	}

	void setImage(){
		switch( itemType ){
		case NodeType.State:
			NodeImage.sprite = StateImage;
			break;
		case NodeType.Action:
			NodeImage.sprite = ActionImage;
			break;
		case NodeType.Operator:
			NodeImage.sprite = OperatorImage;
			break;
		case NodeType.Param:
			NodeImage.sprite = ParamImage;
			break;
		}
	}

	// Use this for initialization
	void Start () {
		RectTransform rect = GetComponent<RectTransform>();
		rect.localScale = new Vector3 (1, 1, 1);
	}

	// Update is called once per frame
	void Update () {		
	}

	#region Instantiate
	public static Debugger_Node instantiate( Debugger_Node prefab, ABState state, bool init, Transform parent ){
		Debugger_Node result = Instantiate<Debugger_Node> ( prefab, parent );

		result.State = state;
		return result;
	}
	#endregion
}
