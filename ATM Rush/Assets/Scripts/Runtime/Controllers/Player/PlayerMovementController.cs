using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Private Variables")]
    private PlayerMovementData _data;

    internal void SetMovementData(PlayerMovementData movementData)
    {
        _data = movementData;
    }
}
