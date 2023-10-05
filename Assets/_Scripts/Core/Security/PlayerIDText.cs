using TMPro;
using UnityEngine;

public class PlayerIDText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private string idPrefix = "ID користувача: ";
    
    private void OnEnable()
    {
        string idString = Mathf.Abs(PlayerPrefs.GetInt(PlayerID.PLAYER_UNIQ_ID)).ToString();
        text.text = idPrefix + idString.Substring(0,Mathf.Min(idString.Length, 6));
    }
}
