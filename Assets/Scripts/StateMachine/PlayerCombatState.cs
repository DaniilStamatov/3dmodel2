using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts.StateMachine
{
    public class PlayerCombatState : PlayerBaseState
    {
        private EquipmentSystem system;
        

        private float turnSmoothVelocity;
        public PlayerCombatState(Hero context, PlayerStateFactory factory) : base(context, factory)
        {
        }

        public override void CheckSwitchStates()
        {
            if (Context.IsInCombat)
            {
                Context.Animator.SetTrigger("sheatWeapon");

                SwitchState(Factory.Walk());
            }
            if (Context.IsAttacking)
            {
                SwitchState(Factory.Attack());
            }
        }

        public override void EnterState()
        {

            Debug.Log("hello from combat State");
            Context.Animator.SetTrigger("drawWeapon");
        }

        public override void ExitState()
        {
            Context.IsInCombat = false;
        }

        public override void InitializeSubState()
        {
        }

        public override void UpdateState()
        {
            Context.Animator.SetFloat("speed", Context.Rigidbody.velocity.magnitude / Context.MaxSpeed);
            CheckSwitchStates();
            Context.SetLookDirection();

            MoveAndLook();
        }

        private void MoveAndLook()
        {
            Context.Rigidbody.AddForce(Context.SetDirection() * Context.Speed * Time.deltaTime, ForceMode.Impulse);
        }
    }
}
