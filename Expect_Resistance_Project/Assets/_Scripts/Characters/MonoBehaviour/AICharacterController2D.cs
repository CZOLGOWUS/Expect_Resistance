using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace noGame.Characters
{
    public class AICharacterController2D : GeneralCharacterController2D
    {
        [Header("AI Utility")]
        [SerializeField] float sustainJumpTime = 0.05f;
        float sustainTimer;
        public void OnAIHorizontal(float movementImput)
        {
            this.movementInput = movementImput;
        }


        public void OnAIJump(bool isAIJumping)
        {
            isJumpPressed = isAIJumping;
            if(isAIJumping)
                sustainTimer = sustainJumpTime;
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

        private void Update()
        {
            if (sustainTimer>0)
            {
                sustainTimer -= Time.deltaTime;
                if (sustainTimer <= 0)
                {
                    sustainTimer = 0;
                    isJumpPressed = false;
                }
            }
        }
    }

}