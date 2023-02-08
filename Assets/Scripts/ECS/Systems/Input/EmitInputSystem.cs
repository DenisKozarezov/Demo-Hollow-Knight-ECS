using UnityEngine;
using Entitas;
using Core.Services;

namespace Core.ECS.Systems
{
    public class EmitInputSystem : IExecuteSystem
    {
        private readonly IGroup<InputEntity> _leftMouse;
        private readonly IGroup<InputEntity> _rightMouse;
        private readonly IGroup<InputEntity> _keyboard;
        private readonly IInputService _inputSystem;

        public EmitInputSystem(InputContext inputContext)
        {
            _inputSystem = inputContext.input as IInputService;
            _leftMouse = inputContext.GetGroup(InputMatcher.LeftMouse);
            _rightMouse = inputContext.GetGroup(InputMatcher.RightMouse);
            _keyboard = inputContext.GetGroup(InputMatcher.Keyboard);
        }

        public void Execute()
        {
            foreach (InputEntity leftMouse in _leftMouse)
            {
                foreach (InputEntity rightMouse in _rightMouse)
                {
                    foreach (InputEntity keyboard in _keyboard)
                    {
                        Vector2 screenPosition = _inputSystem.GetScreenMousePosition();
                        Vector2 worldPosition = _inputSystem.GetWorldMousePosition();

                        leftMouse.isMouseDown = _inputSystem.GetLeftMouseButtonDown();
                        leftMouse.isMouse = _inputSystem.GetLeftMouseButton();
                        leftMouse.Do(l => l.ReplaceMouseScreenPosition(screenPosition), when: Input.GetLeftMouseButton());
                        leftMouse.Do(l => l.ReplaceMouseWorldPosition(worldPosition), when: Input.GetLeftMouseButton());
                        leftMouse.isMouseUp = _inputSystem.GetLeftMouseButtonUp();

                        rightMouse.isMouseDown = _inputSystem.GetRightMouseButtonDown();
                        rightMouse.isMouse = _inputSystem.GetRightMouseButton();
                        rightMouse.Do(r => r.ReplaceMouseScreenPosition(screenPosition), when: Input.GetRightMouseButton());
                        rightMouse.Do(r => r.ReplaceMouseWorldPosition(worldPosition), when: Input.GetRightMouseButton());
                        rightMouse.isMouseUp = _inputSystem.GetRightMouseButtonUp();

                        keyboard.ReplaceHorizontal(_inputSystem.Horizontal);
                        keyboard.isJump = _inputSystem.Jump > 0;
                    }
                }
            }
        }
    }
}
