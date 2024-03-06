using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackInitializerCommand
{
    private StackManager _stackManager;
    private GameObject _money;

    public StackInitializerCommand(StackManager stackManager,
        ref GameObject money)
    {
        _stackManager = stackManager;
        _money = money;
    }
}
