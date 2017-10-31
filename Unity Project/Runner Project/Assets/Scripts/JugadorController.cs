using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorController : MonoBehaviour {

	public bool llegoAMeta=false;
	public GameObject objGameController;
	GameController gameController;
	// Use this for initialization
	void Start () {
		gameController = objGameController.GetComponent<GameController> ();	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Meta")
		{
			llegoAMeta = true;
		}
		if (other.gameObject.tag == "Moneda")
		{
			gameController.IncrementarPuntaje ();
			Destroy (other.gameObject);
		}
		if (other.gameObject.tag == "Enemigo")
		{
			gameController.QuitarVida ();
			Destroy (other.gameObject);
		}
	}
}
