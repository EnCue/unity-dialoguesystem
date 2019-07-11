using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class questNPC : MonoBehaviour {
    public NPCQuestManager qManager;
    public string npcTitle;

	public bool qCompleted;

    
    void Awake()
    {
        npcTitle = "sampleNPC_I";
    }

	void OnTriggerStay2D(Collider2D Player){
		if (Player.CompareTag ("User")) {
			if (Input.GetKeyDown (KeyCode.E)) {
				
				if (!qCompleted) {
					StartCoroutine(qManager.qNPCDirectory (npcTitle));
				}
			}
		}
	}

}
