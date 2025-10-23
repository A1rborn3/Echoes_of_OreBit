using System.Collections.Generic;

[System.Serializable]
public class SaveData
{
    public int galaxySeed;
    public List<string> destroyedPlanets = new List<string>();
    public InventoryData inventory = new InventoryData();
    public List<string> upgrades = new List<string>();

    //all transfer data
    public int current_fuel_ammount;
    public int current_fuel_capacity;
    public string Star_name;
    public int System_ring;
    public int current_star;





}


