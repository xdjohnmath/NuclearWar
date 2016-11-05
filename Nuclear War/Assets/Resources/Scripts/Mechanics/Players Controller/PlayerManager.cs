using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.EventSystems;

public class PlayerManager : MechanicsManager {

    //Singleton
    public static PlayerManager instance = null;
    public Text energyText;
    public Button[] organelas, lines;
    GameObject linesGameObject, disableLines;

    void Awake () {

        //Singleton
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy (gameObject);
        }
        //EndOfSingleton
        SettingButtons (organelas);

        instance.energyTime = 1;
        linesGameObject = GameObject.Find ("Lines");
        linesGameObject.SetActive (false);


    }

    public void SettingButtons (Button[] b) {
        //Setting buttons to the right organels
        var prefabs = Resources.LoadAll ("Prefabs/Organelas", typeof (GameObject)).Cast<GameObject> ().ToArray ();
        instance.prefabsArray = new GameObject[prefabs.Length];
        for (int i = 0; i < prefabs.Count (); i++) {
            instance.prefabsArray[i] = prefabs[i];
            b[i].name = prefabs[i].name;
            b[i].GetComponent<Image> ().sprite = prefabs[i].GetComponent<SpriteRenderer> ().sprite;
        }

        //Disabling non used buttons
        for (int i = 0; i < b.Length; i++) {
            if (b[i].name == "Character") {
                b[i].interactable = false;
            }
        }
    }


    public override void SelectingCharacter () {
        base.SelectingCharacter ();
        for (int i = 0; i < instance.prefabsArray.Length; i++) {
            if (EventSystem.current.currentSelectedGameObject.name == instance.prefabsArray[i].name) {
                linesGameObject.SetActive(true);
                instance.selectedCharacter = instance.prefabsArray[i];
            }
        }
    }

    public override void SelectingStartingPosition () {
        base.SelectingStartingPosition ();
        for (int i = 0; i < lines.Length; i++) {
            if (EventSystem.current.currentSelectedGameObject.name == lines[i].name) {
                
                Instantiate (instance.selectedCharacter, new Vector3(instance.startingPos, instance.yPosition[i], selectedCharacter.transform.position.z), Quaternion.identity);
                instance.energy -= instance.selectedCharacter.GetComponent<CharactersManager> ().creatureCost;
            }

        }

        linesGameObject.SetActive (false);
    }

    void FixedUpdate () {
        energyText.text = Energy (1, 1).ToString();
        CheckingEnergyToButtons (organelas);
    }

}
