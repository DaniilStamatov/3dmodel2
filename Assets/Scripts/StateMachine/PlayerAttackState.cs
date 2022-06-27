using Assets.Scripts.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.StateMachine
{
    public class PlayerAttackState : PlayerBaseState
    {
        private float _attackTime;
        private float _clipLength;
        private float _clipSpeed;

        public PlayerAttackState(Hero context, PlayerStateFactory factory) : base(context, factory)
        {
        }

        public override void CheckSwitchStates()
        {
            HowToAttack();
        }

        public override void EnterState()
        {
            Context.IsAttacking = false;
            Context.Animator.SetTrigger("is-attacking");
            Context.Animator.SetFloat("speed", 0f);
            _attackTime = 0f;
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

        private void HowToAttack()
        {
            _attackTime += Time.deltaTime;

            _clipLength = Context.Animator.GetCurrentAnimatorClipInfo(1)[0].clip.length;
            _clipSpeed = Context.Animator.GetCurrentAnimatorStateInfo(1).speed;

            if (_attackTime >= _clipLength / _clipSpeed && Context.IsAttacking)
            {
                SwitchState(Factory.Attack());
            }
            if (_attackTime >= _clipLength / _clipSpeed)
            {
                SwitchState(Factory.Combat());
                Context.Animator.SetTrigger("is-moving");
            }
        }

      
    }
}
