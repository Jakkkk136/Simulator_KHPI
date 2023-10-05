using System.Collections;
using System.Collections.Generic;
using _Scripts.Controllers;
using TMPro;
using UnityEngine;

public class LevelNameText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string idPrefix = "Назва рівня: ";

    private void OnEnable()
    {
        text.text = idPrefix + LevelManager.Instance.levelSo.levelName;
    }
}
