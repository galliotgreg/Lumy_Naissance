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

	[SerializeField]
	private GameObject imagePrefab;

	public Sprite getItemImage( System.Object item ){
		string imageFileName = "";

		if (item is ABState) {
			ABState state = (ABState)item;

			if (state.Action != null) {
				string prefix = "Action/Action_";
				imageFileName = prefix + getAction (state.Action.Type);
			}
		} else if (item is IABOperator) {
			IABOperator operat = (IABOperator)item;
			// Macro
			if( ABMacroOperator<ABBool>.isMacro( operat.GetType() )){
				string prefix = "MacroOperator/Macro_";

				imageFileName = prefix + getMacroOperator(operat);
			}
			else{
				string prefix = "Operator/Operator_";

				imageFileName = prefix + getOperatorCategorie(operat.OpType);
			}
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

	public GameObject getProxyImage( MCEditor_Proxy proxy ){
		Sprite imageSprite = null;

		if( proxy is ProxyABAction ){
			imageSprite = MCEditor_ProxyIcon_Manager.instance.getItemImage ( ((ProxyABAction)proxy).AbState );
		} else if( proxy is ProxyABOperator ){
			imageSprite = MCEditor_ProxyIcon_Manager.instance.getItemImage ( ((ProxyABOperator)proxy).AbOperator );
		} else if( proxy is ProxyABParam ){
			imageSprite = MCEditor_ProxyIcon_Manager.instance.getItemImage ( ((ProxyABParam)proxy).AbParam );
		}

		// Create Material
		if( imageSprite != null ){
			GameObject resultObject = Instantiate<GameObject>( imagePrefab, proxy.transform );
			resultObject.GetComponent<MeshRenderer>().material.mainTexture = imageSprite.texture;
			return resultObject;
		}
		return null;
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

	public static string getOperatorCategorie( OperatorType op ){
		switch (op) {
		case OperatorType.Bool_And_Bool_Bool:
			return "0_And";
		case OperatorType.Bool_Or_Bool_Bool:
			return "0_Or";
		case OperatorType.Bool_Not_Bool:
			return "0_Not";
		case OperatorType.Bool_IsSet_Bool:
		case OperatorType.Bool_IsSet_Color:
		case OperatorType.Bool_IsSet_Ref:
		case OperatorType.Bool_IsSet_Scal:
		case OperatorType.Bool_IsSet_Txt:
		case OperatorType.Bool_IsSet_Vec:
			return "1_IsSet";
		case OperatorType.Bool_Equals_Bool_Bool:
		case OperatorType.Bool_Equals_Color_Color:
		case OperatorType.Bool_RefEquals_Ref_Ref:
		case OperatorType.Bool_Equals_Scal_Scal:
		case OperatorType.Bool_Equals_Txt_Txt:
		case OperatorType.Bool_Equals_Vec_Vec:
			return "2_Equals";
		case OperatorType.Bool_GreaterThan_Scal_Scal:
			return "2_Greater";
		case OperatorType.Bool_LessThan_Scal_Scal:
			return "2_Smaller";
		case OperatorType.Bool_NotEquals_Bool_Bool:
		case OperatorType.Bool_NotEquals_Color_Color:
		case OperatorType.Bool_NotEquals_Ref_Ref:
		case OperatorType.Bool_NotEquals_Scal_Scal:
		case OperatorType.Bool_NotEquals_Txt_Txt:
		case OperatorType.Bool_NotEquals_Vec_Vec:
			return "2_NotEquals";
		case OperatorType.ScalTab_Dist_VecTab_Vec:
			return "3_DistVTab";
		case OperatorType.Scal_Dist_Vec_Vec:
			return "3_DistV";
		case OperatorType.Scal_Div_Scal_Scal:
		case OperatorType.Vec_TermDiv_Vec_Vec:
			return "3_Div";
		case OperatorType.Scal_Prod_Scal_Scal:
		case OperatorType.Vec_TermProd_Vec_Vec:
		case OperatorType.Vec_Prod_Vec_Scal:
		case OperatorType.Vec_Prod_Scal_Vec:
			return "3_Prod";
		case OperatorType.Scal_Sum_Scal_Scal:
		case OperatorType.Vec_Sum_Vec_Vec:
			return "3_Sum";
		case OperatorType.Scal_Sub_Scal_Scal:
		case OperatorType.Vec_Sub_Vec_Vec:
			return "3_Sub";
		case OperatorType.Vec_RandCircle_Vec_Scal:
			return "3_RandCircle";
		case OperatorType.Bool_Get_BoolTab_Scal:
		case OperatorType.Color_Get_ColorTab_Scal:
		case OperatorType.Ref_Get_RefTab_Scal:
		case OperatorType.Scal_Get_ScalTab_Scal:
		case OperatorType.Vec_Get_VecTab_Scal:
		case OperatorType.Txt_Get_TxtTab_Scal:
			return "4_GetIndex";
		case OperatorType.BoolTab_Get_RefTab_Txt:
		case OperatorType.VecTab_Get_Ref_Txt:
		case OperatorType.VecTab_Get_RefTab_Txt:
		case OperatorType.ScalTab_Get_RefTab_Txt:
			return "4_GetTab";
		case OperatorType.Bool_Get_Ref_Txt:
		case OperatorType.Color_Get_Ref_Txt:
		case OperatorType.Ref_Get_Ref_Txt:
		case OperatorType.Scal_Get_Ref_Txt:
		case OperatorType.Vec_Get_Ref_Txt:
		case OperatorType.Txt_Get_Ref_Txt:
			return "4_Get";
		case OperatorType.BoolTab_Agg_BoolStar:
		case OperatorType.ColorTab_Agg_ColorStar:
		case OperatorType.RefTab_Agg_RefStar:
		case OperatorType.ScalTab_Agg_ScalStar:
		case OperatorType.VecTab_Agg_VecStar:
		case OperatorType.TxtTab_Agg_TxtStar:
			return "4_Agg";
		case OperatorType.RefTab_WhereEquals_RefTab_Txt_Bool:
		case OperatorType.RefTab_WhereEquals_RefTab_Txt_Color:
		case OperatorType.RefTab_WhereEquals_RefTab_Txt_Ref:
		case OperatorType.RefTab_WhereEquals_RefTab_Txt_Scal:
		case OperatorType.RefTab_WhereEquals_RefTab_Txt_Txt:
		case OperatorType.RefTab_WhereEquals_RefTab_Txt_Vec:
			return "5_WhereEquals";
		case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Bool:
		case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Color:
		case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Ref:
		case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Scal:
		case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Txt:
		case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Vec:
			return "5_WhereNotEquals";
		case OperatorType.RefTab_WhereGreater_RefTab_Txt_Scal:
			return "5_WhereGreater";
		case OperatorType.RefTab_WhereLess_RefTab_Txt_Scal:
			return "5_WhereSmaller";
		case OperatorType.VecTab_Reverse_VecTab:
			return "6_Reverse";
		case OperatorType.Scal_MinVal_ScalTab:
			return "7_MinVal";
		case OperatorType.Scal_MaxVal_ScalTab:
			return "7_MaxVal";
		case OperatorType.Scal_MinId_ScalTab:
			return "7_MinId";
		case OperatorType.Scal_MaxId_ScalTab:
			return "7_MaxId";
		case OperatorType.Scal_Size_BoolTab:
		case OperatorType.Scal_Size_ColorTab:
		case OperatorType.Scal_Size_RefTab:
		case OperatorType.Scal_Size_ScalTab:
		case OperatorType.Scal_Size_TxtTab:
		case OperatorType.Scal_Size_VecTable:
			return "7_Size";
		}
		return "";
	}

	string getMacroOperator( IABOperator op ){
		int macroIndex = op.SymbolName.LastIndexOf ("_macro");
		return op.SymbolName.Substring (0, macroIndex);
	}
	#endregion

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
