using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyManager : MechanicsManager {

    public int energyRate;

    //Singleton
    public static EnemyManager instance = null;

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
        instance.Energy (energyRate, 1);
        SelectingCharacter ();

    }

    public override void SelectingCharacter () {
        base.SelectingCharacter ();
        int counter = 0;
        var prefabs = Resources.LoadAll ("Prefabs/Virus", typeof (GameObject)).Cast<GameObject> ().ToArray ();
        instance.prefabsArray = new GameObject[prefabs.Length];
        for (int i = 0; i < prefabs.Count (); i++) {
            instance.prefabsArray[i] = prefabs[i];
            if (instance.prefabsArray[i].GetComponent<CharactersManager>().creatureCost < instance.energy) {
                counter++;
            }
        }

        GameObject[] placeholder = new GameObject[counter];

        for (int i = 0; i < instance.prefabsArray.Length; i++) {
            if (instance.prefabsArray[i].GetComponent<CharactersManager>().creatureCost < instance.energy) {
                placeholder[i] = instance.prefabsArray[i];
            }
        }

        if (counter > 1) {
            instance.selectedCharacter = placeholder[Random.Range (0, counter - 1)];
            SelectingStartingPosition ();
            instance.energy -= selectedCharacter.GetComponent<CharactersManager> ().creatureCost;
        }


    }

    public override void SelectingStartingPosition () {
        base.SelectingStartingPosition ();
        
        Instantiate (instance.selectedCharacter, new Vector3 (instance.startingPos, instance.yPosition[(int)Random.Range(0, instance.yPosition.Length)], instance.selectedCharacter.transform.position.z), Quaternion.identity);
    }

}

