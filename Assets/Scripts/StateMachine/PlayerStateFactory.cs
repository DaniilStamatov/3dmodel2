using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.StateMachine
{
    public class PlayerStateFactory
    {
        private Hero _context;

        public PlayerStateFactory(Hero currentContext)
        {
            _context = currentContext;
        }

        public PlayerBaseState Walk() 
        {
            return new PlayerWalkState(_context, this);
        }
        
        public PlayerBaseState Run() 
        {
            return new PlayerRunningState(_context, this);
        }

        public PlayerBaseState Jump() {
            return new PlayerJumpState(_context, this);
        }
        public PlayerBaseState Grounded() {
            return new PlayerGroundedState(_context, this);
        }

        public PlayerBaseState Crouched()
        {
            return new PlayerCrouchState(_context, this);
        }

        public PlayerBaseState Combat()
        {
            return new PlayerCombatState(_context, this);
        }

        public PlayerBaseState Attack()
        {
            return new PlayerAttackState(_context, this);
        }

        public PlayerBaseState Arching()
        {
            return new PlayerArchingState(_context, this);
        }

        public PlayerBaseState Shooting()
        {
            return new PlayerShootingState(_context, this);
        }

    }
}
