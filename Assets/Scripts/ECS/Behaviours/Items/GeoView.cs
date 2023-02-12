using System;
using UnityEngine;
using Zenject;

namespace Core.ECS.Behaviours
{
    [RequireComponent(typeof(Rigidbody2D))]
    public sealed class GeoView : CollisionEntityBehaviour, IPoolable<ushort, IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.velocity = GetRandomForce(15f);
        }
        public override void Dispose() => _pool?.Despawn(this);

        private Vector2 GetRandomForce(float force)
        {
            float randomAngle = UnityEngine.Random.Range(50f, 80f);
            return Vector2.up.RotateVector(randomAngle).normalized * force;
        }

        void IPoolable<ushort, IMemoryPool>.OnDespawned()
        {
            _pool = null;
        }
        void IPoolable<ushort, IMemoryPool>.OnSpawned(ushort value, IMemoryPool pool)
        {
            _pool = pool;
            Entity.ReplaceGeo(value);
        }

        public class Factory : PlaceholderFactory<ushort, GeoView> { }
    }
}