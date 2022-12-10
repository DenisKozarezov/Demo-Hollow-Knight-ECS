using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Core.ECS.Events.Player;
using Voody.UniLeo;

namespace Core
{
    public class GeoView : MonoBehaviour, IPoolable<IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        public event Action<GeoView> Disposed;

        public void Dispose()
        {
            _pool?.Despawn(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == Constants.PlayerLayer)
            {
                WorldHandler.GetWorld().NewEntity(new PlayerObtainedGeoEvent { Value = 3 });
                Dispose();
            }
        }

        void IPoolable<IMemoryPool>.OnDespawned()
        {
            _pool = null;
            Disposed?.Invoke(this);
        }
        void IPoolable<IMemoryPool>.OnSpawned(IMemoryPool pool)
        {
            _pool = pool;
        }

        public class Factory : PlaceholderFactory<GeoView> 
        {
            private LinkedList<GeoView> _geos = new LinkedList<GeoView>();
            public override GeoView Create()
            {
                GeoView geo = base.Create();
                geo.Disposed += OnGeoDisposed;
                _geos.AddLast(geo);
                return geo;
            }
            private void OnGeoDisposed(GeoView geo)
            {
                _geos.Remove(geo);
                geo.Disposed -= OnGeoDisposed;
            }
            public void Dispose()
            {
                while (_geos.Count > 0)
                {
                    _geos.First.Value.Dispose();
                }
            }
        }
    }
}