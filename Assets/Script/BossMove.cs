﻿using UnityEngine;
using RAIN.Core;
using RAIN.Minds;
using RAIN.Serialization;
//using System.Collections;
using RAIN.Navigation.Targets;

[RAINSerializableClass]
public class BossMove : RAINMind
{

	float vitesseDep;
	float jaugeEngueulage; //se remplit quand on appuie sur le boss.
	Vector3 pos;
	//float timer = 0;
	
	//bool charge = false;
	
	//Transform actionArea; 
	//[RAINSerializableField]
	//private GameObject boxDeTravail;

	//[RAINSerializableField]
	//private GameObject chill;
	
	//[RAINSerializableField]
	//private Transform _target;
	RAIN.Memory.BasicMemory tMemory;
	bool charge;

	// Use this for initialization
	public override void Start()
	{

		
		tMemory = AI.WorkingMemory as RAIN.Memory.BasicMemory; 
		

		charge = (bool)tMemory.GetItem("charge");



	}



//	[RAINSerializableField]
	//private float motivation = 100;// variable conditionnant le départ en pause. motivation = 0 -> go to Pause;

	public override void Think()
	{

		//Vector3 pos;
		if (!charge)
		{
			if(Input.GetMouseButton(0))
			{

			//	timer += Time.deltaTime;
				
				
				
			//	actionArea.localScale = new Vector3(timer,actionArea.localScale.y,timer);
		//	}
		//	else 
		//	{
				pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				//pos.y = transform.position.y;
				
				//navComponent.SetDestination (pos);
				

			}

			if (pos != null) {
				//targ.gameObject.GetComponentInChildren<NavigationTargetRig>().Target.MountPoint = target.transform;
				//targ.gameObject.GetComponentInChildren<NavigationTargetRig>().Target.TargetName = "NavTarget";
				
				//AI.Motor.MoveTo (targ.transform.GetChild (0).position);
				//	AI.Motor.MoveTo (targ.gameObject.GetComponentInChildren<NavigationTargetRig>().Target.Position);
				AI.Motor.MoveTo (pos);
			}
			//print ("mouseDown: "+ pos);
			
		}

	
		//Debug.Log ("boxxboxxboxxboxx: "+ boxx.name);

		//print ("dd");

		// create target
		//GameObject target = (GameObject)Instantiate(Resources.Load("prefab/target2"));
		//target.transform.position = new Vector3(22, 1, 6);

		//_target = targ.transform.GetChild (0);

	//	Debug.Log ("_target: "+ _target.name);


	}
	
	/*

	// Use this for initialization
	public void Repos () {

		Debug.Log ("REPPOOOOOS");

		motivation += 1;
		
	//	Transform target;
		
		//foreach(Transform child in transform){
		//if(child.CompareTag("Nav")){
		//	target = child.gameObject;		
		
		_target = chill.transform.GetChild (0);
		
		if (motivation > 50) 
		{

			//	chill = GameObject.FindGameObjectWithTag("Repos"); 

			//GameObject player = GameObject.FindWithTag("Player");  GameObject player = GameObject.FindWithTag("Player"); 

			_target = boxDeTravail.transform.GetChild (0);
			
			//gameObject.transform.FindChild()
		}

		//}
		//} 

	}

	// Use this for initialization
	public void Travaille () {
		
		motivation -= 1;
		Debug.Log ("TRAAVAILLLEEEEEEEEEEEEE");

		//Transform target;
		
		//	foreach(Transform child in transform){
		//		if(child.CompareTag("Nav")){

		//target = child.gameObject;
		
		_target = boxDeTravail.transform.GetChild (0);
		
		if (motivation < 5 && _target.position == boxDeTravail.transform.position) 
		{
			//print ("REPOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOSEEEEEEEEEEEEEEEEEEEEEEE");
			
			chill = GameObject.FindGameObjectWithTag("Repos"); 

			_target = chill.transform.GetChild (0);
			
			//GameObject player = GameObject.FindWithTag("Player");  GameObject player = GameObject.FindWithTag("Player"); 

			//target.position = chill.transform.position;
			
			//gameObject.transform.FindChild()

		}

		//}
		//} 

	}
*/

}