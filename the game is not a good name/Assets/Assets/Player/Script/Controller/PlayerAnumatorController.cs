using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerTransform
{
    public class PlayerAnumatorController : MonoBehaviour
    {
        private Animator _animator;
        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
        }
        public void StartMove()
        {
            _animator.SetBool("Moving", true);
            _animator.SetFloat("Velocity X", 0);
            _animator.SetFloat("Velocity Z", 0);
        }
        public void EndMove()
        {
            _animator.SetBool("Moving", false);
        }
        public void MoveAnimator(float x, float z)
        {
            _animator.SetFloat("Velocity X", x);
            _animator.SetFloat("Velocity Z", z);
        }
        public void MoveAnimator(float radius)
        {
            _animator.SetFloat("Velocity Z", radius);
        }
    }
}
