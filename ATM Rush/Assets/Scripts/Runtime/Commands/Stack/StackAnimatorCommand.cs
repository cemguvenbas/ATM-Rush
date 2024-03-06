using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackAnimatorCommand 
{
    private StackManager _stackManager;
    private StackData _stackData;
    private List<GameObject> _collectableStack;

    public StackAnimatorCommand(StackManager stackManager, StackData stackData,
        ref List<GameObject> collectableStack)
    {
        _stackManager = stackManager;
        _stackData = stackData;
        _collectableStack = collectableStack;
    }
}
