using FSM;

namespace Millinator
{
    public class MillinatorWalk : AState<MillinatorController>
    {

        public override void Start(MillinatorController runner)
        {
            base.Start(runner);
            //start walk animation
        }

        public override void Update(MillinatorController runner)
        {
            base.Update(runner);
            //transition to idle animation if no input is given     
            if (runner.moveVector.magnitude > 0.1)
            {
                onSwitch(runner.idleState);
            }

            //transition to punch when specific input is given
        }

        public override void Complete(MillinatorController runner)
        {
            base.Complete(runner);
        }
    }
}