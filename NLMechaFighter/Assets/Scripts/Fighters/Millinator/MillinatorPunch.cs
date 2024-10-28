using FSM;
using UnityEngine;

namespace Millinator
{
    public class MillinatorPunch : AState<MillinatorController>
    {
        private int _punchAnim;
        private float _switchTime;
        private RaycastHit _hit;
        private bool _donePunching = false;

        public override void Start(MillinatorController runner)
        {
            base.Start(runner);
            //start Punch Animation
            _punchAnim = Animator.StringToHash("Punch");
            runner.anim.SetTrigger(_punchAnim);
            if(_switchTime <= 0)
            {
                _switchTime = runner.anim.GetCurrentAnimatorStateInfo(0).normalizedTime; //#Fix Get Animation time 
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
                Debug.Log("punch");
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
            Vector3 rayOrigin = new Vector3(pos.x, pos.y + 2, pos.z);

            if(Physics.Raycast(rayOrigin, runner.transform.TransformDirection(Vector3.forward), out _hit, 5f))
            {
                IDestroyable destroyable = _hit.transform.gameObject.GetComponent<IDestroyable>();
                if (destroyable != null)
                {
                    destroyable.DestructionUpdate();
                }

                IFighter fighter = _hit.transform.gameObject.GetComponent<IFighter>();
                if (fighter != null)
                {
                    //calculate how close you are to other fighter
                    //deal damage approximatly on your distance
                }
            }
        }
    }
}
