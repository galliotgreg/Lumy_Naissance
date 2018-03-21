using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinColor {

    static Color boolColor = Color.blue; // blue
    static Color scalColor = new Color(0.438f, 0.258f, 0.078f); //sepia
    static Color textColor = new Color(0.25f, 0.875f, 0.813f); // turquoise
    static Color colorColor = new Color(0.785f, 0.410f, 0.117f); // chocolat
    static Color refColor = Color.red; // red
    static Color vecColor = new Color(0.930f, 0.508f, 0.930f); // violet

    public static Color GetBoolColor()
    {
            return boolColor;
    }

    public static Color GetScalColor()
    {
            return scalColor;
    }

    public static Color GetTextColor()
    {
            return textColor;        
    }

    public static Color GetColorColor()
    {
            return colorColor;        
    }

    public static Color GetRefColor()
    {
            return refColor;       
    }

    public static Color GetVecColor()
    {
            return vecColor;        
    }

	/// <summary>
	/// Gets the color of the pin based on its type
	/// </summary>
	/// <returns>The color of the pin based on its type.</returns>
	/// <param name="type">Type.</param>
	public static Color GetColorPinFromType(System.Type type){
		Color color = new Color();
		// Check type (simple type) or internal type (complex type)
		System.Type internalType = ABStar<ABBool>.getInternalIfComplexType(type);
		if (type == typeof(ABBool) || internalType == typeof(ABBool) )
		{
			color = PinColor.GetBoolColor();
		}
		else if (type == typeof(ABScalar) || internalType == typeof(ABScalar))
		{
			// Silver
			color = PinColor.GetScalColor();
		}
		else if (type == typeof(ABText) || internalType == typeof(ABText) )
		{
			// Turquoise
			color = PinColor.GetTextColor();
		}
		else if (type == typeof(ABColor) || internalType == typeof(ABColor) )
		{
			// Chocolat
			color = PinColor.GetColorColor();
		}
		else if (type == typeof(ABRef) || internalType == typeof(ABRef) )
		{
			color = PinColor.GetRefColor();
		}
		else if (type == typeof(ABVec) || internalType == typeof(ABVec) )
		{
			// Violet
			color = PinColor.GetVecColor();
		} else
		{
			Debug.LogWarning("Type non reconnu " + type);
		}
		return color;
	}

    public static Color GetColorPinFromType(string type)
    {
        Color color = new Color();
        if (type.Contains("Bool"))
        {
            // Blue 
            color = PinColor.GetBoolColor();
        }
        else if (type.Contains("Scal"))
        {
            // Sepia
            color = PinColor.GetScalColor();
        }
        else if (type.Contains("Text") || type.Contains("Txt"))
        {
            // Turquoise
            color = PinColor.GetTextColor();
        }
        else if (type.Contains("Color"))
        {
            // Chocolat
            color = PinColor.GetColorColor();
        }
        else if (type.Contains("Ref"))
        {
            // Red
            color = PinColor.GetRefColor();
        }
        else if (type.Contains("Vec"))
        {
            // Violet
            color = PinColor.GetVecColor();
        } else
        {
            Debug.LogWarning("Type non reconnu " + type);
        }
        return color;
    }
}

