
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

            LevelStatus levelStatus = LevelManeger.Instance.GetLevelStatus(LevelName);
            switch (levelStatus)
            {
                case LevelStatus.locked:
                    Debug.Log(Levelnum + " This Level is Locked:");
                    break;
                case LevelStatus.unlocked:
                    SoundManager.Instance.Play(Sounds.ButtonClick);
                    Debug.Log(Levelnum + " This Level is Unlocked:");
                    LevelManeger.Instance.LoadAnyLevel(Levelnum);
                    break;
                case LevelStatus.completed:
                    SoundManager.Instance.Play(Sounds.ButtonClick);
                    Debug.Log(Levelnum + " This Level is Completed:");
                    LevelManeger.Instance.LoadAnyLevel(Levelnum);
                    break;
            }

        }
    }

}
