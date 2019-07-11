using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class contBtnEvents : MonoBehaviour {

	public void contButtonHandler(int choice){
		//Debug.Log ("Button Pressed");
		Messenger<int>.Broadcast ("contBtnRtrn", choice);
	}
}
