using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CharacterSystem;

namespace CharacterSystem
{
    public class CharacterAnimationControl : MonoBehaviour
    {
        [SerializeField]
        private CharacterMovement charMove;
        [SerializeField]
        private Animator animator;      // ���� �Ҵ� ���ּ���

        private void Start()
        {
            charMove = GetComponent<CharacterMovement>() as CharacterMovement;
        }
       
        private void Update()
        {
            AnimationController();
        }
        public void AnimationController()
        {
            float mag = charMove.rigidbody.velocity.magnitude;
            animator.SetFloat("MoveSpeed", mag);
            animator.SetFloat("x", charMove.inputValue.x);
            animator.SetFloat("y", charMove.inputValue.y);
        }
    }

}
