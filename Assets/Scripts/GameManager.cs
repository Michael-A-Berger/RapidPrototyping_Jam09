using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject[] shipPrefabs, customerPrefabs;
    private List<ShipStats> ships;
    private int previousCustomerIndex = -1;
    private int currentInterviewPreference;

    public static GameManager instance;

    public ShipStats currentShip;
    public CustomerStats currentCustomer;

    public float finalBuyingPrice;

    public Button inter;
    public Button boa;
    public Button sna;
    public Button offer;

    public Text incomeText;
    private float income;
    private float totalShipValue;

    void Start()
    {
        income = 0.0f;
        totalShipValue = 0.0f;
        incomeText = GameObject.Find("TotalIncome").GetComponent<Text>();
        ships = new List<ShipStats>();
        SpawnShips();
        SpawnCustomer();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interview()
    {
        currentCustomer.Interview(currentInterviewPreference);
        currentInterviewPreference--;

        if (currentInterviewPreference == 2)
            GameObject.Find("Interview").GetComponent<Button>().interactable = false;
    }

    public void Boast(int stat)
    {
        currentCustomer.Boast(stat);
    }

    public void Snacks()
    {
        currentCustomer.OfferSnacks();
        GameObject.Find("Snacks").GetComponent<Button>().interactable = false;
    }

    void Offer(float amount, ShipStats ship)
    {
        currentCustomer.MakeOffer(amount, ship);
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
    }

    public void AddIncome(float amount)
    {
        income += amount;
        incomeText.text = "Amount Earned: " + income + " / " + totalShipValue;
    }

    private void ShowToolTip(string toolTip)
    {

    }
}
