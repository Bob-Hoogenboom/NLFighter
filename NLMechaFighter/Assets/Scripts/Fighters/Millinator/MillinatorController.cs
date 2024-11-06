using FSM;
using UnityEngine;


namespace Millinator
{
    [RequireComponent(typeof(Rigidbody))]
    public class MillinatorController : MonoBehaviour, IStateRunner, IFighter
    {
        [Header("General")]
        public Animator anim;
        public Rigidbody rb;
        public Transform cam;
        [HideInInspector] public bool isStunned = false;

        [Header("StateMachine")]
        public StateMachine<MillinatorController> stateMachine;
        public ScratchPad sharedData => new ScratchPad();

        //States
        public MillinatorIdle idleState { get; private set; } = new MillinatorIdle();
        public MillinatorWalk walkState { get; private set; } = new MillinatorWalk();
        public MillinatorPunch punchState { get; private set; } = new MillinatorPunch();
        public MillinatorDeath deathState { get; private set; } = new MillinatorDeath();

        [Header("IFighter")]
        [SerializeField] private int _fighterIndex; 
        public int fighterIndex => _fighterIndex;

        [SerializeField] private float health;
        private float currentHealth;
        public float Health 
        {
            get => health;
            set => currentHealth = value;
        }
        public int score { get; private set; }

        public HealthBar healthBar;

        [Header("Input")]
        [HideInInspector] public Vector2 moveVector;


        private void Start()
        {
            //initialize statemachine and entry state
            stateMachine = new StateMachine<MillinatorController>(this);
            stateMachine.SetState(idleState);

            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody>();
            cam = Camera.main.transform;

            if (healthBar != null)
            {
                currentHealth = health;
                healthBar.SetMaxHealth(Health);
                Debug.Log("HP: " + Health);
            }
        }

        private void Update()
        {
            stateMachine?.Update();
        }

        public void HealthUpdate(float amount)
        {
            currentHealth -= amount;
            if (healthBar != null)
            {
                healthBar.SetHealth(currentHealth);

                if (currentHealth <= 0 && !isStunned)
                {
                    stateMachine.SetState(deathState);
                    isStunned = true;
                    currentHealth = health;
                }
            }
        }

        #region InputValues
        public void SetMoveVector(Vector2 value)
        {
            moveVector = value;
        }

        public void TriggerPunch()
        {
            if (!isStunned)
            {
                stateMachine.SetState(punchState);
            }
        }

        public void AddScore(int score)
        {
            this.score += score;
        }
        #endregion
    }
}
