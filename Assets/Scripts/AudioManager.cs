using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

	public GameObject[] 		musicParts;
	public GameObject[] 		ambiences;
	public AudioClip[] 			audioClips;
	public AudioMixer			masterMixer;

	private SetLevels 			setLevels;
	private AudioSource		 	audioSource;
	private AudioSource[] 		musicAudioSources;
	private AudioSource[] 		ambienceAudioSources;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
	}
	void Start() {
		musicAudioSources = new AudioSource[4];
		for(int i = 0; i < musicAudioSources.Length; i++) {
			musicAudioSources[i] = musicParts[i].GetComponent<AudioSource>();
		}
		ambienceAudioSources = new AudioSource[2];
		for(int i = 0; i < ambienceAudioSources.Length; i++) {
			ambienceAudioSources[i] = ambiences[i].GetComponent<AudioSource>();
		}

//		PlayMusicPart1();
	}

	//Ambiences
	public void FilterAmbiencesOn() {
		StartCoroutine (VolumeFade ("AmbienceLowPass", 8000.0f, 2.0f));
		StartCoroutine (VolumeFade ("AmbienceVolume", -15.0f, 2.0f));
	}
	public void FilterAmbiencesOff() {
		StartCoroutine (VolumeFade ("AmbienceLowPass", 20000.0f, 2.0f));
		StartCoroutine (VolumeFade ("AmbienceVolume", 0f, 2.0f));
	}
	public void PlayAmbience() {
		StartCoroutine (VolumeFade ("AmbienceVolume", 0.0f, 2.0f));
		for(int i = 0; i < ambiences.Length; i++) {
			float randomStartPosition = Random.Range(0.0f, ambienceAudioSources[i].clip.length - 1.0f);
			ambienceAudioSources[i].time = randomStartPosition;
			ambienceAudioSources[i].Play();
		}
	}
	public void StopAmbience() {
		StartCoroutine (VolumeFade ("AmbienceVolume", -80.0f, 2.0f));
	}


	//Music
	public void PlayMusicPart1() {
		StartCoroutine (VolumeFade ("MusicVolume", -15.0f, 2.0f));
		musicAudioSources[0].Play();
	}
	public void StopMusicPart1() {
		masterMixer.SetFloat("MusicVolume", -80.0f);
		musicAudioSources[0].Stop();
	}
	public void PlayMusicPart2() {

		StartCoroutine (VolumeFade ("MusicVolume", -15.0f, 2.0f));
		musicAudioSources[1].Play();
	}
	public void StopMusicPart2() {
		masterMixer.SetFloat("MusicVolume", -80.0f);
		musicAudioSources [1].Stop ();
	}
	public void PlayMusicPart3() {

		StartCoroutine (VolumeFade ("MusicVolume", -15.0f, 2.0f));
		musicAudioSources[2].Play();
	}
	public void StopMusicPart3() {
		masterMixer.SetFloat("MusicVolume", -80.0f);
		musicAudioSources [2].Stop ();
	}
	public void PlayMusicPart4() {
		StartCoroutine (VolumeFade ("MusicVolume", -15.0f, 2.0f));
		musicAudioSources[3].Play();
	}
	public void StopMusicPart4() {
		masterMixer.SetFloat("MusicVolume", -80.0f);
		musicAudioSources [3].Stop ();
	}


	//Sound effects
	public void PlayCreakFX() {
		audioSource.PlayOneShot(audioClips[0], 0.5f);
	}
	public void PlayKissFX() {
		audioSource.PlayOneShot(audioClips[1], 0.5f);
	}
	public void PlayKnifeFX() {
		audioSource.PlayOneShot(audioClips[2], 0.5f);
	}
	public void PlayParcelFX() {
		audioSource.PlayOneShot(audioClips[3], 0.5f);
	}
	public void PlayPaperFX() {
		audioSource.PlayOneShot(audioClips[4], 0.5f);
	}
	public void PlayStatueUpFX() {
		audioSource.PlayOneShot(audioClips[5], 0.5f);
	}
	public void PlayStatueDownFX() {
		audioSource.PlayOneShot(audioClips[6], 0.5f);
	}
	public void PlayRustyDoorFX() {
		audioSource.PlayOneShot(audioClips[8], 0.5f);
	}

	private IEnumerator VolumeFade(string loopName, float endValue, float length) {

		float 		fadeStart = Time.time;
		float 		timeSinceStart = 0.0f;
		float 		myVolume;
		float 		startValue;

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
		//Debug.Log("Thread finished");
		yield break;
	}

}

