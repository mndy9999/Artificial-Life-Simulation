using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Selection : MonoBehaviour
{

    GameObject bunnyParent;
    GameObject foxParent;

    float maleBunnySum;
    float femaleBunnySum;
    float maleFoxSum;
    float femaleFoxSum;

    public GameObject selectedMaleBunny;
    public GameObject selectedFemaleBunny;
    public GameObject selectedMaleFox;
    public GameObject selectedFemaleFox;

    float time = 3;

    private void Start()
    {
        bunnyParent = GameObject.FindGameObjectWithTag("Bunny Parent");
        foxParent = GameObject.FindGameObjectWithTag("Fox Parent");
    }

    private void Update()
    {
        resetSum();
        time -= Time.deltaTime;
        if (time <= 0)
        {
            addSum("bunny");
            addSum("fox");

            select("bunny", "male", maleBunnySum);
            select("bunny", "female", femaleBunnySum);
            select("fox", "male", maleFoxSum);
            select("fox", "female", femaleFoxSum);

           //Debug.Log("Selected Male Bunny: " + selectedMaleBunny.name + "    "  + selectedMaleBunny.GetComponent<GA_Traits>().fitness);
           //Debug.Log("Selected Female Bunny: " + selectedFemaleBunny.name + "    " + selectedFemaleBunny.GetComponent<GA_Traits>().fitness);
           //Debug.Log("Selected Male Fox: " + selectedMaleFox.name);
           //Debug.Log("Selected Female Fox: " + selectedFemaleFox.name);
            time = 3f;
        }       

    }
    /// <summary>
    /// Creates 4 sums by adding up the fitness values
    /// </summary>
    /// <param name="indiv"></param>
    /// <param name="gender"></param>
    void addSum(string indiv)
    {
        if (indiv == "bunny")
        {
            for (int i = 0; i < bunnyParent.transform.childCount; i++)
            {
                if (bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().gender == GA_Traits.Gender.male) { maleBunnySum += bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness; }
                else if (bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().gender == GA_Traits.Gender.female) { femaleBunnySum += bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness; }
            }
        }
        else if (indiv == "fox")
        {
            for (int i = 0; i < foxParent.transform.childCount; i++)
            {
                if (foxParent.transform.GetChild(i).GetComponent<GA_Traits>().gender == GA_Traits.Gender.male) { maleFoxSum += foxParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness; }
                else if (foxParent.transform.GetChild(i).GetComponent<GA_Traits>().gender == GA_Traits.Gender.female) { femaleFoxSum += foxParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness; }
            }
        }

    }
    /// <summary>
    /// Selects a random individual
    /// </summary>
    /// <param name="indiv"></param>
    /// <param name="gender"></param>
    /// <param name="sum"></param>
	void select(string indiv, string gender, float sum)
    {
        float rand = Random.Range(0, sum);  //generate random number between 0 and the initial sum
        float randSum = 0;

        if (indiv == "bunny")
        {
            for (int i = 0; i < bunnyParent.transform.childCount; i++)
            {
                if (gender == "male" && bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().gender == GA_Traits.Gender.male)
                {
                    randSum += bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness;         //add up the fitness values until the new sum is higher than the random number generated
                    if (rand < randSum) { selectedMaleBunny = bunnyParent.transform.GetChild(i).gameObject; break; }    //then set the selected individual to the corresponding gameobject
                }
                else if (gender == "female" && bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().gender == GA_Traits.Gender.female)
                {
                    randSum += bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness;
                    if (rand < randSum) { selectedFemaleBunny = bunnyParent.transform.GetChild(i).gameObject; break; }
                }
            }

        }
        else if (indiv == "fox")
        {
            for (int i = 0; i < foxParent.transform.childCount; i++)
            {
                if (gender == "male" && foxParent.transform.GetChild(i).GetComponent<GA_Traits>().gender == GA_Traits.Gender.male)
                {
                    randSum += foxParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness;
                    if (rand < randSum) { selectedMaleFox = foxParent.transform.GetChild(i).gameObject; break; }
                }
                else if (gender == "female" && foxParent.transform.GetChild(i).GetComponent<GA_Traits>().gender == GA_Traits.Gender.female)
                {
                    randSum += foxParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness;
                    if (rand < randSum) { selectedFemaleFox = foxParent.transform.GetChild(i).gameObject; break; }
                }
            }
        }
    }
    void resetSum()
    {
        maleBunnySum = 0;
        femaleBunnySum = 0;
        maleFoxSum = 0;
        femaleFoxSum = 0;
    }
}
