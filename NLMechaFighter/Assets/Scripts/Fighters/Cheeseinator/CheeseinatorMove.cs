using FSM;
using Millinator;
using UnityEngine;

namespace Cheeseinator
{
    public class CheeseinatorMove : AState<CheeseinatorController>
    {
        private int _walkAnim;
        private float _walkSpeed = 1.5f;

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
        }

        public override void Complete(CheeseinatorController runner)
        {
            base.Complete(runner);
            runner.anim.SetBool(_walkAnim, false);
        }

        private void Move(CheeseinatorController runner)
        {
            // Movement and Direction Calculation
            Vector3 verticalAxis = new Vector3(runner.cam.transform.forward.x, 0, runner.cam.transform.forward.z).normalized;
            Vector3 horizontalAxis = new Vector3(runner.cam.transform.right.x, 0, runner.cam.transform.right.z).normalized;

            Vector3 direction = new Vector3(runner.moveVector.x, 0.0f, runner.moveVector.y).normalized;
            direction = direction.x * horizontalAxis + direction.z * verticalAxis;

            // Apply movement and rotation with delta time factored in only once
            float stepSpeed = _walkSpeed * Time.fixedDeltaTime;

            Vector3 newPosition = runner.transform.position + direction * stepSpeed;
            Vector3 newDirection = Vector3.RotateTowards(runner.transform.forward, direction, stepSpeed, 0.0f);

            runner.rb.MovePosition(newPosition);  // Smoothly move the character
            runner.transform.rotation = Quaternion.LookRotation(newDirection);  // Smoothly rotate the character
        }
    }
}
