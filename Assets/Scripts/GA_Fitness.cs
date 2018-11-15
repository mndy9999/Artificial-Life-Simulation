using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Fitness : MonoBehaviour {

    GameObject bunnyParent;
    GameObject foxParent;

    GameObject bunnyGO;
    GameObject foxGO;

    BunnyTraits bunnyTraits;
    FoxTraits foxTraits;

    private void Start()
    {
        bunnyParent = GameObject.FindGameObjectWithTag("Bunny Parent");
        foxParent = GameObject.FindGameObjectWithTag("Fox Parent");
    }

    private void Update()
    {
        calculateFitness();
    }

    void calculateFitness()
    {
        for(int i=0; i < bunnyParent.transform.childCount; i++)
        {
            bunnyTraits = bunnyParent.transform.GetChild(i).GetComponent<BunnyTraits>();
            bunnyTraits.fitness = (bunnyTraits.speed + bunnyTraits.sigth + bunnyTraits.collect) / 3;
        }
        for(int i = 0; i < foxParent.transform.childCount; i++)
        {
            
            foxTraits = foxParent.transform.GetChild(i).GetComponent<FoxTraits>();
            foxTraits.fitness = (foxTraits.speed + foxTraits.sight + foxTraits.collect) / 3;
        }
    }
}
