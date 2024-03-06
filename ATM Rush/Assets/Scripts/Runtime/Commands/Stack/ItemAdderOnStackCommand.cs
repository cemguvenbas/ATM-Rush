using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAdderOnStackCommand
{
    private StackManager _stackManager;
    private List<GameObject> _collectableStack;
    private StackData _data;

    public ItemAdderOnStackCommand(StackManager stackManager, ref List<GameObject> collectableStack,
        ref StackData stackData)
    {
        _stackManager = stackManager;
        _collectableStack = collectableStack;
        _data = stackData;
    }
}
