using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class BoolGetRefTxt_Op_Test {

	private string symbol = "BgetRT";

	ABParam<ABRef> CreateRefParam(string identifier, ABRef r){
		ABParam<ABRef> param = new ABRefParam(identifier, r);

		return param;
	}

	ABRef createRef( System.String s, IABType obj ){
		ABText s1 = new ABText();
		s1.Value = s;

		ABRef r1 = new ABRef();
		r1.SetAttr( s, obj );

		return r1;
	}

	/// <summary>
	/// Create an operator based on the list of parameters (correct type)
	/// </summary>
	/// <returns>The operator</returns>
	/// <param name="param">Parameters that will compose the operator</param>
	private AB_Bool_Get_Ref_Txt_Operator getOperator( ABContext ctx, params object[] parameters ){

		// Generate a list of ABParams based on the params
		ABNode[] listParams = new ABNode[ parameters.Length ];
		if( parameters.Length > 0 ){
			listParams[0] = CreateRefParam( "ref", (ABRef)parameters[0] );
		}
		if( parameters.Length > 1 ){
			listParams[1] = ABParamFactory.CreateTextParam( "key", ((ABText)parameters[1]).Value );
		}

		// Create an operator and associates it to the params
		return Operator_Test<AB_Bool_Get_Ref_Txt_Operator>.getOperator_ABParams( symbol, ctx, listParams );
	}

	#region TESTS
	[Test]
	public void Test_basic01() {
		// Test values
		bool expected = true;

		ABBool value = new ABBool();
		value.Value = true;
		ABRef arg1 = createRef( "a", value );

		ABText arg2 = new ABText(); arg2.Value = "a";

		// Create operator
		ABContext ctx = new ABContext();
		AB_Bool_Get_Ref_Txt_Operator ope = getOperator( ctx, arg1, arg2 );
		// Test operator
		Assert.AreEqual( expected, ope.Evaluate( ctx ).Value );
	}

	[Test]
	public void Test_basic02() {
		// Test values
		bool expected = true;

		ABBool value = new ABBool();
		value.Value = true;
		ABBool value2 = new ABBool();
		value2.Value = false;

		ABRef arg1 = createRef( "a", value );
		arg1.SetAttr( "b", value2 );

		ABText arg2 = new ABText(); arg2.Value = "a";

		// Create operator
		ABContext ctx = new ABContext();
		AB_Bool_Get_Ref_Txt_Operator ope = getOperator( ctx, arg1, arg2 );
		// Test operator
		Assert.AreEqual( expected, ope.Evaluate( ctx ).Value );
	}

	[Test]
	public void Test_basic03() {
		// Test values
		ABBool value = new ABBool();
		value.Value = true;
		ABRef arg1 = createRef( "a", value );
		ABText arg2 = new ABText(); arg2.Value = "b";

		try{
			// Create operator
			ABContext ctx = new ABContext();
			AB_Bool_Get_Ref_Txt_Operator ope = getOperator( ctx, arg1, arg2 );
			ope.Evaluate( ctx );
		}
		catch( System.Exception ex ){
			// If an exception occurs, the test is succeeded
			Assert.Pass( ex.Message );
		}
		// If an exception does occur, the test is failed
		Assert.Fail();
	}

	[Test]
	public void Test_emptyString() {
		// Test values
		ABBool value = new ABBool();
		value.Value = true;
		ABRef arg1 = createRef( "a", value );
		ABText arg2 = new ABText();

		try{
			// Create operator
			ABContext ctx = new ABContext();
			AB_Bool_Get_Ref_Txt_Operator ope = getOperator( ctx, arg1, arg2 );
			ope.Evaluate( ctx );
		}
		catch( System.Exception ex ){
			// If an exception occurs, the test is succeeded
			Assert.Pass( ex.Message );
		}
		// If an exception does occur, the test is failed
		Assert.Fail();
	}

	[Test]
	public void Test_emptyString2() {
		// Test values
		ABRef arg1 = new ABRef();
		ABText arg2 = new ABText();

		try{
			// Create operator
			ABContext ctx = new ABContext();
			AB_Bool_Get_Ref_Txt_Operator ope = getOperator( ctx, arg1, arg2 );
			ope.Evaluate( ctx );
		}
		catch( System.Exception ex ){
			// If an exception occurs, the test is succeeded
			Assert.Pass( ex.Message );
		}
		// If an exception does occur, the test is failed
		Assert.Fail();
	}

	[Test]
	public void Test_emptyRef() {
		// Test values
		ABRef arg1 = new ABRef();
		ABText arg2 = new ABText(); arg2.Value = "a";

		try{
			// Create operator
			ABContext ctx = new ABContext();
			AB_Bool_Get_Ref_Txt_Operator ope = getOperator( ctx, arg1, arg2 );
			ope.Evaluate( ctx );
		}
		catch( System.Exception ex ){
			// If an exception occurs, the test is succeeded
			Assert.Pass( ex.Message );
		}
		// If an exception does occur, the test is failed
		Assert.Fail();
	}

	[Test]
	public void Test_emptyArgs() {
		// Test values

		try{
			// Create operator
			ABContext ctx = new ABContext();
			AB_Bool_Get_Ref_Txt_Operator ope = getOperator( ctx );
			ope.Evaluate( ctx );
		}
		catch( System.Exception ex ){
			// If an exception occurs, the test is succeeded
			Assert.Pass( ex.Message );
		}
		// If an exception does occur, the test is failed
		Assert.Fail();
	}

	[Test]
	public void Test_arg1Null() {
		// Test values
		ABRef arg1 = null;
		ABText arg2 = new ABText(); arg2.Value = "a";

		try{
			// Create operator
			ABContext ctx = new ABContext();
			AB_Bool_Get_Ref_Txt_Operator ope = getOperator( ctx, arg1, arg2 );
			ope.Evaluate( ctx );
		}
		catch( System.Exception ex ){
			// If an exception occurs, the test is succeeded
			Assert.Pass( ex.Message );
		}
		// If an exception does occur, the test is failed
		Assert.Fail();
	}

	[Test]
	public void Test_arg2Null() {
		// Test values
		ABBool value = new ABBool();
		value.Value = true;
		ABRef arg1 = createRef( "a", value );
		ABText arg2 = null;

		try{
			// Create operator
			ABContext ctx = new ABContext();
			AB_Bool_Get_Ref_Txt_Operator ope = getOperator( ctx, arg1, arg2 );
			ope.Evaluate( ctx );
		}
		catch( System.Exception ex ){
			// If an exception occurs, the test is succeeded
			Assert.Pass( ex.Message );
		}
		// If an exception does occur, the test is failed
		Assert.Fail();
	}

	[Test]
	public void Test_arg1Type() {
		// Test values
		ABText value = new ABText(); value.Value = "b";
		ABRef arg1 = createRef( "a", value );
		ABText arg2 = new ABText(); arg2.Value = "a";

		try{
			// Create operator
			ABContext ctx = new ABContext();
			AB_Bool_Get_Ref_Txt_Operator ope = getOperator( ctx, arg1, arg2 );
			ope.Evaluate( ctx );
		}
		catch( System.Exception ex ){
			// If an exception occurs, the test is succeeded
			Assert.Pass( ex.Message );
		}
		// If an exception does occur, the test is failed
		Assert.Fail();
	}
	#endregion

}
