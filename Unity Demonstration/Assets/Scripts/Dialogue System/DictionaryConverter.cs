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
        
    }

	private Dictionary<string, string> compileDictionary(string[] tags, string[] text){
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

