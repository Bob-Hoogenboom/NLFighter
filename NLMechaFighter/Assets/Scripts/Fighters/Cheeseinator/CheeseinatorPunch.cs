using FSM;
using UnityEngine;

namespace Cheeseinator
{
    public class CheeseinatorPunch : AState<CheeseinatorController>
    {
        private int _punchAnim;
        private float _switchTime;
        private RaycastHit _hit;
        private bool _donePunching = false;

        public override void Start(CheeseinatorController runner)
        {
            base.Start(runner);

            _punchAnim = Animator.StringToHash("Punch");
            runner.anim.SetTrigger(_punchAnim);
            if (_switchTime <= 0)
            {
                _switchTime = 1f; //#Fix Get Animation time 
            }
        }

        public override void Update(CheeseinatorController runner)
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

        public override void Complete(CheeseinatorController runner)
        {
            base.Complete(runner);

            runner.anim.ResetTrigger(_punchAnim);
            _donePunching = false;
        }

        private void CheckHit(CheeseinatorController runner)
        {
            Vector3 pos = runner.transform.position;
            Vector3 rayOrigin = new Vector3(pos.x, pos.y + 2, pos.z);

            if (Physics.Raycast(rayOrigin, runner.transform.TransformDirection(Vector3.forward), out _hit, 5f))
            {
                if (_hit.transform.gameObject.TryGetComponent(out IDestroyable destroyable))
                {
                    destroyable.DestructionUpdate();
                }

                if (_hit.transform.gameObject.TryGetComponent(out IFighter fighter))
                {
                    //calculate how close you are to other fighter
                    //deal damage approximatly on your distance
                }
            }
        }
    }
}
