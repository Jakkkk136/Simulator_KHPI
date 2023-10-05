using System;
using _Scripts.Controllers;
using UnityEngine;
using UnityEngine.UI;
using _Scripts.UI;
using TMPro;

namespace _Scripts.Core.Security
{
	[RequireComponent(typeof(Button))]
	public class SaveLevelPasswordButton : BaseButton
	{
		[SerializeField] private TMP_InputField passwordInputField;
		
		protected override void OnClick()
		{
			string hash = SecurePasswordHasher.Hash(passwordInputField.text.ToString());
			LevelManager.Instance.levelSo.SetLevelPasswordHash(hash);
			passwordInputField.text = String.Empty;
		}
	}
}