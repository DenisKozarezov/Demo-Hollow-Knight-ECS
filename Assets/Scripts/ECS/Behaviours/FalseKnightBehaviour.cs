using UnityEngine;
using Core.Models;

namespace Core.ECS.Behaviours
{
    public sealed class FalseKnightBehaviour : EntityBehaviour
    {
        [SerializeField]
        private FalseKnightModel _model;
        [SerializeField]
        private BehaviourTree.Runtime.BehaviourTree _AI;

        private void Start()
        {
            Entity.isUnit = true;
            Entity.isBoss = true;
            Entity.isHittable = true;
            Entity.AddEnemy(_model);
            Entity.AddTransform(transform);
            Entity.AddPosition(transform.position);
            Entity.AddCurrentHp(_model.MaxHealth);
            Entity.AddMaxHp(_model.MaxHealth);
            Entity.AddMovable(_model.MovementSpeed);
            Entity.AddDamage(_model.BaseDamage);
            Entity.AddAttackRange(_model.AttackRange);
            Entity.AddBehaviourTree(_AI);
        }
    }
}
