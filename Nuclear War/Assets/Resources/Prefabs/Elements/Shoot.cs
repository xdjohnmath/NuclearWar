using UnityEngine;
using System.Collections;

public class Shoot : MonoBehaviour {

	public GameObject shootOBJ;

	private Animator a;

	void Start () {
		a = GetComponent <Animator> ();
	}
	
	void Update () {
		if (shootOBJ.activeSelf == true) {
			a.SetBool ("pow", true);
		} else {
			a.SetBool ("pow", false);
		
		}
	}
}
