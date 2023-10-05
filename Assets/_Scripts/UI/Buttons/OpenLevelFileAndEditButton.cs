using UnityEngine;

namespace _Scripts.UI
{
	public class OpenLevelFileAndEditButton : CanvasSampleOpenFileText
	{
		[SerializeField] private CreatorWindowPasswordPanel _passwordPanel;
		
		protected override void OnLevelFileOpened()
		{
			base.OnLevelFileOpened();

			_passwordPanel.OpenEditLevelPasswordField();
		}
	}
}