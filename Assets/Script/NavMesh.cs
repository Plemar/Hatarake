﻿using UnityEngine;
using System.Collections;
using RAIN.Navigation.NavMesh;

public class NavMesh : MonoBehaviour {

	private int _threadCount = 4;
	public GameObject navMesh;
	public bool isNavMeshDone = false;

	NavMeshRig tRig;

	// This will regenerate the navigation mesh when called
	public IEnumerator GenerateNavmesh()
	{
		float officeMiddle = ((float)gameObject.GetComponent<LevelManager> ().getOfficeInstance ().size-1.0f)/2.0f;


		navMesh = Instantiate (navMesh);
	//transform.position = 
		LevelManager level = gameObject.GetComponent<LevelManager>();
		navMesh.transform.position = level.officePrefab.transform.position + new Vector3(officeMiddle, 0 ,officeMiddle);

		tRig = navMesh.GetComponent<RAIN.Navigation.NavMesh.NavMeshRig>();

		// Unregister any navigation mesh we may already have (probably none if you are using this)
		tRig.NavMesh.UnregisterNavigationGraph();
		tRig.NavMesh.Size = gameObject.GetComponent<LevelManager> ().getOfficeInstance ().size;
		float startTime = Time.time;
		tRig.NavMesh.StartCreatingContours(tRig, _threadCount);
		while (tRig.NavMesh.Creating)
		{
			tRig.NavMesh.CreateContours();
			
			yield return new WaitForSeconds(1);
		}
		isNavMeshDone = true;
		float endTime = Time.time;
		Debug.Log("NavMesh generated in " + (endTime - startTime) + "s");
		tRig.NavMesh.RegisterNavigationGraph();
		tRig.Awake();
		
	}
}