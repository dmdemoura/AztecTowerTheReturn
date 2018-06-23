using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buttons : MonoBehaviour {
	GameManager gameManager;
	GameObject panel;
	GameObject panel2;
	GameObject error;
	Player player;

	void Start(){
		gameManager = GetComponent<GameManager>();
		player= GetComponent<Player>();
	}

	public void  Continue()	{
		panel.SetActive(false);
		gameManager.NextWave();
	}

	public void buildTower(){
		panel2.SetActive(true);
	}

	public void buildFireTower(){
		if(player.hearts>15)
			gameManager.FireTower++;
		else
			error.SetActive(true);
	}

	public void buildCurseTower(){
		if(player.hearts>20)
			gameManager.CurseTower++;
		else
			error.SetActive(true);
	}

	public void buildArrowTower(){
		if(player.hearts>10)
			gameManager.ArrowTower++;
		else
			error.SetActive(true);
	}

	public void Back(){
		panel2.SetActive(false);
	}

	public void NotEnough(){
		error.SetActive(false);
	}
}