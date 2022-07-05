using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public interface IPoolObject<T> where T : notnull, Object
    {
        IPoolContainer<T> Container { get; }
        void SetPool(IPoolContainer<T> pool);
        void Release();
        void Reset();
    }
    public interface IPoolContainer<T> where T : notnull, Object
    {
        Stack<IPoolObject<T>> Stack { get; }
        IPoolObject<T> Get();
        void Release(IPoolObject<T> obj);
        void Dispose();
    }

    public class ObjectPool<T> : IPoolContainer<T> where T : Object
    {
        private T _object;
        public Stack<IPoolObject<T>> Stack { private set; get; }

        public ObjectPool(T prefab, int capacity)
        {
            _object = prefab;
            Stack = new Stack<IPoolObject<T>>(capacity);

            for (int i = 0; i < capacity; i++)
            {
                Stack.Push(InstantiatePrefab());
            }
        }
        public ObjectPool(string prefabPath, int capacity)
        {
            _object = Resources.Load<T>(prefabPath);
            Stack = new Stack<IPoolObject<T>>(capacity);

            for (int i = 0; i < capacity; i++)
            {
                Stack.Push(InstantiatePrefab());
            }
        }

        private IPoolObject<T> InstantiatePrefab()
        {
#if UNITY_EDITOR
            if (_object == null)
            {
                Debug.LogError("Unable to instantiate an object for pool. Prefab is null.");
            }
#endif
            var obj = (IPoolObject<T>)GameObject.Instantiate(_object);
            obj.Release();
            return obj;
        }
        public void Dispose()
        {
            while (Stack.Count > 0)
            {
                GameObject.Destroy(Stack.Pop() as T);
            }
        }

        public IPoolObject<T> Get()
        {
            if (_object == null) return null;

            IPoolObject<T> obj;
            if (Stack.Count > 0)
            {
                obj = Stack.Pop();
                obj.Reset();
            }
            else
            {
                obj = InstantiatePrefab();
            }
            return obj;
        }

        public void Release(IPoolObject<T> obj)
        {
            if (obj != null)
            {
                obj.Release();
                if (obj.Container.Equals(this) && !Stack.Contains(obj)) Stack.Push(obj);
            }
        }
    }
}