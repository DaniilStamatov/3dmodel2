using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public class PlayerCrouchState : PlayerBaseState
    {

        private float turnSmoothVelocity;

        public PlayerCrouchState(Hero context, PlayerStateFactory playerStateFactory) : base(context, playerStateFactory)
        {
        }

        public override void CheckSwitchStates()
        {
            if (Context.IsCrouching)
            {
                Context.Animator.SetTrigger("is-moving");
                SwitchState(Factory.Walk());
            }
        }

        public override void EnterState()
        {
            Context.IsCrouching = false;
            Context.Animator.SetTrigger("is-crouched");
            Debug.Log("Hello from CrouchState");
        }

        public override void ExitState()
        {
            Context.IsCrouching = false;
        }

        public override void InitializeSubState()
        {
        }

        public override void UpdateState()
        {

            Context.Animator.SetFloat("speed", Context.Rigidbody.velocity.magnitude / Context.MaxSpeed);
            MoveAndLook();
            CheckSwitchStates();
        }

        private void MoveAndLook()
        {
            Context.Rigidbody.AddForce(Context.SetDirection() * Context.CrouchSpeed * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
