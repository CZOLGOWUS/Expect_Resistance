using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;



namespace noGame.Characters
{
    public class PlayerCharacterController2D : GeneralCharacterController2D
    {
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
    }
}