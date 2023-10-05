using System;
using _Scripts.Patterns;
using _Scripts.Patterns.Events;
using TMPro;
using UnityEngine;


namespace _Scripts.Controllers.LevelTaskText
{
	public class LevelTaskTextGame : Singleton<LevelTaskTextGame>
	{
		[SerializeField] private GameObject textGameObject;
		[SerializeField] private TextMeshProUGUI taskText;

		private bool taskTextSet;

		private bool onWinWindow;

		private void Awake()
		{
			this.Subscribe(EventID.LEVEL_DONE, OnLevelDone);
		}

		private void OnDestroy()
		{
			this.Unsubscribe(EventID.LEVEL_DONE, OnLevelDone);
		}

		private void OnEnable()
		{
			Instance = this;
			
			if (taskTextSet == false)
			{
				SetLevelTaskText(LevelManager.Instance.levelSo.GetLevelTaskText());
				
				taskTextSet = true;
			}
		}

		private void OnLevelDone()
		{
			ShowLevelTaskText(true);
			onWinWindow = true;
		}

		private void SetLevelTaskText(string taskText)
		{
			this.taskText.SetText(taskText);
		}

		public void ShowLevelTaskText(bool show)
		{
			if(onWinWindow == true) return;

			textGameObject.SetActive(show);
		}
	}
}