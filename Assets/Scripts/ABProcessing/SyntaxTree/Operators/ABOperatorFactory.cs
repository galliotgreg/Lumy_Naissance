using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABOperatorFactory {

    public static IABOperator CreateOperator(String typeStr)
    {
        OperatorType type = GetTypeFromStr(typeStr);
        return CreateOperator(type);
    }

    private static OperatorType GetTypeFromStr(string typeStr)
    {
        OperatorType type = OperatorType.None;

        switch (typeStr)
        {
            case "B&&BB":
                type = OperatorType.Bool_And_Bool_Bool;
                break;
            case "B[]aggB*":
                type = OperatorType.BoolTab_Agg_BoolStar;
                break;
            case "B==BB":
                type = OperatorType.Bool_Equals_Bool_Bool;
                break;
            case "BgetRT":
                type = OperatorType.Bool_Get_Ref_Txt;
                break;
            case "B[]getR[]T":
                type = OperatorType.BoolTab_GetRefTab_Txt;
                break;
            case "BgetB[]S":
                type = OperatorType.Bool_Get_BoolTab_Scal;
                break;
            case "BisSetB":
                type = OperatorType.Bool_IsSet_Bool;
                break;
            case "B!=BB":
                type = OperatorType.Bool_NotEquals_Bool_Bool;
                break;
            case "SsizeB[]":
                type = OperatorType.Scal_Size_BoolTab;
                break;
            case "C[]aggC*":
                type = OperatorType.ColorTab_Agg_CStar;
                break;
            case "B==CC":
                type = OperatorType.Bool_Equals_Color_Color;
                break;
            case "CgetRT":
                type = OperatorType.Color_Get_Ref_Txt;
                break;
            case "C[]getR[]T":
                type = OperatorType.ColorTab_Get_RefTab_Txt;
                break;
            case "CgetC[]S":
                type = OperatorType.Color_Get_ColorTab_Scal;
                break;
            case "BisSetC":
                type = OperatorType.Bool_IsSet_Color;
                break;
            case "B!=CC":
                type = OperatorType.Bool_NotEquals_Color_Color;
                break;
            case "SsizeC[]":
                type = OperatorType.Scal_Size_ColorTab;
                break;
            case "SdistVV":
                type = OperatorType.Scal_Dist_Vec_Vec;
                break;
            case "S[]distV[]V":
                type = OperatorType.ScalTab_Dist_VecTab_Vec;
                break;
            case "B>SS":
                type = OperatorType.Bool_GreaterThan_Scal_Scal;
                break;
            case "B<SS":
                type = OperatorType.Bool_LessThan_Scal_Scal;
                break;
            case "SmaxIdS[]":
                type = OperatorType.Scal_MaxId_ScalTab;
                break;
            case "SmaxValS[]":
                type = OperatorType.Scal_MaxVal_ScalTab;
                break;
            case "SminIdS[]":
                type = OperatorType.Scal_MinId_ScalTab;
                break;
            case "SminValS[]":
                type = OperatorType.Scal_MinVal_ScalTab;
                break;
            case "B!B":
                type = OperatorType.Bool_Not_Bool;
                break;
            case "B||BB":
                type = OperatorType.Bool_Or_Bool_Bool;
                break;
            case "VrandCircleVS":
                type = OperatorType.Vec_RandCircle_Vec_Scal;
                break;
            case "B==RR":
                type = OperatorType.Bool_RefEquals_Ref_Ref;
                break;
            case "R[]aggR*":
                type = OperatorType.RefTab_Agg_RefStar;
                break;
            case "RgetRT":
                type = OperatorType.Ref_Get_Ref_Txt;
                break;
            case "R[]getR[]T":
                type = OperatorType.RefTab_Get_RefTab_Txt;
                break;
            case "R[]where==R[]TB":
                type = OperatorType.RefTab_WhereEquals_RefTab_Txt_Bool;
                break;
            case "R[]where==R[]TC":
                type = OperatorType.RefTab_WhereEquals_RefTab_Txt_Color;
                break;
            case "R[]where==R[]TR":
                type = OperatorType.RefTab_WhereEquals_RefTab_Txt_Ref;
                break;
            case "R[]where==R[]TS":
                type = OperatorType.RefTab_WhereEquals_RefTab_Txt_Scal;
                break;
            case "R[]where==R[]TT":
                type = OperatorType.RefTab_WhereEquals_RefTab_Txt_Txt;
                break;
            case "R[]where==R[]TV":
                type = OperatorType.RefTab_WhereEquals_RefTab_Txt_Vec;
                break;
            case "R[]where<R[]TS":
                type = OperatorType.RefTab_WhereLess_RefTab_Txt_Scal;
                break;
            case "R[]where>R[]TS":
                type = OperatorType.RefTab_WhereGreater_RefTab_Txt_Scal;
                break;
            case "R[]where!=R[]TB":
                type = OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Bool;
                break;
            case "R[]where!=R[]TC":
                type = OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Color;
                break;
            case "R[]where!=R[]TR":
                type = OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Ref;
                break;
            case "R[]where!=R[]TS":
                type = OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Scal;
                break;
            case "R[]where!=R[]TT":
                type = OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Txt;
                break;
            case "R[]where!=R[]TV":
                type = OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Vec;
                break;
            case "RgetR[]S":
                type = OperatorType.Ref_Get_RefTab_Scal;
                break;
            case "BisSetR":
                type = OperatorType.Bool_IsSet_Ref;
                break;
            case "B!=RR":
                type = OperatorType.Bool_NotEquals_Ref_Ref;
                break;
            case "SsizeR[]":
                type = OperatorType.Scal_Size_RefTab;
                break;
            case "S[]aggS*":
                type = OperatorType.ScalTab_Agg_ScalStar;
                break;
            case "S/SS":
                type = OperatorType.Scal_Div_Scal_Scal;
                break;
            case "B==SS":
                type = OperatorType.Bool_Equals_Scal_Scal;
                break;
            case "SgetRT":
                type = OperatorType.Scal_Get_Ref_Txt;
                break;
            case "S[]getR[]T":
                type = OperatorType.ScalTab_Get_RefTab_Txt;
                break;
            case "SgetS[]S":
                type = OperatorType.Scal_Get_ScalTab_Scal;
                break;
            case "BisSetS":
                type = OperatorType.Bool_IsSet_Scal;
                break;
            case "B!=SS":
                type = OperatorType.Bool_NotEquals_Scal_Scal;
                break;
            case "S*SS":
                type = OperatorType.Scal_Prod_Scal_Scal;
                break;
            case "SsizeS[]":
                type = OperatorType.Scal_Size_ScalTab;
                break;
            case "S-SS":
                type = OperatorType.Scal_Sub_Scal_Scal;
                break;
            case "S+SS":
                type = OperatorType.Scal_Sum_Scal_Scal;
                break;
            case "T[]aggT*":
                type = OperatorType.TxtTab_Agg_TxtStar;
                break;
            case "B==TT":
                type = OperatorType.Bool_Equals_Txt_Txt;
                break;
            case "TgetRT":
                type = OperatorType.Txt_Get_Ref_Txt;
                break;
            case "T[]getR[]T":
                type = OperatorType.TxtTab_Get_RefTab_Txt;
                break;
            case "TgetT[]S":
                type = OperatorType.Txt_Get_TxtTab_Scal;
                break;
            case "BisSetT":
                type = OperatorType.Bool_IsSet_Txt;
                break;
            case "B!=TT":
                type = OperatorType.Bool_NotEquals_Txt_Txt;
                break;
            case "SsizeT[]":
                type = OperatorType.Scal_Size_TxtTab;
                break;
            case "V[]aggV*":
                type = OperatorType.VecTab_Agg_VecStar;
                break;
            case "VXVV":
                type = OperatorType.Vec_Cross_Vec_Vec;
                break;
            case "S.VV":
                type = OperatorType.Scal_Dot_Vec_Vec;
                break;
            case "B==VV":
                type = OperatorType.Bool_Equals_Vec_Vec;
                break;
            case "VgetRT":
                type = OperatorType.Vec_Get_Ref_Txt;
                break;
            case "V[]getR[]T":
                type = OperatorType.VecTab_Get_RefTab_Txt;
                break;
            case "VgetV[]S":
                type = OperatorType.Vec_Get_VecTab_Scal;
                break;
            case "BisSetV":
                type = OperatorType.Bool_IsSet_Vec;
                break;
            case "B!=VV":
                type = OperatorType.Bool_NotEquals_Vec_Vec;
                break;
            case "V*VS":
                type = OperatorType.Vec_Prod_Vec_Scal;
                break;
            case "V*SV":
                type = OperatorType.Vec_Prod_Scal_Vec;
                break;
            case "SsizeV[]":
                type = OperatorType.Scal_Size_VecTable;
                break;
            case "V-VV":
                type = OperatorType.Vec_Sub_Vec_Vec;
                break;
            case "V+VV":
                type = OperatorType.Vec_Sum_Vec_Vec;
                break;
            case "V/VV":
                type = OperatorType.Vec_TermDiv_Vec_Vec;
                break;
            case "V*VV":
                type = OperatorType.Vec_TermProd_Vec_Vec;
                break;
            default:
                throw new NotImplementedException("Type " + typeStr + " not implemented");
        }

        return type;
    }

    public static IABOperator CreateOperator(OperatorType type)
    {
        IABOperator abOperator = null;
        switch (type)
        {
            case OperatorType.Bool_And_Bool_Bool:
                abOperator = new AB_Bool_And_Bool_Bool_Operator();
                break;
            case OperatorType.BoolTab_Agg_BoolStar:
                abOperator = new AB_BoolTab_Agg_BoolStar_Operator();
                break;
            case OperatorType.Bool_Equals_Bool_Bool:
                abOperator = new AB_Bool_Equals_Bool_Bool_Operator();
                break;
            case OperatorType.Bool_Get_Ref_Txt:
                abOperator = new AB_Bool_Get_Ref_Txt_Operator();
                break;
            case OperatorType.BoolTab_GetRefTab_Txt:
                abOperator = new AB_BoolTab_GetRefTab_Txt_Operator();
                break;
            case OperatorType.Bool_Get_BoolTab_Scal:
                abOperator = new AB_Bool_Get_BoolTab_Scal_Operator();
                break;
            case OperatorType.Bool_IsSet_Bool:
                abOperator = new AB_Bool_IsSet_Bool_Operator();
                break;
            case OperatorType.Bool_NotEquals_Bool_Bool:
                abOperator = new AB_Bool_NotEquals_Bool_Bool_Operator();
                break;
            case OperatorType.Scal_Size_BoolTab:
                abOperator = new AB_Scal_Size_BoolTab_Operator();
                break;
            case OperatorType.ColorTab_Agg_CStar:
                abOperator = new AB_ColorTab_Agg_CStar_Operator();
                break;
            case OperatorType.Bool_Equals_Color_Color:
                abOperator = new AB_Bool_Equals_Color_Color_Operator();
                break;
            case OperatorType.Color_Get_Ref_Txt:
                abOperator = new AB_Color_Get_Ref_Txt_Operator();
                break;
            case OperatorType.ColorTab_Get_RefTab_Txt:
                abOperator = new AB_ColorTab_Get_RefTab_Txt_Operator();
                break;
            case OperatorType.Color_Get_ColorTab_Scal:
                abOperator = new AB_Color_Get_ColorTab_Scal_Operator();
                break;
            case OperatorType.Bool_IsSet_Color:
                abOperator = new AB_Bool_IsSet_Color_Operator();
                break;
            case OperatorType.Bool_NotEquals_Color_Color:
                abOperator = new AB_Bool_NotEquals_Color_Color_Operator();
                break;
            case OperatorType.Scal_Size_ColorTab:
                abOperator = new AB_Scal_Size_ColorTab_Operator();
                break;
            case OperatorType.Scal_Dist_Vec_Vec:
                abOperator = new AB_Scal_Dist_Vec_Vec_Operator();
                break;
            case OperatorType.ScalTab_Dist_VecTab_Vec:
                abOperator = new AB_ScalTab_Dist_VecTab_Vec_Operator();
                break;
            case OperatorType.Bool_GreaterThan_Scal_Scal:
                abOperator = new AB_Bool_GreaterThan_Scal_Scal_Operator();
                break;
            case OperatorType.Bool_LessThan_Scal_Scal:
                abOperator = new AB_Bool_LessThan_Scal_Scal_Operator();
                break;
            case OperatorType.Scal_MinId_ScalTab:
                abOperator = new AB_Scal_MinId_ScalTab_Operator();
                break;
            case OperatorType.Scal_MaxId_ScalTab:
                abOperator = new AB_Scal_MaxId_ScalTab_Operator();
                break;
            case OperatorType.Bool_Not_Bool:
                abOperator = new AB_Bool_Not_Bool_Operator();
                break;
            case OperatorType.Bool_Or_Bool_Bool:
                abOperator = new AB_Bool_Or_Bool_Bool_Operator();
                break;
            case OperatorType.Vec_RandCircle_Vec_Scal:
                abOperator = new AB_Vec_RandCircle_Vec_Scal_Operator();
                break;
            case OperatorType.Bool_RefEquals_Ref_Ref:
                abOperator = new AB_Bool_RefEquals_Ref_Ref_Operator();
                break;
            case OperatorType.RefTab_Agg_RefStar:
                abOperator = new AB_RefTab_Agg_RefStar_Operator();
                break;
            case OperatorType.Ref_Get_Ref_Txt:
                abOperator = new AB_Ref_Get_Ref_Txt_Operator();
                break;
            case OperatorType.RefTab_Get_RefTab_Txt:
                abOperator = new AB_RefTab_Get_RefTab_Txt_Operator();
                break;
            case OperatorType.RefTab_WhereEquals_RefTab_Txt_Bool:
                abOperator = new AB_RefTab_WhereEquals_RefTab_Txt_Bool_Operator();
                break;
            case OperatorType.RefTab_WhereEquals_RefTab_Txt_Color:
                abOperator = new AB_RefTab_WhereEquals_RefTab_Txt_Color_Operator();
                break;
            case OperatorType.RefTab_WhereEquals_RefTab_Txt_Ref:
                abOperator = new AB_RefTab_WhereEquals_RefTab_Txt_Ref_Operator();
                break;
            case OperatorType.RefTab_WhereEquals_RefTab_Txt_Scal:
                abOperator = new AB_RefTab_WhereEquals_RefTab_Txt_Scal_Operator();
                break;
            case OperatorType.RefTab_WhereEquals_RefTab_Txt_Txt:
                abOperator = new AB_RefTab_WhereEquals_RefTab_Txt_Txt_Operator();
                break;
            case OperatorType.RefTab_WhereEquals_RefTab_Txt_Vec:
                abOperator = new AB_RefTab_WhereEquals_RefTab_Txt_Vec_Operator();
                break;
            case OperatorType.RefTab_WhereLess_RefTab_Txt_Scal:
                abOperator = new AB_RefTab_WhereLess_RefTab_Txt_Scal_Operator();
                break;
            case OperatorType.RefTab_WhereGreater_RefTab_Txt_Scal:
                abOperator = new AB_RefTab_WhereGreater_RefTab_Txt_Scal_Operator();
                break;
            case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Bool:
                abOperator = new AB_RefTab_WhereNotEquals_RefTab_Txt_Bool_Operator();
                break;
            case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Color:
                abOperator = new AB_RefTab_WhereNotEquals_RefTab_Txt_Color_Operator();
                break;
            case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Ref:
                abOperator = new AB_RefTab_WhereNotEquals_RefTab_Txt_Ref_Operator();
                break;
            case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Scal:
                abOperator = new AB_RefTab_WhereNotEquals_RefTab_Txt_Scal_Operator();
                break;
            case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Txt:
                abOperator = new AB_RefTab_WhereNotEquals_RefTab_Txt_Txt_Operator();
                break;
            case OperatorType.RefTab_WhereNotEquals_RefTab_Txt_Vec:
                abOperator = new AB_RefTab_WhereNotEquals_RefTab_Txt_Vec_Operator();
                break;
            case OperatorType.Bool_IsSet_Ref:
                abOperator = new AB_Bool_IsSet_Ref_Operator();
                break;
            case OperatorType.Bool_NotEquals_Ref_Ref:
                abOperator = new AB_Bool_NotEquals_Ref_Ref_Operator();
                break;
            case OperatorType.Scal_Size_RefTab:
                abOperator = new AB_Scal_Size_RefTab_Operator();
                break;
            case OperatorType.ScalTab_Agg_ScalStar:
                abOperator = new AB_ScalTab_Agg_ScalStar_Operator();
                break;
            case OperatorType.Scal_Div_Scal_Scal:
                abOperator = new AB_Scal_Div_Scal_Scal_Operator();
                break;
            case OperatorType.Bool_Equals_Scal_Scal:
                abOperator = new AB_Bool_Equals_Scal_Scal_Operator();
                break;
            case OperatorType.Scal_Get_Ref_Txt:
                abOperator = new AB_Scal_Get_Ref_Txt_Operator();
                break;
            case OperatorType.ScalTab_Get_RefTab_Txt:
                abOperator = new AB_ScalTab_Get_RefTab_Txt_Operator();
                break;
            case OperatorType.Scal_Get_ScalTab_Scal:
                abOperator = new AB_Scal_Get_ScalTab_Scal_Operator();
                break;
            case OperatorType.Bool_IsSet_Scal:
                abOperator = new AB_Bool_IsSet_Scal_Operator();
                break;
            case OperatorType.Bool_NotEquals_Scal_Scal:
                abOperator = new AB_Bool_NotEquals_Scal_Scal_Operator();
                break;
            case OperatorType.Scal_Prod_Scal_Scal:
                abOperator = new AB_Scal_Prod_Scal_Scal_Operator();
                break;
            case OperatorType.Scal_Size_ScalTab:
                abOperator = new AB_Scal_Size_ScalTab_Operator();
                break;
            case OperatorType.Scal_Sub_Scal_Scal:
                abOperator = new AB_Scal_Sub_Scal_Scal_Operator();
                break;
            case OperatorType.Scal_Sum_Scal_Scal:
                abOperator = new AB_Scal_Sum_Scal_Scal_Operator();
                break;
            case OperatorType.TxtTab_Agg_TxtStar:
                abOperator = new AB_TxtTab_Agg_TxtStar_Operator();
                break;
            case OperatorType.Bool_Equals_Txt_Txt:
                abOperator = new AB_Bool_Equals_Txt_Txt_Operator();
                break;
            case OperatorType.Txt_Get_Ref_Txt:
                abOperator = new AB_Txt_Get_Ref_Txt_Operator();
                break;
            case OperatorType.TxtTab_Get_RefTab_Txt:
                abOperator = new AB_TxtTab_Get_RefTab_Txt_Operator();
                break;
            case OperatorType.Txt_Get_TxtTab_Scal:
                abOperator = new AB_Txt_Get_TxtTab_Scal_Operator();
                break;
            case OperatorType.Bool_IsSet_Txt:
                abOperator = new AB_Bool_IsSet_Txt_Operator();
                break;
            case OperatorType.Bool_NotEquals_Txt_Txt:
                abOperator = new AB_Bool_NotEquals_Txt_Txt_Operator();
                break;
            case OperatorType.Scal_Size_TxtTab:
                abOperator = new AB_Scal_Size_TxtTab_Operator();
                break;
            case OperatorType.VecTab_Agg_VecStar:
                abOperator = new AB_VecTab_Agg_VecStar_Operator();
                break;
            case OperatorType.Vec_Cross_Vec_Vec:
                abOperator = new AB_Vec_Cross_Vec_Vec_Operator();
                break;
            case OperatorType.Scal_Dot_Vec_Vec:
                abOperator = new AB_Scal_Dot_Vec_Vec_Operator();
                break;
            case OperatorType.Bool_Equals_Vec_Vec:
                abOperator = new AB_Bool_Equals_Vec_Vec_Operator();
                break;
            case OperatorType.Vec_Get_Ref_Txt:
                abOperator = new AB_Vec_Get_Ref_Txt_Operator();
                break;
            case OperatorType.VecTab_Get_RefTab_Txt:
                abOperator = new AB_VecTab_Get_RefTab_Txt_Operator();
                break;
            case OperatorType.Vec_Get_VecTab_Scal:
                abOperator = new AB_Vec_Get_VecTab_Scal_Operator();
                break;
            case OperatorType.Bool_IsSet_Vec:
                abOperator = new AB_Bool_IsSet_Vec_Operator();
                break;
            case OperatorType.Bool_NotEquals_Vec_Vec:
                abOperator = new AB_Bool_NotEquals_Vec_Vec_Operator();
                break;
            case OperatorType.Vec_Prod_Vec_Scal:
                abOperator = new AB_Vec_Prod_Vec_Scal_Operator();
                break;
            case OperatorType.Vec_Prod_Scal_Vec:
                abOperator = new AB_Vec_Prod_Scal_Vec_Operator();
                break;
            case OperatorType.Scal_Size_VecTable:
                abOperator = new AB_Scal_Size_VecTable_Operator();
                break;
            case OperatorType.Vec_Sub_Vec_Vec:
                abOperator = new AB_Vec_Sub_Vec_Vec_Operator();
                break;
            case OperatorType.Vec_Sum_Vec_Vec:
                abOperator = new AB_Vec_Sum_Vec_Vec_Operator();
                break;
            case OperatorType.Vec_TermDiv_Vec_Vec:
                abOperator = new AB_Vec_TermDiv_Vec_Vec_Operator();
                break;
            case OperatorType.Vec_TermProd_Vec_Vec:
                abOperator = new AB_Vec_TermProd_Vec_Vec_Operator();
                break;
            case OperatorType.Scal_MinVal_ScalTab:
                abOperator = new AB_Scal_MinVal_ScalTab_Operator();
                break;
            case OperatorType.Ref_Get_RefTab_Scal:
                abOperator = new AB_Ref_Get_RefTab_Scal_Operator();
                break;
            default:
                throw new NotImplementedException("Type " + type + " not implemented");
        }

        return abOperator;
    }
}
