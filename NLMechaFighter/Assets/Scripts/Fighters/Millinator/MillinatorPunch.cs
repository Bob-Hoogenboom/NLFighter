using FSM;
using UnityEngine;
using UnityEngine.UIElements;

namespace Millinator
{
    public class MillinatorPunch : AState<MillinatorController>
    {
        private int _punchAnim;
        private float _switchTime;
        private RaycastHit _hit;
        private bool _donePunching = false;

        private float _damageMultiplier = 10f;
        private float _punchRange = 1f;

        public override void Start(MillinatorController runner)
        {
            base.Start(runner);

            _punchAnim = Animator.StringToHash("Punch");
            runner.anim.SetTrigger(_punchAnim);
            if (_switchTime <= 0)
            {
                _switchTime = 1f; //#Fix Get Animation time 
            }
        }

        public override void Update(MillinatorController runner)
        {
            base.Update(runner);
            
            _switchTime -= Time.deltaTime;

            if (_switchTime <= 0.0f && !_donePunching) 
            {
                _donePunching = true;
                CheckHit(runner);
                onSwitch(runner.idleState);
            }
        }

        public override void Complete(MillinatorController runner)
        {
            base.Complete(runner);

            runner.anim.ResetTrigger(_punchAnim);
            _donePunching = false;
        }

        private void CheckHit(MillinatorController runner)
        {
            Vector3 pos = runner.transform.position;
            Vector3 rayOrigin = new Vector3(pos.x, pos.y + 1, pos.z);

            if (Physics.Raycast(rayOrigin, runner.transform.TransformDirection(Vector3.forward), out _hit, _punchRange))
            {
                if (_hit.transform.gameObject.TryGetComponent(out IDestroyable destroyable))
                {
                    destroyable.DestructionUpdate();
                    runner.AddScore(destroyable.scoreValue);
                }

                if (_hit.transform.gameObject.TryGetComponent(out IFighter fighter))
                {
                    //calculate how close you are to other fighter
                    //deal damage approximatly on your distance
                    //calculate how close you are to other fighter
                    float dist = Vector3.Distance(_hit.transform.gameObject.transform.position, runner.transform.position);

                    fighter.HealthUpdate(_damageMultiplier / dist);
                    runner.AddScore(Mathf.RoundToInt(_damageMultiplier / dist));
                }
            }
        }
    }
}
