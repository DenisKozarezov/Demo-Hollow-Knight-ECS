using Entitas;
using Core.Services;
using UnityEngine;

namespace Core.ECS.Systems
{
    public sealed class EmitInputSystem : IInitializeSystem, IExecuteSystem
    {
        private readonly IGroup<InputEntity> _leftMouse;
        private readonly IGroup<InputEntity> _rightMouse;
        private readonly IGroup<InputEntity> _keyboard;
        private readonly InputContext _inputContext;

        public EmitInputSystem(InputContext inputContext)
        {
            _inputContext = inputContext;
            _leftMouse = inputContext.GetGroup(InputMatcher.LeftMouse);
            _rightMouse = inputContext.GetGroup(InputMatcher.RightMouse);
            _keyboard = inputContext.GetGroup(InputMatcher.Keyboard);      
        }

        public void Initialize()
        {
            _inputContext.isLeftMouse = true;
            _inputContext.isRightMouse = true;
            _inputContext.isKeyboard = true;
        }
        public void Execute()
        {
            foreach (InputEntity leftMouse in _leftMouse)
            {
                foreach (InputEntity rightMouse in _rightMouse)
                {
                    foreach (InputEntity keyboard in _keyboard)
                    {
                        IInputService inputSystem = _inputContext.input.Value;

                        //Vector2 screenPosition = inputSystem.GetScreenMousePosition();
                        //Vector2 worldPosition = inputSystem.GetWorldMousePosition();

                        //leftMouse.isMouseDown = inputSystem.GetLeftMouseButtonDown();
                        //leftMouse.isMouse = inputSystem.GetLeftMouseButton();
                        //leftMouse.Do(l => l.ReplaceMouseScreenPosition(screenPosition), when: Input.GetLeftMouseButton());
                        //leftMouse.Do(l => l.ReplaceMouseWorldPosition(worldPosition), when: Input.GetLeftMouseButton());
                        //leftMouse.isMouseUp = inputSystem.GetLeftMouseButtonUp();

                        //rightMouse.isMouseDown = inputSystem.GetRightMouseButtonDown();
                        //rightMouse.isMouse = inputSystem.GetRightMouseButton();
                        //rightMouse.Do(r => r.ReplaceMouseScreenPosition(screenPosition), when: Input.GetRightMouseButton());
                        //rightMouse.Do(r => r.ReplaceMouseWorldPosition(worldPosition), when: Input.GetRightMouseButton());
                        //rightMouse.isMouseUp = inputSystem.GetRightMouseButtonUp();

                        keyboard.ReplaceHorizontal(inputSystem.Direction.x);
                        keyboard.ReplaceVertical(inputSystem.Direction.y);
                        keyboard.isJump = inputSystem.JumpHoldTime > 0;
                        keyboard.isLook = inputSystem.IsLook;
                        keyboard.isAttack = inputSystem.IsAttack;
                    }
                }
            }
        }
    }
}
