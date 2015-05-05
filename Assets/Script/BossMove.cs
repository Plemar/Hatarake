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
	//Transform actionArea;

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
		tMemory = AI.WorkingMemory as RAIN.Memory.BasicMemory;

	}



//	[RAINSerializableField]
	//private float motivation = 100;// variable conditionnant le départ en pause. motivation = 0 -> go to Pause;

	public override void Think()
	{
		charge = (bool)tMemory.GetItem("charge");

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

}