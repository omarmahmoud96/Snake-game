using UnityEngine;
using System.Collections;
using System;

public class Snake : MonoBehaviour {

	private Snake next;
	static public Action<string> hit;

	public void setNext(Snake IN)
	{
		next = IN;
	}

	public Snake getNext()
	{
		return next;
	}

	public void removeTail()
	{
		Destroy (this.gameObject); 
	}

	void onTriggerEnter(Collider other)
	{
		if (hit != null) 
		{
			hit (other.tag);
		}
		if(other.tag == "Food")
		{
			Destroy(other.gameObject);
		}
	}
}