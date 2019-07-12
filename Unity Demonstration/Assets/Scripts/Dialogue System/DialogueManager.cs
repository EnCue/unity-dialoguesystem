using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour {
	[SerializeField] private GameObject DialogueCanvas;
	[SerializeField] private GameObject btnLoad;
	[SerializeField] private Button leaveBtn;

    public Text Speaker;
    public Text dialogueText;
	public Button contBtn1;
	public Button contBtn2;
	public Text contBtn1Txt;
	public Text contBtn2Txt;
	public Text leaveBtnTxt;

    public Animator anim;
	private bool multipleSentences;


    public string[] getContOptions(string branchPath, Dictionary<string, string[]> dRef)
    {
        string[] contOptions = new string[2];
        try
        {
            contOptions = dRef[branchPath];
        }
        catch (KeyNotFoundException)
        {
            contOptions = new string[2];
        }

        return contOptions;
    }

    public string[] getOration(string branchPath, Dictionary<string, string> dRef)
    {
        string[] newOration = new string[] { };
        try
        {
            string Oration = dRef[branchPath];
            newOration = new string[] { Oration };
        }
        catch (KeyNotFoundException)
        {
            string Oration_a = dRef[branchPath + "a"];
            string Oration_b = dRef[branchPath + "b"];
            try
            {
                string Oration_c = dRef[branchPath + "c"];
                newOration = new string[] { Oration_a, Oration_b, Oration_c };
            }
            catch (KeyNotFoundException)
            {
                newOration = new string[] { Oration_a, Oration_b };
            }
        }

        return newOration;
    }

    

    public IEnumerator displayResponse(Dictionary<string, string> responseArchive, string path, Action<bool> canContinue)
    {
        string[] options_Filler = new string[0];

        string[] Response = new string[] { responseArchive[path] };
        outputDialogue("Faust", Response, options_Filler);

        yield return new WaitForSeconds(3.0f);

        canContinue(true);
        yield break;
    }



    public void outputDialogue(string NPCName, string[] sentences, string[] btnTitles)
	{
		anim.SetBool("isOpen", true);
		leaveBtn.gameObject.SetActive (false);
		btnLoad.SetActive(false);
		Speaker.text = NPCName;

		int rOptions = 0;
		//Loading response-type appropriate settings

		//Conversational reponse display
		if (btnTitles.Length == 0) {
            //Empty title arrays denote player response displays
			rOptions = 1;
		} else if (btnTitles.Length == 1) {
			//Title arrays of length 1 denote closing NPC sentence displays
			rOptions = 2;
			leaveBtnTxt.text = btnTitles [0];
		} else {
			//Regular sentence display load
			rOptions = 0;
			contBtn1Txt.text = btnTitles [0];
			contBtn2Txt.text = btnTitles [1];
		}

		//Initiating TypeSentence coroutine
		StartCoroutine (iterateSentences (sentences, rOptions));
	}

	IEnumerator iterateSentences(string[] Sentences, int btnLoad){
		int iterator = Sentences.Length - 1;

        bool iterating = false;
        if (Sentences.Length > 1) {
            iterating = true;
        }
        
        for (int i = 0; i <= iterator; i++) {
			string[] currentSentence = new string[] { Sentences [i] };

			if (i == iterator) {
				iterating = false;
			}

            StartCoroutine(TypeSentence(currentSentence, btnLoad, iterating));

            if (iterating) {
                yield return new WaitForSeconds(4.5f);
            }
            else {
                yield break;
            }
        
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
				btnLoad.SetActive (true);
			} else if (rType == 2) {
				leaveBtn.gameObject.SetActive (true);
				yield break;
			}
		}
    }


	public void EndDialogue()
	{
		Debug.Log("End of conversation");
		anim.SetBool("isOpen", false);
	}
}
