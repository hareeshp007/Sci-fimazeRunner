
    using Assets.Scripts.Essentials;
    using System;
    using System.Collections;
    using UnityEngine;
namespace Game.player
{
    public class PlayerView : MonoBehaviour,IDamagable
    {
        public Transform GroundCheck;
        public LayerMask GroundMask;
        public UiManger UIManager;

        public static event Action<int> AchevementsUnlock;
        private PlayerController playerController;
        [SerializeField]
        private AudioSource playerAudioSource;

        [SerializeField]
        private float groundDistance = 0.4f;
        [SerializeField]
        private Vector3 velocity;
        [SerializeField]
        private Vector3 move;
        [SerializeField]
        private float gravity;
        [SerializeField]
        private bool isGrounded;
        [SerializeField]
        private bool jumpButton;
        [SerializeField]
        private float jumpHeignt = 6f;
        [SerializeField]
        private  float jumbVariable = -2f;
        [SerializeField]
        private  float minYVelocity = -10f;
        [SerializeField]
        private CharacterController characterController;
        [SerializeField]
        private float hoverPower;
        [SerializeField]
        private float runSpeed;
        [SerializeField]
        private float speed;
        [SerializeField]
        private bool isRunning;
        [SerializeField]
        private bool isSoundPlay;
        [SerializeField]
        private float HealthDecreaseRate = 5f;

        private float verticalInput;
        private float horizontalInput;

        public int Healvalue;

        


        public void SetPlayerController(PlayerController Controller)
        {
            this.playerController = Controller;
        }
        void Start()
        {
           characterController = GetComponent<CharacterController>();
           playerAudioSource = GetComponent<AudioSource>();
            GameService.Instance.SoundManager.SetPlayerSound(playerAudioSource);
           StartCoroutine(DealConstantDamage());
           UIManager.SetMaxHealth(playerController.GetMaxHealth());    
            
        }

        void Update()
        {
            Checks();
            HandleInput();
            Movement();
        }
        private void Checks()
        {
            isGrounded =Physics.CheckSphere(GroundCheck.position, groundDistance, GroundMask);
            if(isGrounded && velocity.y < 0)
            {
                velocity.y = jumbVariable;
            }
        }
        private void HandleInput()
        {
            horizontalInput = Input.GetAxis("Horizontal");
            verticalInput = Input.GetAxis("Vertical");
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (Time.timeScale != 0)
                {
                    UIManager.Pause();
                }
                else
                {
                    UIManager.Resume();
                }
                
            }
            if(jumpButton= Input.GetButtonDown("Jump") && isGrounded)
            {
                GameService.Instance.SoundManager.PlayerSoundPlay(Sounds.jump);
                Jump();
                
            }
            if( Input.GetButton("Jump") && !isGrounded && velocity.y<0)
            {
                Hover();
            }
            
            else if (Input.GetKeyUp(KeyCode.LeftShift) ||!isGrounded)
            {
                isRunning= false;
            }        
            if((horizontalInput!=0 || verticalInput!=0 ) ) {

               
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Run();
                }
                else
                {
                    move = transform.right * horizontalInput + transform.forward * verticalInput;
                }
            }

            if (isRunning && !isSoundPlay)
            {
                GameService.Instance.SoundManager.PlayerSoundLoopPlay(Sounds.walk);
                isSoundPlay = true;
                Debug.Log("ismoving");
            }if(!isRunning && isSoundPlay)
            {
                GameService.Instance.SoundManager.PlayerSoundStop(Sounds.walk);
                isSoundPlay = false;
            }

        }
        private void Run()
        {
            runSpeed = playerController.GetRunSpeed();
            move = transform.right * horizontalInput + transform.forward * verticalInput *runSpeed;
            if(!isRunning && isGrounded)
            {
                Debug.Log("Running");
                isRunning = true;
            }
        }
        private void Hover()
        {
            velocity.y += gravity * Time.deltaTime * hoverPower;
        }
        private void Jump()
        {
            velocity.y = Mathf.Sqrt(jumpHeignt * jumbVariable * gravity);
        }
        private void Movement()
        {
            speed = playerController.GetSpeed();
            characterController.Move(move * speed * Time.deltaTime);
            if (velocity.y > minYVelocity)
            {
                velocity.y += gravity * Time.deltaTime;
            }
            characterController.Move(velocity *Time.deltaTime);
        }

        public void Death()
        {
            UIManager.GameOver();
        }

        public void Heal()
        {
            playerController.Heal();
            Debug.Log("Healing");
            ShowHealth();
        }
        public void ShowHealth()
        {
            int health= playerController.GetHealth();
            UIManager.HealthSet(health);
        }
        private void TakeDamage()
        {
           int health= playerController.TakeDamage();
           UIManager.HealthSet(health);
        }
        public void TakeDamage(int damage)
        {
            int health = playerController.TakeDamage(damage);
            UIManager.HealthSet(health);
        }
        public IEnumerator DealConstantDamage()
        {
            TakeDamage();
            yield return new WaitForSeconds(HealthDecreaseRate);
            if (playerController.GetHealth() > 0)
            {
                StartCoroutine(DealConstantDamage());
            }
        }
        private void checkAchevement()
        {
            int health=playerController.GetHealth();
            int length = GameConstants.Achevemento2levels.Length;
            for (int i = 0; i < length; i++)
            {
                if (health >= GameConstants.Achevemento2levels[i])
                {
                    AchevementsUnlock?.Invoke(length - i);
                    break; 
                }
            }
            
        }

        public void SetUIManager(UiManger _uiManger)
        {
            UIManager = _uiManger;
        }

        public void gameWon()
        {
            checkAchevement();
            UIManager.gameWonMenu();
        }

        public void Died()
        {
            UIManager.GameOver();
        }
    }
}