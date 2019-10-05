using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBehavior : MonoBehaviour {

	public AnimationCurve redForceCurve;
	public AnimationCurve greenForceCurve;
	public AnimationCurve purpleForceCurve;
	public AnimationCurve blueForceCurve;

	public float maxDistance;
	public float redForceMultiplier;
	public float greenForceMultiplier;
	public float purpleForceMultiplier;
	public float blueForceMultiplier;

	Vector2 screenBounds;
	// Use this for initialization
	void Start () {
		screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		// Particles can't go inside each other so the force is zero at the edge of the particle
		redForceCurve.keys[1].time = ParticleCreator.particleScale / maxDistance;
		redForceCurve.keys[1].value = 0f;
		greenForceCurve.keys[1].time = ParticleCreator.particleScale / maxDistance;
		greenForceCurve.keys[1].value = 0f;
		purpleForceCurve.keys[1].time = ParticleCreator.particleScale / maxDistance;
		purpleForceCurve.keys[1].value = 0f;
		blueForceCurve.keys[1].time = ParticleCreator.particleScale / maxDistance;
		blueForceCurve.keys[1].value = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		Collider[] particlesInRadius = Physics.OverlapSphere(transform.position, maxDistance);
		// Apply forces to particle according to other particles around it
		for (int i = 0; i < particlesInRadius.Length; i++) {
			GameObject currentParticle = particlesInRadius[i].gameObject;
			float distance = Mathf.Clamp(Vector3.Distance(transform.position, currentParticle.transform.position), 0f, maxDistance);
			Vector3 forceVector = new Vector3(currentParticle.transform.position.x - transform.position.x, currentParticle.transform.position.y - transform.position.y, 0f);
			float forceMultiplier = 1f / (Mathf.Pow(forceVector.magnitude, 2) + 0.000000000000000000000000001f); // So the force multiplier isn't 1/0

			if (currentParticle.transform.tag.Equals("redparticle")) {
				transform.GetComponent<Rigidbody>().AddForce(redForceMultiplier * this.redForceCurve.Evaluate(distance / maxDistance) * forceVector.normalized * forceMultiplier);
			} else if (currentParticle.transform.tag.Equals("greenparticle")) {
				transform.GetComponent<Rigidbody>().AddForce(greenForceMultiplier * this.greenForceCurve.Evaluate(distance / maxDistance) * forceVector.normalized * forceMultiplier);
			} else if (currentParticle.transform.tag.Equals("purpleparticle")) {
				transform.GetComponent<Rigidbody>().AddForce(purpleForceMultiplier * this.purpleForceCurve.Evaluate(distance / maxDistance) * forceVector.normalized * forceMultiplier);
			} else if (currentParticle.transform.tag.Equals("blueparticle")) {
				transform.GetComponent<Rigidbody>().AddForce(blueForceMultiplier * this.blueForceCurve.Evaluate(distance / maxDistance) * forceVector.normalized * forceMultiplier);
			}
		}

		//Teleport particles to the other side of the screen
		if (transform.position.x > screenBounds.x) {
			transform.position = new Vector3(-screenBounds.x, transform.position.y, transform.position.z);
		}
		if (transform.position.x < -screenBounds.x) {
			transform.position = new Vector3(screenBounds.x, transform.position.y, transform.position.z);
		}
		if (transform.position.y > screenBounds.y) {
			transform.position = new Vector3(transform.position.x, -screenBounds.y, transform.position.z);
		}
		if (transform.position.y < -screenBounds.y) {
			transform.position = new Vector3(transform.position.x, screenBounds.y, transform.position.z);
		}
	}
}
