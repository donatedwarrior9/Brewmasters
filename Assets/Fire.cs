using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

	public Light light1;
	public Light light2;
	public ParticleSystem particlesystem1;
	public ParticleSystem particlesystem2;
	public ParticleSystem splashingsystem;
	public ParticleSystem boilSystem;
	public ParticleSystem steamSystem;
	public AudioSource bubblingSounds;
	public AudioSource fireSounds;
	public Cauldron cauldron;

	public Transform needle;
	public Transform waterMesh;
	Vector3 waterMeshScale;

	void Start()
	{
		waterMeshScale = waterMesh.localScale;
		emission1 = particlesystem1.emission;
		emission2 = particlesystem2.emission;
		splashEmission = splashingsystem.emission;
		boilEmission = boilSystem.emission;
		steamEmission = steamSystem.emission;
	}

	Quaternion desiredrotation;
	ParticleSystem.EmissionModule emission1;
	ParticleSystem.EmissionModule emission2;
	ParticleSystem.EmissionModule splashEmission;
	ParticleSystem.EmissionModule boilEmission;
	ParticleSystem.EmissionModule steamEmission;
	float smoothHeat;
	void Update()
	{
		smoothHeat = Mathf.SmoothStep (smoothHeat, cauldron.heat, Time.deltaTime * 5);
		light1.intensity = Mathf.Clamp(cauldron.heat / 25 + 0.5f, 0.5f, 5);
		light2.intensity = light1.intensity;
		desiredrotation = Quaternion.Euler (-90, 0, Mathf.Clamp(cauldron.heat, 0, 100) * 0.4f - 20);
		needle.localRotation = Quaternion.Lerp (needle.localRotation, desiredrotation, Time.deltaTime * 5);
		waterMeshScale.y = Mathf.Clamp (cauldron.heat / 50, 0.1f, 2);
		waterMesh.localScale = waterMeshScale;
		particlesystem1.startSize = cauldron.heat / 50 + 0.5f;
		particlesystem2.startSize = particlesystem1.startSize;
		emission1.rate = cauldron.heat + 30;
		emission2.rate = emission1.rate;
		splashEmission.rate = cauldron.heat / 2;
		boilEmission.enabled = cauldron.heat > 50;
		steamEmission.enabled = cauldron.heat > 25;
		steamSystem.startColor = Cauldron.smoothedColor;
		splashingsystem.startColor = Cauldron.smoothedColor;
		bubblingSounds.volume = smoothHeat / 300;
		fireSounds.volume = smoothHeat / 300 + 0.05f;
	}
	public GameObject stickIgniteEffectPrefab;
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Stick") {
			Instantiate (stickIgniteEffectPrefab, other.transform.position, Quaternion.identity);
			Destroy (other.gameObject);
			cauldron.AddHeat ();
		}
	}
}
