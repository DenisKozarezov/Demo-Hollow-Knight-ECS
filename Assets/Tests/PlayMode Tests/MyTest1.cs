using System.Collections;
using NUnit.Framework;
using Moq;
using UnityEngine.TestTools;
using UnityEngine;
using Core.Units;

public class MyTest1
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
        Object.Destroy(_obj);
    }

    [Test]
    public void EnemyTauntedAndTargetIsNotNull()
    {
        var enemy = new GameObject();
        var zombie = enemy.AddComponent<Zombie>();

        zombie.Taunt(_obj.transform);

        Assert.True(zombie.Taunted);
        Assert.True(zombie.Target != null);
    }

    [Test]
    public void MyTestObjectHasName()
    {
        Assert.AreEqual(_obj.name, "My Test Object");
    }

    // A UnityTest behaves like a coroutine in Play Mode. In Edit Mode you can use
    // `yield return null;` to skip a frame.
    [UnityTest]
    public IEnumerator SomeTest1WithEnumeratorPasses()
    {
        // Use the Assert class to test conditions.
        // Use yield to skip a frame.
        yield return null;
    }
}