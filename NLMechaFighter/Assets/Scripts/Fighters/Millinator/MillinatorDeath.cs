using FSM;
using UnityEngine;

namespace Millinator
{
    public class MillinatorDeath : AState<MillinatorController>
    {
        private int _deathAnim;
        private float _switchTime;

        public override void Start(MillinatorController runner)
        {
            base.Start(runner);

            _deathAnim = Animator.StringToHash("Death");

            runner.anim.SetBool(_deathAnim, true);
            if (_switchTime <= 0)
            {
                _switchTime = 5f;
            }
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
            runner.healthBar.SetHealth(runner.Health);
            runner.anim.SetBool(_deathAnim, false);
            runner.isStunned = false;
        }
    }
}
