using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class StartTravelTest:MonoBehaviour
{
    private StarUIManager travelScript;

    [SetUp]
    public void Setup()
    {
        var go = new GameObject();
        travelScript = go.AddComponent<StarUIManager>();

        // Create a GameObject for System_data
        var starObj = new GameObject("StarData");
        var starData = starObj.AddComponent<System_data>();
        starData.name = "Alpha";
        starData.System_Ring = 1;
        starData.Seed = 42;
        starData.DistanceFromPlayer = 5f;

        travelScript.selectedStarData = starData;
        Data_Transfer.current_fuel_ammount = 100;
    }

    [Test]
    public void CanTravel_WhenEnoughFuel_ReturnsTrue()
    {
        travelScript.fuel_cost = 50;
        Assert.IsTrue(travelScript.CanTravel());
    }

    [Test]
    public void CanTravel_WhenNotEnoughFuel_ReturnsFalse()
    {
        travelScript.fuel_cost = 150;
        Assert.IsFalse(travelScript.CanTravel());
    }

    [Test]
    public void CanTravel_WhenNoStarSelected_ReturnsFalse()
    {
        travelScript.selectedStarData = null;
        travelScript.fuel_cost = 10;
        Assert.IsFalse(travelScript.CanTravel());
    }
    
}
