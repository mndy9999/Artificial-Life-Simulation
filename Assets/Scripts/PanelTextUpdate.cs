using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelTextUpdate : MonoBehaviour {

    GameObject bunny1Text;
    GameObject bunny2Text;

    public GameObject bunny1GO;
    public GameObject bunny2GO;

	// Use this for initialization
	void Start () {
        bunny1Text = this.transform.Find("Bunny1").gameObject;
        bunny2Text = this.transform.Find("Bunny2").gameObject;

    }
	
	// Update is called once per frame
	void Update () {
        bunny1Text.GetComponent<Text>().text = bunny1GO.name +  "\n Gender: " + bunny1GO.GetComponent<BunnyTraits>().gender.ToString() +
            "\n Food Level: " + bunny1GO.GetComponent<BunnyTraits>().foodLevel.ToString() + "\n Can Mate: " + bunny1GO.GetComponent<BunnyTraits>().canMate().ToString() +
            "\n Target: " + bunny1GO.GetComponent<Player>().getTarget();

        bunny2Text.GetComponent<Text>().text = bunny2GO.name + "\n Gender: " + bunny2GO.GetComponent<BunnyTraits>().gender.ToString() +
            "\n Food Level: " + bunny2GO.GetComponent<BunnyTraits>().foodLevel.ToString() + "\n Can Mate: " + bunny2GO.GetComponent<BunnyTraits>().canMate().ToString() +
            "\n Target: " + bunny2GO.GetComponent<Player>().getTarget();
    }
}
