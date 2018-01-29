using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecieParser
{
    private Specie specie;

    private bool isInQueenCastBlock;
    private bool isInResourcesBlock;
    private bool isInCastBlock;

    private void InitialiseParser()
    {
        isInQueenCastBlock = false;
        isInResourcesBlock = false;
        isInCastBlock = false;
    }

    public Specie Parse(List<string> lines)
    {
        InitialiseParser();

        specie = new Specie();

        foreach (string line in lines)
        {
            string[] tokens = line.Split(',');
            if (tokens.Length > 0 && tokens[0] != "" && !DetectBlocks(tokens))
            {
                if (isInQueenCastBlock)
                {
                    ParseQueenCastLine(tokens);
                }
                else if (isInResourcesBlock)
                {
                    ParseResourcesLine(tokens);
                }
                else if (isInCastBlock)
                {
                    ParseCastLine(tokens);
                }
            }
        }

        return specie;
    }

    private void ParseCastLine(string[] tokens)
    {
        Cast cast = new Cast();
        cast.BehaviorModelIdentifier = tokens[1];
        int headSize = int.Parse(tokens[2]);
        for (int i = 3; i < headSize + 3; i++)
        {
            if (tokens[i] == "") break;
            int id = int.Parse(tokens[i]);
            AgentComponent component = ComponentFactory.CreateComponent(id);
            cast.Head.Add(component);
        }
        for (int i = headSize + 3; i < tokens.Length; i++)
        {
            if (tokens[i] == "") break;
            int id = int.Parse(tokens[i]);
            AgentComponent component = 
                ComponentFactory.CreateComponent(id);
            cast.Tail.Add(component);
        }
        specie.Casts.Add(tokens[0], cast);
    }

    private void ParseResourcesLine(string[] tokens)
    {
        specie.RedResAmount = float.Parse(tokens[0]);
        specie.GreenResAmount = float.Parse(tokens[1]);
        specie.BlueResAmount = float.Parse(tokens[2]);
    }

    private void ParseQueenCastLine(string[] tokens)
    {
        specie.QueenCastName = tokens[0];
    }

    private bool DetectBlocks(string[] tokens)
    {
        switch (tokens[0])
        {
            case "Queen Cast":
                isInQueenCastBlock = true;
                isInResourcesBlock = false;
                isInCastBlock = false;
                return true;
            case "Resources (rgb)":
                isInQueenCastBlock = false;
                isInResourcesBlock = true;
                isInCastBlock = false;
                return true;
            case "Name":
                isInQueenCastBlock = false;
                isInResourcesBlock = false;
                isInCastBlock = true;
                return true;
            default:
                return false;
        }
    }
}
