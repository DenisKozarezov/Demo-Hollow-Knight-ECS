using System;
using UnityEngine;
using Zenject;

namespace Core.ECS.Behaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class GeoView : CollisionEntityBehaviour, IPoolable<ushort, IMemoryPool>, IDisposable
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        private IMemoryPool _pool;

        protected override void Awake() { }

        private Vector2 GetRandomForce(float force)
        {
            float randomAngle = UnityEngine.Random.Range(50f, 80f);
            return Vector2.up.RotateVector(randomAngle).normalized * force;
        }
        public override void Dispose() => _pool?.Despawn(this);

        void IPoolable<ushort, IMemoryPool>.OnDespawned()
        {
            _pool = null;
        }
        void IPoolable<ushort, IMemoryPool>.OnSpawned(ushort value, IMemoryPool pool)
        {
            base.Awake();
            _pool = pool;
            Entity.ReplaceGeo(value);
            _rigidbody.velocity = GetRandomForce(15f);
        }

        public class Factory : PlaceholderFactory<ushort, GeoView> { }
    }
}