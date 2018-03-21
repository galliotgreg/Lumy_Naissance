using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_DialogBox_Param_String : MCEditor_DialogBox_Param {

	[SerializeField]
	BlockerDropdown_DialogBox valueType;	// Class to list items OR string

	[SerializeField]
	BlockerDropdown_DialogBox valueTypeItem;

	[SerializeField]
	UnityEngine.UI.InputField valueText;

	Dictionary< string, System.Type > types = new Dictionary< string, System.Type >(){
		{ "--Predefined--", null },
		{ "Game", typeof(GameParamsScript) },
		{ "Lumy", typeof(AgentScript) },
		{ "Home", typeof(HomeScript) },
		{ "Resource", typeof(ResourceScript) },
		{ "Trace", typeof(TraceScript) },
		{ "Cast", typeof(string) }
	};

	// Use this for initialization
	void Start () {
		base.Start ();

		valueType.DialogBox = this;
		valueTypeItem.DialogBox = this;
		valueType.onValueChanged.AddListener (LoadType);
		valueTypeItem.onValueChanged.AddListener (setItem);

		LoadTypes ();
	}

	// Update is called once per frame
	void Update () {
		base.Update ();
	}

	#region Value Types
	void LoadTypes(){
		valueType.ClearOptions ();
		valueType.AddOptions( new List<string>(types.Keys ) );
		LoadType (0);
	}

	// Change valueType
	void LoadType( int newIndex ){
		System.Type selectedType = types [new List<string>(types.Keys )[valueType.value]];
		valueText.ActivateInputField (); // set focus

		if (selectedType == null) {
			valueTypeItem.interactable = false;
		} else {
			valueTypeItem.interactable = true;
			valueTypeItem.ClearOptions ();

			if (selectedType == typeof(string)) { // Load Casts
				List<string> newItems = itemsFromCasts();
				valueTypeItem.AddOptions (newItems);
			} else { // open DropDown
				List<string> newItems = itemsFromType (selectedType);
				valueTypeItem.AddOptions (newItems);
			}
		}
	}

	List<string> itemsFromType( System.Type type ){
		List<string> result = new List<string> ();
		result.Add ("--");
		foreach (System.Reflection.MemberInfo field in type.GetFields(System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)) {
			if (field.Name != "key" && field.GetCustomAttributes (typeof(AttrName), false).Length > 0) {
				result.Add (field.Name);
			}
		}
		return result;
	}

	List<string> itemsFromCasts(){
		return new List<string> (AppContextManager.instance.GetSpeciesFolderNames());
	}

	// Change valueTypeItem
	void setItem(int newIndex){
		if (valueTypeItem.value != 0) {
			valueText.text = valueTypeItem.options [valueTypeItem.value].text;
		}
	}

	/*List<string> keysToStringList( Dictionary<string,System.Type>.KeyCollection keys ){
		List<string> result = new List<string> ();
		foreach
		return result;
	}*/
	#endregion

	#region implemented abstract members of MCEditor_DialogBox_Param

	protected override void confirmParam ()
	{
		((ABTextParam)this.paramProxy.AbParam).Value.Value = valueText.text;
	}

	protected override void configParam ()
	{
		valueText.text = ((ABTextParam)this.paramProxy.AbParam).Value.Value;
	}

	protected override void deactivateParam ()
	{
		// Nothing
	}

	protected override string dialogTitle ()
	{
		return "Text";
	}

	#endregion
}
