using System;

namespace Game.player
{
    public class PlayerModel
    {
        private PlayerController playerController;

        public int health { get; private set; }
        public int Maxhealth { get; private set; }
        public int speed { get; private set; }
        public int Runspeed { get; private set; }
        public int HealValue { get; private set; }
        public float JumpHeight { get; private set; }
        public float JumpHoverValue { get; private set; }
        private float RotationSpeed { get; set; }

        public int KeysNeeded { get; set; }
        public int DamageValue { get; private set; }

        public PlayerModel()
        {

        }
        public void SetPlayerController(PlayerController Controller)
        {
            this.playerController = Controller;
        }
        public int SetHealth(int _health)
        {
            health = _health;
            return health;
        }
        public int SetSpeed(int _speed)
        {
            speed = _speed;
            return speed;
        }

        public void SetValues(PlayerSO playerSO)
        {
            Maxhealth = playerSO.maxHealth;
            health = Maxhealth;
            speed = playerSO.speed;
            Runspeed = playerSO.RunSpeed;
            JumpHeight = playerSO.jumpHeight;
            JumpHoverValue = playerSO.JumpHoveringValue;
            HealValue = playerSO.HealValue;
            DamageValue = playerSO.DamageValue;
        }
    }
}

