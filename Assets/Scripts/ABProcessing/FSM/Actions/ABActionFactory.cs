using System;

public class ABActionFactory {
    public static ABAction CreateAction(String typeStr)
    {
        ActionType type = GetTypeFromStr(typeStr);
        return CreateAction(type);
    }

    private static ActionType GetTypeFromStr(string typeStr)
    {
        switch (typeStr)
        {
            case "drop":
                return ActionType.Drop;
            case "goto":
                return ActionType.Goto;
            case "hit":
                return ActionType.Hit;
            case "hold":
                return ActionType.Hold;
            case "lay":
                return ActionType.Lay;
            case "pick":
                return ActionType.Pick;
            case "spread":
                return ActionType.Spread;
            case "trace":
                return ActionType.Trace;
        }
        return ActionType.None;
    }

    public static ABAction CreateAction(ActionType type)
    {
        ABAction action = null;
        switch(type)
        {
            case ActionType.Drop:
                throw new NotImplementedException();
                break;
            case ActionType.Goto:
                action = new ABGotoAction();
                break;
            case ActionType.Hit:
                throw new NotImplementedException();
                break;
            case ActionType.Hold:
                throw new NotImplementedException();
                break;
            case ActionType.Lay:
                action =  new ABLayAction();
                break;
            case ActionType.Pick:
                throw new NotImplementedException();
                break;
            case ActionType.Spread:
                throw new NotImplementedException();
                break;
            case ActionType.Trace:
                action = new ABTraceAction();
                break;
        }

        return action;
    }
}
