using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class ComponentsParser_Test {

	string pathFile = "Assets/Inputs/Components/components.csv";

	string readFile( string path ){
        System.IO.StreamReader reader = new System.IO.StreamReader(path);
        string content = reader.ReadToEnd();
        reader.Close();
        return content;
    }

	[Test]
	public void notCreated() {
		try{
            ComponentInfo component = ComponentFactory.instance.CreateComponent( 1 );
			Debug.Log( "ATTENTION : this test uses a static value that is initialized by other methods." );
		}
		catch( ComponentFactory.ComponentFactoryNotCreated ex ){
			Assert.Pass();
		}
		Assert.Fail();
	}

	[Test]
	public void basic01() {
        ComponentInfo component = ComponentFactory.instance.CreateComponent( 1 );

		Assert.AreEqual( "Tête Blinde", component.Name );
	}

	[Test]
	public void basic02() {
        ComponentInfo component = ComponentFactory.instance.CreateComponent( 9 );

		Assert.AreEqual( "Pinces tranchantes", component.Name );
		Assert.AreEqual( (Color32)UnityEngine.Color.red, component.Color );
		Assert.AreEqual( 0, component.ProdCost );
		Assert.AreEqual( true, component.EnablePickDrop );
		Assert.AreEqual( 1, component.StrengthBuff );
		Assert.AreEqual( 0, component.ActionSpeedBuff );
	}

	[Test]
	public void basic_doubleAttr() {
        ComponentInfo component = ComponentFactory.instance.CreateComponent( 22 );

		Assert.AreEqual( "Musculature Démoniaque", component.Name );
		Assert.AreEqual( (Color32)UnityEngine.Color.red, component.Color );
		Assert.AreEqual( 0, component.ProdCost );
		Assert.AreEqual( false, component.EnablePickDrop );
		Assert.AreEqual( false, component.EnableGotoHold );
		Assert.AreEqual( 1, component.StrengthBuff );
		Assert.AreEqual( 1, component.VitalityBuff );
		Assert.AreEqual( -0.5, component.MoveSpeedBuff );
		Assert.AreEqual( 0, component.HeatRangeBuff );
	}
}
