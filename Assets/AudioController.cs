//Chris Wratt 2016
//Audio fader control

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {

	// This is access to whole mixer
	public AudioMixer masterMixer;

	void Awake() {
	}

	//Fade in or out function. This creates and destroys new routines...
	private IEnumerator VolumeFade(string loopName, float endValue, float length) {

		float fadeStart = Time.time;
		float timeSinceStart = 0.0f;
		float myVolume;
		float startValue;

		//set starting value to current fader position
		masterMixer.GetFloat(loopName, out startValue);

		//delay
		yield return new WaitForSeconds (0.05f);

		//lerp fade loop with delay
		while(timeSinceStart < length){
			timeSinceStart = Mathf.Abs (Time.time - fadeStart);

			yield return new WaitForSeconds (0.05f);

			myVolume = Mathf.Lerp (startValue, endValue, timeSinceStart / length);
			masterMixer.SetFloat(loopName, myVolume);
		}
		yield break;
	}
}
