using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierManager : MonoBehaviour
{
	private static BarrierManager _instance;

	public static BarrierManager Instance
	{
		get { return _instance; }
	}

	public List<GameObject> BarriersAll;
	public List<GameObject> BarriersMovabel;
	private Transform _tranform;
	private float spawnTime;

	public int barriersCount;
	public GameObject barrierPrefab;
	public float speed;
	public float frequencySpawn;
	public Vector3 startPos;
	public Vector3 endPos;
	public Vector3 maxHeightPos;
	public Vector3 minHeightPos;

	private Vector3 newPos;
	
	//---------------------------------------
	
	private void Awake()
	{
		if (!_instance)
		{
			_instance = this;
		}
	}
	
	//---------------------------------------
	
	public void Initialize()
	{
		spawnTime = 0;
		_tranform = this.gameObject.transform;
		BarriersAll = new List<GameObject>();
		BarriersMovabel = new List<GameObject>();
		for (int i = 0; i < barriersCount; i++)
		{
			BarriersAll.Add(GameObject.Instantiate(barrierPrefab, _tranform));
			BarriersAll[BarriersAll.Count - 1].transform.parent = null;
			BarriersAll[BarriersAll.Count - 1].transform.position = startPos;
		}
	}
	
	public void MoveBarriers()
	{
		foreach (GameObject barrier in BarriersMovabel)
		{
			barrier.transform.position = Vector3.MoveTowards(
				barrier.transform.position,
				new Vector3(
					endPos.x,
					barrier.transform.position.y,
					endPos.z
				),
				speed * Time.deltaTime);
		}
		AddBarrierToMovabel();
	}
	
	//---------------------------------------
	
	private void AddBarrierToMovabel()
	{
		if (BarriersMovabel.Count < BarriersAll.Count)
		{
			if (Time.time - spawnTime >= frequencySpawn)
			{
				spawnTime = Time.time;
				BarriersMovabel.Add(BarriersAll[BarriersMovabel.Count]);
				BarriersMovabel[BarriersMovabel.Count-1].transform.position = GetNewRandomPos();
			}
		}
		else
		{
			foreach (GameObject barrier in BarriersMovabel)
			{
				if (barrier.transform.position.x == endPos.x && Time.time - spawnTime >= frequencySpawn)
				{
					spawnTime = Time.time;
					barrier.transform.position = _tranform.position;
					barrier.transform.parent = null;
					barrier.transform.position = GetNewRandomPos();//startPos;
				}
			}
		}
	}

	private Vector3 GetNewRandomPos()
	{
		newPos = startPos;
		newPos.y = Random.Range(minHeightPos.y, maxHeightPos.y);
		return newPos;
	}
}
