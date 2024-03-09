using UnityEngine.Events;

public class CollectableSignals : MonoSingleton<CollectableSignals>
{
    public UnityAction<int> onCollectableUpgrade = delegate { };
}
