using FSM;
using UnityEngine;

namespace Millinator
{
    public class MillinatorWalk : AState<MillinatorController>
    {
        private int _walkAnim;
        public override void Start(MillinatorController runner)
        {
            base.Start(runner);
            //start walk animation
            _walkAnim = Animator.StringToHash("IsWalking");
            runner.anim.SetBool(_walkAnim, true);
        }

        public override void Update(MillinatorController runner)
        {
            base.Update(runner);
            //transition to idle animation if no input is given     
            if (runner.moveVector.magnitude < 0.1)
            {
                onSwitch(runner.idleState);
            }

            if (runner.pressedPunched)
            {
                onSwitch(runner.punchState);
            }

            //transition to punch when specific input is given
        }

        public override void Complete(MillinatorController runner)
        {
            base.Complete(runner);
            runner.anim.SetBool(_walkAnim, false);
        }
    }
}