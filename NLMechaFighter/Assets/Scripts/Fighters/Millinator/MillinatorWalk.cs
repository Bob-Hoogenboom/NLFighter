using FSM;
using UnityEngine;
using UnityEngine.Windows;

namespace Millinator
{
    public class MillinatorWalk : AState<MillinatorController>
    {
        private int _walkAnim;
        private float _walkSpeed = 20f;

        public override void Start(MillinatorController runner)
        {
            base.Start(runner);
            //start walk animation
            _walkAnim = Animator.StringToHash("IsWalking");
            runner.anim.SetBool(_walkAnim, true);
        }

        public override void Update(MillinatorController runner)
        {
            base.Update(runner);

            Move(runner);

            //transition to idle animation if no input is given     
            if (runner.moveVector.magnitude < 0.1)
            {
                onSwitch(runner.idleState);
            }

            //transition to punch when specific input is given
            if (runner.pressedPunched)
            {
                onSwitch(runner.punchState);
            }

        }

        public override void Complete(MillinatorController runner)
        {
            base.Complete(runner);
            runner.anim.SetBool(_walkAnim, false);
        }


        private void Move(MillinatorController runner)
        {
            //movement
            Vector3 verticalAxis = new Vector3(runner.cam.transform.forward.x, 0, runner.cam.transform.forward.z).normalized;
            Vector3 horizontalAxis = new Vector3(runner.cam.transform.right.x, 0, runner.cam.transform.right.z).normalized;

            //rotation
            float singleStep = _walkSpeed * Time.deltaTime ;
            Vector3 direction = new Vector3(runner.moveVector.x, 0.0f, runner.moveVector.y).normalized;
            direction = direction.x * horizontalAxis + direction.z * verticalAxis;

            Vector3 newDirection = Vector3.RotateTowards(runner.transform.forward, direction, singleStep, 0.0f);

            //Apply rotation and movement
            runner.transform.rotation = Quaternion.LookRotation(newDirection);
            runner.rb.MovePosition(runner.transform.position + direction * _walkSpeed * Time.deltaTime);
        }
    }
}
