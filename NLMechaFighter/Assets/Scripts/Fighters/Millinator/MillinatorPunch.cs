using FSM;
using UnityEngine;

namespace Millinator
{
    public class MillinatorPunch : AState<MillinatorController>
    {
        private int _punchAnim;
        private float _switchTime;

        public override void Start(MillinatorController runner)
        {
            base.Start(runner);
            //start Punch Animation
            _punchAnim = Animator.StringToHash("Punch");
            runner.anim.SetTrigger(_punchAnim);
            _switchTime = runner.anim.GetCurrentAnimatorStateInfo(0).normalizedTime; //#Fix Get Animation time 
            //Debug.Log(_switchTime);
        }

        public override void Update(MillinatorController runner)
        {
            base.Update(runner);
            _switchTime -= Time.deltaTime;
            if (_switchTime <= 0.0f) 
            {
                onSwitch(runner.idleState);
            }
        }

        public override void Complete(MillinatorController runner)
        {
            base.Complete(runner);
            runner.anim.ResetTrigger(_punchAnim);
        }
    }
}
