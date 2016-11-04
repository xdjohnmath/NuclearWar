using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[System.Serializable]

public class MechanicsManager : MonoBehaviour {

    //All properties
    public int life { get; set; }
    public int energy { get; set; }
    public float energyTime { get; set; }
    public GameObject[] prefabsArray { get; set; }
    public float[] yPos = new float[5] { 39, 23, 7, -9, -25 };
    public float startingPos { get; set; }
    public float endingPos { get; set;}

    //value to be added and how long you must wait
    public int Energy (int value, float rate) {
        if (energyTime >= rate) {
            this.energy += value;
            energyTime = 0;
            return this.energy;
        }
        else {
            energyTime += Time.deltaTime;
            return this.energy;
        }
    }

    public void CheckingEnergyToButtons (Button[] b) {
        for (int i = 0; i < prefabsArray.Length; i++) {
            if (prefabsArray[i].GetComponent<CharactersManager> ().creatureCost > this.energy) {
                b[i].interactable = false;
            }
            else {
                b[i].interactable = true;
            }
        }
    }

    public virtual void SelectingCharacter () {
       
    }

    public virtual void SelectingStartingPosition () {

    }
}
