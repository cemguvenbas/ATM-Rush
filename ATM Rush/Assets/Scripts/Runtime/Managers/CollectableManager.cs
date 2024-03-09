using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    private CollectableData _data;
    private byte _currentValue = 0;
    private readonly string _collectableDataPath = "Data/CD_Collectable";

    [SerializeField] private CollectableMeshController meshController;
    [SerializeField] private CollectablePhysicsController physicsController;

    private void Awake()
    {
        _data = GetCollectableData();
        SendDataToController();
    }

    private CollectableData GetCollectableData()
    {
        return Resources.Load<CD_Collectable>(_collectableDataPath).Data;
    }

    private void SendDataToController()
    {
        meshController.SetMeshData(_data.MeshData);
    }

    internal void CollectableUpgrade(int value)
    {
        if (_currentValue < 2) _currentValue++;
        meshController.UpgradeCollectableVisual(_currentValue);
        StackSignals.Instance.onUpdateType?.Invoke();
    }

    public byte GetCurrentValue()
    {
        return _currentValue;
    }

    public void InteractionWithCollectable(GameObject collectableGameObject)
    {
        StackSignals.Instance.onInteractionCollectable?.Invoke(collectableGameObject);
    }

    public void InteractionWithAtm(GameObject collectableGameObject)
    {
        StackSignals.Instance.onInteractionATM?.Invoke(collectableGameObject);
    }

    public void InteractionWithObstacle(GameObject collectableGameObject)
    {
        StackSignals.Instance.onInteractionObstacle?.Invoke(collectableGameObject);
    }

    public void InteractionWithConveyor()
    {
        StackSignals.Instance.onInteractionConveyor?.Invoke();
    }
}
