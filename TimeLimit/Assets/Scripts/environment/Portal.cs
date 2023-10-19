
using Game.player;
using UnityEngine;

namespace Game.Others
{
    public class Portal : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PlayerView>()!=null)
            {
                PlayerView player =other.GetComponent<PlayerView>();
                player.gameWon();
            }
        }
        
    }

}
