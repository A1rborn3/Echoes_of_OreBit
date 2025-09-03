using UnityEngine;
using UnityEngine.Rendering;
//this is to hold small data variables between scenes where neccacery
public static class Data_Transfer
{
    //clicked system data when moving between star map and system
    public static string Star_name;
    public static int System_ring;

    public static int current_star; //seed of the curently visited star. home base == 0000 as the ring count starts at 1
}
