using DG.Tweening;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

[System.Serializable]
public class Sound
{
    public TypeOfSound Name;
    public AudioClip[] Clip;

    [Range (0f, 1f)]
    public float Volume = 0.7f;
    [Range(0.5f, 1.5f)]
    public float Pitch = 1f;

    [Range(0f, 0.5f)]
    public float RandomVolume = 0.1f;
    [Range(0f, 0.5f)]
    public float RandomPitch = 0.1f;

    public bool Loop = false;

    [HideInInspector] public AudioSource Source;

    public void SetSource(AudioSource audioSource)
    {
        Source = audioSource;
        Source.loop = Loop;
    }

    public void Play()
    {
        Source.volume = Volume * (1 + Random.Range(-RandomVolume / 2f, RandomVolume / 2f));
        Source.pitch = Pitch * (1 + Random.Range(-RandomPitch / 2f, RandomPitch / 2f));
        Source.Play();
    }

    public void Stop()
    {
        Source.Stop();
    }
}
public class AudioManager : MonoBehaviour, IAudioManager
{
    public static IAudioManager Instance;

    [SerializeField] private AudioMixerGroup SoundMixer;
    [SerializeField] private AudioMixerGroup MusicMixer;
    [SerializeField] private Sound[] SoundsList;
    [SerializeField] private Sound[] MusicList;

    private static AudioSource _musicSource;
    private static TypeOfSound _curMusic;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        for (var i = 0; i < SoundsList.Length; i++)
        {
            var _go = new GameObject("Sound_" + i + "_" + SoundsList[i].Name);
            _go.transform.SetParent(transform);
            
            var source = _go.AddComponent<AudioSource>();
            source.outputAudioMixerGroup = SoundMixer;
            SoundsList[i].SetSource(source);
        }
        
        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.outputAudioMixerGroup = MusicMixer;
    }

    private void Start()
    {
        Settings.OnSoundVolumeChange += ChangeSoundsVolume;
        Settings.OnMusicVolumeChange += ChangeMusicVolume;
        ChangeSoundsVolume();
        ChangeMusicVolume();
    }

    public void PlaySound(TypeOfSound soundType)
    {
        for (var i = 0; i < SoundsList.Length; i++)
        {
            if (SoundsList[i].Name == soundType)
            {
                SoundsList[i].Source.clip = SoundsList[i].Clip[Random.Range(0, SoundsList[i].Clip.Length)];
                SoundsList[i].Play();
                return;
            }
        }

        //No sound with _name.
        Debug.LogWarning("AudioManager: Sound not found in list: " + soundType);
    }
    
    public void StopSound(TypeOfSound soundType)
    {
        for (int i = 0; i < SoundsList.Length; i++)
        {
            if (SoundsList[i].Name == soundType)
            {
                SoundsList[i].Stop();
                return;
            }
        }

        //No sound with _name.
        Debug.LogWarning("AudioManager: Sound not found in list: " + soundType);
    }

    public void PlayMusic(TypeOfSound musicType)
    {
        if (_curMusic == musicType) return;
        _curMusic = musicType;
        
        for (var i = 0; i < MusicList.Length; i++)
        {
            if (MusicList[i].Name == musicType) {
                _musicSource.DOFade(0, 1f).SetUpdate(true).OnComplete(() => {
                    MusicList[i].SetSource(_musicSource);
                    _musicSource.clip = MusicList[i].Clip[Random.Range(0, MusicList[i].Clip.Length)];
                    MusicList[i].Play();
                    _musicSource.DOFade(0, 1f).From().SetUpdate(true);
                });
                return;
            }
        }
    }
    public void StopMusic()
    {
        _musicSource.DOFade(0, 0.3f);
        _curMusic = default;
    }
    
    private void ChangeSoundsVolume()
    {
        SoundMixer.audioMixer.SetFloat(SoundMixer.name, Mathf.Log10(Mathf.Clamp(Settings.SoundsVolume, 0.0001f, 1)) * 20);
    }
    
    private void ChangeMusicVolume()
    {
        MusicMixer.audioMixer.SetFloat(MusicMixer.name, Mathf.Log10(Mathf.Clamp(Settings.MusicVolume, 0.0001f, 1)) * 20);
    }
}

public enum TypeOfSound { 
    Explosion, 
    BulletHit, 
    CoinsDrop, 
    CoinsAttraction, 
    Music, 
    MonsterShooting, 
    PlayerFall, 
    GameOver, 
    LowHp, 
    PlayerHurt,
    MenuMusic,
    PartyPoppers,
    PlayerFly,
    FallPlank,
    PressButton,
    LevelComplete, 
    WinPanel,
    LevelHolder,
    Counting,
    CoinScale,
    ClockScale,
    UpgradeButtonActive,
    UpgradeButtonInactive,
    StarButton
}
