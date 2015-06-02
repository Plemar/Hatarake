﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Navigation;
public class Employe : MonoBehaviour {

    GameObject employeProfile;
	public GameObject boxDeTravail;// Box de l’employé
   
	/**
	 * true = au taff
	 * false = en train de glander (engueulable)
	 * si auTravail == true (true vers la photocop, vers le box) || false (true vers la salle de repos, vers les chiottes)
	 * si auTravail == true (false dans le box, à la photcop) || false (false à la salle de repos, au chiottes)
	**/
	bool enDeplacement;// utile pour l’animation notamment
	Employe[] amis;// liste d’amis agissant sur la fatigue en cas de suicide;

	public GameObject floor;
    public static GameObject boss;

    public static List<GameObject> suicide;
	//public static List<GameObject> chill;
    public static List<GameObject> emptyChill;

	//public static List<GameObject> workingHelp;
    public static List<GameObject> emptyWorkingHelp;

    public RAIN.Memory.BasicMemory tMemory;
    private RAIN.Navigation.BasicNavigator tNav;

	public EmployeeData data;
	//Awake is always called before any Start functions

    public bool isAlreadyInRange, moveMemory, workingMemory,suicideMemory;

    //public AudioSource[] coffeeSound, facebookSound, workingSound;

    public AudioSource coffeeSound,keyboardSound,photocopierSound,depressionSound,gamingSound,facebookSound,vendingMachineSound,suicideSoundMale,suicideSoundFemale;

    public AudioSource[] engueulageSound;

    public float seuilDepression = 0.8f;
    public bool depressif = false;

	void Awake()
	{
		AIRig aiRig = GetComponentInChildren<AIRig>();
        tMemory = aiRig.AI.WorkingMemory as RAIN.Memory.BasicMemory;
        tNav = aiRig.AI.Navigator as RAIN.Navigation.BasicNavigator;          
	}

	// Use this for initialization
	void Start () 
	{
        isAlreadyInRange = false;
        suicideMemory = tMemory.GetItem<bool>("suicidaire");
        moveMemory = tMemory.GetItem<bool>("enDeplacement");
        workingMemory=tMemory.GetItem<bool>("auTravail");

        employeProfile = GameObject.Find("EmployeeProfile");

        //setActiveSound(false, false, false);

		data.InitializeEmployee ();

		if (boss == null)
		{
			boss = GameObject.FindGameObjectWithTag("Boss");
			//chill = new List<GameObject>();
            emptyChill = new List<GameObject>();
			Repos[] chills = floor.GetComponentsInChildren<Repos>();
			foreach (Repos chi in chills)
			{
                emptyChill.Add(chi.gameObject);
			}

            suicide = new List<GameObject>();
            Window[] suicidesWindows = floor.GetComponentsInChildren<Window>();
            foreach (Window window in suicidesWindows)
            {
                suicide.Add(window.gameObject);
				emptyChill.Add(window.gameObject);
            }
			
			//workingHelp = new List<GameObject>();
            emptyWorkingHelp = new List<GameObject>();
			Box[] boxes = floor.GetComponentsInChildren<Box>();
			foreach (Box box in boxes)
			{
				if (box.CompareTag("WorkHelp"))
				{
                    emptyWorkingHelp.Add(box.gameObject);
				}
			}
		}

      	
	}

