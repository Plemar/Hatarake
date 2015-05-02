﻿using UnityEngine;
using RAIN.Core;
using RAIN.Minds;
using RAIN.Serialization;
//using System.Collections;
using RAIN.Navigation.Targets;

[RAINSerializableClass]
public class RAINMove : RAINMind
{
	//[RAINSerializableField]
	//private GameObject boxDeTravail;

	//[RAINSerializableField]
	//private GameObject chill;
	
	//[RAINSerializableField]
	//private Transform _target;


//	[RAINSerializableField]
	//private float motivation = 100;// variable conditionnant le départ en pause. motivation = 0 -> go to Pause;

	public override void Think()
	{

		RAIN.Memory.BasicMemory tMemory = AI.WorkingMemory as RAIN.Memory.BasicMemory; 
		
		GameObject targ = tMemory.GetItem("myTarget") as GameObject;

		//Debug.Log ("boxxboxxboxxboxx: "+ boxx.name);

		//print ("dd");

		// create target
		//GameObject target = (GameObject)Instantiate(Resources.Load("prefab/target2"));
		//target.transform.position = new Vector3(22, 1, 6);

		//_target = targ.transform.GetChild (0);

	//	Debug.Log ("_target: "+ _target.name);

		if (targ != null) {
			//targ.gameObject.GetComponentInChildren<NavigationTargetRig>().Target.MountPoint = target.transform;
			//targ.gameObject.GetComponentInChildren<NavigationTargetRig>().Target.TargetName = "NavTarget";

			//AI.Motor.MoveTo (targ.transform.GetChild (0).position);
		//	AI.Motor.MoveTo (targ.gameObject.GetComponentInChildren<NavigationTargetRig>().Target.Position);
			AI.Motor.MoveTo (targ.transform.position);
		}
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