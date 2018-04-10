using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_Inventory_Operators : MC_Inventory {

	protected List<IABOperator> allOperators;

	[SerializeField]
	protected UnityEngine.UI.Dropdown returnTypeDropdown;

	// Use this for initialization
	protected void Start () {
		base.Start ();

		// Load Operators
		allOperators = new List<IABOperator> ();
		foreach (OperatorType operatorType in System.Enum.GetValues( typeof( OperatorType ) )) {
			if (operatorType != OperatorType.None) {
				try{
					allOperators.Add ( ABOperatorFactory.CreateOperator( operatorType ) );
				}
				catch(System.NotImplementedException ex){
					Debug.Log (ex);
				}
			}
		}
		setItems ( listToObject<IABOperator>( allOperators ));

		loadReturnTypeDropdown ();

		returnTypeDropdown.onValueChanged.AddListener (changeReturnTypeDropdown);
		changeReturnTypeDropdown ( returnTypeDropdown.value );
	}

	protected List<System.Object> listToObject<T>( List<T> list ){
		List<System.Object> result = new List<object> ();

		foreach( T item in list ){
			result.Add ( item );
		}

		return result;
	}

	#region Filter
	protected void loadReturnTypeDropdown(){
		returnTypeDropdown.ClearOptions ();

		List<string> types = new List<string> ();
		foreach( ParamType type in System.Enum.GetValues( typeof( ParamType ) ) ){
			if (type != ParamType.None) {
				types.Add (type.ToString ());
			}
		}
		returnTypeDropdown.AddOptions ( types );
	}

	public void changeReturnTypeDropdown( int index ){
		setItems (listToObject<IABOperator>( filterReturnType (index, allOperators) ));
	}

	public List<IABOperator> filterReturnType( int index, List<IABOperator> operators ){
		// other types
		System.Type selectedType = ABModel.ParamTypeToType( (ParamType) System.Enum.GetValues( typeof( ParamType ) ).GetValue( index ) );
		List<IABOperator> result = new List<IABOperator>();
		foreach ( IABOperator oper in operators ) {
			if( oper.getOutcomeType() == selectedType ){
				result.Add ( oper );
			}
		}
		return result;
	}
	#endregion
	
	// Update is called once per frame
	protected void Update () {
		base.Update ();
	}

	#region implemented abstract members of MC_Inventory

	protected override void configItem (MC_InventoryItem item)
	{
		item.TextItem.text = MCEditor_Proxy.getNodeName((ABNode)item.Item);
		IABOperator op = ((IABOperator)item.Item);
		// Setting return type as title
		// item.Title = MCEditor_Proxy.getNodeName((ABNode)item.Item);
		item.Title = MCEditor_Proxy.typeToString( op.getOutcomeType() );
		// Setting param type as subtitle
		string subTitle = "";
		for(int i=0; i<op.Inputs.Length; i++){
			subTitle += (i>0?"\n":"") + MCEditor_Proxy.typeToString( op.getIncomeType(i) );
			if (ABStar<ABBool>.isStar (op.getIncomeType (i))) {
				break;
			}
		}
		item.SubTitle = subTitle;

		((MC_Inventory_NodeItem)item).ItemType = MC_Inventory_NodeItem.NodeItemType.Operator;
	}

	public override GameObject instantiateProxy (MC_InventoryItem item)
	{
		IABOperator oper = ((IABOperator)item.Item).Clone();
		return MCEditor_Proxy_Factory.instantiateOperator(oper, false).gameObject;
	}

	protected override void Drop (GameObject proxy, MC_InventoryItem item)
	{
		MCEditorManager.instance.registerOperator( proxy.GetComponent<ProxyABOperator>() );
	}

	#endregion
}
