using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoreGameSignals : MonoSingleton<CoreGameSignals>
{
    public UnityAction<GameStates> onChangeGameStates = delegate { };
    public UnityAction<byte> onLevelInitialize = delegate { };
    public UnityAction onClearActiveLevel = delegate { };
    public UnityAction onLevelSuccessful = delegate { };
    public UnityAction onLevelFailed = delegate { };
    public UnityAction onNextLevel = delegate { };
    public UnityAction onRestartLevel = delegate { };
    public UnityAction onPlay = delegate { };
    public UnityAction onReset = delegate { };
    public Func<byte> onGetLevelID = delegate { return 0; };

    public UnityAction onMiniGameEntered = delegate { };
    public UnityAction<GameObject> onAtmTouched = delegate { };
    public UnityAction onMiniGameStart = delegate { };



    public Func<byte> onGetIncomeLevel = delegate { return 0; };
    public Func<byte> onGetStackLevel = delegate { return 0; };

}
