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
        //PUBLISHING THIS QUEST PACKAGE TO QUEST MANAGER DICTIONARY:
        //questManager qMn = gameObject.GetComponent<questManager>();
        //questPackageI designatedQInstance = new questPackageI();

        //qMn.registerQBase(designatedQInstance, 1);

        //stgRef_Setup();

        Messenger<int>.AddListener("contBtnRtrn", compileDecision);
    }



    public IEnumerator qNPCDirectory(string caller)
    {
        //refLibrary lib = GameObject.FindGameObjectWithTag("ManagerSystem").GetComponent<refLibrary>();

        //Debug.Log("AIDirectory");
        /*
        playerStatusLog qStatus = updateQStatus ();
        if (qStatus.onQuest == 0) {
            //SET TO ZERO AFTER COMPLETION
            qStatus.onQuest = 1;
        }*/

        if (caller == "sampleNPC_I")
        {
            //Debug.Log ("T");
            //if (!BarStatusLog.onQ)
            //{
            //if (BarStatusLog.Tester || !BarStatusLog.q1)
            //{
            //Issue quest 1:
            //BarStatusLog.onQ = true;

            StartCoroutine(sampleQuest(wasSuccessful =>
            {
                if (!wasSuccessful)
                {
                    Debug.Log("Failed quest; prompting restart.");
                        //lib.GOCanvas.GetComponent<GameOverUI>().promptRestart();
                        //INSERT HANDLER FOR QUEST FAILURE
                    }
                else
                {
                    Debug.Log("Quest succesfully completed; returning to start");
                        //BarStatusLog.onQ = false;
                        //BarStatusLog.q1 = true;
                        //PROCEED
                    }
            }));
            //}
            //else
            //{
            //StartCoroutine(qNPCI_revisit());
            //}
            //}
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
        //refLibrary lib = GameObject.FindWithTag("ManagerSystem").GetComponent<refLibrary>();
        //DictionaryConverter dRef = //ADD REFERENCE TO
		Dictionary<string, string> dRef_NPC = dRef.sample_sDict;
		Dictionary<string, string> dRef_User = dRef.sample_rDict;
        Dictionary<string, string[]> dRef_btnTxt = dRef.btnText_Sample;

        Debug.Log("Engaged Sample NPC");
        string NPCname = "NPC_I";
        string branchPath = "smplDialogue";
        string[] contOptions = new string[2];
        bool qFailed = false;
        //Set qPhase to 1 to denote quest initiation
        //qStatus.qPhase[0] = 1;

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
