using _Scripts.UI;
using UnityEngine;

namespace _Scripts.Core.Security
{
	public class OpenSaveLevelPanelButton : BaseButton
	{
		[SerializeField] private CreatorWindowPasswordPanel _passwordPanel;

		protected override void OnClick()
		{
			base.OnClick();
			
			_passwordPanel.OpenSaveLevelPanel();
		}
	}
}