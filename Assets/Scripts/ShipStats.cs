using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipStats : MonoBehaviour
{
    public int appearance;
    public int interior;
    public int safety;
    public int speed;
    public enum SizeCategory { Small, Regular, Large }
    public SizeCategory size;
    public enum ShipModifier { None }
    public ShipModifier modifier = ShipModifier.None;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    /*void Update()
    {
        
    }*/
}
