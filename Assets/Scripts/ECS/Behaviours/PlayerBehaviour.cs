﻿using UnityEngine;
using Core.Models;

namespace Core.ECS.Behaviours
{
    public sealed class PlayerBehaviour : EntityBehaviour
    {
        [SerializeField]
        private PlayerModel _playerModel;

        private void Start()
        {
            Entity.isPlayer = true;
            Entity.isUnit = true;
            Entity.isHittable = true;
            Entity.isCanAttack = true;
            Entity.AddTransform(transform);
            Entity.AddPosition(transform.position);
            Entity.AddCurrentHp(_playerModel.MaxHealth);
            Entity.AddMaxHp(_playerModel.MaxHealth);
            Entity.AddMovable(_playerModel.MovementSpeed);
            Entity.AddDamage(_playerModel.BaseDamage);
            Entity.AddAttackRange(_playerModel.AttackRange);
            Entity.AddAttackCooldown(_playerModel.AttackCooldown);
            Entity.AddJump(_playerModel.JumpHeightRange);
            Entity.AddMaxEnergy(_playerModel.EnergyCapacity);
            Entity.AddCurrentEnergy(_playerModel.EnergyCapacity);
            Entity.AddCurrentGeo(0);
        }
    }
}
