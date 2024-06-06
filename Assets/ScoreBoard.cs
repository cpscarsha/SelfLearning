using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text="Poison Resistant Count: "+Scene.DoughGuysCount+'\n'+"Toxic Dough Guys Count: "+Scene.ToxicGuysCount+'\n'+"Round: "+Scene.Round;
    }
}
