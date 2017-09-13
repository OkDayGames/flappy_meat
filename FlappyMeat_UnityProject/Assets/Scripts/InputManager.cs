using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
	private static InputManager _instance;

	public static InputManager Instance
	{
		get { return _instance; }
	}
	
	public PlayerBehaviour playerBehaviour;
	void Awake ()
	{
		if (!_instance)
		{
			_instance = this;
		}
	}

	public void OnTap()
	{
		playerBehaviour.Jump();
	}

	public void Initialize()
	{
		playerBehaviour = GameObject.Find("Player").GetComponent<PlayerBehaviour>();
		playerBehaviour.Initialize();
	}
}
