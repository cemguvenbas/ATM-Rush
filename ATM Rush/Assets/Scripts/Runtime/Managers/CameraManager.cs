using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using Unity.Mathematics;

public class CameraManager : MonoBehaviour
{
    [Header("Serialized Variables")]
    [SerializeField] private CinemachineStateDrivenCamera stateDrivenCamera;
    [SerializeField] private Animator animator;

    [Header("Private Variables")]
    private float3 _initialPoisiton;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        _initialPoisiton = transform.position;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        CoreGameSignals.Instance.onReset += OnReset;
        CameraSignals.Instance.onSetCinemachineTarget += OnSetCinemachineTarget;
        CameraSignals.Instance.onChangeCameraState += OnChangeCameraState;
    }

    private void OnChangeCameraState(CameraStates state)
    {
        animator.SetTrigger(state.ToString());
    }

    private void OnSetCinemachineTarget()
    {
        //var playerManager = FindObjectOfType<PlayerManager>().transform;
        //stateDrivenCamera.Follow = playerManager;
    }

    private void OnReset()
    {
        CameraSignals.Instance.onChangeCameraState?.Invoke(CameraStates.Idle);
        stateDrivenCamera.Follow = null;
        stateDrivenCamera.LookAt = null;
        transform.position = _initialPoisiton;
    }

    private void UnSubscribeEvents()
    {
        CoreGameSignals.Instance.onReset -= OnReset;
        CameraSignals.Instance.onSetCinemachineTarget -= OnSetCinemachineTarget;
        CameraSignals.Instance.onChangeCameraState -= OnChangeCameraState;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

}
