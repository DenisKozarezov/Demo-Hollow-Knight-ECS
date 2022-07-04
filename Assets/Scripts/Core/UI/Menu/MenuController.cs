using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;

namespace Core.UI
{
    public enum MenuState : byte
    {
        Main = 0x00,
        Settings = 0x01,
        Credits = 0x02,
    }

    public class MenuController : MonoBehaviour, IStateMachine<MenuController>
    {
        [SerializeField]
        private SerializableDictionaryBase<MenuState, BaseMenuState> States = new SerializableDictionaryBase<MenuState, BaseMenuState>();

        private MenuState _currentState;
        public IState<MenuController> CurrentState => States[_currentState];

        public void GetBack()
        {

        }

        public void SwitchState(MenuState state)
        {
            _currentState = state;
            SwitchState(States[_currentState]);
        }
        public void SwitchState<State>(State state) where State : IState<MenuController>
        {
            CurrentState?.Exit();
            state.Enter();
        }
    }
}
