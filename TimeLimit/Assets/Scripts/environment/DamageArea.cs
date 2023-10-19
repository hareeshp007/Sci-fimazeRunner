using Game.player;
using UnityEngine;

namespace Game.Others
{
    public class DamageArea : MonoBehaviour
    {
        public int Damage;
        private void OnTriggerStay(Collider other)
        {
            if (other.GetComponent<IDamagable>() != null)
            {
                Debug.Log("player Draining");
                PlayerView player = other.GetComponent<PlayerView>();
                player.TakeDamage(Damage);
            }
        }
    }

}
