using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelPanelController : MonoBehaviour
{
    #region Self Variables

    #region Serialized Variables

    [SerializeField] private TextMeshProUGUI levelText, moneyText;

    #endregion

    #region Private Variables

    private int _moneyValue;

    #endregion

    #endregion

    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        UISignals.Instance.onSetNewLevelValue += OnSetNewLevelValue;
        UISignals.Instance.onSetMoneyValue += OnSetMoneyValue;
        UISignals.Instance.onGetMoneyValue += OnGetMoneyValue;
    }

    private int OnGetMoneyValue()
    {
        return _moneyValue;
    }

    private void OnSetNewLevelValue(byte levelValue)
    {
        levelText.text = "LEVEL " + ++levelValue;
    }

    private void OnSetMoneyValue(int moneyValue)
    {
        _moneyValue = moneyValue;
        moneyText.text = moneyValue.ToString();
    }

    private void UnsubscribeEvents()
    {
        UISignals.Instance.onSetNewLevelValue -= OnSetNewLevelValue;
        UISignals.Instance.onSetMoneyValue -= OnSetMoneyValue;
        UISignals.Instance.onGetMoneyValue -= OnGetMoneyValue;
    }

    private void OnDisable()
    {
        UnsubscribeEvents();
    }
}
