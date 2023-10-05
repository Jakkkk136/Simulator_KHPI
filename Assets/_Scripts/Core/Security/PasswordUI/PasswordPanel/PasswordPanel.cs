using UnityEngine;

namespace _Scripts.Core.Security
{
	public abstract class PasswordPanel : MonoBehaviour
	{
		public static PasswordPanel ActivePanel;

		private void OnEnable()
		{
			ActivePanel = this;
		}

		public virtual void Close()
		{
			gameObject.SetActive(false);
		}
	}
}