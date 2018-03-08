using UnityEngine;
using System.Collections;

public class OptionController : MonoBehaviour
{
	public GameObject shot;
	public Transform shotSpawn;

	public OptionStats os;
	void Start ()
	{
		os = GetComponent<OptionStats> ();
	}

	void Update () {
		if (Input.GetButton ("Fire1") && Time.time > os.nextFire) {
			os.nextFire = Time.time + os.fireRate;
			Instantiate (shot, shotSpawn.position, Quaternion.identity);
		}
	}

	/*void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * ps.speed;

		rb.position = new Vector3 (
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax), 
			0.0f, 
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax));

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -ps.tilt);
	}*/
}
