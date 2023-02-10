using UnityEngine;
using Core.Models;

namespace Core.ECS.Behaviours
{
    public sealed class PlayerBehaviour : EntityBehaviour
    {
        [SerializeField]
        private PlayerModel _playerModel;

        protected override void Start()
        {
            Entity.isPlayer = true;
            Entity.isUnit = true;
            Entity.isCanInteract = true;
            Entity.AddTransform(transform);
            Entity.AddPosition(transform.position);
            Entity.AddCurrentHp(_playerModel.MaxHealth);
            Entity.AddMaxHp(_playerModel.MaxHealth);
            Entity.AddMovable(_playerModel.MovementSpeed);
            Entity.AddDamage(_playerModel.BaseDamage);
            Entity.AddAttackRange(_playerModel.AttackRange);
            Entity.AddJump(_playerModel.JumpHeightRange);
            Entity.AddMaxEnergy(_playerModel.EnergyCapacity);
            Entity.AddCurrentEnergy(_playerModel.EnergyCapacity);
            Entity.AddCurrentGeo(0);
        }
    }
}
