﻿using UnityEngine;
using System.Collections;
//using RAIN.Core;



public class Repos : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other)
	{


		
		//AIRig rig = other.GetComponentInChildren<AIRig>();
		
	//	RAIN.Memory.BasicMemory tMemory = rig.AI.WorkingMemory as RAIN.Memory.BasicMemory; 
		
		//GameObject boxx = tMemory.GetItem("Repos") as GameObject;



		
		if (other.tag == "Employe") 
		{


		//	print ("REPOS");


		//	tMemory.SetItem<T>("Box", <T> value); 
			

			//rig.AI.WorkingMemory.SetItem<T>(string name, <T> value);

		//	RAIN.Minds.RAINMind tMind = rig.AI.Mind as RAIN.Minds.RAINMind;

			//tMind.


			other.GetComponentInChildren<Employe>().Repos();
			//
			
			//gameObject.start () as Employe;
			
		}
		
		
	}




}

