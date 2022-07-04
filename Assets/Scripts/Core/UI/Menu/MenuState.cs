using UnityEngine;

namespace Core.UI
{
    public interface IState<T>
    {
        T Target { get; }
        void Enter();
        void Exit();
    }

    public interface IStateMachine<Type>
    {
        IState<Type> CurrentState { get; }
        void SwitchState<State>(State state) where State : IState<Type>;
    }

    public abstract class BaseMenuState : MonoBehaviour, IState<MenuController>
    {
        public MenuController Target { get; }

        public abstract void Enter();
        public abstract void Exit();
    }
}