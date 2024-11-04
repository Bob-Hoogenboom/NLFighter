using FSM;
using UnityEngine;

namespace Cheeseinator
{
    [RequireComponent(typeof(Rigidbody))]
    public class CheeseinatorController : MonoBehaviour, IStateRunner, IFighter
    {
        [Header("General")]
        public Animator anim;
        public Rigidbody rb;
        public Transform cam;

        [Header("StateMachine")]
        public StateMachine<CheeseinatorController> stateMachine;
        public ScratchPad sharedData => new ScratchPad();

        //States
        public CheeseinatorIdle idleState { get; private set; } = new CheeseinatorIdle();
        public CheeseinatorMove walkState { get; private set; } = new CheeseinatorMove();
        public CheeseinatorPunch punchState { get; private set; } = new CheeseinatorPunch();

        [Header("IFighter")]
        [SerializeField] private int _fighterIndex;
        public int fighterIndex => _fighterIndex;

        [HideInInspector] public Vector2 moveVector;
        [HideInInspector] public bool pressedPunched = false;

        private void Start()
        {
            //initialize statemachine and entry state
            stateMachine = new StateMachine<CheeseinatorController>(this);
            stateMachine.SetState(idleState);

            anim = GetComponentInChildren<Animator>();
            rb = GetComponent<Rigidbody>();
            cam = Camera.main.transform;
        }

        private void Update()
        {
            stateMachine?.Update();
        }

        #region InputValues
        public void SetMoveVector(Vector2 value)
        {
            moveVector = value;
        }

        public void SetPunchBool(bool value)
        {
            pressedPunched = value;
            Debug.Log("Punched: " + value);
        }
        #endregion
    }
}
