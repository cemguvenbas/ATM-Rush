using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackJumperCommand
{
    private StackData _data;
    private List<GameObject> _collectableStack;
    private Transform _levelHolder;

    public StackJumperCommand(StackData stackData, ref List<GameObject> collectableStack)
    {
        _data = stackData;
        _collectableStack = collectableStack;
        _levelHolder = GameObject.Find("LevelHolder").transform;
    }
}
