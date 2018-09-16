using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/// <summary>
/// This class is a singleton that travels between scenes as a persistent GameObject. This allows us to continue playing audio without cutting off on scene load.
/// Audio can be played using functions on this class. You should play music from this object as well as any generic 2D One Shots that don't require a positional component.
/// </summary>
[DisallowMultipleComponent]                         //Prevent multiple copies of this component to be added to the same gameObject

public class SoundManager : MonoBehaviour {

    [Header("Sound Settings")]
    [Tooltip("AudioSource that plays the 2D sound effects")]
    public AudioSource soundSource;		//Reference to the sound effects audio source
    [Tooltip("AudioSource that plays the music")]
    public AudioSource musicSource;		//Reference to the music audio source

    #region Singleton Pattern - Lazy Load
    private static SoundManager instance = null;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
                if (instance == null)
                {
                    GameObject go = new GameObject(typeof(SoundManager).ToString());
                    instance = go.AddComponent<SoundManager>();
                }
            }

            return instance;
        }
    }
    #endregion

    // Use this for initialization
    void Awake () {
        #region Singleton Pattern - Create/Destroy
        if(Instance != this)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += HandleNewSceneLoaded;
        }
        #endregion
    }

    // this function gets called each time a new scene is loaded
    private void HandleNewSceneLoaded(Scene scene, LoadSceneMode mode)
    {

    }

	public void PlayMusic(AudioClip clip){
		musicSource.clip = clip;
		musicSource.Play();
	}

	public void StopMusic(AudioClip clip){
		musicSource.Stop();
	}

	public void RandomizeSFX (params AudioClip[] clips)
	{
		int randomIndex = Random.Range(0,clips.Length);
		soundSource.clip = clips[randomIndex];
		soundSource.Play();
	}

	public void PlaySound2DOneShot(AudioClip newClip, float volume, float minPitch, float maxPitch)
    {
        //TODO object pooling here
        GameObject audioObject = Instantiate(gameObject);
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        audioSource.clip = newClip;
        audioSource.spatialBlend = 0;
        audioSource.volume = volume;

        RandomizePitch(minPitch, maxPitch, audioSource);

        audioSource.Play();
        StartCoroutine(DisableSoundOnEnd(audioSource));
    }

    private static void RandomizePitch(float minPitch, float maxPitch, AudioSource audioSource)
    {
        float randomPitch = Random.Range(minPitch, maxPitch);
        audioSource.pitch = randomPitch;
    }

    public void PlaySound3DOneShot(AudioClip newClip, float volume, float minPitch, float maxPitch, Vector3 pointToPlay){
        //TODO object pooling here
        GameObject audioObject = Instantiate(gameObject);
        AudioSource audioSource = audioObject.AddComponent<AudioSource>();

        audioObject.transform.position = pointToPlay;

        audioSource.spatialBlend = 1;
        audioSource.clip = newClip;
		audioSource.volume = volume;

        RandomizePitch(minPitch, maxPitch, audioSource);

		audioSource.Play();
		StartCoroutine(DisableSoundOnEnd(audioSource));
	}

	IEnumerator DisableSoundOnEnd(AudioSource aSource){
        if (aSource.clip == null)
        {
            Destroy(aSource.gameObject);
        }
        else
        {
            yield return new WaitForSeconds(aSource.clip.length);
            Destroy(aSource.gameObject);
        }
	}
}
