using System.Text;
using _Scripts.Controllers;
using _Scripts.Core.Elements;
using _Scripts.Patterns;
using TMPro;
using UnityEngine;

public class CorrectElementsGroupsCounter : Singleton<CorrectElementsGroupsCounter>
{
    [SerializeField] private TextMeshProUGUI counterText;
    [SerializeField] private string counterPrefix = "правильних перемикань: ";

    private StringBuilder sb;

    private void Awake()
    {
        sb = new StringBuilder();
    }

    private void OnEnable()
    {
        Instance = this;
        SetText(ElementInGame.CorrectPressedElements, LevelManager.Instance.levelSo.elementsCountForCorrectResult);
    }

    public void SetText(int correctMovesCount, int maxMovesCount)
    {
        sb.Clear();
        sb.Append(counterPrefix);
        sb.Append(correctMovesCount);
        sb.Append('/');
        sb.Append(maxMovesCount);

        counterText.SetText(sb.ToString());
    }
}
