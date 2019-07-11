using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DictionaryConverter : MonoBehaviour
{
    public Dialogue dialogue;

	public Dictionary<string, string> sample_sDict {
		get;
		set;
    }
    public Dictionary<string, string> sample_rDict {
		get;
		set;
    }
    public Dictionary<string, string[]> btnText_Sample {
		get;
		set;
    }
    /*public Dictionary<string, string> sentences_Siebel = new Dictionary<string, string>();
    public Dictionary<string, string> sentences_Frosch = new Dictionary<string, string>();
	public Dictionary<string, string> sentences_Altmayer = new Dictionary<string, string>();
	public Dictionary<string, string> sentences_Brawler = new Dictionary<string, string>();
    public Dictionary<string, string> responses_Siebel = new Dictionary<string, string>();
    public Dictionary<string, string> responses_Frosch = new Dictionary<string, string>();
	public Dictionary<string, string> responses_Altmayer = new Dictionary<string, string>();
	public Dictionary<string, string> responses_Brawler = new Dictionary<string, string>();
	public Dictionary<string, string[]> btnText_Siebel = new Dictionary<string, string[]>();
	public Dictionary<string, string[]> btnText_Frosch = new Dictionary<string, string[]>();*/


    void Awake()
    {
        //Provided example
        Dialogue sampleDialogue = JsonUtility.FromJson<Dialogue>(File.ReadAllText(Application.streamingAssetsPath + "/SampleDialogue.json"));
        //Retrieving sentence information (dictionary tags and values)
        string[] sTags_Sample = sampleDialogue.tags_NPC;
        string[] sentences_Sample = sampleDialogue.Sentences;
        //Retrieving response information (dictionary tags and values)
        string[] rTags_Sample = sampleDialogue.tags_Usr;
        string[] responses_Sample = sampleDialogue.Responses;
        //Retrieving interaction button information (dictionary tags and values)
        string[] bTags_Sample = sampleDialogue.tags_bText;
        string[] buttonTitle_Sample = sampleDialogue.btnText;

        //Constructing Unity dictionaries with information from json file above

        //Compiling dictionary for sentences
        sample_sDict = compileDictionary(sTags_Sample, sentences_Sample);
        //Compiling dictionary for responses
        sample_rDict = compileDictionary(rTags_Sample, responses_Sample);
        //Compiling dictionary for button entries
        btnText_Sample = compileBtnDictionary(bTags_Sample, buttonTitle_Sample);


        /*Dialogue siebelDialogue = JsonUtility.FromJson<Dialogue>(File.ReadAllText(Application.streamingAssetsPath + "/SiebelDialogue.json"));
        string[] dSTags_Siebel = siebelDialogue.tags_NPC;
        string[] dRTags_Siebel = siebelDialogue.tags_Usr;
        string[] dSentences_Siebel = siebelDialogue.Sentences;
        string[] dResponses_Siebel = siebelDialogue.Responses;
		string[] bText_Siebel = siebelDialogue.btnText;
		string[] bTTags_Siebel = siebelDialogue.tags_bText;

        Dialogue froschDialogue = JsonUtility.FromJson<Dialogue>(File.ReadAllText(Application.streamingAssetsPath + "/FroschDialogue.json"));
        string[] dSTags_Frosch = froschDialogue.tags_NPC;
        string[] dRTags_Frosch = froschDialogue.tags_Usr;
        string[] dSentences_Frosch = froschDialogue.Sentences;
        string[] dResponses_Frosch = froschDialogue.Responses;
		string[] bText_Frosch = froschDialogue.btnText;
		string[] bTTags_Frosch = froschDialogue.tags_bText;

		Dialogue altmayerDialogue = JsonUtility.FromJson<Dialogue>(File.ReadAllText(Application.streamingAssetsPath + "/AltmayerDialogue.json"));
        string[] dSTags_Altmayer = altmayerDialogue.tags_NPC;
        string[] dRTags_Altmayer = altmayerDialogue.tags_Usr;
        string[] dSentences_Altmayer = altmayerDialogue.Sentences;
        string[] dResponses_Altmayer = altmayerDialogue.Responses;

		Dialogue brawlerDialogue = JsonUtility.FromJson<Dialogue>(File.ReadAllText(Application.streamingAssetsPath + "/AltmayerDialogue.json"));
        string[] dSTags_Brawler = brawlerDialogue.tags_NPC;
        string[] dRTags_Brawler = brawlerDialogue.tags_Usr;
        string[] dSentences_Brawler = brawlerDialogue.Sentences;
        string[] dResponses_Brawler = brawlerDialogue.Responses;*/

        /*sentences_Siebel = compileDDictionary (dSTags_Siebel, dSentences_Siebel, sentences_Siebel);
		responses_Siebel = compileDDictionary (dRTags_Siebel, dResponses_Siebel, responses_Siebel);
		btnText_Siebel = compileBtnDictionary (bTTags_Siebel, bText_Siebel, btnText_Siebel);
		sentences_Frosch = compileDDictionary (dSTags_Frosch, dSentences_Frosch, sentences_Frosch);
		responses_Frosch = compileDDictionary (dRTags_Frosch, dResponses_Frosch, responses_Frosch);
		btnText_Frosch = compileBtnDictionary (bTTags_Frosch, bText_Frosch, btnText_Frosch);
		sentences_Altmayer = compileDDictionary (dSTags_Altmayer, dSentences_Altmayer, sentences_Altmayer);
		responses_Altmayer = compileDDictionary (dRTags_Altmayer, dResponses_Altmayer, responses_Altmayer);
		sentences_Brawler = compileDDictionary (dSTags_Brawler, dSentences_Brawler, sentences_Brawler);
		responses_Brawler = compileDDictionary (dRTags_Brawler, dResponses_Brawler, responses_Brawler);*/

		//refLibrary lib = GameObject.FindGameObjectWithTag ("ManagerSystem").GetComponent<refLibrary> ();
		//lib.dialogueCanvas = gameObject;
		//lib.dDictionaryArchive = this;
    }

	private Dictionary<string, string> compileDictionary(string[] tags, string[] text){
        //Debug.Log (tags.Length);
        //Debug.Log (text.Length);
		Dictionary<string, string> DictDump = new Dictionary<string, string>();
		for (int i = 0; i < tags.Length; i++) {
			string indexedKey = tags[i];
			string indexedElem = text[i];
			DictDump.Add(indexedKey, indexedElem);
		}

		return DictDump;
	}

	private Dictionary<string, string[]> compileBtnDictionary(string[] tags, string[] text){
        Dictionary<string, string[]> DictDump = new Dictionary<string, string[]>();
		for (int i = 0; i < tags.Length; i++)
		{
			string indexedKey = tags[i];
			string[] newEntries = new string[]{ };
			try{
				string[] entries = DictDump [indexedKey];
				newEntries = new string[]{ entries [0], text [i] };

				DictDump[indexedKey] = newEntries;
			}catch(KeyNotFoundException){
				newEntries = new string[]{ text [i] };

				DictDump.Add(indexedKey, newEntries);
			}
		}

		return DictDump;
	}
}

