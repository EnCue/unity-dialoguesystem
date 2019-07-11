using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class questNPC : MonoBehaviour {
    //private iQBase AIMnRef;
    //private IEnumerator runPath;
    public NPCQuestManager qManager;
    //private bool activePrmpt;
    //private float timer;
    public string npcTitle;

	public bool qCompleted;
	//public GameObject InstructionPrmpt;
	//public float timeSet;

	//public Animator animator;

	//public void assignMnRef(){
	void Awake(){
        npcTitle = "sampleNPC_I";
		//GameObject Manager = GameObject.FindWithTag ("ManagerSystem");
		//Hard-coded reference for simple single-level game
		//Use of interface allows for generalized "questPackage" assignment
		//e.g "questPackageII" in event of second level
		//AIMnRef = Manager.GetComponent<questPackageI> ();

		//InstructionPrmpt.SetActive (false);
		//activePrmpt = false;
	}
	//}

	void OnTriggerStay2D(Collider2D Player){
		if (Player.CompareTag ("User")) {
			if (Input.GetKeyDown (KeyCode.E)) {
				/*if (activePrmpt) {
					animator.SetBool ("isOpen", false);
					activePrmpt = false;
				}*/
				/*RaycastHit2D hit0 = Physics2D.Raycast(transform.position, Vector2.up);
				RaycastHit2D hit1 = Physics2D.Raycast(transform.position, Vector2.down);
				RaycastHit2D hit2 = Physics2D.Raycast(transform.position, Vector2.left);
				RaycastHit2D hit3 = Physics2D.Raycast(transform.position, Vector2.right);

				if(hit0.collider != null && hit0.collider.CompareTag("User"))
				{
					Debug.Log("UP");
				}
				else if(hit1.collider != null && hit1.collider.CompareTag("User"))
				{
					Debug.Log("DOWN");
				}
				else if(hit2.collider != null && hit2.collider.CompareTag("User"))
				{
					Debug.Log("LEFT");
				}
				else if(hit3.collider != null && hit3.collider.CompareTag("User"))
				{
					Debug.Log("RIGHT");
				}*/
				
				if (!qCompleted) {
					//Debug.Log ("E");
					StartCoroutine(qManager.qNPCDirectory (npcTitle));
					/*
					if (AIMnRef == null) {
						Debug.Log ("Empty");
					} else {
						AIMnRef.AIDirectory (gameObject.tag);
					}*/
				}
			}
		}
	}

	/*void OnTriggerEnter2D(Collider2D Player){
		if (Player.CompareTag ("User")) {
			//Debug.Log ("Entered");
			//InstructionPrmpt.SetActive (true);
			if(!GlobalStatusLog.q2){
				animator.SetBool("isOpen", true);
				activePrmpt = true;
				timer = timeSet;
			}
		}
	}
	void OnTriggerExit2D(Collider2D Player){
		if (Player.CompareTag ("User")) {
			//Debug.Log ("Left");
			//InstructionPrmpt.SetActive (false);
			if(activePrmpt){
				animator.SetBool("isOpen", false);
				activePrmpt = false;
			}
		}
	}

	void Update(){
		if (activePrmpt) {
			timer -= Time.deltaTime;

			if (timer <= 0) {
				timer = 0;
				//InstructionPrmpt.SetActive (false);
				animator.SetBool("isOpen", false);
				activePrmpt = false;
				timer = 0;
			}
		}
	}*/
	/*
	public void setRunPath(IEnumerator newRunPath){
		Debug.Log ("New run path declared/updated");

		runPath = newRunPath;
	}*/
	/*
    void OnTriggerStay(Collider Player)
    {
        if (Player.CompareTag("User"))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!qCompleted)
                {
                    //Debug.Log ("E");
                    if (AI_qLoader == null)
                    {
                        Debug.Log("Empty");
                    }
                    AI_qLoader.qPack_AI(gameObject.tag);
                }
            }
        }
    }*/
}
