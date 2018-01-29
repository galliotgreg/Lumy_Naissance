using System.Collections;
using System.Collections.Generic;

public class ComponentParser {

	public class ComponentParserException : System.Exception{};
	public class ComponentParser_RequiredAttribute_Exception : System.Exception{};

	public static Dictionary<int,AgentComponent> parse( string fileContent ){
		Dictionary<int,AgentComponent> components = new Dictionary<int,AgentComponent>();

		List<List<string>> lines = splitComponents( fileContent, 9 );

		// 2 header lines
		for( int i = 2; i < lines.Count; i++ ){
			AgentComponent comp = parseComponent( lines[i] );
			components.Add( comp.Id, comp );
		}

		return components;
	}

	private static bool required( string v ){
		return v.Length > 0;
	}

	private static List<List<string>> splitComponents( string s, int componentsAttrAmount ){
		List<string> lines = splitLines( s );

		List<List<string>> res = new List<List<string>>();

		// One component can be splited by a new line or a comma
		bool newComponent = true;
		foreach( string line in lines ){
			string[] lineComponents = line.Split( new char[]{','} );

			if( newComponent ){
				res.Add( new List<string>() );
				newComponent = false;
			}

			if( res[res.Count-1].Count == 0 && lineComponents.Length <= componentsAttrAmount ){
				foreach( string compAttr in lineComponents ){
					res[ res.Count-1 ].Add( compAttr );
				}
			}else if( lineComponents.Length > 0 && lineComponents.Length + res[res.Count-1].Count <= componentsAttrAmount+1 ){
				res[res.Count-1][ res[res.Count-1].Count-1 ] += '\n'+lineComponents[0];
				for( int i = 1; i < lineComponents.Length; i++ ){
					res[ res.Count-1 ].Add( lineComponents[i] );
				}
			}
			else{
				throw new ComponentParserException();
			}

			if( res[res.Count-1].Count == componentsAttrAmount ){
				newComponent = true;
			}
		}

		return res;
	}

	private static List<string> splitLines( string s ){
		string[] withEmpty = s.Split( new char[]{'\n','\r'} );

		List<string> res = new List<string>();

		foreach( string str in withEmpty ){
			if( str.Length > 0 ){
				res.Add(str);
			}
		}

		return res;
	}

	private static AgentComponent parseComponent( List<string> componentContent ){
		// TODO check if has some filled field
		AgentComponent agent = new AgentComponent();
		agent.init();

		List<string> values = componentContent;
		int componentIt = 0;		// indicates the current index

		// ID
		if( required( values[componentIt] ) ){
			agent.Id = int.Parse( values[componentIt] );
		}else{
			throw new ComponentParser_RequiredAttribute_Exception();
		}
		componentIt++;

		// NAME
		if( required( values[componentIt] ) ){
			agent.Name = values[componentIt];
		}else{
			throw new ComponentParser_RequiredAttribute_Exception();
		}
		componentIt++;

		// ACTIONS
		List<string> actions = splitLines( values[componentIt].Trim( new char[]{'"',' '} ) );
		foreach( string action in actions ){
			parseAction( action, agent );
		}
		componentIt++;

		// SENSORS
		List<string> sensors = splitLines(values[componentIt].Trim( new char[]{'"',' '} ));
		foreach( string sensor in sensors ){
			parseSensor( sensor, agent );
		}
		componentIt++;

		// ATTRIBUTS
		List<string> attributes = splitLines(values[componentIt].Trim( new char[]{'"',' '} ));
		foreach( string att in attributes ){
			parseAttribute( att, agent );
		}
		componentIt++;

		// OTHER EFFECTS
		List<string> effects = splitLines(values[componentIt].Trim( new char[]{'"',' '} ));
		foreach( string effect in effects ){
			parseEffect( effect, agent );
		}
		componentIt++;

		// COLOR
		if( required( values[componentIt] ) ){
			if( values[componentIt] == "Red" ){
				agent.Color = UnityEngine.Color.red;
			}else if( values[componentIt] == "Green" ){
				agent.Color = UnityEngine.Color.green;
			}else if( values[componentIt] == "Blue" ){
				agent.Color = UnityEngine.Color.blue;
			}else if( values[componentIt] == "Incolor" ){
				agent.Color = UnityEngine.Color.white;
			}
		}else{
			throw new ComponentParser_RequiredAttribute_Exception();
		}
		componentIt++;

		// PROD COST
		/*if( required( values[componentIt] ) ){
			agent.ProdCost = float.Parse( values[componentIt] );
		}else{
			throw new ComponentParser_RequiredAttribute_Exception();
		}*/
		agent.ProdCost = values[componentIt].Length == 0 ? 0 : float.Parse( values[componentIt] );
		componentIt++;

		// BUY COST
		/*if( required( values[componentIt] ) ){
			agent.BuyCost = float.Parse( values[componentIt] );
		}else{
			throw new ComponentParser_RequiredAttribute_Exception();
		}*/
		agent.BuyCost = values[componentIt].Length == 0 ? 0 : float.Parse( values[componentIt] );
		componentIt++;

		return agent;
	}

