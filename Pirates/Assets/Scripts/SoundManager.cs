using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {

	private static SoundManager instance;

	private AudioSource musicChannel;
	private float musicVolume = 0.5f;

	public static SoundManager Instance {

		get 
		{
			if (instance == null)
			{
				GameObject sm = new GameObject("SoundManager");
				instance = sm.AddComponent<SoundManager>();
				instance.Init();
			}
			return instance;
		}
	}

	private void Init() {

		musicChannel = CreateMusicChannel();
	}

	private AudioSource CreateMusicChannel() {

		AudioSource channel = gameObject.AddComponent<AudioSource>();
		channel.playOnAwake = false;
		channel.loop = true;
		channel.volume = musicVolume;
		return channel;
	}

	public static void PlaySfx(Sfx sfx) {
		AudioSource.PlayClipAtPoint(LoadSfx(sfx), Vector3.zero);
	}

	public void PlayMusic(Music music) {

		if (musicChannel.isPlaying)
			StartCoroutine(FadeAndSwitchMusicRoutine(music));
		else
		{
			musicChannel.clip = LoadMusic(music);
			musicChannel.volume = musicVolume;
			musicChannel.Play();
		}

	}

	private IEnumerator FadeAndSwitchMusicRoutine(Music music) {

		AudioSource newMusicChannel = CreateMusicChannel();
		newMusicChannel.clip = LoadMusic(music);
		newMusicChannel.volume = 0f;
		newMusicChannel.Play();

		float elapsedTime = 0f;
		float fadeTime = 0.5f;
		while (musicChannel.volume > 0f)
		{
			musicChannel.volume = Mathf.Lerp(musicVolume, 0f, elapsedTime/fadeTime);
			newMusicChannel.volume = Mathf.Lerp(0f, musicVolume, elapsedTime/fadeTime);

			yield return null;
			elapsedTime += Time.smoothDeltaTime;
		}
		Destroy(musicChannel);
		musicChannel = newMusicChannel;
	}

	public void FadeOutMusic() {

		if (musicChannel.isPlaying)
			StartCoroutine(FadeOutMusicRoutine());
	}

	private IEnumerator FadeOutMusicRoutine() {

		float elapsedTime = 0f;
		float fadeTime = 0.5f;
		while (musicChannel.volume > 0f)
		{
			musicChannel.volume = Mathf.Lerp(musicVolume, 0f, elapsedTime/fadeTime);
			yield return null;
			elapsedTime += Time.smoothDeltaTime;
		}
		musicChannel.Stop();
	}


	private static AudioClip LoadSfx(Sfx sfx) {
		return (AudioClip) Resources.Load("Sfx/"+sfx.ToString());
	}

	private static AudioClip LoadMusic(Music music) {
		return (AudioClip) Resources.Load("Music/"+music.ToString());
	}
}
