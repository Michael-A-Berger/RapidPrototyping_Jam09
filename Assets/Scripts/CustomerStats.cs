using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomerStats : MonoBehaviour
{
    private Text speechBubble;
    private string[] greetings = { "Hi there!", "Whatta ya got?", "What're ya sellin'?", "Is this the right place?", "Can I get some service, please?" };
    private string[] boastResponse = { "You don't say!", "I hadn't considered that...", "I'll take your word for it.", "Impressive!", "Wow!" };
    private string[] snackResponse = { "Thank you!", "Thanks!", "For me? Thanks!", "Talk about customer service!", "You have my attention" };
    private string[] appearanceResponse = { "I guess it would have to be the looks?", "Style is everything!", "I want something that looks cool", "Something that'll turn heads", "One that looks as good as I do" };
    private string[] interiorResponse = { "Something that looks good from the inside", "A luxury interior!", "Comfortable seats for long trips", "CUP HOLDERS", "Lots of flashing buttons!" };
    private string[] safteyResponse = { "Something that'll keep my family safe", "Got anything that can blow up a small planet?", "Guns. Lots of them. Don't ask.", "State of the art defense system", "Airbags. Wait, do you need airbags in space?" };
    private string[] speedResponse = { "GOTTA GO FAST", "The fastest ya got", "Speed is key!", "I want to break some speed records", "Something quick would be nice" };
    private string[] sizeResponseSmall = { "Something that doesn't take up too much space", "The smaller the better", "I don't need anything too big", "A smaller one will do", "Itsy bitsy teeny weeny spacey shipy" };
    private string[] sizeResponseRegular = { "Something not too big or too small", "Something sized juuuuuust right", "Average sized would be fine", "Got anything regular sized?", "I'm not looking for anything crazy for size"};
    private string[] sizeResponseLarge = { "Biggest ya got!", "I need something to fit the whole family", "BIG SHIP PLEASE", "I would prefer something on the large side", "Something big enough to fit an asteroid. No reason." };
    private string[] purchaseResponseCheap = { "You're practically giving it away!", "What a steal!", "How do you stay in business with such low prices?!", "Haha, sucker!", "Way less than I was expecting!" };
    private string[] purchaseResponseAverage = { "You got yourself a deal", "Sounds reasonable", "Sure, sounds fair", "A fair price", "I can do that" };
    private string[] purchaseResponseExpensive = { "I can't afford that", "No way, pal", "That's way too expensive", "For that hunk of junk?! No way!", "You're out of your mind!" };

    // Prioritized ship stats are weighted the highest at 5, lowest at 1
    public int appearanceWeight;
    public int interiorWeight;
    public int safetyWeight;
    public int speedWeight;

    // Ship size preference is given if it falls in the top three, otherwise it is set to Irrelevant
    public int sizeRankNumber;
    public ShipStats.SizeCategory sizePreference;

    // Customer modifiers are distinct from ship modifiers
    public enum CustomerModifier { None }
    public CustomerModifier modifier = CustomerModifier.None;

    private Text patienceText;
    private float patience;

    private GameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        // All weight values are set in the Inspector

        speechBubble = GameObject.Find("SpeechText").GetComponent<Text>();
        speechBubble.text = greetings[Random.Range(0, greetings.Length)];
        manager = FindObjectOfType<GameManager>();
        manager.currentCustomer = this;

        patienceText = GameObject.Find("CustomerPatience").GetComponent<Text>();
        patience = 100.0f;
        patienceText.text = "Customer Patience: " + patience + "%";
    }

    public void Interview(int preference)
    {
        if (appearanceWeight == preference)
            speechBubble.text = appearanceResponse[Random.Range(0, appearanceResponse.Length)];
        else if (interiorWeight == preference)
            speechBubble.text = interiorResponse[Random.Range(0, interiorResponse.Length)];
        else if (safetyWeight == preference)
            speechBubble.text = safteyResponse[Random.Range(0, safteyResponse.Length)];
        else if (speedWeight == preference)
            speechBubble.text = speedResponse[Random.Range(0, speedResponse.Length)];
        else if (sizeRankNumber == preference)
        {
            switch (sizePreference)
            {
                case ShipStats.SizeCategory.Large:
                    speechBubble.text = sizeResponseLarge[Random.Range(0, sizeResponseLarge.Length)];
                    break;
                case ShipStats.SizeCategory.Regular:
                    speechBubble.text = sizeResponseRegular[Random.Range(0, sizeResponseRegular.Length)];
                    break;
                case ShipStats.SizeCategory.Small:
                    speechBubble.text = sizeResponseSmall[Random.Range(0, sizeResponseSmall.Length)];
                    break;
            }
        }

        UpdatePatience(-1.0f);
    }

    public void Boast(int stat)
    {
        speechBubble.text = boastResponse[Random.Range(0, boastResponse.Length)];

        // MORE PLACEHOLDER MATH. SHOULD CHANGE BASED ON HOW MUCH THEY LIKE THE STAT
        switch (stat)
        {
            case 1:
                appearanceWeight += appearanceWeight;
                break;
            case 2:
                interiorWeight += interiorWeight;
                break;
            case 3:
                safetyWeight += safetyWeight;
                break;
            case 4:
                speedWeight += speedWeight;
                break;
            case 5:
                sizeRankNumber += sizeRankNumber;
                break;
               
        }

        UpdatePatience(-1.0f);
    }

    public void OfferSnacks()
    {
        // MORE PLACEHOLDER VALUES
        speechBubble.text = snackResponse[Random.Range(0, snackResponse.Length)];
        UpdatePatience(20.0f);
    }

    public void MakeOffer(float amount, ShipStats ship)
    {
        // THIS IS ALL PLACEHOLDER MATH THAT ALL NEEDS TO BE REPLACED!!!
        float appearanceModifier = appearanceWeight * ship.appearance;
        float interiorModifier = interiorWeight * ship.interior;
        float safetyModifier = safetyWeight * ship.safety;
        float speedModifier = speedWeight * ship.speed;
        float sizeModifier = 1.0f;
        if (sizePreference == ship.size)
            sizeModifier = 1.5f;
        else if (sizePreference != ShipStats.SizeCategory.Irrelevant)
            sizeModifier = 0.5f;

        float maximumOffer = ship.value * patience * appearanceModifier * interiorModifier * safetyModifier * speedModifier * sizeModifier * sizeRankNumber;

        if (amount <= maximumOffer)
        {
            manager.AddIncome(amount);
            if (amount / maximumOffer < 0.85f)
            {
                speechBubble.text = purchaseResponseCheap[Random.Range(0, purchaseResponseCheap.Length)];
                SpawnNextCustomer();
            }
            else
            {
                speechBubble.text = purchaseResponseAverage[Random.Range(0, purchaseResponseAverage.Length)];
                SpawnNextCustomer();
            }
        }
        else
        {
            UpdatePatience(-1.0f);
            speechBubble.text = purchaseResponseExpensive[Random.Range(0, purchaseResponseExpensive.Length)];
        }
    }

    private void UpdatePatience(float amount)
    {
        // ALL CALLS TO THIS HAVE PLACEHOLDER VALUES OF -1.0 RIGHT NOW
        patience += amount;
        patience = Mathf.Clamp(patience, 0.0f, 100.0f);
        patienceText.text = "Customer Patience: " + patience + "%";
    }

    private IEnumerator SpawnNextCustomer()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            manager.SpawnCustomer();
            Destroy(gameObject);
        }
    }
}
