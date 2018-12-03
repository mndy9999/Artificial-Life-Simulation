using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonsManager : MonoBehaviour {

    public GA_Population population;
    public Button bunnyButton;
    public Button foxButton;


    public void genBunny()
    {
        population.genIndividual("bunny", "random");
    }
    public void genFox()
    {
        population.genIndividual("fox", "random");
    }
}
