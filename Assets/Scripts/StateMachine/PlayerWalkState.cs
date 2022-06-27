using Assets.Scripts.Components.Model.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Components.Model;

namespace Assets.Scripts.StateMachine
{
    public class PlayerWalkState : PlayerBaseState
    {
        private float turnSmoothVelocity;


        public PlayerWalkState(Hero context, PlayerStateFactory playerStateFactory) : base(context, playerStateFactory)
        {
        }

        public override void CheckSwitchStates()
        {
            var swordCount = Context.Session.Data.Inventory.Count("Sword");
            var bowCount = Context.Session.Data.Inventory.Count("Bow");

            if (Context.IsJumpPressing&&Context.IsGrounded)
            {
                SwitchState(Factory.Jump());
            }
            if (Context.IsCrouching)
            {
                SwitchState(Factory.Crouched());
            }
            if (Context.IsDashing)
            {
                SwitchState(Factory.Run());
            }
            if (Context.IsInCombat && Context.IsSelectedDef(ItemTags.Sword))
            {
                Context.IsInCombat = false;
                SwitchState(Factory.Combat());
            }
            
            if (Context.IsInCombat && bowCount>0 && Context.IsSelectedDef(ItemTags.Bow))
            {
                Context.IsInCombat = false;
                SwitchState(Factory.Arching());
            }
        }

        public override void EnterState()
        {

            Debug.Log("Hello from WalkState");
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
