using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABMacroOperatorFactory {
    private const string CLASS_NAME_PREFFIX = "AB";
    private const string CLASS_NAME_SUFFIX = "Operator";

    public static IABOperator CreateMacro(ABNode root, string returnType, string name, string[] argTypes)
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
                macro = CreateMacro<ABBool>(root, returnType, name, cleanArgTypes);
                break;
            case "scal":
                macro = CreateMacro<ABScalar>(root, returnType, name, cleanArgTypes);
                break;
            case "txt":
                macro = CreateMacro<ABText>(root, returnType, name, cleanArgTypes);
                break;
            case "color":
                macro = CreateMacro<ABColor>(root, returnType, name, cleanArgTypes);
                break;
            case "vec":
                macro = CreateMacro<ABVec>(root, returnType, name, cleanArgTypes);
                break;
            case "ref":
                macro = CreateMacro<ABRef>(root, returnType, name, cleanArgTypes);
                break;
            case "bool[]":
                macro = CreateMacro<ABTable<ABBool>>(root, returnType, name, cleanArgTypes);
                break;
            case "scal[]":
                macro = CreateMacro< ABTable<ABScalar>>(root, returnType, name, cleanArgTypes);
                break;
            case "txt[]":
                macro = CreateMacro< ABTable<ABText>>(root, returnType, name, cleanArgTypes);
                break;
            case "color[]":
                macro = CreateMacro< ABTable<ABColor>>(root, returnType, name, cleanArgTypes);
                break;
            case "vec[]":
                macro = CreateMacro<ABTable<ABVec>>(root, returnType, name, cleanArgTypes);
                break;
            case "ref[]":
                macro = CreateMacro< ABTable<ABRef>>(root, returnType, name, cleanArgTypes);
                break;
            default:
                throw new System.NotImplementedException();
        }

        return macro; 
    }

    private static ABMacroOperator<T> CreateMacro<T>(ABNode root, string returnType, string name, string[] argTypes)
    {
        ABMacroOperator<T> macro = new ABMacroOperator<T>();
        macro.WrappedTree =
            ((ABOperator<T>)root);
        macro.AllocInputs(argTypes.Length);
        macro.ViewName = BuildViewName(returnType, name, argTypes);
        macro.ClassName = BuildClassNameFromViewName(macro.ViewName);
        macro.SymbolName = BuildSymbolName(returnType, name, argTypes);

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
                return "VecTab";
            default:
                throw new System.NotImplementedException();
        }
    }
}
