﻿using UnityEngine;
using System.Collections;
//using RAIN.Core;

public class EmployeNAVMESH : MonoBehaviour {


//bool suicidaire;

	float feignantise = 30;// variable conditionnant le temps entre chaque pause (constante définit lors de la génération aléatoire d’employés, valeur unique pour le premier proto);

//Chaque seconde : motivation -= feignantise DONC si feignantise est grand, les pauses seront plus fréquentes.

float motivation = 100;// variable conditionnant le départ en pause. motivation = 0 -> go to Pause;

	float fatigue= 0;// variable similaire à la vie, diminue qd il se fait engueuler, augmente lors de ses pauses. si == fatigueMAX -> suicidaire = true;

	float fatigueMAX = 100;

/*Box*/  public GameObject boxDeTravail;// => Box de l’employé

//true = au taff
//false = en train de glander (engueulable)
public bool auTravail;
	
bool enDeplacement;// utile pour l’animation notamment

//si auTravail == true (true vers la photocop, vers le box) || false (true vers la salle de repos, vers les chiottes)

//si auTravail == true (false dans le box, à la photcop) || false (false à la salle de repos, au chiottes)


float vitesseDeTravail = 50;// => vitesse de production (plus grand si l’employé est bosseur => valeur unique par défaut pour le premier proto).

	Walk walker;

	Employe[] amis;// liste d’amis agissant sur la fatigue en cas de suicide;


	public GameObject chill;



	//private Transform target;

	// Use this for initialization
	void Start () {

		walker = transform.GetComponent <Walk> ();
		//foreach(Transform child in transform){
			//if(child.CompareTag("Nav")){
			//	target = child.gameObject;	

	//	target = boxDeTravail.transform.GetChild (0);


		//Walk walker = transform.GetComponent <Walk>();

		walker.targetObject = boxDeTravail.transform;

		
	/*	AIRig rig = GetComponentInChildren<AIRig>();
		
		RAIN.Memory.BasicMemory tMemory = rig.AI.WorkingMemory as RAIN.Memory.BasicMemory; 
		
		//GameObject boxx = tMemory.GetItem("Box") as GameObject;
		
		
		tMemory.SetItem("myTarget",boxDeTravail);



*/


		//RAINMove tMind = rig.AI.Mind as RAINMove; 

		//tMind.Travaille();



				//target.position = boxDeTravail.transform.position;
			//}
		//} 
		


	}

	// Use this for initialization
	public void Travaille () {

		motivation -= 1;

		//Transform target;

	//	foreach(Transform child in transform){
	//		if(child.CompareTag("Nav")){
				


		//target = child.gameObject;
				
		//target = boxDeTravail.transform.GetChild (0);

		if (motivation < 5) 
				{
			//print ("REPOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOSEEEEEEEEEEEEEEEEEEEEEEE");
					
					chill = GameObject.FindGameObjectWithTag("Repos"); 



			
			//AIRig rig = GetComponentInChildren<AIRig>();
			
		//	RAIN.Memory.BasicMemory tMemory = rig.AI.WorkingMemory as RAIN.Memory.BasicMemory; 
			
			//GameObject boxx = tMemory.GetItem("Box") as GameObject;

			walker.targetObject = chill.transform;

			//	tMemory.SetItem("myTarget",chill);
				//	target = chill.transform.GetChild (0);
					
					//GameObject player = GameObject.FindWithTag("Player");  GameObject player = GameObject.FindWithTag("Player"); 
					
					
					//target.position = chill.transform.position;
					
					//gameObject.transform.FindChild()
					
					
				}



			//}
		//} 


	



	}


	// Use this for initialization
	public void Repos () {
		
		motivation += 1;
		
		//Transform target;
		
		//foreach(Transform child in transform){
			//if(child.CompareTag("Nav")){
			//	target = child.gameObject;		

		//target = chill.transform.GetChild (0);

				if (motivation > 50) 
				{
					
					
				//	chill = GameObject.FindGameObjectWithTag("Repos"); 
					

			
		//	AIRig rig = GetComponentInChildren<AIRig>();
			
		//	RAIN.Memory.BasicMemory tMemory = rig.AI.WorkingMemory as RAIN.Memory.BasicMemory; 
			
			//GameObject boxx = tMemory.GetItem("Box") as GameObject;
			
			walker.targetObject = boxDeTravail.transform;

			//tMemory.SetItem("myTarget",boxDeTravail);


					//target = boxDeTravail.transform.GetChild (0);
					//GameObject player = GameObject.FindWithTag("Player");  GameObject player = GameObject.FindWithTag("Player"); 
					
					
					//target.transform.position = boxDeTravail.transform.position;
					
					//gameObject.transform.FindChild()
					
					
				}


			//}
		//} 
		
		
	
		
		
		
	}


	
	// Update is called once per frame
	void Update () {
	

	//print ("MOTIVATION: " + motivation);


	}
}
