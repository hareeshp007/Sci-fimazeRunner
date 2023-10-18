

namespace Game.player
{
    public class PlayerController
    {
        private PlayerModel _Model;
        private PlayerView _View;

        private int speed;
        public PlayerController(PlayerView playerView, PlayerModel playerModel,PlayerSO playerSO)
        {
            _Model = playerModel;
            _View = playerView;
            _Model.SetValues(playerSO);
            _Model.SetPlayerController(this);
            _View.SetPlayerController(this);
            speed = _Model.speed;
        }

        public int TakeDamage()
        {
            int damage=_Model.DamageValue;
            int health = _Model.health;
            if (health - damage > 0)
            {
                health -= damage;
                _Model.SetHealth(health);
                return health;
                
            }
            else
            {
                _View.Death();
                return 0;
            }
        }
        public int TakeDamage(int damage)
        {
            int health = _Model.health;
            if (health - damage > 0)
            {
                health -= damage;
                _Model.SetHealth(health);
                return health;
            }
            else
            {
                _View.Death();
                return 0;
            }
        }
        public int GetHealth()
        {
            return _Model.health;
        }
        public int GetMaxHealth()
        {
            return _Model.Maxhealth;
        }
        public int GetSpeed()
        {
            return _Model.speed;
        }

        public void Heal()
        {
            
            int health=GetHealth();
            int healValue = _Model.HealValue;
            int maxHealth =_Model.Maxhealth;
            if( health< maxHealth)
            {
                health += (healValue);
                
            }if(health> maxHealth)
            {
                health= maxHealth;
            }
            _Model.SetHealth(health);
        }
    }
}

