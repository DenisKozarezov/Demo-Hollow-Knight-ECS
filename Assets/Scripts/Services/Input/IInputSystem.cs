using System;
using UnityEngine;

namespace Core.Input
{
    public interface IInputSystem
    {
        Vector3 Direction { get; }
        event Action Fire;
    }
}