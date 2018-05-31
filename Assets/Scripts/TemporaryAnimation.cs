using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TemporaryAnimation : MonoBehaviour {

    Animator anim;

	void Start () {
        anim = transform.Find("Image").GetComponent<Animator>();
	}

    private bool currentRotation = false;

	public void RotateCharacter () {
        currentRotation = !currentRotation;
        anim.SetBool("fram", currentRotation);
    }
}
