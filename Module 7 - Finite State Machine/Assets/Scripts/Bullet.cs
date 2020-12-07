using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public GameObject explosion;
	public string selfTag;
	int damage = 10;

	// Use this for initialization
	void Start () {
		Physics.IgnoreLayerCollision(8, 8);
	}
	void OnCollisionEnter(Collision col)
    {
		if(col.gameObject.CompareTag("Unit"))
			col.gameObject.GetComponent<Health>().ReduceHp(damage);

		GameObject e = Instantiate(explosion, this.transform.position, Quaternion.identity);
		Destroy(e,1.5f);
		Destroy(this.gameObject);	
    }
}
