using System;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABParser
{
    private ABModel model;
    private bool isInStatesBlock;
    private bool isInTransitionBlock;
    private bool isInSyntaxTreeBlock;
    private bool isInNodeBlock;

    private IABGateOperator curGateOperator;
    private List<ABNode> curNodes;

    private void InitialiseParser()
    {
        isInStatesBlock = false;
        isInTransitionBlock = false;
        isInSyntaxTreeBlock = false;
        isInNodeBlock = false;
        curGateOperator = null;
        curNodes = new List<ABNode>();
    }

    public ABModel Parse(List<string> lines)
    {
        InitialiseParser();
       
        model = new ABModel();

        foreach (string line in lines)
        {
            String[] tokens = line.Split(',');
            if (tokens.Length > 0 && tokens[0] != "" && !DetectBlocks(tokens))
            {
                if (isInStatesBlock)
                {
                    ParseStateLine(tokens);
                } else if (isInTransitionBlock)
                {
                    ParseTransitionLine(tokens);
                }
                else if (isInSyntaxTreeBlock)
                {
                    ParseSyntaxTreeLine(tokens);
                }
                else if (isInNodeBlock)
                {
                    ParseNodeLine(tokens);
                }
            }
        }

        return model;
    }

    private void ParseStateLine(String[] tokens)
    {
        string name = tokens[1];
        string type = tokens[2];

        if (type == "Init")
        {
            int id = model.AddState(name, null);
            model.InitStateId = id;
        } else if (type == "Inter")
        {
            model.AddState(name, null);
        } else
        {
            char[] separators = { '{', '}' };
            string[] subToks = type.Split(separators);
            ABAction action = ABActionFactory.CreateAction(subToks[1]);
            int id = model.AddState(name, action);
        }
    }

    private void ParseTransitionLine(String[] tokens)
    {
        model.LinkStates(tokens[1], tokens[2]);
    }

    private void ParseSyntaxTreeLine(String[] tokens)
    {
        curGateOperator = null;
        curNodes.Clear();

        int transId;
        if (int.TryParse(tokens[1], out transId))
        {
            curGateOperator = new AB_BoolGate_Operator();
            model.SetCondition(transId, (AB_BoolGate_Operator) curGateOperator);
        } else
        {
            char[] separators = { '-', '>' };
            String stateName = tokens[1].Split(separators)[0];
            String pinIdStr = tokens[1].Split(separators)[2];
            int id = int.Parse(pinIdStr);
            curGateOperator = model.FindStateGate(stateName, id);
        }
    }

    private void ParseNodeLine(String[] tokens)
    {
        String typeName = ExtractNodeType(tokens[1]);
        String typeParams = ExtractTypeParams(tokens[1]);

        //Create Node
        ABNode node = null;
        switch (typeName)
        {
            case "operator":
                node = (ABNode)ParseOperator(typeParams);
                break;
            case "param":
                node = (ABNode)ParseParam(typeParams);
                break;
            default:
                throw new NotSupportedException("Node type" + typeName + " not handled !");
        }
        curNodes.Add(node);

        //Link Node
        char[] separators = { '-', '>' };
        if (tokens.Length > 0 && tokens[2] != "")
        {
            String nodeIdStr = tokens[2].Split(separators)[0];
            String pinIdStr = tokens[2].Split(separators)[2];
            int nodeId = int.Parse(nodeIdStr);
            int pinId = int.Parse(pinIdStr);
            IABOperator outputNode = (IABOperator)curNodes[nodeId];
            outputNode.Inputs[pinId] = node;
            if (node == null || outputNode == null)
            {
                Debug.Log(node.GetType());
            }
            node.Output = (ABNode)outputNode;
        } else
        {
            curGateOperator.Inputs[0] = node;
            node.Output = (ABNode) curGateOperator;
        }
    }

    private IABParam ParseParam(string typeParams)
    {
        char[] separators = { ' ', '=', ':' };
        String[] tokens = typeParams.Split(separators);

        if (tokens.Length == 2)             //Param
        {
            return ABParamFactory.CreateParam(tokens[0], tokens[1], null);
        } else if (tokens.Length == 3)      //Contant
        {
            return ABParamFactory.CreateParam(tokens[1], tokens[0], tokens[2]);
        }

        return null;
    }

    private IABOperator ParseOperator(string typeParams)
    {
        return ABOperatorFactory.CreateOperator(typeParams);
    }

    private string ExtractTypeParams(string typeString)
    {
        Regex rgx = new Regex(@"\{.+\}");
        Match match = rgx.Match(typeString);
        String matchVal = match.Value;
        return matchVal.Substring(1, matchVal.Length - 2);
    }

    private string ExtractNodeType(String typeString)
    {
        String[] tokens = typeString.Split('{');
        return tokens[0];
    }

    private bool DetectBlocks(string[] tokens)
    {
        switch(tokens[0])
        {
            case "States":
                isInStatesBlock = true;
                isInTransitionBlock = false;
                isInSyntaxTreeBlock = false;
                isInNodeBlock = false;
                return true;
            case "Transitions":
                isInStatesBlock = false;
                isInTransitionBlock = true;
                isInSyntaxTreeBlock = false;
                isInNodeBlock = false;
                return true;
            case "Syntax Tree":
                isInStatesBlock = false;
                isInTransitionBlock = false;
                isInSyntaxTreeBlock = true;
                isInNodeBlock = false;
                return true;
            case "Nodes":
                isInStatesBlock = false;
                isInTransitionBlock = false;
                isInSyntaxTreeBlock = false;
                isInNodeBlock = true;
                return true;
            default:
                return false;
        }
    }
}
