using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public class PlayerJumpState : PlayerBaseState
    {
        public PlayerJumpState(Hero context, PlayerStateFactory playerStateFactory) : base(context, playerStateFactory)
        {
        }

        public override void CheckSwitchStates()
        {
            if (Context.IsGrounded)
            {
                SwitchState(Factory.Grounded());
            }
        }

        public override void EnterState()
        {
            Jump();
           SpeedUpFalling();
        }

        public override void ExitState()
        {
        }

        public override void InitializeSubState()
        {
        }

        public override void UpdateState()
        {
            CheckSwitchStates();
        }


        private void SpeedUpFalling()
        {
            if (Context.Rigidbody.velocity.y < 0f)
                Context.Rigidbody.velocity -= Vector3.down * Physics.gravity.y * Time.fixedDeltaTime;
        }

        private void Jump()
        {
            Context.Animator.SetTrigger("is-jumping");
            Context.Animator.SetFloat("speed", 0);
            Context.Rigidbody.velocity += Vector3.up * Context.JumpSpeeed;
            Context.IsJumpPressing = false;
        }
    }
}
