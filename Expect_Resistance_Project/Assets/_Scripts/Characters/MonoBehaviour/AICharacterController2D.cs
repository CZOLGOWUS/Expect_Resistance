using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace noGame.Characters
{
    public class AICharacterController2D : GeneralCharacterController2D
    {
        public void OnAIHorizontal(float movementImput)
        {
            this.movementInput = movementImput;
        }


        public void OnAIJump(bool isAIJumping)
        {
            isJumpPressed = isAIJumping;
        }

        public void OnAIDownKey(bool isAIPressingDown)
        {
            if (isAIPressingDown)
            {
                isDownKeyPressed = true;
                CheckIfStandingOnPhasePlatform();

            }
            else
            {
                isDownKeyPressed = false;
            }
        }
    }

}