	void Update () 
	{
        Vector3 distance = boss.transform.position - this.transform.position;

        //if (boss == null) print("patate");

        if (distance.magnitude < 15 && !isAlreadyInRange) // si il vient d'entrer dans le champ de vision du boss
        {
            emitActivitySign();
            isAlreadyInRange = true;

            suicideMemory = tMemory.GetItem<bool>("suicidaire");
            moveMemory = tMemory.GetItem<bool>("enDeplacement");
            workingMemory = tMemory.GetItem<bool>("auTravail");
        }
        else if (distance.magnitude >= 15 && isAlreadyInRange) // si il en sort
        {

            isAlreadyInRange = false;
        }else if((suicideMemory  != tMemory.GetItem<bool>("suicidaire") ||
            moveMemory != tMemory.GetItem<bool>("enDeplacement") || workingMemory != tMemory.GetItem<bool>("auTravail")) )
        { // si il change d'état
            
            if( isAlreadyInRange)emitActivitySign();

            GameObject target = tMemory.GetItem<GameObject>("myTarget");
            suicideMemory = tMemory.GetItem<bool>("suicidaire");
            moveMemory = tMemory.GetItem<bool>("enDeplacement");
            workingMemory = tMemory.GetItem<bool>("auTravail");

            //print("ratio : "+(float)(data.fatigue) / (float)(data.fatigueMAX));
            if (((float)(data.fatigue) / (float)(data.fatigueMAX)) > seuilDepression ) 
            {
                //play photocopier;
                if (!depressif)
                {
                    depressif = true;
                    print(this.name + " viens de rentrer en dépression");
                    this.setActiveSound(false, false, false, false, true, false, false, false, false);
                }
            }
            else if (workingMemory && !moveMemory && target.CompareTag("WorkHelp"))
            {
                depressif = false;
                //play photocopier;
                this.setActiveSound(false, false, false, true,false,false,false,false,false);
            }
            else if (workingMemory && !moveMemory && target.CompareTag("Box"))
            {
                depressif = false;
                //play clavier PC
                this.setActiveSound(false, false, true, false, false, false, false, false, false);
            }
            else if (!workingMemory && !moveMemory && target.CompareTag("Box"))
            {
                depressif = false;
                //play facebook
                this.setActiveSound(false, false, false, false, false, true, false, false, false);
            }
            else if (!workingMemory && !moveMemory && target.name.Equals("CoffeeTrigger") )
            {
                depressif = false;
                //play coffee
                this.setActiveSound(false, false, false, false, false, false, true, false, false);
            }
            else if (!workingMemory && !moveMemory && target.name.Equals("DrinkTrigger"))
            {
                depressif = false;
                //play vending machine
                this.setActiveSound(false, false, false, false, false, false, false, true, false);
            }
            else if (!workingMemory && !moveMemory && target.name.Equals("TVTrigger"))
            {
                depressif = false;
                //play gaming
                this.setActiveSound(false, false, false, false, false, false, false, false, true);
            }
            else
            {
                depressif = false;
                this.setActiveSound(false, false, false, false, false, false, false, false, false);
            }
        }
 
	}
    
    public void setActiveSound(bool engueulade,bool suicide, bool keyboard, bool photocopier, bool depression, bool facebook,bool coffee, bool vendingMachine, bool gaming)
    {
        /*
        this.gameObject.transform.Find("soundCoffee").gameObject.SetActive(coffee);
        this.gameObject.transform.Find("soundKeyboard").gameObject.SetActive(keyboard);
        this.gameObject.transform.Find("soundPhotocopier").gameObject.SetActive(photocopier);*/
        
        //if (coffee) this.gameObject.transform.Find("soundCoffee").gameObject.GetComponent<AudioSource>().Play();
       // else this.gameObject.transform.Find("soundCoffee").gameObject.GetComponent<AudioSource>().Stop();
        if (engueulade)
        {
            int index = Random.Range(0, engueulageSound.Length);
            engueulageSound[index].Play();
        }
        else
        {
            for (int i = 0; i < engueulageSound.Length; i++)
            {
                engueulageSound[i].Stop();
            }

        }

        if (keyboard && !keyboardSound.isPlaying) keyboardSound.Play();
        else keyboardSound.Stop();

        if (photocopier && !photocopierSound.isPlaying) photocopierSound.Play();
        else photocopierSound.Stop();

        if (depression && !depressionSound.isPlaying) depressionSound.Play();
        //else depressionSound.Stop();

        if (facebook && !facebookSound.isPlaying) facebookSound.Play();
        else facebookSound.Stop();

        if (vendingMachine && !vendingMachineSound.isPlaying) vendingMachineSound.Play();
        else vendingMachineSound.Stop();

        if (gaming && !gamingSound.isPlaying) gamingSound.Play();
        else gamingSound.Stop();
    }


