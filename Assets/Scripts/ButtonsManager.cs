using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour {

    public GA_Population population;


    public void genBunny()
    {
        population.genIndividual("bunny", "random");
    }
    public void genFox()
    {
        population.genIndividual("fox", "random");
    }
    public void genBush()
    {
        population.spawnBush();
    }
}
