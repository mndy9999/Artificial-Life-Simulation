using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Selection : MonoBehaviour {

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

        time -= Time.deltaTime;
        if (time <= 0)
        {
            addSum("bunny", "male");
            addSum("bunny", "female");
            addSum("fox", "male");
            addSum("fox", "female");

            select("bunny", "male", maleBunnySum);
            select("bunny", "female", femaleBunnySum);
            select("fox", "male", maleFoxSum);
            select("fox", "female", femaleFoxSum);

            //Debug.Log("Selected Male Bunny: " + selectedMaleBunny.name);
            //Debug.Log("Selected Female Bunny: " + selectedFemaleBunny.name);
            //Debug.Log("Selected Male Fox: " + selectedMaleFox.name);
            //Debug.Log("Selected Female Fox: " + selectedFemaleFox.name);
            time = 3;
        }
    }

    void addSum(string indiv, string gender)
    {
        if (indiv == "bunny")
        {
            for (int i = 0; i < bunnyParent.transform.childCount; i++)
            {
                if (gender == "male" && bunnyParent.transform.GetChild(i).tag == "male") { maleBunnySum += bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness; }
                else if (gender == "female" && bunnyParent.transform.GetChild(i).tag == "female") { femaleBunnySum += bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness; }
            }
        }
        else if (indiv == "fox")
        {
            for (int i = 0; i < foxParent.transform.childCount; i++)
            {
                if (gender == "male" && foxParent.transform.GetChild(i).tag == "male") { maleFoxSum += foxParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness; }
                else if (gender == "female" && foxParent.transform.GetChild(i).tag == "female") { femaleFoxSum += foxParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness; }
            }
        }

    }

	void select(string indiv, string gender, float sum)
    {
        float rand = Random.Range(0, sum);
        float randSum = 0;
        
        if(indiv == "bunny")
        {
            for(int i = 0; i < bunnyParent.transform.childCount;i++)
            {
                if (gender == "male" && bunnyParent.transform.GetChild(i).tag == "male")
                {
                    randSum += bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness;
                    if (rand < randSum) { selectedMaleBunny = bunnyParent.transform.GetChild(i).gameObject; break; }
                }
                else if(gender =="female" && bunnyParent.transform.GetChild(i).tag == "female")
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
                if (gender == "male" && foxParent.transform.GetChild(i).tag == "male")
                {
                    randSum += foxParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness;
                    if (rand < randSum) { selectedMaleFox = foxParent.transform.GetChild(i).gameObject; break; }
                }
                else if (gender == "female" && foxParent.transform.GetChild(i).tag == "female")
                {
                    randSum += foxParent.transform.GetChild(i).GetComponent<GA_Traits>().fitness;
                    if (rand < randSum) { selectedFemaleFox = foxParent.transform.GetChild(i).gameObject; break; }
                }               
            }
        }
    }
}
