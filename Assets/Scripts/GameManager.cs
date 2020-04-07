using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    
    public static GameManager instance;

    public ShipStats currentShip;
    public CustomerStats currentCustomer;
 

    public float currentCustomerPatience = 5f;
    public float finalBuyingPrice;

    public InputField price;
    public Button inter;
    public Button boa;
    public Button sna;
    public Button offer;
<<<<<<< Updated upstream
 

=======
    public GameObject BoastPanel;
    public GameObject OfferPanel;
>>>>>>> Stashed changes


    void Start()
    {
<<<<<<< Updated upstream
=======
        income = 0.0f;
        totalShipValue = 0.0f;
        incomeText = GameObject.Find("TotalIncome").GetComponent<Text>();
        ships = new List<ShipStats>();
        SpawnShips();
        SpawnCustomer();
        BoastPanel.SetActive(false);
        OfferPanel.SetActive(false);


        currentShip = GetComponent<ShipStats>();

>>>>>>> Stashed changes

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
<<<<<<< Updated upstream
        
        currentCustomerPatience -= 1f;
=======
        currentCustomer.Boast(stat);
        BoastPanel.SetActive(false);
        GameObject.Find("Boast").GetComponent<Button>().interactable = false;
    }

    public void Snacks()
    {
        currentCustomer.OfferSnacks();
        GameObject.Find("Snacks").GetComponent<Button>().interactable = false;
    }


    
    public void OfferOnClick()
    {
        OfferPanel.SetActive(true);

    }
    public void Offer()
    {
        Debug.Log("233");
        currentCustomer.MakeOffer(float.Parse(price.text), currentShip);
        //GameObject.Find("Offer").GetComponent<Button>().interactable = false;
        OfferPanel.SetActive(false);
        Debug.Log("111");
    }

    private void SpawnShips()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("ShipSpawnPoint");
        HashSet<int> exclude = new HashSet<int>();

        foreach (GameObject spawn in spawnPoints)
        {
            IEnumerable<int> range = Enumerable.Range(0, shipPrefabs.Length).Where(i => !exclude.Contains(i));

            int randomIndex = Random.Range(0, shipPrefabs.Length - exclude.Count);
            int shipIndex = range.ElementAt(randomIndex);

            GameObject spawnedShip = Instantiate(shipPrefabs[shipIndex], spawn.transform.position, Quaternion.identity);
            exclude.Add(shipIndex);

            totalShipValue += spawnedShip.GetComponent<ShipStats>().value;
        }

        AddIncome(0.0f);
    }

    public void SpawnCustomer()
    {
        currentInterviewPreference = 5;
        Vector3 spawnPoint = GameObject.FindGameObjectWithTag("CustomerSpawnPoint").transform.position;

        if (previousCustomerIndex == -1)
        {
            int randomIndex = Random.Range(0, customerPrefabs.Length);
            Instantiate(customerPrefabs[randomIndex], spawnPoint, Quaternion.identity);
        }
        else
        {
            HashSet<int> exclude = new HashSet<int>() { previousCustomerIndex };
            IEnumerable<int> range = Enumerable.Range(0, shipPrefabs.Length).Where(i => !exclude.Contains(i));

            int randomIndex = Random.Range(0, customerPrefabs.Length - exclude.Count);
            int customerIndex = range.ElementAt(randomIndex);

            Instantiate(customerPrefabs[customerIndex], spawnPoint, Quaternion.identity);
            previousCustomerIndex = customerIndex;
        }
>>>>>>> Stashed changes
    }

    void Snackes()
    {
        currentCustomerPatience = 5;//TODO
    }

    void Offer()
    {

    }


}
