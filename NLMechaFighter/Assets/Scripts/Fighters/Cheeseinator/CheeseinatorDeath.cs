using FSM;
using UnityEngine;

namespace Cheeseinator
{
    public class CheeseinatorDeath : AState<CheeseinatorController>
    {
        private int _deathAnim;
        private float _switchTime;

        public override void Start(CheeseinatorController runner)
        {
            base.Start(runner);

            _deathAnim = Animator.StringToHash("Death");

            runner.anim.SetBool(_deathAnim, true);
            if (_switchTime <= 0)
            {
                _switchTime = 5f;
            }
        }

        public override void Update(CheeseinatorController runner)
        {
            base.Update(runner);
            _switchTime -= Time.deltaTime;

            if (_switchTime <= 0.0f)
            {
                onSwitch(runner.idleState);
            }
        }

        public override void Complete(CheeseinatorController runner)
        {
            base.Complete(runner);
            runner.healthBar.SetHealth(runner.Health);
            runner.anim.SetBool(_deathAnim, false);
            runner.isStunned = false;
        }
    }
}
