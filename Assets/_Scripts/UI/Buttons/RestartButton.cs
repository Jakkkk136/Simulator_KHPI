using _Scripts.Controllers;
using UnityEngine.SceneManagement;

namespace _Scripts.UI
{
    public sealed class RestartButton : BaseButton
    {
        protected override void OnClick()
        {
            base.OnClick();

            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);            
        }
    }
}
