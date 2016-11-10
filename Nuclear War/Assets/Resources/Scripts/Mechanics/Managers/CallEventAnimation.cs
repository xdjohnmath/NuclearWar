using UnityEngine;
using System.Collections;

public class CallEventAnimation : MonoBehaviour {

    Animator anim;

    void Awake () {
        anim = GetComponent<Animator> ();
        anim.SetInteger ("life", gameObject.GetComponentInChildren<CharactersManager> ().creatureLife);
        anim.SetBool ("shot", false);
        anim.SetBool ("spawned", false);
    }

    void Update () {

        if (this.gameObject.GetComponentInChildren<CharactersManager> ().creatureLife <= 0) {
            print ("EU USO ÓCULOS");
            anim.SetInteger ("life", 0);
        }

        if (this.gameObject.GetComponentInChildren<CharactersManager>().move == true) {
            anim.SetBool ("move", true);
        }

        switch (gameObject.GetComponentInChildren<CharactersManager>().type) {
            case creatureAttackType.Tank:
                anim.SetInteger ("type", 2);
                if (gameObject.GetComponentInChildren<CharactersManager> ().isAttacking == true) {
                    anim.SetBool ("move", false);
                    anim.SetBool ("isAttacking", true);
                }
                else if (gameObject.GetComponentInChildren<CharactersManager> ().beingAttacked == true) {
                    anim.SetBool ("move", false);
                    anim.SetBool ("beingAttacked", true);
                }
            break;
            case creatureAttackType.LowRange:
                anim.SetInteger ("type", 0);
                if (gameObject.GetComponentInChildren<CharactersManager> ().isAttacking == true) {
                    anim.SetBool ("move", false);
                    anim.SetBool ("isAttacking", true);
                }
                else if (gameObject.GetComponentInChildren<CharactersManager>().beingAttacked == true) {
                    anim.SetBool ("move", false);
                    anim.SetBool ("beingAttacked", true);
                }
            break;
            case creatureAttackType.HighRange:
                anim.SetInteger ("type", 1);
                if (gameObject.GetComponentInChildren<CharactersManager> ().isAttacking == true) {
                    anim.SetBool ("move", false);
                    anim.SetBool ("isAttacking", true);
                    gameObject.GetComponentInChildren<CharactersManager> ().move = false;
                }
                else if (gameObject.GetComponentInChildren<CharactersManager> ().beingAttacked == true) {
                    anim.SetBool ("move", false);
                    anim.SetBool ("beingAttacked", true);
                }
            break;
        }
    }

    public void Dead () {
        Destroy (gameObject);
    }

    public void ResetAnimationLowRange () {
        anim.SetBool ("isAttacking", false);
        anim.SetBool ("move", true);
        anim.SetBool ("beingAttacked", false);
        anim.SetBool ("shot", false);
        gameObject.GetComponentInChildren<CharactersManager> ().move = true;
    }

    public void ResetAnimationHighRange () {
        anim.SetBool ("isAttacking", false);
        anim.SetBool ("beingAttacked", false);
        anim.SetBool ("shot", false);
        gameObject.GetComponentInChildren<CharactersManager> ().move = true;
    }

    public void ResetMove () {
        if (anim.GetComponentInChildren<CharactersManager> ().move == false) {
            anim.GetComponentInChildren<CharactersManager> ().move = true;
            
        }
        print ("O MOVE É " + anim.GetComponentInChildren<CharactersManager> ().move);
    }

    public void LowRange () {
        gameObject.GetComponentInChildren<CharactersManager> ().LowRangeAttack ();

    }

    public void HighRange () {
        gameObject.GetComponentInChildren<CharactersManager> ().HighRangeAttack ();
        anim.SetBool ("shot", true);
    }
}
