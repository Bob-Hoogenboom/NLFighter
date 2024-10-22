using FSM;

namespace Millinator
{
    public class MillinatorIdle : AState<MillinatorController>
    {
        public override void Start(MillinatorController runner)
        {
            base.Start(runner);
            //start default Idle animation
        }

        public override void Update(MillinatorController runner)
        {
            base.Update(runner);
            //trigger 2nd idle animation so often
            //pick a Taunt animation according to alpha buttons 1,2,3,4,etc.


            //transition to punch or walk animation if input is given
            if (runner.moveVector.magnitude > 0.1)
            {
                onSwitch(runner.walkState);
            }
        }

        public override void Complete(MillinatorController runner)
        {
            base.Complete(runner);
        }
    }
}
