using UnityEngine;
using System.Collections;

public class ShotsManager : MonoBehaviour {

    public float shotSpeed;
    public int shotAttack;
    public GoodOrEvil type;
    public direction shotDir;

	void Update () {
        transform.Translate (Vector2.right *shotSpeed *Time.deltaTime *(int)shotDir);
       
	}

    void OnTriggerEnter2D (Collider2D other) {
        if (this.type == GoodOrEvil.organel && other.gameObject.tag != "Organel" || this.type == GoodOrEvil.virus && other.gameObject.tag != "Virus") {
            other.gameObject.GetComponentInChildren<CharactersManager> ().creatureLife -= shotAttack;
            Destroy (this.gameObject);
        }
    }
}
