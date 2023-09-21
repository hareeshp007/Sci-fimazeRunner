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
                PlayerView player = other.GetComponent<PlayerView>();
                player.TakeDamage(Damage);
            }
        }
    }

}
