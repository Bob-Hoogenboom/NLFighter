using FSM;

namespace Millinator
{
    public class MillinatorPunch : AState<MillinatorController>
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
            //transition to punch when specific input is given
        }

        public override void Complete(MillinatorController runner)
        {
            base.Complete(runner);
        }
    }
}
