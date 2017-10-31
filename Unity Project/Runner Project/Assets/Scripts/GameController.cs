using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public GameObject jugador;
	public GameObject CanvasWin;
	public GameObject CanvasLose;
	public Text txtContadorVida;
	public Text txtContadorPuntaje;
	public int vida;
	int maxVida;
	public int puntaje;

	void Start () {
		Time.timeScale = 1.0f; 
		maxVida = vida;
	}

	void Update () {
		RevisarJugador ();
		DibujarUI ();
	}

	public void IncrementarPuntaje(){
		puntaje++;
	}

	public void QuitarVida(){
		vida--;
	}

	//Revisa la posición del usuario para determinar si cayó por un agujero o no.
	void RevisarJugador()
	{
		if (jugador.transform.position.y < -2) {
			//jugador cayó por un hoyo.
			MostrarGameOver();
		}
		if (jugador != null) {
			if (jugador.GetComponent<JugadorController> ().llegoAMeta) {
				MostrarWin ();
			}
		}
			
	}

	void DibujarUI()
	{
		txtContadorPuntaje.text = puntaje.ToString ();
		txtContadorVida.text = vida.ToString () + "/" + maxVida.ToString ();;
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
