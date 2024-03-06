using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackInteractionWithConveyorCommand 
{
    private StackManager _stackManager;
    private List<GameObject> _collectableStack;
    private Transform _levelHolder;

    public StackInteractionWithConveyorCommand(StackManager stackManager, ref List<GameObject> collectableStack)
    {
        _stackManager = stackManager;
        _collectableStack = collectableStack;
        _levelHolder = GameObject.Find("LevelHolder").transform;
    }
}
