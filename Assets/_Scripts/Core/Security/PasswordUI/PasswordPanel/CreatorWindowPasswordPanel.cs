using _Scripts.Controllers;
using _Scripts.Core.Security;
using UnityEngine;

public class CreatorWindowPasswordPanel : PasswordPanel
{
    [SerializeField] private GameObject editLevelPasswordWindow;
    [SerializeField] private EditLevelPasswordField _editLevelPasswordField;
    [Space]
    [SerializeField] private GameObject saveLevelPasswordWindow;

    public override void Close()
    {
        base.Close();
        
        editLevelPasswordWindow.SetActive(false);
        saveLevelPasswordWindow.SetActive(false);
        
        gameObject.SetActive(false);
    }

    public void OpenEditLevelPasswordField()
    {
        Close();

        gameObject.SetActive(true);
        editLevelPasswordWindow.SetActive(true);
        _editLevelPasswordField.SetPassword(LevelManager.Instance.levelSo.GetPasswordHash());
    }

    public void OpenSaveLevelPanel()
    {
        Close();
        
        gameObject.SetActive(true);
        saveLevelPasswordWindow.SetActive(true);
    }
}
