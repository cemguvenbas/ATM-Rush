using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StackManager : MonoBehaviour
{
    [Header("Private Variables")]
    private StackData _data;
    private List<GameObject> _collectableStack = new List<GameObject>();

    public bool LastCheck { get; set; }

    [Header("Commands")]
    private StackMoverCommand _stackMoverCommand;
    private ItemRemoverOnStackCommand _itemRemoverOnStackCommand;
    private StackAnimatorCommand _stackAnimatorCommand;
    private StackInteractionWithConveyorCommand _stackInteractionWithConveyorCommand;
    private StackInitializerCommand _stackInitializerCommand;

    private readonly string _stackDataPath = "Data/CD_Stack";

    [Header("Serialized Variables")]
    [SerializeField] private GameObject money;

    public StackJumperCommand StackJumperCommand { get; private set; }

    public StackTypeUpdaterCommand StackTypeUpdaterCommand { get; private set; }
        
    public ItemAdderOnStackCommand AdderOnStackCommand { get; private set; }

    private void Awake()
    {
        _data = GetStackData();
        Init();
    }

    private StackData GetStackData()
    {
        return Resources.Load<CD_Stack>(_stackDataPath).Data;
    }

    private void Init()
    {
        _stackMoverCommand = new StackMoverCommand(this, ref _data);
        AdderOnStackCommand = new ItemAdderOnStackCommand(this, ref _collectableStack, ref _data);
        _itemRemoverOnStackCommand = new ItemRemoverOnStackCommand(this, ref _collectableStack);
        _stackAnimatorCommand = new StackAnimatorCommand(this, _data, ref _collectableStack);
        StackJumperCommand = new StackJumperCommand(_data, ref _collectableStack);
        _stackInteractionWithConveyorCommand = new StackInteractionWithConveyorCommand(this, ref _collectableStack);
        StackTypeUpdaterCommand = new StackTypeUpdaterCommand(ref _collectableStack);
        _stackInitializerCommand = new StackInitializerCommand(this, ref money);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        StackSignals.Instance.onInteractionCollectable += OnInteractionWithCollectable;
        StackSignals.Instance.onInteractionObstacle += _itemRemoverOnStackCommand.Execute;
        StackSignals.Instance.onInteractionATM += OnInteractionWithATM;
        StackSignals.Instance.onInteractionConveyor += _stackInteractionWithConveyorCommand.Execute;
        StackSignals.Instance.onStackFollowPlayer += OnStackMove;
        StackSignals.Instance.onUpdateType += StackTypeUpdaterCommand.Execute;
        CoreGameSignals.Instance.onPlay += OnPlay;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void OnInteractionWithCollectable(GameObject collectableGameObject)
    {
        DOTween.Complete(StackJumperCommand);
        AdderOnStackCommand.Execute(collectableGameObject);
        StartCoroutine(_stackAnimatorCommand.Execute());
        StackTypeUpdaterCommand.Execute();
    }

    private void OnInteractionWithATM(GameObject collectableGameObject)
    {
        //ScoreSignals.Instance.onSetAtmScore?.Invoke((int)collectableGameObject.GetComponent<CollectableManager>()
        //        .GetCurrentValue() + 1);
        if (LastCheck == false)
        {
            _itemRemoverOnStackCommand.Execute(collectableGameObject);
        }
        else
        {
            collectableGameObject.SetActive(false);
        }
    }

    private void OnStackMove(Vector2 direction)
    {
        transform.position = new Vector3(0, gameObject.transform.position.y, direction.y + 2f);
        if (gameObject.transform.childCount > 0)
        {
            _stackMoverCommand.Execute(direction.x, _collectableStack);
        }
    }

    private void OnPlay()
    {
        _stackInitializerCommand.Execute();
    }

    private void OnReset()
    {
        LastCheck = false;
        _collectableStack.Clear();
        _collectableStack.TrimExcess();
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {
        StackSignals.Instance.onInteractionCollectable -= OnInteractionWithCollectable;
        StackSignals.Instance.onInteractionObstacle -= _itemRemoverOnStackCommand.Execute;
        StackSignals.Instance.onInteractionATM -= OnInteractionWithATM;
        StackSignals.Instance.onInteractionConveyor -= _stackInteractionWithConveyorCommand.Execute;
        StackSignals.Instance.onStackFollowPlayer -= OnStackMove;
        StackSignals.Instance.onUpdateType -= StackTypeUpdaterCommand.Execute;
        CoreGameSignals.Instance.onPlay -= OnPlay;
        CoreGameSignals.Instance.onReset -= OnReset;
    }
}
