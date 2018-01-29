using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class ABParamFactory
{
    public static IABParam CreateParam(String typeStr, String identifier, String value)
    {
        ParamType type = GetTypeFromStr(typeStr);
        return CreateParam(type, identifier, value);
    }

    private static ParamType GetTypeFromStr(string typeStr)
    {
        switch (typeStr)
        {
            case "bool":
                return ParamType.Bool;
            case "scal":
                return ParamType.Scalar;
            case "txt":
                return ParamType.Text;
            case "vec":
                return ParamType.Vec;
            case "color":
                return ParamType.Color;
            case "ref":
                return ParamType.Ref;
            case "bool[]":
                return ParamType.BoolTable;
            case "scal[]":
                return ParamType.ScalarTable;
            case "txt[]":
                return ParamType.TextTable;
            case "vec[]":
                return ParamType.VecTable;
            case "color[]":
                return ParamType.ColorTable;
            case "ref[]":
                return ParamType.RefTable;
        }
        return ParamType.None;
    }

    public static IABParam CreateParam(ParamType type, String identifier, String value)
    {
        IABParam param = null;
        switch (type)
        {
            case ParamType.Bool:
                ABBool boolVal = TypeFactory.CreateEmptyBool();
                if (value != null)
                {
                    boolVal.Value = bool.Parse(value);
                }
                param = new ABBoolParam(identifier, boolVal);
                break;
            case ParamType.Scalar:
                ABScalar scalVal = TypeFactory.CreateEmptyScalar();
                if (value != null)
                {
                    scalVal.Value = float.Parse(value);
                }
                param = new ABScalParam(identifier, scalVal);
                break;
            case ParamType.Text:
                ABText textVal = TypeFactory.CreateEmptyText();
                if (value != null)
                {
                    textVal.Value = value;
                }
                param = new ABTextParam(identifier, textVal);
                break;
            case ParamType.Vec:
                ABVec vecVal = TypeFactory.CreateEmptyVec();
                if (value != null)
                {
                    //TODO spec & parse vector
                    throw new NotSupportedException();
                }
                param = new ABVecParam(identifier, vecVal);
                break;
            case ParamType.Color:
                ABColor colorVal = TypeFactory.CreateEmptyColor();
                if (value != null)
                {
                    switch (value)
                    {
                        case "red":
                            colorVal.Value = ABColor.Color.Red;
                            break;
                        case "green":
                            colorVal.Value = ABColor.Color.Green;
                            break;
                        case "blue":
                            colorVal.Value = ABColor.Color.Blue;
                            break;
                    }
                }
                param = new ABColorParam(identifier, colorVal);
                break;
            case ParamType.Ref:
                ABRef refVal = TypeFactory.CreateEmptyRef();
                if (value != null)
                {
                    //TODO spec & parse vector
                    throw new NotSupportedException();
                }
                param = new ABRefParam(identifier, refVal);
                break;
            case ParamType.BoolTable:
                ABTable<ABBool> boolTab = TypeFactory.CreateEmptyTable<ABBool>();
                if (value != null)
                {
                    throw new NotSupportedException();
                }
                param = new ABTableParam<ABBool>(identifier, boolTab);
                break;
            case ParamType.ScalarTable:
                ABTable<ABScalar> scalTab = TypeFactory.CreateEmptyTable<ABScalar>();
                if (value != null)
                {
                    throw new NotSupportedException();
                }
                param = new ABTableParam<ABScalar>(identifier, scalTab);
                break;
            case ParamType.TextTable:
                ABTable<ABText> textTab = TypeFactory.CreateEmptyTable<ABText>();
                if (value != null)
                {
                    throw new NotSupportedException();
                }
                param = new ABTableParam<ABText>(identifier, textTab);
                break;
            case ParamType.VecTable:
                ABTable<ABVec> vecTab = TypeFactory.CreateEmptyTable<ABVec>();
                if (value != null)
                {
                    throw new NotImplementedException();
                }
                param = new ABTableParam<ABVec>(identifier, vecTab);
                break;
            case ParamType.ColorTable:
                ABTable<ABColor> colorTab = TypeFactory.CreateEmptyTable<ABColor>();
                if (value != null)
                {
                    throw new NotImplementedException();
                }
                param = new ABTableParam<ABColor>(identifier, colorTab);
                break;
            case ParamType.RefTable:
                ABTable<ABRef> refTab = TypeFactory.CreateEmptyTable<ABRef>();
                if (value != null)
                {
                    throw new NotSupportedException();
                }
                param = new ABTableParam<ABRef>(identifier, refTab);
                break;
        }

        return param;
    }

    public static ABBoolParam CreateBoolParam(String identifier, bool value)
    {
        ABBool boolVal = TypeFactory.CreateEmptyBool();
        boolVal.Value = value;
        ABBoolParam param = new ABBoolParam(identifier, boolVal);

        return param;
    }

    public static IABParam CreateBoolTableParam(string identifier, bool[] values)
    {
        ABTable<ABBool> boolTab = TypeFactory.CreateEmptyTable<ABBool>();
        boolTab.Values = new ABBool[values.Length];
        for (int i = 0; i < values.Length; i++)
        {
            ABBool boolVal = TypeFactory.CreateEmptyBool();
            boolVal.Value = values[i];
            boolTab.Values[i] = boolVal;
        }
        ABTableParam<ABBool> param = new ABTableParam<ABBool>(identifier, boolTab);

        return param;
    }

    public static ABScalParam CreateScalarParam(String identifier, float value)
    {
        ABScalar scalVal = TypeFactory.CreateEmptyScalar();
        scalVal.Value = value;
        ABScalParam param = new ABScalParam(identifier, scalVal);

        return param;
    }

    public static IABParam CreateScalarTableParam(string identifier, float[] values)
    {
        ABTable<ABScalar> scalTab = TypeFactory.CreateEmptyTable<ABScalar>();
        scalTab.Values = new ABScalar[values.Length];
        for (int i = 0; i < values.Length; i++)
        {
            ABScalar scalVal = TypeFactory.CreateEmptyScalar();
            scalVal.Value = values[i];
            scalTab.Values[i] = scalVal;
        }
        ABTableParam<ABScalar> param = new ABTableParam<ABScalar>(identifier, scalTab);

        return param;
    }

    public static ABTextParam CreateTextParam(String identifier, String value)
    {
        ABText textVal = TypeFactory.CreateEmptyText();
        textVal.Value = value;
        ABTextParam param = new ABTextParam(identifier, textVal);

        return param;
    }

    public static IABParam CreateTextTableParam(string identifier, string[] values)
    {
        ABTable<ABText> textTab = TypeFactory.CreateEmptyTable<ABText>();
        textTab.Values = new ABText[values.Length];
        for (int i = 0; i < values.Length; i++)
        {
            ABText textVal = TypeFactory.CreateEmptyText();
            textVal.Value = values[i];
            textTab.Values[i] = textVal;
        }
        ABTableParam<ABText> param = new ABTableParam<ABText>(identifier, textTab);

        return param;
    }

    public static ABVecParam CreateVecParam(String identifier, float x, float y)
    {
        ABVec vecVal = TypeFactory.CreateEmptyVec();
        vecVal.X = x;
        vecVal.Y = y;
        ABVecParam param = new ABVecParam(identifier, vecVal);

        return param;
    }

    public static IABParam CreateVecTableParam(string identifier, float[] x, float[] y)
    {
        ABTable<ABVec> vecTab = TypeFactory.CreateEmptyTable<ABVec>();
        vecTab.Values = new ABVec[x.Length];
        for (int i = 0; i < x.Length; i++)
        {
            ABVec vecVal = TypeFactory.CreateEmptyVec();
            vecVal.X = x[i];
            vecVal.Y = y[i];
            vecTab.Values[i] = vecVal;
        }
        ABTableParam<ABVec> param = new ABTableParam<ABVec>(identifier, vecTab);

        return param;
    }

    public static IABParam CreateColorParam(String identifier, int r, int g, int b)
    {
        ABColor colorVal = TypeFactory.CreateEmptyColor();
        ABColor.Color color = ABColor.Color.Red;
        if (g > r && g > b)
        {
            color = ABColor.Color.Green;
        } else if (b > r && b > g)
        {
            color = ABColor.Color.Blue;
        }
        colorVal.Value = color;

        ABColorParam param = new ABColorParam(identifier, colorVal);
        return param;
    }

    public static IABParam CreateColorTableParam(String identifier, int[] r, int[] g, int[] b)
    {
        ABTable<ABColor> colorTab = TypeFactory.CreateEmptyTable<ABColor>();
        colorTab.Values = new ABColor[r.Length];
        for (int i = 0; i < r.Length; i++)
        {
            ABColor colorVal = TypeFactory.CreateEmptyColor();
            ABColor.Color color = ABColor.Color.Red;
            if (g[i] > r[i] && g[i] > b[i])
            {
                color = ABColor.Color.Green;
            }
            else if (b[i] > r[i] && b[i] > g[i])
            {
                color = ABColor.Color.Blue;
            }
            colorVal.Value = color;
            colorTab.Values[i] = colorVal;
        }

        ABTableParam<ABColor> param = new ABTableParam<ABColor>(identifier, colorTab);
        return param;
    }

    public static IABParam CreateRefParam(String identifier, Dictionary<string, int> dict)
    {
        //ABRefParam param = new ABRefParam(identifier, refVal);

        return null;
    }

        public static IABParam CreateRefParam(String identifier, object[] objects)
    {
        ABRef refVal = null;

        if (objects != null)
        {
            refVal = TypeFactory.CreateEmptyRef();
            foreach (object obj in objects)
            {
                FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.NonPublic | BindingFlags.Instance);
                foreach (FieldInfo field in fields)
                {
                    object[] customAttrs = field.GetCustomAttributes(typeof(AttrName), false);
                    if (customAttrs.Length > 0)
                    {
                        AttrName attrNameAttr = (AttrName)customAttrs[0];
                        IABType type = CreateRefAttr(field, obj);
                        refVal.SetAttr(attrNameAttr.Identifier, type);
                    }
                }
            }
        }

        ABRefParam param = new ABRefParam(identifier, refVal);
        return param;
    }

    public static IABParam CreateRefTableParam(String identifier, object[][] objects)
    {
        ABTable<ABRef> refTab = TypeFactory.CreateEmptyTable<ABRef>();
        refTab.Values = new ABRef[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            ABRef refVal = null;
            if (objects[i] != null)
            {
                refVal = TypeFactory.CreateEmptyRef();
                foreach (object obj in objects)
                {
                    FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.NonPublic | BindingFlags.Instance);
                    foreach (FieldInfo field in fields)
                    {
                        object[] customAttrs = field.GetCustomAttributes(typeof(AttrName), false);
                        if (customAttrs.Length > 0)
                        {
                            AttrName attrNameAttr = (AttrName)customAttrs[0];
                            IABType type = CreateRefAttr(field, obj);
                            refVal.SetAttr(attrNameAttr.Identifier, type);
                        }
                    }
                }
                refTab.Values[i] = refVal;
            }
        }
        ABTableParam<ABRef> param = new ABTableParam<ABRef>(identifier, refTab);
        return param;
    }

    private static IABType CreateRefAttr(FieldInfo field, object obj)
    {
        IABType type = null;
        if (field.FieldType == typeof(bool))
        {
            bool value = (bool)field.GetValue(obj);
            type = TypeFactory.CreateEmptyBool();
            ((ABBool)type).Value = value;
        }
        else if (field.FieldType == typeof(byte))
        {
            byte value = (byte)field.GetValue(obj);
            type = TypeFactory.CreateEmptyScalar();
            ((ABScalar)type).Value = value;
        }
        else if (field.FieldType == typeof(short))
        {
            short value = (short)field.GetValue(obj);
            type = TypeFactory.CreateEmptyScalar();
            ((ABScalar)type).Value = value;
        }
        else if (field.FieldType == typeof(int))
        {
            int value = (int)field.GetValue(obj);
            type = TypeFactory.CreateEmptyScalar();
            ((ABScalar)type).Value = value;
        }
        else if (field.FieldType == typeof(long))
        {
            long value = (long)field.GetValue(obj);
            type = TypeFactory.CreateEmptyScalar();
            ((ABScalar)type).Value = value;
        }
        else if (field.FieldType == typeof(float))
        {
            float value = (float)field.GetValue(obj);
            type = TypeFactory.CreateEmptyScalar();
            ((ABScalar)type).Value = value;
        }
        else if (field.FieldType == typeof(double))
        {
            double value = (double)field.GetValue(obj);
            type = TypeFactory.CreateEmptyScalar();
            ((ABScalar)type).Value = (float)value;
        }
        else if (field.FieldType == typeof(string))
        {
            String value = (string)field.GetValue(obj);
            type = TypeFactory.CreateEmptyText();
            ((ABText)type).Value = value;
        }
        else if (field.FieldType == typeof(Vector2))
        {
            Vector2 value = (Vector2)field.GetValue(obj);
            type = TypeFactory.CreateEmptyVec();
            ((ABVec)type).X = value.x;
            ((ABVec)type).Y = value.y;
        }
        else if (field.FieldType == typeof(Color32))
        {
            Color32 value = (Color32)field.GetValue(obj);
            ABColor.Color color = ABColor.Color.Red;
            if (value.g > value.r && value.g > value.b)
            {
                color = ABColor.Color.Green;
            }
            else if (value.b > value.r && value.b > value.g)
            {
                color = ABColor.Color.Blue;
            }
            ((ABColor)type).Value = color;
        }
        else if (field.FieldType == typeof(Dictionary<string, int>))
        {
            Dictionary<string, int> dict = (Dictionary<string, int>)field.GetValue(obj);
            ABRef refType = TypeFactory.CreateEmptyRef();
            foreach (string key in dict.Keys)
            {
                ABScalar scalVal = TypeFactory.CreateEmptyScalar();
                scalVal.Value = dict[key];
                refType.SetAttr(key, scalVal);
            }
            type = refType;
        }
        else if (field.FieldType == typeof(GameObject))
        {
            FieldInfo[] fields = obj.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.NonPublic | BindingFlags.Instance);
            foreach (FieldInfo subField in fields)
            {
                object[] customAttrs = subField.GetCustomAttributes(typeof(AttrName), false);
                if (customAttrs.Length > 0)
                {
                    AttrName attrNameAttr = (AttrName)customAttrs[0];
                    type = CreateRefAttr(field, obj);
                }
            }
        } else
        {
            throw new NotSupportedException("ABParamFactory " + field.FieldType + " not supported !");
        }

        return type;
    }
}
