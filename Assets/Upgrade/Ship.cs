using UnityEngine;

public class Ship : MonoBehaviour
{
    public int Level = 1;

    public GameObject ShipPixel;
    
    private void Start()
    {
        Setlevel(Level); 
    }

    public void Setlevel(int lvl)
    {
        if (lvl == 1)
        {
            ShipPixel.SetActive(true);
        }
    }
}
