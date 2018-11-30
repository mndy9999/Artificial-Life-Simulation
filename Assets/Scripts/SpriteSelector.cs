using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSelector : MonoBehaviour {

    public Sprite maleBunny;
    public Sprite femaleBunny;
    public Sprite maleFox;
    public Sprite femaleFox;

    GA_Traits.Gender gender;

	// Use this for initialization
	void Start () {
        gender = GetComponent<GA_Traits>().gender;
        chooseSprite();
	}
    private void Update()
    {
        gender = GetComponent<GA_Traits>().gender;
        chooseSprite();
    }


    void chooseSprite()
    {
        if (gameObject.tag == "bunny")
        {
            if (gender == GA_Traits.Gender.male) { GetComponent<SpriteRenderer>().sprite = maleBunny; }
            else if (gender == GA_Traits.Gender.female) { GetComponent<SpriteRenderer>().sprite = femaleBunny; }
        }
        else if (gameObject.tag == "fox")
        {
            if (gender == GA_Traits.Gender.male) { GetComponent<SpriteRenderer>().sprite = maleFox; }
            else if (gender == GA_Traits.Gender.female) { GetComponent<SpriteRenderer>().sprite = femaleFox; }
        }
    }
    
}
