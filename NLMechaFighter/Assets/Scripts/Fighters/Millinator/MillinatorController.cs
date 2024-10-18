using FSM;
using UnityEngine;


namespace Millinator
{
    public class MillinatorController : MonoBehaviour, IStateRunner
    {
        [Header("StateMachine")]
        public StateMachine<MillinatorController> stateMachine;
        public ScratchPad sharedData => new ScratchPad();

        //States
        public MillinatorIdle idleState { get; private set; } = new MillinatorIdle();
        public MillinatorWalk walkState { get; private set; } = new MillinatorWalk();


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
