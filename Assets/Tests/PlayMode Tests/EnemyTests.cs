using System.Collections;
using System.Linq;
using NUnit.Framework;
using Moq;
using UnityEngine.TestTools;
using UnityEngine;

namespace TestFramework.Playmode
{
    public class EnemyTests
    {
        private GameObject _obj;

        [SetUp]
        public void Setup()
        {
            _obj = new GameObject("My Test Object");
        }

        [TearDown]
        public void TearDown()
        {
            Object.FindObjectsOfType<GameObject>().ToList().ForEach(x => Object.DestroyImmediate(x));
        }

        [Test]
        public void EnemyTauntedAndTargetIsNotNull()
        {
       
        }
    }
}