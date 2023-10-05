using _Scripts.Patterns;
using _Scripts.Patterns.Events;

namespace _Scripts.Controllers
{
    public sealed class LevelManager : Singleton<LevelManager>
    {
        public LevelSO levelSo;

        private bool levelEndSet = false;
        
        private void Start()
        {
            this.OnEvent(EventID.LEVEL_START);
        }
        

        public void CheckLevelEnd(int correctPressedElements)
        {
            CorrectElementsGroupsCounter.Instance.SetText(correctPressedElements, levelSo.elementsCountForCorrectResult);
            
            if (correctPressedElements == levelSo.elementsCountForCorrectResult)
            {
                SetLevelEnd(true);
            }
        }
        
        public void SetLevelEnd(bool win)
        {
            if(levelEndSet == true) return;
            levelEndSet = true;
            
            this.OnEvent(win ? EventID.LEVEL_DONE : EventID.LEVEL_FAIL);
        }
    }
}
