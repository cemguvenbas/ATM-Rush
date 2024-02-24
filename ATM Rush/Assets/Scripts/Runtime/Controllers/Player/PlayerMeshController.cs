using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerMeshController : MonoBehaviour
{
    [Header("Serialized Variables")]
    [SerializeField] private TextMeshPro scoreText;

    internal void OnSetTotalScore(int value)
    {
        scoreText.text = value.ToString();
    }
}
