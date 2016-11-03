using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class PlayerManager : MechanicsManager {

    //Singleton
    public static PlayerManager instance = null;
    public Text energyText;
    public Button[] organelas;

    void Awake () {
       
        //Singleton
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy (gameObject);
        }
        //EndOfSingleton

        instance.energyTime = 1;

        var prefabs = Resources.LoadAll ("Prefabs/Organelas", typeof (GameObject)).Cast<GameObject> ().ToArray();
        for (int i = 0; i < prefabs.Count(); i++) {
            organelas[i].name = prefabs[i].name;
            organelas[i].GetComponent<Image> ().sprite = prefabs[i].GetComponent<SpriteRenderer> ().sprite;
        }
    }



    void FixedUpdate () {
        energyText.text = Energy (1, 1).ToString();
    }

}
