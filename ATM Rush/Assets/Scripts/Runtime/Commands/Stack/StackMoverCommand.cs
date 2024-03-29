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
    public void Execute(float directionX, List<GameObject> collectableStack)
    {
        float direct = Mathf.Lerp(collectableStack[0].transform.localPosition.x, directionX,
            _data.LerpSpeed);
        collectableStack[0].transform.localPosition = new Vector3(direct, 1f, 0.335f);
        StackItemsLerpMove(collectableStack);
    }

    private void StackItemsLerpMove(List<GameObject> collectableStack)
    {
        for (int i = 1; i < collectableStack.Count; i++)
        {
            Vector3 pos = collectableStack[i].transform.localPosition;
            pos.x = collectableStack[i - 1].transform.localPosition.x;
            float direct = Mathf.Lerp(collectableStack[i].transform.localPosition.x, pos.x, _data.LerpSpeed);
            collectableStack[i].transform.localPosition = new Vector3(direct, pos.y, pos.z);
        }
    }
}
