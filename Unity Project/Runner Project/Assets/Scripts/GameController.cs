using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

	public GameObject jugador;
	public GameObject metaNivel;
	public GameObject CanvasWin;
	public GameObject CanvasLose;
	public GameObject txtContadorVida;
	public GameObject txtContadorPuntaje;
	public int vida;
	public int puntaje;

	void Start () {
		Time.timeScale = 1.0f; 
	}

	void Update () {
		RevisarPosicion ();
	}

	//Revisa la posición del usuario para determinar si cayó por un agujero o no.
	void RevisarPosicion()
	{
		if (jugador.transform.position.y < -2) {
			//jugador cayó por un hoyo.
			MostrarGameOver();
		}

		if (jugador.transform.position.z >= metaNivel.transform.position.z) {
			MostrarWin ();
		}
	}

	//Muestra el canvas de Game Over.
	void MostrarGameOver(){
		CanvasLose.SetActive (true);
		DetenerTiempo ();
	}

	//Muestra el canvas de ganador
	void MostrarWin(){
		CanvasWin.SetActive (true);
		DetenerTiempo ();
	}

	public void ResetearNivel(){
		Application.LoadLevel("Scene1");
	}

	void DetenerTiempo(){
		Time.timeScale = 0.0f; 
	}
}
