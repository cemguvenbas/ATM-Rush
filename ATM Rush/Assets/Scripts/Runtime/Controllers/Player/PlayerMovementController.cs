using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private PlayerManager manager;
    [SerializeField] private new Rigidbody rigidbody;

    #endregion

    #region Private Variables

    [Header("Data")] 
    private PlayerMovementData _data;
    private bool _isReadyToMove, _isReadyToPlay;
    private float _inputValue;
    private Vector2 _clampValues;

    #endregion

    #endregion

    internal void SetMovementData(PlayerMovementData movementData)
    {
        _data = movementData;
    }
    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        PlayerSignals.Instance.onPlayConditionChanged += OnPlayConditionChanged;
        PlayerSignals.Instance.onMoveConditionChanged += OnMoveConditionChanged;
    }

    private void OnPlayConditionChanged(bool condition) => _isReadyToPlay = condition;
    private void OnMoveConditionChanged(bool condition) => _isReadyToMove = condition;
    private void UnSubscribeEvents()
    {
        PlayerSignals.Instance.onPlayConditionChanged -= OnPlayConditionChanged;
        PlayerSignals.Instance.onMoveConditionChanged -= OnMoveConditionChanged;
    }

    private void OnDisable()
    {
        UnSubscribeEvents();
    }

    internal void UpdateInputValue(HorizontalInputParams inputParams)
    {
        _inputValue = inputParams.HorizontalInputValue;
        _clampValues = inputParams.HorizontalInputClampSides;
    }
    private void Update()
    {
        if (_isReadyToPlay)
        {
            manager.SetStackPosition();
        }
    }

    private void FixedUpdate()
    {
        if (_isReadyToPlay)
        {
            if (_isReadyToMove)
            {
                Move();
            }
            else
            {
                StopSideways();
            }
        }
        else
            Stop();
    }

    private void Move()
    {
        var velocity = rigidbody.velocity;
        velocity = new Vector3(_inputValue * _data.HorizontalSpeed, velocity.y,
            _data.ForwardSpeed);
        rigidbody.velocity = velocity;

        Vector3 position;
        position = new Vector3(
            Mathf.Clamp(rigidbody.position.x, _clampValues.x,
                _clampValues.y),
            (position = rigidbody.position).y,
            position.z);
        rigidbody.position = position;
    }

    internal void OnReset()
    {
        Stop();
        _isReadyToPlay = false;
        _isReadyToMove = false;
    }
    private void StopSideways()
    {
        rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _data.ForwardSpeed);
        rigidbody.angularVelocity = Vector3.zero;
    }

    private void Stop()
    {
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = Vector3.zero;
    }
}
