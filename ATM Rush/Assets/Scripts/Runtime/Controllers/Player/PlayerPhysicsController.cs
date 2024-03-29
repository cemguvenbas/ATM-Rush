using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerPhysicsController : MonoBehaviour
{
    [Header("Serialized Variables")]
    [SerializeField] private Rigidbody managerRigidbody;

    [Header("Private Variables")]
    private readonly string _obstacle = "Obstacle";
    private readonly string _atm = "ATM";
    private readonly string _collectable = "Collectable";
    private readonly string _conveyor = "Conveyor";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(_obstacle))
        {
            managerRigidbody.transform.DOMoveZ(managerRigidbody.transform.position.z - 10f, 1f).SetEase(Ease.OutBack);
            return;
        }

        if (other.CompareTag(_atm))
        {
            CoreGameSignals.Instance.onAtmTouched?.Invoke(other.gameObject);
            return;
        }

        if (other.CompareTag(_collectable))
        {
            other.tag = "Collected";
            //StackSignals.Instance.onInteractionCollectable?.Invoke(other.transform.parent.gameObject);
            return;
        }

        if (other.CompareTag(_conveyor))
        {
            CoreGameSignals.Instance.onMiniGameEntered?.Invoke();
            return;
        }
    }
}
