using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCreator : MonoBehaviour {

	public GameObject redParticle;
	public GameObject greenParticle;
	public GameObject purpleParticle;
	public GameObject blueParticle;

	public int particleCount;

	public static GameObject[] particles;
	public static float particleScale;

	// Use this for initialization
	void Start () {
		Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
		particles = new GameObject[particleCount];
		// Randomly creates each type of particle
		for (int i = 0; i < particleCount; i++) {
			float randomX = Random.Range(-screenBounds.x, screenBounds.x);
			float randomY = Random.Range(-screenBounds.y, screenBounds.y);
			int particleChooser = Random.Range(0, 4);
			if (particleChooser == 0) {
				particles[i] = GameObject.Instantiate(redParticle, new Vector3(randomX, randomY, redParticle.transform.position.z), Quaternion.Euler(Vector3.zero));
			}
			if (particleChooser == 1) {
				particles[i] = GameObject.Instantiate(greenParticle, new Vector3(randomX, randomY, redParticle.transform.position.z), Quaternion.Euler(Vector3.zero));
			}
			if (particleChooser == 2) {
				particles[i] = GameObject.Instantiate(purpleParticle, new Vector3(randomX, randomY, redParticle.transform.position.z), Quaternion.Euler(Vector3.zero));
			}
			if (particleChooser == 3) {
				particles[i] = GameObject.Instantiate(blueParticle, new Vector3(randomX, randomY, redParticle.transform.position.z), Quaternion.Euler(Vector3.zero));
			}
			
		}
	}
}
