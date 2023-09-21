using Game.player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Others
{
    public class HealArea : MonoBehaviour
    {
        private void OnTriggerStay(Collider other)
        {
            if(other.GetComponent<PlayerView>() != null)
            {
                PlayerView player=other.GetComponent<PlayerView>();
                player.Heal();
            }
        }
    }

}
