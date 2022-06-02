using NUnit.Framework;
using Moq;
using UnityEngine.TestTools;
using UnityEngine;
using Core.Services;

namespace TestFramework.Playmode
{
    public class CameraTests
    {
        private CameraComponent _camera;

        [SetUp]
        public void Setup()
        {
            _camera = new GameObject().AddComponent<CameraComponent>();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(_camera);
        }

        [Test]
        public void CameraAttachedToTargetAndTargetIsNotNull()
        {
            var target = new GameObject();

            _camera.Attach(target.transform);

            Assert.NotNull(_camera.Target);
        }
    }
}