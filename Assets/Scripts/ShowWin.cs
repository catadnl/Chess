using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowWin: Singleton<ShowWin>{

	public void showPrettyMessage (string winner) {
		GameObject myText = GameObject.Find("WinText");
		Text textComponent = myText.GetComponent<Text>();
		textComponent.enabled = true;
		textComponent.text = winner + "Win !"; 
		StartCoroutine(sleepFor(7.0F));
	}
	public IEnumerator sleepFor(float nrSec){
		yield return new WaitForSeconds(nrSec);
		GameObject myText = GameObject.Find("WinText");
		Text textComponent = myText.GetComponent<Text>();
		textComponent.enabled = false;
	}

}
