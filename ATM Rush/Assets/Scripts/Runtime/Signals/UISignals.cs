using System;
using UnityEngine.Events;

public class UISignals : MonoSingleton<UISignals>
{
    public UnityAction onSetIncomeLvlText = delegate { };
    public UnityAction onSetStackLvlText = delegate { };
    public UnityAction<byte> onSetNewLevelValue = delegate { };
    public UnityAction<int> onSetMoneyValue = delegate { };
    public Func<int> onGetMoneyValue = delegate { return 0; };

    public UnityAction onClickIncome = delegate { };
    public UnityAction onClickStack = delegate { };
}
