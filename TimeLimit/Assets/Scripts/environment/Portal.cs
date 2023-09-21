using Assets.Scripts.Essentials;
using Game.player;
using Game.UI;
using UnityEngine;

namespace Game.Others
{
    public class Portal : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerView>()!=null)
            {
                UIService.Instance.GameWon();
            }
        }
        
    }

}
