using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.InputSystem;

namespace Assets.Scripts.StateMachine
{
    public abstract class PlayerBaseState
    {

        protected Hero Context;
        protected PlayerStateFactory Factory;

        public PlayerBaseState(Hero context, PlayerStateFactory factory)
        {
            Factory = factory;
            Context = context;

        }
        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();

        public abstract void CheckSwitchStates();

        public abstract void InitializeSubState();

        protected void SwitchState(PlayerBaseState newState)
        {
            ExitState();

            newState.EnterState();
            Context.CurrentState = newState;
        }
    }
}
