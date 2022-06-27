using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public class PlayerArchingState : PlayerBaseState
    {
        private float turnSmoothVelocity;
        private EquipmentSystem system;


        public PlayerArchingState(Hero context, PlayerStateFactory factory) : base(context, factory)
        {
        }

        public override void CheckSwitchStates()
        {
            if (Context.IsInCombat)
            {
                SwitchState(Factory.Walk());
            }
            if (Context.IsAttacking)
            {
                SwitchState(Factory.Shooting());
            }
        }

        public override void EnterState()
        {
            Context.Animator.SetLayerWeight(3, 1f);
            Context.Animator.SetBool("is-arching", true);
        }

        public override void ExitState()
        {
            Context.IsInCombat = false;
            Context.Animator.SetBool("is-arching", false);

            //Context.Animator.SetLayerWeight(3, 0f);
        }

        public override void InitializeSubState()
        {
        }

        public override void UpdateState()
        {
            Context.Animator.SetFloat("speed", Context.Rigidbody.velocity.magnitude/ Context.MaxSpeed);

            CheckSwitchStates();
            MoveAndLook();
            Context.SetLookDirection();
        }

        private void MoveAndLook()
        {
            Context.Rigidbody.AddForce(Context.SetDirection() * Context.Speed * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
