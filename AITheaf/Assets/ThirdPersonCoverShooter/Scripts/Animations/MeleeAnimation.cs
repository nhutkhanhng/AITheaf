﻿using UnityEngine;

namespace CoverShooter
{
    public class MeleeAnimation : StateMachineBehaviour
    {
        /// <summary>
        /// Limb that is used in the attack.
        /// </summary>
        [Tooltip("Limb that is used in the attack.")]
        public Limb Limb = Limb.RightHand;

        /// <summary>
        /// Normalized time in the animation when to start scanning for collisions.
        /// </summary>
        [Tooltip("Normalized time in the animation when to start scanning for collisions.")]
        [Range(0, 1)]
        public float ScanStart = 0;

        /// <summary>
        /// Normalized time in the animation when to stop scanning for collisions.
        /// </summary>
        [Tooltip("Normalized time in the animation when to stop scanning for collisions.")]
        [Range(0, 1)]
        public float ScanEnd = 1;

        /// <summary>
        /// Are there any following combo attacks.
        /// </summary>
        [Tooltip("Are there any following combo attacks.")]
        public bool EnableCombo = false;

        /// <summary>
        /// Normalized time to start checking for following combo attacks.
        /// </summary>
        [Tooltip("Normalized time to start checking for following combo attacks.")]
        [Range(0, 1)]
        public float ComboCheck = 0.5f;

        /// <summary>
        /// Normalized time in the animation when to stop the attack chain.
        /// </summary>
        [Tooltip("Normalized time in the animation when to stop the attack chain.")]
        [Range(0, 1)]
        public float End = 0.9f;

        /// <summary>
        /// Normalized time in the animation when to stop the attack chain.
        /// </summary>
        [Tooltip("Normalized time in the animation when to stop the attack chain.")]
        [Range(0, 1)]
        public float Moment = 0;

        public override void OnStateEnter(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            animator.ResetTrigger("ComboHit");
            animator.ResetTrigger("Hit");
            animator.ResetTrigger("EndMelee");

            var motor = CharacterMotor.animatorToMotorMap[animator];

            if (!motor.IsPerformingMelee)
                motor.InputMeleeAttackStart(animatorStateInfo.fullPathHash);
            else
                motor.InputMeleeComboStart(animatorStateInfo.fullPathHash);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            var motor = CharacterMotor.animatorToMotorMap[animator];

            if (!motor.IsPerformingMelee)
                motor.StopMeleeRootMotion();

            motor.InputEndMeleeScan(animatorStateInfo.fullPathHash, Limb);

            if (motor.IsPerformingMeleeId(animatorStateInfo.fullPathHash))
                motor.InputMeleeAttackEnd();
        }

        public override void OnStateUpdate(Animator animator, AnimatorStateInfo animatorStateInfo, int layerIndex)
        {
            var motor = CharacterMotor.animatorToMotorMap[animator];

            if (animatorStateInfo.normalizedTime >= End)
            {
                motor.InputMeleeMoment(animatorStateInfo.fullPathHash, Limb);

                if (motor.IsPerformingMeleeId(animatorStateInfo.fullPathHash))
                {
                    animator.SetTrigger("EndMelee");
                    motor.InputMeleeAttackEnd();
                }
            }
            else
            {
                if (animatorStateInfo.normalizedTime >= Moment)
                    motor.InputMeleeMoment(animatorStateInfo.fullPathHash, Limb);

                if (EnableCombo && animatorStateInfo.normalizedTime >= ComboCheck)
                    motor.InputMeleeComboCheck();

                if (animatorStateInfo.normalizedTime >= ScanEnd)
                    motor.InputEndMeleeScan(animatorStateInfo.fullPathHash, Limb);
                else if (animatorStateInfo.normalizedTime >= ScanStart)
                    motor.InputBeginMeleeScan(animatorStateInfo.fullPathHash, Limb);
            }
        }
    }
}
