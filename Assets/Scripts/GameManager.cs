using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    public ShipStats currentShip;
    public CustomerStats currentCustomer;

    public float currentCustomerPatience = 5f;
    public float finalBuyingPrice;

 



    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void Interview()
    {
        
        
        currentCustomerPatience -= 1f;
    }

    void Boast()
    {
        
        currentCustomerPatience -= 1f;
    }

    void Snackes()
    {
        currentCustomerPatience = 5;//TODO
    }

    void Offer()
    {

    }
}
