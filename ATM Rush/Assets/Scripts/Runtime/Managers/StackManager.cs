using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    [Header("Private Variables")]
    private StackData _data;
    private List<GameObject> _collectableStack = new List<GameObject>();
    private bool _lastCheck;

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

    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    private void UnSubscribeEvents()
    {

    }
}
