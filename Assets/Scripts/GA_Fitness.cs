using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GA_Fitness : MonoBehaviour {

    GameObject bunnyParent;
    GameObject foxParent;

    GA_Traits traits;


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
        for(int i=0; i < (bunnyParent.transform.childCount); i++)
        {
            traits = bunnyParent.transform.GetChild(i).GetComponent<GA_Traits>();
            traits.fitness = (traits.speed + traits.sight + traits.collect + traits.spec) / 4;
        }
        for (int i = 0; i < (foxParent.transform.childCount); i++)
        {
            traits = foxParent.transform.GetChild(i).GetComponent<GA_Traits>();
            traits.fitness = (traits.speed + traits.sight + traits.collect + traits.spec) / 4;
        }
    }
}
