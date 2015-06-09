﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Navigation.Targets;
using RAIN.Minds;
using RAIN.Serialization;
using RAIN.Motion;


public class Boss : MonoBehaviour {

    public Animator animator;

   // public GameObject gameManager;
    public bool tutoLock = false;

	//Vector2 position; //peut utiliser son transform
	float vitesseDep;
    public bool moveLocked ;
    public bool hatarakeLocked ;
    public int yellingO_Meter = 0;

    public int maxYellingO_Meter = 50;
    public int gainByBubble = 2;
    public int maxLossByScream = 10;
	//GameObject[] boxies;

    public AudioSource[] bossMovingSounds;
    public AudioSource[] voicelessBossSounds;
    public AudioSource[] bubbleSound;

    public float jaugeEngueulageMin = 4.0f; //se remplit quand on appuie sur le boss.
    public float jaugeEngueulageMax = 15.0f; //se remplit quand on appuie sur le boss.
    public float tempsRemplissageJauge = 1.0f; 

    public float vitesseJauge = 2.0f; //se remplit quand on appuie sur le boss.

	//float timer = 0;

	public bool charge = false;
    Vector3 pos;

	Transform actionArea;
	public RAIN.Memory.BasicMemory tMemory;
    private RAIN.Navigation.BasicNavigator tNav;
    private RAIN.Motion.MecanimMotor tMotor;
	// Use this for initialization
	void Start () {
       // gameManager = GameObject.Find("GameManager");
        if (GameManager.instance.tutoIsOn)
        {
            moveLocked = hatarakeLocked = true ;
        }
        else
        {
            moveLocked = hatarakeLocked = false;
        }

		AIRig aiRig = GetComponentInChildren<AIRig>();		
		tMemory = aiRig.AI.WorkingMemory as RAIN.Memory.BasicMemory;
        tMotor = aiRig.AI.Motor as RAIN.Motion.MecanimMotor;

        /*
        moveLocked = true;
        hatarakeLocked = true;*/

        foreach (Transform go in transform)
        {
            if (go.name == "Cylinder") actionArea = go;

        }

        tNav = aiRig.AI.Navigator as RAIN.Navigation.BasicNavigator;
       
        //navComponent = this.transform.GetComponent <NavMeshAgent>();

	}

    public void playRandomBossSound()
    {
        bool alreadyPlaying = false;
        for (int i = 0; i < bossMovingSounds.Length; i++)
            if (bossMovingSounds[i].isPlaying)
            {
                alreadyPlaying = true;
            };

        if (!alreadyPlaying)
        {
            int rdmIndex = Random.Range(0, bossMovingSounds.Length);
            bossMovingSounds[rdmIndex].Play();
        }
    }

    public void playRandomVoicelessBossSound()
    {
        bool alreadyPlaying = false;
        for (int i = 0; i < voicelessBossSounds.Length; i++)
            if (voicelessBossSounds[i].isPlaying)
            {
                alreadyPlaying = true;
            };

        if (!alreadyPlaying)
        {
            int rdmIndex = Random.Range(0, voicelessBossSounds.Length);
            voicelessBossSounds[rdmIndex].Play();
        }
    }

    public void addBubble()
    {
        for (int i = 0; i < bubbleSound.Length;i++ )
            if (!bubbleSound[i].isPlaying)
            {
                bubbleSound[i].Play();
                break;
            }
        yellingO_Meter = yellingO_Meter + gainByBubble;
        if (yellingO_Meter > maxYellingO_Meter) yellingO_Meter = maxYellingO_Meter;
    }
    
    public bool moveSoundLock=false;

	// Update is called once per frame
	void Update () {
       
        if (Input.GetMouseButtonUp(0) && !charge && !moveLocked)
        {
            pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //pos.y = transform.position.y;
            pos.y = 2;
            //navComponent.SetDestination (pos);
            //colliders = Physics.OverlapSphere(pos, 1f /* Radius */);

            //print("un deux un deux");

            Collider[] colliders = Physics.OverlapSphere(pos, 1f /* Radius */);

            if (!(colliders.Length > 0 && colliders[0].tag == "Employe") && pos != null && pos != transform.position)
            {
                if (tNav.OnGraph(pos, 0))
                {
                    tMemory.SetItem("sabotage", false);
                    setTarget(pos);
            
                }
               
            }
        }

        if (tMemory.GetItem<bool>("enDeplacement") && !moveSoundLock)
        {

            int rdmIndex = Random.Range(0, bossMovingSounds.Length);
            bossMovingSounds[rdmIndex].Play();
            moveSoundLock = true;
        }
        else if (!tMemory.GetItem<bool>("enDeplacement") && moveSoundLock)
        {
            moveSoundLock = false;
        }

	}

