using Assets.Scripts.Components;
using Assets.Scripts.Components.UI.Hud;
using System.Collections;
using UnityEngine;


namespace Assets.Scripts.StateMachine
{
    public class PlayerShootingState : PlayerBaseState
    {
        HudController controller;
        public PlayerShootingState(Hero context, PlayerStateFactory factory) : base(context, factory)
        {
        }

        public override void CheckSwitchStates()
        {
            if (!Context.IsAttacking)
            {
                SwitchState(Factory.Arching());
            }
        }

        public override void EnterState()
        {   
            ActivateAimCamera();
            Context.Animator.SetBool("is-shooting", true);
            Context.Animator.SetFloat("speed", 0f);
        }

        public override void ExitState()
        {
            ActivateStandartCamera();
            FireArrow();
            Context.Animator.SetBool("is-shooting", false);
            Context.Arrow.SetActive(false);
        }

        public override void InitializeSubState()
        {
        }
        
        public override void UpdateState()
        {
            ActivateArrowPrefab();
            CheckSwitchStates();
            Context.SetLookDirection();
            Context.Animator.SetFloat("speed", Context.Rigidbody.velocity.magnitude / Context.MaxSpeed);
            Context.Rigidbody.AddForce(Context.SetDirection() * Context.Speed * Time.deltaTime, ForceMode.Impulse);
        }

        

        private void FireArrow()
        {
            GameObject projectile = Object.Instantiate(Context.ArrowPrefab);
            projectile.transform.forward = Context.FollowTransform.transform.forward;
            projectile.transform.position = Context.FireTransform.position + Context.FireTransform.forward;

            projectile.GetComponent<ArrowProjectile>().Fire();
        }
        
        private void ActivateAimCamera()
        {
            Context.AimCamera.SetActive(true);
            Context.MainCamera.SetActive(false);
            var hud = HudController.FindObjectOfType<HudController>();
            hud._target.SetActive(true);
        }

        private void ActivateStandartCamera()
        {
            Context.AimCamera.SetActive(false);
            Context.MainCamera.SetActive(true);
            var hud = HudController.FindObjectOfType<HudController>();
            hud._target.SetActive(true);
        }

        private void ActivateArrowPrefab()
        {
            if (Context.Animator.GetCurrentAnimatorStateInfo(3).IsName("drawArrow"))
            {
                Context.Arrow.SetActive(false);
            }
            if (Context.Animator.GetCurrentAnimatorStateInfo(3).IsName("drawArrow 0"))
            {
                Context.Arrow.SetActive(true);
            }
        }





    }
}
