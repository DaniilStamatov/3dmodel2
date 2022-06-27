using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public class PlayerRunningState : PlayerBaseState
    {
        private float turnSmoothVelocity;

        public PlayerRunningState(Hero context, PlayerStateFactory playerStateFactory) : base(context, playerStateFactory)
        {
        }

        public override void CheckSwitchStates()
        {
            if (!Context.IsDashing)
            {
                SwitchState(Factory.Walk());
            }
        }

        public override void EnterState()
        {
            Debug.Log("Hello from sprintState");
        }

        public override void ExitState()
        {
        }

        public override void InitializeSubState()
        {
        }

        public override void UpdateState()
        {
            Context.Animator.SetFloat("speed", Context.Rigidbody.velocity.magnitude / Context.MaxSpeed);


            MoveAndLook();
            Context.SetLookDirection();
            CheckSwitchStates();
        }

        private void MoveAndLook()
        {
            Context.Rigidbody.AddForce(Context.SetDirection() * Context.AccelerationSpeed * Time.deltaTime, ForceMode.Impulse);
        }

    }
}
