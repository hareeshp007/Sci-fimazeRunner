namespace Game.player
{
    using System;
    using System.Collections;
    using UnityEngine;

    public class PlayerView : MonoBehaviour,IDamagable
    {
        public Transform GroundCheck;
        public LayerMask GroundMask;
        public UiManger uiManager;

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



        private CharacterController characterController;
        private float verticalInput;
        private float horizontalInput;

        public int Healvalue;

        [SerializeField]
        private float hoverPower;
        [SerializeField]
        private float Runspeed;
        [SerializeField]
        private bool isMoving;
        [SerializeField]
        private bool ishover;
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
                velocity.y = -2f;
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
                ishover = true;
                Hover();
            }
            
            else if (Input.GetKeyUp(KeyCode.LeftShift) ||!isGrounded)
            {
                isRunning= false;
            }        
            if((horizontalInput!=0 || verticalInput!=0 ) ) {
                isMoving = true;
               
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Run();
                }
                else
                {
                    move = transform.right * horizontalInput + transform.forward * verticalInput;
                }
            }
            else
            {
                isMoving = false;
                
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
            velocity.y = Mathf.Sqrt(jumpHeignt * -2 * gravity);
        }
        private void Movement()
        {
            characterController.Move(move * playerController.GetSpeed() *Time.deltaTime);
            if (velocity.y > -10f)
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

        public void SetUIManager(UiManger _uiManger)
        {
            uiManager = _uiManger;
        }

        internal void GameWon()
        {
            uiManager.GameWonMenu();
        }

        internal void Died()
        {
            uiManager.GameOver();
        }
    }
}