	private static void parseAction( string action, AgentComponent component ){
		if( action.Length > 0 ){
			switch( action ){
			case "strike": component.EnableStrike = true; return;
			case "pick/drop": component.EnablePickDrop = true; return;
			case "goto/hold": component.EnableGotoHold = true; return;
			case "lay": component.EnableLay = true; return;
			default: component.NotHandledTokens.Add( action ); return;
			}
		}
	}
	private static void parseSensor( string sensor, AgentComponent component ){
		if( sensor.Length > 0 ){
			string[] sensorValues = sensor.Split( new char[]{' '} );
			if( sensorValues.Length > 1 ){
				switch( sensorValues[0] ){
				case "vision": component.VisionRange = float.Parse(sensorValues[1]); return;
				case "vibration": component.VibrationRange = float.Parse(sensorValues[1]); return;
				case "heat": component.HeatRange = float.Parse(sensorValues[1]); return;
				case "smell": component.SmellRange = float.Parse(sensorValues[1]); return;
				default: component.NotHandledTokens.Add( sensor ); return;
				}
			}
			// default action
			component.NotHandledTokens.Add( sensor );
		}
	}
	private static void parseAttribute( string attribute, AgentComponent component ){
		if( attribute.Length > 0 ){
			string[] attrValues = attribute.Split( new char[]{' '} );
			if( attrValues.Length > 1 ){
				switch( attrValues[0] ){
				case "move_spd.": component.MoveSpeedBuff = float.Parse(attrValues[1]); return;
				case "act._spd": component.ActionSpeedBuff = float.Parse(attrValues[1]); return;
				case "strength": component.StrengthBuff = float.Parse(attrValues[1]); return;
				case "vitality": component.VitalityBuff = float.Parse(attrValues[1]); return;
				case "stamina": component.StaminaBuff = float.Parse(attrValues[1]); return;
				default: component.NotHandledTokens.Add( attribute ); return;
				}
			}
			// default action
			component.NotHandledTokens.Add( attribute );
		}
	}
	private static void parseEffect( string effect, AgentComponent component ){
		if( effect.Length > 0 ){
			string[] attrValues = effect.Split( new char[]{' '} );
			if( attrValues.Length > 2 ){
				// Vision range
				if( attrValues[0] == "Vision" && attrValues[1] == "range" ){
					component.VisionRangeBuff = float.Parse(attrValues[2].Split( new char[]{'%'} )[0]) / 100f;
				}
				// Vibration range
				if( attrValues[0] == "Vibration" && attrValues[1] == "range" ){
					component.VibrationRange = float.Parse(attrValues[2].Split( new char[]{'%'} )[0]) / 100f;
				}
				// Heat range
				if( attrValues[0] == "Heat" && attrValues[1] == "range" ){
					component.HeatRangeBuff = float.Parse(attrValues[2].Split( new char[]{'%'} )[0]) / 100f;
				}
				// Smell range
				if( attrValues[0] == "Smell" && attrValues[1] == "range" ){
					component.SmellRangeBuff = float.Parse(attrValues[2].Split( new char[]{'%'} )[0]) / 100f;
				}
				// Indetectable
				if( attrValues[0] == "Indetectable" && attrValues[1] == "for" ){
					if( attrValues[2] == "vision" ){
						component.VisionIndetectable = true;
					}
					if( attrValues[2] == "vibration" ){
						component.VibrationIndetectable = true;
					}
					if( attrValues[2] == "heat" ){
						component.HeatIndetectable = true;
					}
					if( attrValues[2] == "smell" ){
						component.SmellIndetectable = true;
					}
				}
			}
			// default action
			component.NotHandledTokens.Add( effect );
		}
	}
}
