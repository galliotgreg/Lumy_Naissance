using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MCEditor_ProxyIcon_Manager : MonoBehaviour {

	#region SINGLETON
	// The static instance of the Singleton for external access
	public static MCEditor_ProxyIcon_Manager instance = null;

	// Enforce Singleton properties
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

	[SerializeField]
	private string iconsFolder = "MCEditor_Proxy_Icons/";

	public Sprite getItemImage( System.Object item ){
		string imageFileName = "";

		if (item is ABState) {
			ABState state = (ABState)item;

			if( state.Action != null ){
				string prefix = "Action/Action_";
				imageFileName = prefix + getAction(state.Action.Type);
			}
		} else if (item is IABOperator) {
			string prefix = "Operator/Operator_";
			IABOperator operat = (IABOperator)item;

			imageFileName = prefix + getOperatorCategorie(operat.OpType);
		} else if (item is IABParam) {
			string prefix = "Parameter/Parameter_";
			IABParam param = (IABParam)item;

			imageFileName = prefix + getParam(param);
		}

		return getItemImage ( imageFileName );
	}

	Sprite getItemImage( string itemName ){
		try{
			return Resources.Load<Sprite> ( iconsFolder+itemName );
		}catch( System.Exception ex ){
			return null;
		}
	}

	#region DICTIONARIES
	string getAction( ActionType action ){
		return action.ToString();
	}

	string getParam( IABParam param ){
		if (param is ABColorParam) {
			ABColor color = ((ABColorParam)param).Value;
			return "Color_" + color.Value.ToString();
		}
		else if (param is ABTextParam) {
			return "Text";
		}
		return "";
	}

	string getOperatorCategorie( OperatorType op ){
		switch (op) {
		case OperatorType.Bool_And_Bool_Bool:
			return "And";
		case OperatorType.Bool_Or_Bool_Bool:
			return "Or";
		case OperatorType.Bool_Not_Bool:
			return "Not";
		case OperatorType.Bool_IsSet_Bool:
		case OperatorType.Bool_IsSet_Color:
		case OperatorType.Bool_IsSet_Ref:
		case OperatorType.Bool_IsSet_Scal:
		case OperatorType.Bool_IsSet_Txt:
		case OperatorType.Bool_IsSet_Vec:
			return "IsSet";
		case OperatorType.Bool_Equals_Bool_Bool:
		case OperatorType.Bool_Equals_Color_Color:
		case OperatorType.Bool_RefEquals_Ref_Ref:
		case OperatorType.Bool_Equals_Scal_Scal:
		case OperatorType.Bool_Equals_Txt_Txt:
		case OperatorType.Bool_Equals_Vec_Vec:
			return "Equals";
		case OperatorType.Bool_GreaterThan_Scal_Scal:
			return "Greater";
		case OperatorType.Bool_LessThan_Scal_Scal:
			return "Smaller";
		case OperatorType.Bool_NotEquals_Bool_Bool:
		case OperatorType.Bool_NotEquals_Color_Color:
		case OperatorType.Bool_NotEquals_Ref_Ref:
		case OperatorType.Bool_NotEquals_Scal_Scal:
		case OperatorType.Bool_NotEquals_Txt_Txt:
		case OperatorType.Bool_NotEquals_Vec_Vec:
			return "NotEquals";
		case OperatorType.ScalTab_Dist_VecTab_Vec:
			return "DistVTab";
		case OperatorType.Scal_Dist_Vec_Vec:
			return "DistV";
		case OperatorType.Scal_Div_Scal_Scal:
		case OperatorType.Vec_TermDiv_Vec_Vec:
			return "Div";
		case OperatorType.Scal_Prod_Scal_Scal:
		case OperatorType.Vec_TermProd_Vec_Vec:
		case OperatorType.Vec_Prod_Vec_Scal:
		case OperatorType.Vec_Prod_Scal_Vec:
			return "Prod";
		case OperatorType.Scal_Sum_Scal_Scal:
		case OperatorType.Vec_Sum_Vec_Vec:
			return "Sum";
		case OperatorType.Scal_Sub_Scal_Scal:
		case OperatorType.Vec_Sub_Vec_Vec:
			return "Sub";
		case OperatorType.Vec_RandCircle_Vec_Scal:
			return "RandCircle";
		case OperatorType.Bool_Get_BoolTab_Scal:
		case OperatorType.Color_Get_ColorTab_Scal:
		case OperatorType.Ref_Get_RefTab_Scal:
		case OperatorType.Scal_Get_ScalTab_Scal:
		case OperatorType.Vec_Get_VecTab_Scal:
		case OperatorType.Txt_Get_TxtTab_Scal:
			return "GetIndex";
		case OperatorType.BoolTab_Get_RefTab_Txt:
		case OperatorType.VecTab_Get_Ref_Txt:
		case OperatorType.VecTab_Get_RefTab_Txt:
		case OperatorType.ScalTab_Get_RefTab_Txt:
			return "GetTab";
		case OperatorType.Bool_Get_Ref_Txt:
		case OperatorType.Color_Get_Ref_Txt:
		case OperatorType.Ref_Get_Ref_Txt:
		case OperatorType.Scal_Get_Ref_Txt:
		case OperatorType.Vec_Get_Ref_Txt:
		case OperatorType.Txt_Get_Ref_Txt:
			return "Get";
		case OperatorType.BoolTab_Agg_BoolStar:
		case OperatorType.ColorTab_Agg_ColorStar:
		case OperatorType.RefTab_Agg_RefStar:
		case OperatorType.ScalTab_Agg_ScalStar:
		case OperatorType.VecTab_Agg_VecStar:
		case OperatorType.TxtTab_Agg_TxtStar:
			return "Agg";
		case OperatorType.RefTab_WhereEquals_RefTab_Txt_Bool:
		case OperatorType.RefTab_WhereEquals_RefTab_Txt_Color:
		case OperatorType.RefTab_WhereEquals_RefTab_Txt_Ref:
		case OperatorType.RefTab_WhereEquals_RefTab_Txt_Scal:
		case OperatorType.RefTab_WhereEquals_RefTab_Txt_Txt:
		case OperatorType.RefTab_WhereEquals_RefTab_Txt_Vec:
			return "WhereEquals";
		case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Bool:
		case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Color:
		case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Ref:
		case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Scal:
		case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Txt:
		case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Vec:
			return "WhereNotEquals";
		case OperatorType.RefTab_WhereGreater_RefTab_Txt_Scal:
			return "WhereGreater";
		case OperatorType.RefTab_WhereLess_RefTab_Txt_Scal:
			return "WhereSmaller";
		case OperatorType.VecTab_Reverse_VecTab:
			return "Reverse";
		case OperatorType.Scal_MinVal_ScalTab:
			return "MinVal";
		case OperatorType.Scal_MaxVal_ScalTab:
			return "MaxVal";
		case OperatorType.Scal_MinId_ScalTab:
			return "MinId";
		case OperatorType.Scal_MaxId_ScalTab:
			return "MaxId";
		case OperatorType.Scal_Size_BoolTab:
		case OperatorType.Scal_Size_ColorTab:
		case OperatorType.Scal_Size_RefTab:
		case OperatorType.Scal_Size_ScalTab:
		case OperatorType.Scal_Size_TxtTab:
		case OperatorType.Scal_Size_VecTable:
			return "Size";
		}
		return "";
	}

	/*string getMacroOperatorCategorie( ABMacroOperatorFactory op ){
		switch (op) {
		case OperatorType.:
			return "And";
		}
		return "";
	}*/
	#endregion

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
