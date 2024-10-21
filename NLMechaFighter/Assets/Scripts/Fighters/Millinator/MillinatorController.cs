using FSM;
using UnityEngine;


namespace Millinator
{
    public class MillinatorController : MonoBehaviour, IStateRunner, IFighter
    {
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


        private void Start()
        {
            //initialize statemachine and entry state
            stateMachine = new StateMachine<MillinatorController>(this);
            stateMachine.SetState(idleState);
        }

        private void Update()
        {
            stateMachine?.Update();
        }

    }
}
