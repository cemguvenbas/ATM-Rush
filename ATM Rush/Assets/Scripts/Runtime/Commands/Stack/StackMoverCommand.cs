using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackMoverCommand
{
    private StackManager _stackManager;
    private StackData _data;
    public StackMoverCommand(StackManager stackManager, ref StackData stackData)
    {
        _stackManager = stackManager;
        _data = stackData;
    }
}
