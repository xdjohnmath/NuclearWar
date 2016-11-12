using UnityEngine;
using System.Collections;
using System.Linq;

public class EnemyManager : MechanicsManager {

    public int energyRate;
    public float waveRate;
    float waveTime = 0;

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
        WavesTimer (waveRate, 5);
        SelectingCharacter ();

    }

    public override void SelectingCharacter () {
        base.SelectingCharacter ();
        int counter = 0;
        var prefabs = Resources.LoadAll ("Prefabs/Virus", typeof (GameObject)).Cast<GameObject> ().ToArray ();
        instance.prefabsArray = new GameObject[prefabs.Length];
        for (int i = 0; i < prefabs.Count (); i++) {
            instance.prefabsArray[i] = prefabs[i];
            if (instance.prefabsArray[i].GetComponentInChildren<CharactersManager>().creatureCost < instance.energy) {
                counter++;
            }
        }

        GameObject[] placeholder = new GameObject[counter];

        for (int i = 0; i < instance.prefabsArray.Length; i++) {
            if (instance.prefabsArray[i].GetComponentInChildren<CharactersManager>().creatureCost < instance.energy) {
                placeholder[i] = instance.prefabsArray[i];
            }
        }

        if (counter > 1) {
            instance.selectedCharacter = placeholder[Random.Range (0, counter - 1)];
            StartCoroutine (DelayInstantiation (2));
            instance.energy -= selectedCharacter.GetComponentInChildren<CharactersManager> ().creatureCost;
        }


    }

    public override void SelectingStartingPosition () {
        base.SelectingStartingPosition ();
        
        Instantiate (instance.selectedCharacter, new Vector3 (instance.startingPos, instance.yPosition[(int)Random.Range(0, instance.yPosition.Length)], instance.selectedCharacter.transform.position.z), Quaternion.identity);
    }
 
    IEnumerator DelayInstantiation (float f) {
        float a = Random.Range (0, 2);
        yield return new WaitForSeconds (a);
        SelectingStartingPosition ();

    }

    public float WavesTimer (float rate, int value) {
        if (this.waveTime >= rate) {
            this.energy *= value;
            print ("WAAAAAAAAAAAAAAAAAAVE");
            waveTime = 0;
            return this.energy;
        }
        else {
            this.waveTime += Time.deltaTime;
            return this.energy;
        }

    }

}

