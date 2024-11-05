using FSM;
using UnityEngine;
using UnityEngine.Events;


namespace Millinator
{
    [RequireComponent(typeof(Rigidbody))]
    public class MillinatorController : MonoBehaviour, IStateRunner, IFighter
    {
        [Header("General")]
        public Animator anim;
        public Rigidbody rb;
        public Transform cam;

        [Header("StateMachine")]
        public StateMachine<MillinatorController> stateMachine;
        public ScratchPad sharedData => new ScratchPad();

        //States
        public MillinatorIdle idleState { get; private set; } = new MillinatorIdle();
        public MillinatorWalk walkState { get; private set; } = new MillinatorWalk();
        public MillinatorPunch punchState { get; private set; } = new MillinatorPunch();

        [Header("IFighter")]
        [SerializeField] private int _fighterIndex; 
        public int fighterIndex => _fighterIndex;

        [SerializeField] private float health;
        public float Health 
        {
            get => health;
            set => health = value;
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
                healthBar.SetMaxHealth(Health);
            }
        }

        private void Update()
        {
            stateMachine?.Update();
        }

        public void HealthUpdate(float amount)
        {
            Health -= amount;
            if (healthBar != null)
            {
                healthBar.SetHealth(Health);
            }
        }

        #region InputValues
        public void SetMoveVector(Vector2 value)
        {
            moveVector = value;
        }

        public void TriggerPunch()
        {
            stateMachine.SetState(punchState);
        }

        public void AddScore(int score)
        {
            this.score += score;
        }
        #endregion
    }
}
