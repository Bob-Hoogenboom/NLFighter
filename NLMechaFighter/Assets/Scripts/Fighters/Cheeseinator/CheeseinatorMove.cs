using FSM;
using UnityEngine;

namespace Cheeseinator
{
    public class CheeseinatorMove : AState<CheeseinatorController>
    {
        private int _walkAnim;
        private float _walkSpeed = 20f;

        public override void Start(CheeseinatorController runner)
        {
            base.Start(runner);
            _walkAnim = Animator.StringToHash("IsMoving");
            runner.anim.SetBool(_walkAnim, true);
        }

        public override void Update(CheeseinatorController runner)
        {
            base.Update(runner);

            Move(runner);

            // Transition to idle animation if no input is given     
            if (runner.moveVector.magnitude < 0.1)
            {
                onSwitch(runner.idleState);
            }

            // Transition to punch when specific input is given
            if (runner.pressedPunched)
            {
                onSwitch(runner.punchState);
            }
        }

        public override void Complete(CheeseinatorController runner)
        {
            base.Complete(runner);
            runner.anim.SetBool(_walkAnim, false);
        }

        private void Move(CheeseinatorController runner)
        {
            // Movement
            Vector3 verticalAxis = new Vector3(runner.cam.transform.forward.x, 0, runner.cam.transform.forward.z).normalized;
            Vector3 horizontalAxis = new Vector3(runner.cam.transform.right.x, 0, runner.cam.transform.right.z).normalized;

            // Rotation
            float singleStep = _walkSpeed * Time.deltaTime;
            Vector3 direction = new Vector3(runner.moveVector.x, 0.0f, runner.moveVector.y).normalized;
            direction = direction.x * horizontalAxis + direction.z * verticalAxis;

            Vector3 newDirection = Vector3.RotateTowards(runner.transform.forward, direction, singleStep, 0.0f);

            // Apply rotation and movement
            runner.transform.rotation = Quaternion.LookRotation(newDirection);
            runner.rb.MovePosition(runner.transform.position + direction * _walkSpeed * Time.deltaTime);
        }
    }
}
