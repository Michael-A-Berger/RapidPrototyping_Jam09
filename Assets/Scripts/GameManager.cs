using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    // List of prefabs used
    private GameObject[] shipPrefabs, customerPrefabs;
    // List of data for ships
    private List<ShipStats> ships;
    // Initial value for previous customer, set to -1 since the first current customer is at 0
    private int previousCustomerIndex = -1;
    // Customer have 5 preference and are ranked from 1 to 5 where 5 is the most important
    // When customer is first interviewed, they will start by saying the most important thing to them and time customer is interviewed again they will say the next most important thing
    // currentInterviewRank represent current 
    private int currentInterviewRank;

    public static GameManager instance;

    public ShipStats currentShip;
    public CustomerStats currentCustomer;

    public float finalBuyingPrice;

    public Button inter;
    public Button boa;
    public Button sna;
    public Button offer;
    public GameObject BoastPanel;

    public Text incomeText;
    private float income;
    private float totalShipValue;
    private AudioManager audioMng = null;

    void Start()
    {
        income = 0.0f;
        totalShipValue = 0.0f;
        incomeText = GameObject.Find("TotalIncome").GetComponent<Text>();
        ships = new List<ShipStats>();
        SpawnShips();
        SpawnCustomer();
        BoastPanel.SetActive(false);
        audioMng = FindObjectOfType<AudioManager>();
        if (audioMng == null)
            Debug.LogError("\tNo GameObject with the [ AudioManager ] script was found in the current scene!");
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Interview()
    {
        currentCustomer.Interview(currentInterviewRank);
        currentInterviewRank--;

        if (currentInterviewRank == 2)
            GameObject.Find("Interview").GetComponent<Button>().interactable = false;
    }

    public void ActivateBoastPanel()
    {
        BoastPanel.SetActive(true);
    }

    public void Boast(int stat)
    {
        currentCustomer.Boast(stat);
        BoastPanel.SetActive(false);
        GameObject.Find("Boast").GetComponent<Button>().interactable = false;
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
        currentInterviewRank = 5;
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

        GameObject.Find("Interview").GetComponent<Button>().interactable = true;          //Refresh buttons
        GameObject.Find("Boast").GetComponent<Button>().interactable = true;
        GameObject.Find("Snacks").GetComponent<Button>().interactable = true;
        GameObject.Find("Offer").GetComponent<Button>().interactable = true;

        //audioMng.PlayAudio("Customer Arrives");
    }

    public void AddIncome(float amount)
    {
        income += amount;
        incomeText.text = "Amount Earned: " + income + " / " + totalShipValue;
        int randomSound = Random.Range(1, 4);
        //audioMng.PlayAudio("Spaceship Sold " + randomSound);
    }

    private void ShowToolTip(string toolTip)
    {

    }


}
