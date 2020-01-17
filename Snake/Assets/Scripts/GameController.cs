using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {
	
	public int maxSize;
	public int currentSize; 
	public int xBound;
	public int yBound;
	public int Score;
	public GameObject foodPrefab;
	public GameObject currentFood;
	public GameObject SnakePrefab;
	public Snake head;
	public Snake tail;
	public int NESW ;
	public Vector2 nextPos;


	void onEnable()
	{
		Snake.hit += hit;

	}

	// Use this for initialization
	void Start () 
	{
		InvokeRepeating ("TimerInvoke", 0, 0.08f);
		foodFunction ();
	}


	void onDisable()
	{
		Snake.hit -= hit;
	}
	
	// Update is called once per frame
	void Update () {
		comChangeD ();	
	
	}

	void TimerInvoke()
	{
		Movement ();

		if (currentSize >= maxSize)
		{
			tailFunction ();
		}
		else 
		{
			currentSize++;
		}
	}
	void Movement()
	{
		GameObject temp;
		nextPos = head.transform.position;
		switch (NESW) 
		{
		// north or up
		case 0:                                                
			nextPos = new Vector2 (nextPos.x , nextPos.y+1);
			break ;
		// east or right
		case 1:                                                  
			nextPos = new Vector2(nextPos.x+1 , nextPos.y);
			break;
		// south or down
		case 2:		                                            
			nextPos = new Vector2(nextPos.x , nextPos.y-1);
			break;
		// west or left
		case 3:		                                         
			nextPos = new Vector2(nextPos.x-1 , nextPos.y);
			break;					
		}

		temp = (GameObject)Instantiate (SnakePrefab, nextPos, transform.rotation);	
		head.setNext (temp.GetComponent<Snake> ());
		head = temp.GetComponent<Snake> ();
		return; 
		} 

	void comChangeD()
	{
		if (NESW != 2 && Input.GetKeyDown (KeyCode.W))
		{
			NESW = 0;
		}
		if (NESW != 3 && Input.GetKeyDown (KeyCode.D))
		{
			NESW = 1;
		}
		if (NESW != 0 && Input.GetKeyDown (KeyCode.S))
		{
			NESW = 2;
		}
		if (NESW != 1 && Input.GetKeyDown (KeyCode.A))
		{
			NESW = 3;
		}
	}

	void tailFunction()
	{
		Snake tempSnake = tail;
		tail = tail.getNext (); 
		tempSnake.removeTail ();
	}


	void foodFunction()
	{
		int xPos = Random.Range (-xBound, xBound);
		int yPos = Random.Range (-yBound, yBound);

		currentFood = (GameObject)Instantiate (foodPrefab,new Vector2(xPos,yPos),transform.rotation);
		StartCoroutine (CheckRender (currentFood));
	}


	IEnumerator CheckRender(GameObject IN)
	{
		yield return new WaitForEndOfFrame ();
		if (IN.GetComponent<Renderer> ().isVisible == false) 
		{
			if (IN.tag == "Food") 
			{
				Destroy (IN);
				foodFunction ();
			}
		}
	}


	void hit(string WhatWasSent)
	{
		if (WhatWasSent == "Food") 
		{
			foodFunction ();
			maxSize++;
			Score++;
		}
	}
}



