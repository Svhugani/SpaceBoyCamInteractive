using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace HomeomorphicGames
{
    public class AudioManager : AbstractManager
    {
        [Header("AUDIO SOURCES")] 
        [SerializeField] private AudioSource musicSource;
        [SerializeField] private AudioSource[] effectSources;

        [Header("AUDIO CLIPS")]
        [SerializeField] private AudioClip[] musicClips;

        [Header("SETTINGS")]
        [SerializeField][Range(0, 1)] private float baseMusicVolume = .8f;
        [SerializeField] private float musicFadeIn = 1f;
        [SerializeField] private float musicFadeOut = 1f;

        private bool _isBackgroundMusicOn = false;
        private int _trackId = 0;
        private Coroutine _backgroundMusicRoutine;

        public override async Task Prepare()
        {
            PlayBackgroundMusic();
            await Task.Yield();
        }

        void NextTrack(int step)
        {
            _trackId = (_trackId + step) % musicClips.Length;
            musicSource.clip = musicClips[_trackId];
            musicSource.Play();
        }

        public void PlayBackgroundMusic()
        {
            if(!_isBackgroundMusicOn)
            {
                _isBackgroundMusicOn = true;
                _backgroundMusicRoutine = StartCoroutine(MusicPlaylist());
            }

        }

        public void StopBackgroundMusic()
        {
            _isBackgroundMusicOn = false;
        }

        IEnumerator MusicPlaylist()
        {
            float timer = 0;
            float ratio;
            musicSource.volume = 0;
            NextTrack(0);
            yield return null;

            while (timer < musicFadeIn)
            {
                ratio = timer / musicFadeIn;
                musicSource.volume = baseMusicVolume * ratio;
                timer += Time.deltaTime;
                yield return null;
            }

            musicSource.volume = baseMusicVolume;

            while (_isBackgroundMusicOn)
            {
                if (!musicSource.isPlaying) NextTrack(1);
                yield return null;
            }

            timer = 0;

            while (timer < musicFadeOut)
            {
                ratio = 1 - timer / musicFadeOut;
                musicSource.volume = baseMusicVolume * ratio;
                timer += Time.deltaTime;
                yield return null;
            }
            musicSource.volume = 0;
            musicSource.Stop(); 
        }

    }
}
