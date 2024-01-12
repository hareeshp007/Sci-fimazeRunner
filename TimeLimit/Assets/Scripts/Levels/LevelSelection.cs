
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI
{
    [RequireComponent(typeof(Button))]
    public class LevelSelection : MonoBehaviour
    {
        [SerializeField]
        private Button LevelButton;
        public string LevelName;
        public int Levelnum;
        void Start()
        {
            LevelButton = GetComponent<Button>();
            LevelButton.onClick.AddListener(LevelSelect);
        }

        private void LevelSelect()
        {

            LevelStatus levelStatus = GameService.Instance.LevelManeger.GetLevelStatus(LevelName);
            switch (levelStatus)
            {
                case LevelStatus.locked:
                    Debug.Log(Levelnum + " This Level is Locked:");
                    break;
                case LevelStatus.unlocked:
                    GameService.Instance.SoundManager.Play(Sounds.ButtonClick);
                    Debug.Log(Levelnum + " This Level is Unlocked:");
                    GameService.Instance.LevelManeger.LoadAnyLevel(Levelnum);
                    break;
                case LevelStatus.completed:
                    GameService.Instance.SoundManager.Play(Sounds.ButtonClick);
                    Debug.Log(Levelnum + " This Level is Completed:");
                    GameService.Instance.LevelManeger.LoadAnyLevel(Levelnum);
                    break;
            }

        }
    }

}
