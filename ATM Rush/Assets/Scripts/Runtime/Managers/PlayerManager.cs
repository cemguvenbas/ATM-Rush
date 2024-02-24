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
        InputSignals.Instance.onInputTaken += () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
        InputSignals.Instance.onInputReleased += () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(false);
        InputSignals.Instance.onInputDragged += OnInputDragged;
        CoreGameSignals.Instance.onPlay += OnPlay;
        CoreGameSignals.Instance.onLevelSuccessful += () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(false);
        CoreGameSignals.Instance.onLevelFailed += () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(false);
        CoreGameSignals.Instance.onReset += OnReset;

        //ScoreSignals.Instance.onSetTotalScore += meshController.OnSetTotalScore();
        CoreGameSignals.Instance.onMiniGameEntered += OnMiniGameEntered;
    }

    private void OnMiniGameEntered()
    {
        PlayerSignals.Instance.onPlayConditionChanged?.Invoke(false);
        StartCoroutine(WaitForFinal());
    }

    private void OnInputDragged(HorizontalInputParams inputParams)
    {
        movementController.UpdateInputValue(inputParams);
    }

    private void OnPlay()
    {
        PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
        PlayerSignals.Instance.onChangePlayerAnimationState?.Invoke(PlayerAnimationStates.Run);
    }

    private void OnReset()
    {
        movementController.OnReset();
        animationController.OnReset();
    }

    private void UnSubscribeEvents()
    {
        InputSignals.Instance.onInputTaken -= () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(true);
        InputSignals.Instance.onInputReleased -= () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(false);
        InputSignals.Instance.onInputDragged -= OnInputDragged;
        CoreGameSignals.Instance.onPlay -= OnPlay;
        CoreGameSignals.Instance.onLevelSuccessful -= () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(false);
        CoreGameSignals.Instance.onLevelFailed -= () => PlayerSignals.Instance.onPlayConditionChanged?.Invoke(false);
        CoreGameSignals.Instance.onReset -= OnReset;

        //ScoreSignals.Instance.onSetTotalScore -= meshController.OnSetTotalScore();
        CoreGameSignals.Instance.onMiniGameEntered -= OnMiniGameEntered;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    internal void SetStackPosition()
    {
        var position = transform.position;
        Vector2 pos = new Vector2(position.x, position.z);
        //StackSignals.Instance.onStackFollowPlayer?.Invoke(pos);
    }
    private IEnumerator WaitForFinal()
    {
        PlayerSignals.Instance.onChangePlayerAnimationState?.Invoke(PlayerAnimationStates.Idle);
        yield return new WaitForSeconds(2f);
        gameObject.SetActive(false);
        CoreGameSignals.Instance.onMiniGameStart?.Invoke();
    }

}
