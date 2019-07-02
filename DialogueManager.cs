using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	[SerializeField] private GameObject DialogueCanvas;
	[SerializeField] private GameObject responseLoad;
	//[SerializeField] private GameObject rTypeLoad2;
	[SerializeField] private Button leaveBtn;
    public Text Speaker;
    public Text dialogueText;
	public Button contBtn1;
	public Button contBtn2;
	public Text contBtn1Txt;
	public Text contBtn2Txt;
	public Text leaveBtnTxt;

    public Animator anim;
	//private lyricsEntryHandler LEHandler;
	//private int LELoadKey;
	private bool multipleSentences;
	
	void Awake(){
		//LEHandler = DialogueCanvas.GetComponent<lyricsEntryHandler> ();

		refLibrary lib = GameObject.FindGameObjectWithTag ("ManagerSystem").GetComponent<refLibrary> ();
		lib.dlgManager = gameObject;
		lib.dlgInterface = this;
	}

	public void outputDialogue(string NPCName, string[] sentences, string[] btnTitles)
	{
		//anim.SetBool("isOpen", true);
		//contBtnFinal.gameObject.SetActive (false);
		//rTypeLoad1.SetActive(false);
		//rTypeLoad2.SetActive (false);
		Speaker.text = NPCName;

		int rOptions = 0;
		//Loading response-type appropriate settings
		if(chantKey > -1){
			rOptions = 3;
			LELoadKey = chantKey;
		}else {
			//Conversational reponse display
			if (btnTitles.Length == 0) {
				//Empty title arrays denote player response displays
				rOptions = 1;
			} else if (btnTitles.Length == 1) {
				//Title arrays of length 1 denote closing NPC sentence displays
				rOptions = 2;
				contBtnFinalTxt.text = btnTitles [0];
			} else {
				//Regular sentence display load
				rOptions = 0;
				contBtn1Txt.text = btnTitles [0];
				contBtn2Txt.text = btnTitles [1];
			}
		} /*else if (rType == 2) {
			//Lyrics entry response display
			rOptions = 3;
			LELoadKey = chantKey;
		}*/

		//Initiating TypeSentence coroutine
		StartCoroutine (iterateSentences (sentences, rOptions));
	}

	IEnumerator iterateSentences(string[] Sentences, int btnLoad){
		int iterator = Sentences.Length - 1;

        bool iterating = false;
        if (Snentences.Length > 1) {
            iterating = true;
        }
        
        for (int i = 0; i <= iterator; i++) {
			string[] currentSentence = new string[] { Sentences [i] };

            StartCoroutine(TypeSentence(currentSentence, btnLoad, iterating));

            if (iterating) {
                yield return new WaitForSeconds(4.5f);
            }
            else {
                yield break;
            }
            

			/*if (i <= (iterator - 1)) {
				StartCoroutine (TypeSentence (currentSentence, -1, true));
				yield return new WaitForSeconds (4.5f);
			} else {
				StartCoroutine (TypeSentence (currentSentence, btnLoad, false));
			}*/
		}
		yield break;
	}

	//Sentence handler for singular sentence responses
	IEnumerator TypeSentence(string[] sentence, int rType, bool iterating)
    {
		string sentence_I = sentence [0];
        dialogueText.text = "";
        foreach (char letter in sentence_I.ToCharArray())
        {
            dialogueText.text += letter;
			yield return null;
        }

		if (iterating) {
			yield return null;

		} else {
			if (rType == 0) {
				rTypeLoad1.SetActive (true);
			} else if (rType == 2) {
				contBtnFinal.gameObject.SetActive (true);
				yield break;
			} else if (rType == 3) {
				yield return new WaitForSeconds (4.5f);
				rTypeLoad2.SetActive (true);

				LEHandler.initiateLE (LELoadKey);
			}
		}
    }
	/*
	//Sentence handler for multi-sentence responses
	IEnumerator TypeSentences(string[] sentences, int rType)
	{
		Debug.Log ("Reached typeSentences");
		string sentence_I = sentences [0];
		string sentence_II = sentences [1];
		dialogueText.text = "";
		foreach (char letter in sentence_I.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}
		yield return new WaitForSeconds (6.0f);
		dialogueText.text = "";

		foreach (char letter in sentence_II.ToCharArray())
		{
			dialogueText.text += letter;
			yield return null;
		}

		if (rType == 0) {
			rTypeLoad1.SetActive (true);
		} else if (rType == 2) {
			contBtnFinal.gameObject.SetActive (true);
		} else if (rType == 3) {
			yield return new WaitForSeconds (3.0f);
			rTypeLoad2.SetActive (true);

			LEHandler.initiateLE (LELoadKey);
		}
	}*/



	public void EndDialogue()
	{
		Debug.Log("End of conversation");
		anim.SetBool("isOpen", false);
	}
}
