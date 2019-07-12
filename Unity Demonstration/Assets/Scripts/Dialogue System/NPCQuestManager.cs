using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCQuestManager : MonoBehaviour
{
    //ASSIGN TO INSTANCE OF DICTIONARYCONVERTER IN UNITY INSPECTOR
    public DialogueManager dInterface;
    public DictionaryConverter dRef;

    private bool inputReceived;
    private int inputReturn;
	private bool canContinue;




    void Awake() {
        Messenger<int>.AddListener("contBtnRtrn", compileDecision);
    }


	//CALLED BY NPCS UPON USER INTERACTION
    public IEnumerator qNPCDirectory(string caller)
    {
        
		//Handler for NPC in provided example
        if (caller == "sampleNPC_I")
        {
			//Basic template for NPC quest handler

			//Suspends player movement during conversation
			Messenger<bool>.Broadcast ("canMove_Update", false);

			//Begins quest coroutine
            StartCoroutine(sampleQuest(wasSuccessful => {
				
				Messenger<bool>.Broadcast("canMove_Update", true);

                if (!wasSuccessful) {
                    Debug.Log("Failed quest; prompting restart.");
                    //**HANDLER FOR QUEST FAILURE**
               	} else {
                    Debug.Log("Quest succesfully completed; returning to start");
                    //PROCEED
                }
            }));
        }
        else
        {
            //ADD PATHS FOR ADDITIONAL NPCs
        }

        yield return null;
    }



    private void compileDecision(int btnSelection)
    {
        inputReceived = true;
        inputReturn = btnSelection;
    }


	//Sample quest
    private IEnumerator sampleQuest(Action<bool> Accomplished)
    {
		//Generic conversational quest

		//Retrieving dialogue info
		Dictionary<string, string> dRef_NPC = dRef.sample_sDict;
		Dictionary<string, string> dRef_User = dRef.sample_rDict;
        Dictionary<string, string[]> dRef_btnTxt = dRef.btnText_Sample;

        Debug.Log("Engaged Sample NPC");
        string NPCname = "NPC_I";
        string branchPath = "smplDialogue";
        string[] contOptions = new string[2];
        bool qFailed = false;

        while (true)
		{
            string[] newOration = dInterface.getOration(branchPath, dRef_NPC);
            contOptions = dRef_btnTxt[branchPath];
            dInterface.outputDialogue(NPCname, newOration, contOptions);

            inputReceived = false;
            yield return new WaitUntil(() => inputReceived);

            int qBranch = inputReturn;
            if (qBranch == 2)
            {
                if (!qFailed)
                {
                    Debug.Log("Quest Complete");
                    dInterface.EndDialogue();
                    //qStatus.qPhase [0] = 2;

                    Accomplished(true);
                    yield break;
                }
                else
                {
                    Debug.Log("Quest Failed");
                    dInterface.EndDialogue();
                    //qStatus.qPhase [0] = 2;

                    Accomplished(false);
                    yield break;
                }
            }
            branchPath = branchPath + qBranch.ToString();
            canContinue = false;
            StartCoroutine(dInterface.displayResponse(dRef_User, branchPath, conGate => (canContinue = conGate)));
            yield return new WaitUntil(() => canContinue);
        }
    }
}
