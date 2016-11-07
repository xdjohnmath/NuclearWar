using UnityEngine;
using System.Collections;

public class CallEventAnimation : MonoBehaviour {

    Animator anim;

    void Start () {
        anim = GetComponent<Animator> ();
        anim.SetInteger ("life", gameObject.GetComponentInChildren<CharactersManager> ().creatureLife);
    }

    void Update () {

        if (gameObject.GetComponentInChildren<CharactersManager> ().creatureLife <= 0) {
            print ("EU USO ÓCULOS");
            anim.SetInteger ("life", 0);
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

    public void ResetAnimation () {
        anim.SetBool ("isAttacking", false);
        anim.SetBool ("move", true);
        anim.SetBool ("beingAttacked", false);
        gameObject.GetComponentInChildren<CharactersManager> ().move = true;
    }

    public void LowRange () {
        gameObject.GetComponentInChildren<CharactersManager> ().LowRangeAttack ();

    }

    public void HighRange () {
        gameObject.GetComponentInChildren<CharactersManager> ().HighRangeAttack ();
    }
}
