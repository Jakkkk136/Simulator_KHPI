using System;
using _Scripts.Core.Security;
using TMPro;
using UnityEngine;

public abstract class PasswordField : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    private string hash;
    
    private void OnEnable()
    {
        inputField.text = String.Empty;
    }

    private void Update()
    {
        if(hash == null) return;
        
        if (SecurePasswordHasher.Verify(inputField.text, hash))
        {
            OnCorrectPassword();
        }
    }

    public void SetPassword(string hash)
    {
        if (hash == null || String.IsNullOrWhiteSpace(hash))
        {
            OnCorrectPassword();
        }
        else
        {
            this.hash = hash;
        }
    }

    
    protected virtual void OnCorrectPassword()
    {
        PasswordPanel.ActivePanel.Close();
    }
}
