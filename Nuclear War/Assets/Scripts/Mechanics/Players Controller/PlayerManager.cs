using UnityEngine;
using System.Collections;

public class PlayerManager : MechanicsManager {

    //Singleton
    public static PlayerManager instance = null;

    void Awake () {
       
        //Singleton
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy (gameObject);
        }
        //EndOfSingleton

      
    }

    void Update () {
    }

}
