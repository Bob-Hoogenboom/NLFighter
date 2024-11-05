using FSM;
using UnityEngine;

namespace Cheeseinator
{
    public class CheeseinatorIdle : AState<CheeseinatorController>
    {
        private int _idleAnim;

        public override void Start(CheeseinatorController runner)
        {
            base.Start(runner);
        }

        public override void Update(CheeseinatorController runner)
        {
            base.Update(runner);
            //#trigger 2nd idle animation so often

            //#pick a Taunt animation according to alpha buttons 1,2,3,4,etc.

            //transition to punch or walk animation if input is given
            if (runner.moveVector.magnitude > 0.1)
            {
                onSwitch(runner.walkState);
            }

        }

        public override void Complete(CheeseinatorController runner)
        {
            base.Complete(runner);
        }
    }
}
