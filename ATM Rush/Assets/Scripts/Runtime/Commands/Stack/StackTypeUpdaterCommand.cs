using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackTypeUpdaterCommand
{
    private List<GameObject> _collectableStack;
    private int _totalListScore;

    public StackTypeUpdaterCommand(ref List<GameObject> collectableStack)
    {
        _collectableStack = collectableStack;
    }
}
