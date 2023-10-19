
namespace Game.player
{
    public class PlayerModel
    {
        private PlayerController playerController;

        public int health { get; private set; }
        public int Maxhealth { get; private set; }
        public int Speed { get; private set; }
        public int RunSpeed { get; private set; }
        public int HealValue { get; private set; }
        public float JumpHeight { get; private set; }
        public float JumpHoverValue { get; private set; }
        public int DamageValue { get; private set; }

        public void SetPlayerController(PlayerController Controller)
        {
            this.playerController = Controller;
        }
        public int SetHealth(int _health)
        {
            health = _health;
            return health;
        }
        public int SetSpeed(int _Speed)
        {
            Speed = _Speed;
            return Speed;
        }

        public void SetValues(PlayerSO playerSO)
        {
            Maxhealth = playerSO.maxHealth;
            health = Maxhealth;
            Speed = playerSO.Speed;
            RunSpeed = playerSO.RunSpeed;
            JumpHeight = playerSO.jumpHeight;
            JumpHoverValue = playerSO.JumpHoveringValue;
            HealValue = playerSO.HealValue;
            DamageValue = playerSO.DamageValue;
        }
    }
}

