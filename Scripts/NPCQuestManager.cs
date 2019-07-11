using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class NPCQuestManager : MonoBehaviour
{
    //ASSIGN TO INSTANCE OF DICTIONARYCONVERTER IN UNITY INSPECTOR
    public DialogueManager dInterface;
    public DictionaryConverter dRef;

    private bool inputReceived;
    private int inputReturn;
	private bool canContinue;




    void Awake()
    {
        Messenger<int>.AddListener("contBtnRtrn", compileDecision);
    }



    public IEnumerator qNPCDirectory(string caller)
    {

        if (caller == "sampleNPC_I")
        {

            StartCoroutine(sampleQuest(wasSuccessful =>
            {
                if (!wasSuccessful)
                {
                    Debug.Log("Failed quest; prompting restart.");
                        //INSERT HANDLER FOR QUEST FAILURE
                    }
                else
                {
                    Debug.Log("Quest succesfully completed; returning to start");
                        
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


    private IEnumerator sampleQuest(Action<bool> Accomplished)
    {
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
			Debug.Log ("0");
            string[] newOration = dInterface.getOration(branchPath, dRef_NPC);
			Debug.Log ("1");
            contOptions = dRef_btnTxt[branchPath];
			Debug.Log ("2");
            dInterface.outputDialogue(NPCname, newOration, contOptions);
			Debug.Log ("3");
            inputReceived = false;
            yield return new WaitUntil(() => inputReceived);

            int qBranch = inputReturn;
            if (qBranch == 2)
            {
                if (!qFailed)
                {
                    Debug.Log("Quest Complete");
                    dInterface.EndDialogue();

                    Accomplished(true);
                    yield break;
                }
                else
                {
                    Debug.Log("Quest Failed");
                    dInterface.EndDialogue();

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
