using System.Collections.Generic;

public class AudioManager : SingletonMono<AudioManager>
{
    private SaveLoadManager _saveLoadManager;

    private Dictionary<SoundType, Sound> _sounds;
    private Dictionary<MusicType, Music> _musics;

    public void Construct(SaveLoadManager saveLoadManager) =>
        _saveLoadManager = saveLoadManager;

    protected override void InitSingleInstance()
    {
        base.InitSingleInstance();
        
        InitSounds();
        InitMusic();
        
        LoadInitState();
    }
    
    private void InitSounds()
    {
        _sounds = new Dictionary<SoundType, Sound>();
        Sound[] soundObjects = GetComponentsInChildren<Sound>();

        foreach (Sound sound in soundObjects)
        {
            sound.Initialize();
            _sounds[sound.Type] = sound;
        }
    }

    private void InitMusic()
    {
        _musics = new Dictionary<MusicType, Music>();
        Music[] musicObjects = GetComponentsInChildren<Music>();

        foreach (Music music in musicObjects)
        {
            music.Initialize();
            _musics[music.Type] = music;
        }
    }

    private void LoadInitState()
    {
        if (_saveLoadManager.PlayerData.MusicIsMuted)
            MuteMusic();
        else
            UnmuteMusic();
        
        if (_saveLoadManager.PlayerData.SoundIsMuted)
            MuteSounds();
        else
            UnmuteSounds();
    }

    public void PlaySound(SoundType soundType) => _sounds[soundType].Play();
    public void PlayMusic(MusicType musicType) => _musics[musicType].Play();
    public void StopMusic(MusicType musicType) => _musics[musicType].Stop();

    public void StopAllMusic()
    {
        foreach (Music music in _musics.Values)
            music.Stop();
    }

    public void MuteSounds()
    {
        foreach (Sound sound in _sounds.Values)
            sound.Mute();
    }

    public void MuteMusic()
    {
        foreach (Music music in _musics.Values)
            music.Mute();
    }
    
    public void UnmuteSounds()
    {
        foreach (Sound sound in _sounds.Values)
            sound.Unmute();
    }
    
    public void UnmuteMusic()
    {
        foreach (Music music in _musics.Values)
            music.Unmute();
    }
}