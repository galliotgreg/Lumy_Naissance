﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABMacroOperatorFactory {
    private const string CLASS_NAME_PREFFIX = "AB";
    private const string CLASS_NAME_SUFFIX = "Operator";

    public static IABOperator CreateMacro(string filename, ABNode root, string returnType, string name, string[] argTypes)
    {
        IABOperator macro = null;

        //Clean args
        int nbArgs = 0;
        foreach (string argType in argTypes)
        {
            if (argType != "") nbArgs++;
        }
        int ind = 0;
        string[] cleanArgTypes = new string[nbArgs];
        foreach (string argType in argTypes)
        {
            if (argType != "") cleanArgTypes[ind++] = argType;
        }

        //Create Macro Operator
        switch (returnType)
        {
            case "bool":
                macro = CreateMacro<ABBool>(filename, root, returnType, name, cleanArgTypes);
                break;
            case "scal":
                macro = CreateMacro<ABScalar>(filename, root, returnType, name, cleanArgTypes);
                break;
            case "txt":
                macro = CreateMacro<ABText>(filename, root, returnType, name, cleanArgTypes);
                break;
            case "color":
                macro = CreateMacro<ABColor>(filename, root, returnType, name, cleanArgTypes);
                break;
            case "vec":
                macro = CreateMacro<ABVec>(filename, root, returnType, name, cleanArgTypes);
                break;
            case "ref":
                macro = CreateMacro<ABRef>(filename, root, returnType, name, cleanArgTypes);
                break;
            case "bool[]":
                macro = CreateMacro<ABTable<ABBool>>(filename, root, returnType, name, cleanArgTypes);
                break;
            case "scal[]":
                macro = CreateMacro< ABTable<ABScalar>>(filename, root, returnType, name, cleanArgTypes);
                break;
            case "txt[]":
                macro = CreateMacro< ABTable<ABText>>(filename, root, returnType, name, cleanArgTypes);
                break;
            case "color[]":
                macro = CreateMacro< ABTable<ABColor>>(filename, root, returnType, name, cleanArgTypes);
                break;
            case "vec[]":
                macro = CreateMacro<ABTable<ABVec>>(filename, root, returnType, name, cleanArgTypes);
                break;
            case "ref[]":
                macro = CreateMacro< ABTable<ABRef>>(filename, root, returnType, name, cleanArgTypes);
                break;
            default:
                throw new System.NotImplementedException();
        }

        return macro; 
    }

    private static ABMacroOperator<T> CreateMacro<T>(string filename, ABNode root, string returnType, string name, string[] argTypes)
    {
        ABMacroOperator<T> macro = new ABMacroOperator<T>();
        macro.WrappedTree =
            ((ABOperator<T>)root);
        macro.AllocInputs(argTypes.Length);
        macro.ViewName = BuildViewName(returnType, name, argTypes);
        macro.ClassName = BuildClassNameFromViewName(macro.ViewName);
        macro.SymbolName = filename; //BuildSymbolName(returnType, name, argTypes);
		macro.ArgTypes = ArgsToTypes ( argTypes );

        return macro;
    }

    private static string BuildSymbolName(string returnType, string name, string[] argTypes)
    {
        string builtName = ToSymbolType(returnType);
        builtName += name;
        foreach (string argType in argTypes)
        {
            builtName += ToSymbolType(argType);
        }
        return builtName;
    }

    private static string BuildViewName(string returnType, string name, string[] argTypes)
    {
        string builtName = ToClassType(returnType) + "_";
        builtName += Char.ToUpperInvariant(name[0]) + name.Substring(1) + "_";
        foreach (string argType in argTypes)
        {
            builtName += ToClassType(argType);
        }
        return builtName;
    }

    private static string BuildClassNameFromViewName(string viewName)
    {
        return CLASS_NAME_PREFFIX + "_" + viewName + "_" + CLASS_NAME_SUFFIX;
    }

    private static string ToSymbolType(string typeStr)
    {
        switch (typeStr)
        {
            case "bool":
                return "B";
            case "scal":
                return "S";
            case "txt":
                return "T";
            case "color":
                return "C";
            case "vec":
                return "V";
            case "ref":
                return "R";
            case "bool[]":
                return "B[]";
            case "scal[]":
                return "S[]";
            case "txt[]":
                return "T[]";
            case "color[]":
                return "C[]";
            case "vec[]":
                return "V[]";
            case "ref[]":
                return "R[]";
            default:
                throw new System.NotImplementedException();
        }
    }

    private static string ToClassType(string typeStr)
    {
        switch (typeStr)
        {
            case "bool":
                return "Bool";
            case "scal":
                return "Scal";
            case "txt":
                return "Text";
            case "color":
                return "Color";
            case "vec":
                return "Vec";
            case "ref":
                return "Ref";
            case "bool[]":
                return "BoolTab";
            case "scal[]":
                return "ScalTab";
            case "txt[]":
                return "TextTab";
            case "color[]":
                return "ColorTab";
            case "vec[]":
                return "VecTab";
            case "ref[]":
                return "RefTab";
            default:
                throw new System.NotImplementedException();
        }
    }

	private static System.Type ToABType(string typeStr)
	{
		switch (typeStr)
		{
		case "bool":
			return typeof(ABBool);
		case "scal":
			return typeof(ABScalar);
		case "txt":
			return typeof(ABText);
		case "color":
			return typeof(ABColor);
		case "vec":
			return typeof(ABVec);
		case "ref":
			return typeof(ABRef);
		case "bool[]":
			return typeof(ABTable<ABBool>);
		case "scal[]":
			return typeof(ABTable<ABScalar>);
		case "txt[]":
			return typeof(ABTable<ABText>);
		case "color[]":
			return typeof(ABTable<ABColor>);
		case "vec[]":
			return typeof(ABTable<ABVec>);
		case "ref[]":
			return typeof(ABTable<ABRef>);
		default:
			throw new System.NotImplementedException();
		}
	}

	public static List<System.Type> ArgsToTypes( string[] argTypes ){
		List < System.Type > result = new List<Type> ();
		foreach( string arg in argTypes ){
			result.Add ( ToABType( arg ) );
		}
		return result;
	}
}
