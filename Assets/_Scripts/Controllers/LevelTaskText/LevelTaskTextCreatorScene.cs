using System;
using _Scripts.Patterns;
using TMPro;
using UnityEngine;

namespace _Scripts.Controllers.LevelTaskText
{
	public class LevelTaskTextCreatorScene : Singleton<LevelTaskTextCreatorScene>
	{
		[SerializeField] private TMP_InputField levelTaskInputField;

		private void OnEnable()
		{
			levelTaskInputField.SetTextWithoutNotify(LevelManager.Instance.levelSo.levelTaskText);
		}

		public string LevelTaskText => levelTaskInputField.text.ToString();
	}
}