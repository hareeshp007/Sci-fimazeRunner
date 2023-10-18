
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
        public UiManger uiManager;

        public static event Action<int> AchevementsUnlock;
        private PlayerController playerController;
        [SerializeField]
        private AudioSource playerAudioSource;

        [SerializeField]
        private float groundDistance = 0.4f;
        [SerializeField]
        private int speed;
        [SerializeField]
        private Vector3 velocity;
        [SerializeField]
        private Vector3 move;
        [SerializeField]
        private float gravity;
        [SerializeField]
        private bool isGrounded;
        [SerializeField]
        private bool JumpButton;
        [SerializeField]
        private float jumpHeignt = 6f;

        [SerializeField]
        private  float jumbVariable = -2f;
        [SerializeField]
        private  float minYVelocity = -10f;
        private CharacterController characterController;
        private float verticalInput;
        private float horizontalInput;

        public int Healvalue;

        [SerializeField]
        private float hoverPower;
        [SerializeField]
        private float Runspeed;
        [SerializeField]
        private bool isRunning;
        [SerializeField]
        private bool isSoundPlay;

        [SerializeField]
        private float HealthDecreaseRate = 5f;


        public void SetPlayerController(PlayerController Controller)
        {
            this.playerController = Controller;
        }
        void Start()
        {
           characterController = GetComponent<CharacterController>();
           playerAudioSource = GetComponent<AudioSource>();
           SoundManager.Instance.SetPlayerSound(playerAudioSource);
           StartCoroutine(DealConstantDamage());
           uiManager.SetMaxHealth(playerController.GetMaxHealth());    
            
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
                    uiManager.Pause();
                }
                else
                {
                    uiManager.Resume();
                }
                
            }
            if(JumpButton= Input.GetButtonDown("Jump") && isGrounded)
            {
                SoundManager.Instance.PlayerSoundPlay(Sounds.jump);
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
                SoundManager.Instance.PlayerSoundLoopPlay(Sounds.walk);
                isSoundPlay = true;
                Debug.Log("ismoving");
            }if(!isRunning && isSoundPlay)
            {
                SoundManager.Instance.PlayerSoundStop(Sounds.walk);
                isSoundPlay = false;
            }

        }
        private void Run()
        {  
            move = transform.right * horizontalInput + transform.forward * verticalInput *Runspeed;
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
            characterController.Move(move * playerController.GetSpeed() *Time.deltaTime);
            if (velocity.y > minYVelocity)
            {
                velocity.y += gravity * Time.deltaTime;
            }
            characterController.Move(velocity *Time.deltaTime);
        }

        public void Death()
        {
            uiManager.GameOver();
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
            uiManager.HealthSet(health);
        }
        private void TakeDamage()
        {
           int health= playerController.TakeDamage();
           uiManager.HealthSet(health);
        }
        public void TakeDamage(int damage)
        {
            int health = playerController.TakeDamage(damage);
            uiManager.HealthSet(health);
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
            int length = Essential.AchevementO2Levels.Length;
            for (int i = 0; i < length; i++)
            {
                if (health >= Essential.AchevementO2Levels[i])
                {
                    AchevementsUnlock?.Invoke(length - i);
                    break; 
                }
            }
            
        }

        public void SetUIManager(UiManger _uiManger)
        {
            uiManager = _uiManger;
        }

        public void GameWon()
        {
            checkAchevement();
            uiManager.GameWonMenu();
        }

        public void Died()
        {
            uiManager.GameOver();
        }
    }
}