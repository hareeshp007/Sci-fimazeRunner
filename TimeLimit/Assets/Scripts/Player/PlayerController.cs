

using System;

namespace Game.player
{
    public class PlayerController
    {
        private PlayerModel model;
        private PlayerView view;

        public PlayerController(PlayerView playerView, PlayerModel playerModel,PlayerSO playerSO)
        {
            model = playerModel;
            view = playerView;
            model.SetValues(playerSO);
            model.SetPlayerController(this);
            view.SetPlayerController(this);
        }

        public int TakeDamage()
        {
            int damage=model.DamageValue;
            int health = model.health;
            if (health - damage > 0)
            {
                health -= damage;
                model.SetHealth(health);
                return health;
                
            }
            else
            {
                view.Death();
                return 0;
            }
        }
        public int TakeDamage(int damage)
        {
            int health = model.health;
            if (health - damage > 0)
            {
                health -= damage;
                model.SetHealth(health);
                return health;
            }
            else
            {
                view.Death();
                return 0;
            }
        }
        public int GetHealth()
        {
            return model.health;
        }
        public int GetMaxHealth()
        {
            return model.Maxhealth;
        }
        public int GetSpeed()
        {
            return model.Speed;
        }

        public void Heal()
        {
            
            int health=GetHealth();
            int healValue = model.HealValue;
            int maxHealth =model.Maxhealth;
            if( health< maxHealth)
            {
                health += (healValue);
                
            }if(health> maxHealth)
            {
                health= maxHealth;
            }
            model.SetHealth(health);
        }

        public float GetRunSpeed()
        {
            return model.RunSpeed;
        }
    }
}

