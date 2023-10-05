public class EditLevelPasswordField : PasswordField
{
	protected override void OnCorrectPassword()
	{
		base.OnCorrectPassword();
		CreatorWindow.Instance.EditExistingLevelConfig();
	}
}
