using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState{START, PLAY, GAMEOVER}

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	public static GameManager Instance
	{
		get { return _instance; }
	}

	private int _score;

	public int Score
	{
		get { return _score; }
		set
		{
			_score = value;
			UiManager.Instance.SetScoreText(_score);
		}
	}

	private GameState currentState;

	void Awake()
	{
		if (!_instance)
		{
			_instance = this;
		}
	}
	void Start () {
		Initialize();
		BarrierManager.Instance.Initialize();
		InputManager.Instance.Initialize();
	}
	
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			if (currentState == GameState.PLAY)
			{
				InputManager.Instance.OnTap();
			}
			else if(currentState == GameState.START)
			{
				currentState = GameState.PLAY;
				InputManager.Instance.OnTap();
			}
		}
		
		if (currentState == GameState.PLAY)
		{
			BarrierManager.Instance.MoveBarriers();
		}

		if (InputManager.Instance.playerBehaviour.CurrntPlayerState == PlayerState.DEAD)
		{
			Restart();
		}
	}

	private void FixedUpdate()
	{
		if (currentState == GameState.PLAY)
		{
			InputManager.Instance.playerBehaviour.Moved();
		}
	}

	private void Initialize()
	{
		currentState = GameState.START;
	}
	
	//-------------------------------------------------------

	public void Restart()
	{
		SceneManager.LoadScene("game");
	}
}
