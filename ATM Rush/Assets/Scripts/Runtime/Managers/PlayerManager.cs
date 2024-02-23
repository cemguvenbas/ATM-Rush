using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Private Variables")]
    private PlayerData _data;

    [Header("Serialized Variables")]
    [SerializeField] private PlayerMovementController movementController;
    [SerializeField] private PlayerAnimationController animationController;
    [SerializeField] private PlayerPhysicsController physicsController;
    [SerializeField] private PlayerMeshController meshController;

    private void Awake()
    {
        _data = GetPlayerData();
        SendPlayerDataToControllers();
    }

    private PlayerData GetPlayerData()
    {
        return Resources.Load<CD_Player>("Data/CD_Player").Data;
    }

    private void SendPlayerDataToControllers()
    {
        movementController.SetMovementData(_data.MovementData);
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        InputSignals.Instance.onInputTaken += OnActivateMovement;
        InputSignals.Instance.onInputReleased += OnDeactivateMovement;
        InputSignals.Instance.onInputDragged += OnInputDragged;
        CoreGameSignals.Instance.onPlay += OnPlay;
        CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
        CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
        CoreGameSignals.Instance.onReset += OnReset;
    }

    private void OnActivateMovement()
    {
        //movementController.IsReadyToMove(true);
    }

    private void OnDeactivateMovement()
    {
        //movementController.IsReadyToMove(false);
    }

    private void OnInputDragged(HorizontalInputParams inputParams)
    {
        //movementController.UpdateInputValue(inputParams);
    }

    private void OnPlay()
    {
        //movementController.IsReadyToPlay(true);
    }

    private void OnLevelSuccessful()
    {
        //movementController.IsReadyToPlay(false);
    }

    private void OnLevelFailed()
    {
        //movementController.IsReadyToPlay(false);
    }

    private void OnReset()
    {
        //movementController.OnReset();
        //animationController.OnReset();
    }

    private void UnSubscribeEvents()
    {
        InputSignals.Instance.onInputTaken -= OnActivateMovement;
        InputSignals.Instance.onInputReleased -= OnDeactivateMovement;
        InputSignals.Instance.onInputDragged -= OnInputDragged;
        CoreGameSignals.Instance.onPlay -= OnPlay;
        CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
        CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
        CoreGameSignals.Instance.onReset -= OnReset;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

}
