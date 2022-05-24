using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



namespace noGame.Characters
{
    public class PlayerCharacterController2D : GeneralCharacterController2D
    {

        [Header("Dash")]
        [SerializeField] private float dashCooldown = 2f;
        [SerializeField] private Vector2 dashVector;

        private float dashTimer = 0f;

        public void Update()
        {
            dashTimer += Time.deltaTime;
        }

        //input events
        public void OnHorizontal(InputAction.CallbackContext ctx)
        {
            if (ctx.performed || ctx.canceled)
                movementInput = ctx.ReadValue<float>();
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (ctx.performed || ctx.started)
            {
                isJumpPressed = true;
            }
            else if (ctx.canceled)
            {
                isJumpPressed = false;
            }
        }

        public void OnDownKey(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                isDownKeyPressed = true;

                CheckIfStandingOnPhasePlatform();

            }
            else if (ctx.canceled)
            {
                isDownKeyPressed = false;
            }
        }

        public void OnDashKeyPress(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                if (dashTimer >= dashCooldown && movementInput != 0f)
                {
                    AddForce(dashVector * Mathf.Sign(movementInput));
                    dashTimer = 0f;
                }
            }
        }

    }
}