    public void setTarget(Vector3 target)
    {
        tMemory.SetItem("enDeplacement", true);
        tMemory.SetItem("target", target);

    }

      public void faceTarget(Vector3 target)
    {
       // tMotor.FaceTarget = new MoveLookTarget();
     //   tMotor.FaceAt(target);
        tMemory.SetItem("lookTarget", target);
    }
    
                   


    public Vector3 getTarget()
    {     
        return tMemory.GetItem<Vector3>("target");
    }

    public void action()
    {
        Animator ann = gameObject.transform.FindChild("BossSprite"). GetComponent<Animator>();
        ann.SetTrigger("doingStuff");
    }

    public IEnumerator Engueulade()
    {
        float currentYellingOMeter = yellingO_Meter;
        float top = Mathf.Min(maxLossByScream, yellingO_Meter);
        animator.SetTrigger("chargingHatarake");
        actionArea.gameObject.SetActive(true);
        float pos = 0;
        float time = 0;
        while (charge)
        {
            time = time + Time.deltaTime;

            pos = Mathf.Lerp(jaugeEngueulageMin, jaugeEngueulageMin + jaugeEngueulageMax * (top / maxLossByScream), time / (tempsRemplissageJauge * (top / maxLossByScream)));

            actionArea.localScale = new Vector3(pos, actionArea.localScale.y, pos);

            yield return null;
        }
        int yellingO_OnEight = (int)(((pos - jaugeEngueulageMin) / jaugeEngueulageMax) * 8);
        yellingO_Meter = yellingO_Meter - yellingO_OnEight * maxYellingO_Meter / 8;

        //Sign.Create(pos, this.transform.position, SignType.Hatarake);
        SignEmitter.Create(this.transform.position, SignType.Hatarake,pos);

        if (yellingO_OnEight > 6)
        {

            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().shaking = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().shakeMagnitude = pos;
            GameObject audio = this.transform.Find("hatarake_strong").gameObject;
            audio.GetComponent<AudioSource>().Play();
        }
        else if (yellingO_OnEight > 1)
        {
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().shaking = true;
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraController>().shakeMagnitude = pos;
            GameObject audio = this.transform.Find("hatarake_medium").gameObject;
            audio.GetComponent<AudioSource>().Play();
        }
        else if (yellingO_OnEight > 0)
        {
            GameObject audio = this.transform.Find("hatarake_low").gameObject;
            audio.GetComponent<AudioSource>().Play();
        }
        actionArea.gameObject.SetActive(false);
        actionArea.localScale = new Vector3(0.8f, actionArea.localScale.y, 0.8f);
        //ResetTimer
        // jaugeEngueulage = 0;
        List<GameObject> employesEngueulable = actionArea.GetComponent<jaugeEngueulage>().getEmployesJauge();
        foreach (GameObject emp in employesEngueulable)
        {
            if (emp.GetComponent<Decrasseur>() != null)
                emp.GetComponent<Decrasseur>().Engueule();
            else
                emp.GetComponent<Employe>().Engueule();
        }
        if (GameManager.instance.tutoIsOn && GameManager.instance.goingToHatarakeSlacker)
        {
            if (employesEngueulable.Count >= 1)
            {
                GameManager.instance.TutoEmployeeHataraked();
            }
        }

        actionArea.GetComponent<jaugeEngueulage>().clearEmployesJauge();

        animator.SetTrigger("releasingHatarake");
        //actionArea.GetComponent<jaugeEngueulage>().Engueule();
    }


	void OnMouseDown () 
	{
        if (!hatarakeLocked && yellingO_Meter >0 )
        {
            //print("START CHARGE ");
            charge = true;
            tMemory.SetItem("charge", true);
            StartCoroutine(Engueulade());
        }
        else
        {
            playRandomVoicelessBossSound();
        }

	}
    void OnPreviewMouseRightButtonDown( )
    {

    }
	void OnMouseUp ()
	{
		charge = false;
		tMemory.SetItem("charge",false);
	}
	
}



