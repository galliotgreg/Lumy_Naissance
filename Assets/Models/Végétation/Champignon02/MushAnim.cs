using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushAnim : MonoBehaviour {

	private Animation animation;
	public ParticleSystem particle;

	public Vector2 timeBetweenAnim; 

	void Start() {
		animation = gameObject.GetComponent<Animation> ();
		StartCoroutine("Animate");
	}

	IEnumerator Animate() {
		while (true) {
			yield return new WaitForSeconds(Random.Range(timeBetweenAnim.x, timeBetweenAnim.y));
			particle.Play ();
			animation.Play();
		}
	}
}
