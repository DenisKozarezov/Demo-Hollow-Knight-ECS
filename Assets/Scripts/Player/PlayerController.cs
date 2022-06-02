using Core.Input;
using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private IInputSystem _inputSystem;

    [Inject]
    public void Construct(IInputSystem inputSystem)
    {
        _inputSystem = inputSystem;
        Debug.Log(_inputSystem);
    }
}
