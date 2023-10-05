using _Scripts.Core.Security;

namespace _Scripts.UI
{
	public class ClosePasswordPanelButton : BaseButton
	{
		protected override void OnClick()
		{
			base.OnClick();
			
			PasswordPanel.ActivePanel.Close();
		}
	}
}