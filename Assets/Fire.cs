using UnityEngine;
using System.Collections;

public class Fire : MonoBehaviour {

	public Light light1;
	public Light light2;
	public ParticleSystem particlesystem1;
	public ParticleSystem particlesystem2;

	public Cauldron cauldron;

	public Transform needle;
	public Transform waterMesh;
	Vector3 waterMeshScale;

	void Start()
	{
		waterMeshScale = waterMesh.localScale;
	}

	public void SetHeat(float heat)
	{
		light1.intensity = heat / 25;
		desiredrotation = Quaternion.Euler (-90, 0, Mathf.Clamp(heat, 0, 100) * 0.4f - 20);
	}

	Quaternion desiredrotation;
	void Update()
	{
		needle.localRotation = Quaternion.Lerp (needle.localRotation, desiredrotation, Time.deltaTime * 5);
	}

	void OnTriggerEnter(Collider other)
	{
		// If it is a stick
		cauldron.AddHeat();
	}
}
