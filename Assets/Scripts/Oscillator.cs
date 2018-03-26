using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour 
{

	public Vector3 movementVector = new Vector3(10f, 10f);
	public float period = 2f;
	public float rotationSpeed;

	float movementFactor; // 0 for not moved, 1 for fully moved.
	Vector3 startingPos;
	// Use this for initialization
	void Start () 
	{
		startingPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (period <= Mathf.Epsilon) { return; }
		float cycles = Time.time / period;

		const float tau = Mathf.PI * 2;
		float rawSinWave = Mathf.Sin(cycles); // goes from -1 to +1

		movementFactor = rawSinWave / 2f + 0.5f;
		Vector3 offset = movementFactor * movementVector;
		transform.position = startingPos + offset;

		float rotation = rotationSpeed * Time.deltaTime;
		transform.Rotate(Vector3.forward * rotation);
	}
}
