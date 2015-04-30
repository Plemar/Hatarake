﻿using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour {


	Vector2 position; //peut utiliser son transform


	float vitesseDep;


	float jaugeEngueulage; //se remplit quand on appuie sur le boss.

	//float timer = 0;

	bool charge = false;

	Transform actionArea; 
 

	// Use this for initialization
	void Start () {

		actionArea = transform.GetChild (0);

	//	navComponent = this.transform.GetComponent <NavMeshAgent>(); 
	


	}
	
	// Update is called once per frame
	void Update () {
	
		if(Input.GetMouseButton(0))
		{

			Vector3 pos;
 
			if (charge)
			{
				jaugeEngueulage += Time.deltaTime;
				
		
				
				actionArea.localScale = new Vector3(jaugeEngueulage,actionArea.localScale.y,jaugeEngueulage);
			}
			else 
			{
				pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				pos.y = transform.position.y;

			//	navComponent.SetDestination (pos);



			}



			//print ("mouseDown: "+ pos);

		} 
		

	}






	void OnMouseDown () 
	{
		print ("START CHARGE ");
		charge = true;


	}

	void OnMouseUp ()
	{
		charge = false;


		//actionArea.localScale.z = jaugeEngueulage;


		if (jaugeEngueulage >= 2)
		{


			print ("HATARAKE!! ");


		}
		actionArea.localScale = new Vector3(0.2f,actionArea.localScale.y,0.2f);

		print ("END CHARGE ");

		//ResetTimer
		jaugeEngueulage = 0;

	}
 
	
}