    void OnMouseDown() 
    {
        //GameObject camera = GameObject.Find("Main Camera");
       // GameObject boss = GameObject.FindGameObjectWithTag("Boss");
       //oss.getmoveLocked = true;
        //camera.GetComponent<CameraController>().FollowEmployee(this.gameObject, 10000);
        employeProfile.GetComponent<employeeID>().setJProfile(0,this.gameObject);
    }


    public void emitActivitySign()
    {
        //print("emitactivity");
        GameObject target = tMemory.GetItem<GameObject>("myTarget");
        if (target == null) { 
            //print("TARGET NULL MAYDAY MAYDAY");
            return; 
        }
        if (tMemory.GetItem<bool>("suicidaire"))
        {
            SignEmitter.Create(this.transform.position, SignType.Death);
        }
        else if (tMemory.GetItem<bool>("wander"))
        {
                SignEmitter.Create(this.transform.position, SignType.Cellphone);
        }
        else if (tMemory.GetItem<bool>("enDeplacement"))
        {
            if (target.CompareTag("Repos"))
            {
                SignEmitter.Create(this.transform.position, SignType.GoingToGlande);
            }
            if (target.CompareTag("WorkHelp") || target.CompareTag("Box"))
            {
                SignEmitter.Create(this.transform.position, SignType.GoingToWork);
            }
        }
       
        else if (tMemory.GetItem<bool>("auTravail"))
        {
            if (target.name.Equals("PhotocopierTrigger"))
            {
                SignEmitter.Create(this.transform.position, SignType.Photocopier);
            }
            else if (target.name.Equals("WorkBoxTrigger"))
            {
                SignEmitter.Create(this.transform.position, SignType.Work);
            }
        }
        else
        {
            if (target.name.Equals("CoffeeTrigger"))
            {
                SignEmitter.Create(this.transform.position, SignType.Coffee);
            }
            else if (target.name.Equals("ToiletTrigger"))
            {
                SignEmitter.Create(this.transform.position, SignType.Toilet);
            }
            else if (target.name.Equals("DrinkTrigger"))
            {
                SignEmitter.Create(this.transform.position, SignType.Drink);
            }
            else if (target.name.Equals("WorkBoxTrigger"))
            {
                SignEmitter.Create(this.transform.position, SignType.Facebook);
            }
            else if (target.name.Equals("TVTrigger") || target.name.Equals("TVTrigger 1") || target.name.Equals("TVTrigger 2"))
            {
                SignEmitter.Create(this.transform.position, SignType.Tv);
            }
        }
    }

	public void setBox (GameObject box)
	{
		boxDeTravail = box;
	}

	public GameObject getBox ()
	{
		return boxDeTravail;
	}


	//public void auTravail ()
	//{
	//	auTravail = true;
	//}


	// Use this for initialization
	public void Engueule (){
		//Chaque seconde : motivation -= feignantise DONC si feignantise est grand, les pauses seront plus fréquentes.
		data.fatigue += data.effetEngueulement;
        data.motivation += data.effetEngueulement;

		if (data.fatigue < data.fatigueMAX) {
			//suicidaire = true;	
			tMemory.SetItem<bool>("hatarake", true);
			tMemory.SetItem<bool>("auTravail", true);
            tMemory.SetItem<bool>("wander", false);
            this.setActiveSound(true, false, false, false, false, false, false, false, false);

		}else {
            tMemory.SetItem<bool>("suicidaire", true);
        }
		
	}

	// Use this for initialization
	public void Suicide (){

        //tMemory.GetItem("suicidaire"); 
        GameObject window = tMemory.GetItem<GameObject>("myTarget");
        window.transform.Find("tache").gameObject.SetActive(true);
        window.transform.Find("brokenWindow").gameObject.GetComponent<ParticleSystem>().Play();


        if (data.isMale)
            window.transform.Find("suicideMale").GetComponent<AudioSource>().Play();
        else
            window.transform.Find("suicideFemale").GetComponent<AudioSource>().Play();

		//Destroy (this.gameObject);
        this.gameObject.SetActive(false);
	}

}
