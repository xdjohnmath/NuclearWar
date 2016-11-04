using UnityEngine;
using System.Collections;

public class ShotsManager : MonoBehaviour {

    public float shotSpeed;
    public int shotAttack;
    public direction shotDir;

	void Update () {
        transform.Translate (Vector2.right *shotSpeed *Time.deltaTime *(int)shotDir);

        if (this.transform.position.x > PlayerManager.instance.startingPos || this.transform.position.y < PlayerManager.instance.endingPos) {
            Destroy (this.gameObject);
        }
       
	}

    void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.GetComponent<CharactersManager> ().goodOrEvil == GoodOrEvil.virus) {
            other.gameObject.GetComponent<CharactersManager> ().creatureLife -= shotAttack;
            Destroy (this.gameObject);
        }
    }
}
