using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


namespace Assets.Scripts.StateMachine
{
    public class PlayerGroundedState : PlayerBaseState
    {

        float timePassed;
        float fallingTime;

        public PlayerGroundedState(Hero context, PlayerStateFactory playerStateFactory) : base(context, playerStateFactory)
        {
        }

        public override void CheckSwitchStates()
        {
            if (timePassed > fallingTime)
            {
                Context.Animator.SetTrigger("is-moving");
                SwitchState(Factory.Walk());
            }
            timePassed+=Time.deltaTime;
        }

        public override void EnterState()
        {
            Debug.Log("Hello from LandingState");
            Context.Animator.SetTrigger("is-landing");
            timePassed = 0;
            fallingTime = 0.5f;
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
    }
}
