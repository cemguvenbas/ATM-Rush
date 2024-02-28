using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ATMSignals : MonoSingleton<ATMSignals>
{
    public UnityAction<int> onSetAtmScoreText = delegate { };
}
