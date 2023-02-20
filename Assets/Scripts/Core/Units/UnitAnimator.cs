using UnityEngine;
using DG.Tweening;
using Core.ECS.Components.Units;

namespace Core.Units
{
    public class UnitAnimator : MonoBehaviour
    {
        private readonly int _attackHash = Animator.StringToHash("Attack");
        private readonly int _attackUpHash = Animator.StringToHash("AttackUp");
        private readonly int _attackDownHash = Animator.StringToHash("AttackDown");
        private readonly int _hitHash = Animator.StringToHash("Hit");
        private readonly int _deathHash = Animator.StringToHash("Death");
        private readonly int _jumpingHash = Animator.StringToHash("IsJumping");
        private readonly int _jumpHash = Animator.StringToHash("Jump");
        private readonly int _onGroundHash = Animator.StringToHash("OnGround");
        private readonly int _runningHash = Animator.StringToHash("IsRunning");

        private readonly float Duration = 0.3f;
        private readonly Color WhiteColor = new Color(1, 1, 1, 0.9f);

        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private SpriteRenderer _renderer;
        [SerializeReference]
        private GameObject _defaultAttackVFX;
        [SerializeReference]
        private GameObject _attackUpVFX;
        [SerializeReference]
        private GameObject _attackDownVFX;

        public void PlayRun() => _animator.SetBool(_runningHash, true);
        public void PlayJump()
        {
            _animator.SetTrigger(_jumpHash);
            _animator.SetBool(_jumpingHash, true);
            _animator.SetBool(_onGroundHash, false);
        }
        public void PlayGrounded()
        {
            _animator.ResetTrigger(_jumpHash);
            _animator.SetBool(_jumpingHash, false);
            _animator.SetBool(_onGroundHash, true);
        }
        public void PlayDamageTaken()
        {
            _renderer
                .DOColor(WhiteColor, Duration)
                .OnComplete(() => _renderer.DOColor(Color.black, Duration))
                .SetEase(Ease.Linear);
        }
        public void PlayAttack(AttackDirection direction)
        {
            switch (direction)
            {
                case AttackDirection.Default:
                    _animator.SetTrigger(_attackHash);
                    if (_defaultAttackVFX)
                    {
                        _defaultAttackVFX.SetActive(true);
                        Delay.For(0.15f, andThen: () => _defaultAttackVFX.SetActive(false));
                    }
                    break;
                case AttackDirection.Up:
                    _animator.SetTrigger(_attackUpHash);
                    if (_attackUpVFX)
                    {
                        _attackUpVFX.SetActive(true);
                        Delay.For(0.15f, andThen: () => _attackUpVFX.SetActive(false));
                    }
                    break;
                case AttackDirection.Down:
                    _animator.SetTrigger(_attackDownHash);
                    if (_attackDownVFX)
                    {
                        _attackDownVFX.SetActive(true);
                        Delay.For(0.15f, andThen: () => _attackDownVFX.SetActive(false));
                    }
                    break;
            }
        }
        public void PlayDeath() => _animator.SetTrigger(_deathHash);
        public void PlayIdle() => _animator.SetBool(_runningHash, false);
        public void ResetAnimation()
        {
            _animator.ResetTrigger(_hitHash);
            _animator.ResetTrigger(_attackHash);
            _animator.ResetTrigger(_deathHash);
        }
    }
}
