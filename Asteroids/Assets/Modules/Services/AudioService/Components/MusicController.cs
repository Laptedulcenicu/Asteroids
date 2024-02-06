using UnityEngine;

namespace Modules.Services.AudioService
{
    public class MusicController : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private MusicPlaylist musicPlaylists;

        private bool IsPlaylistAvailable => musicPlaylists is { HasTracks: true };

        private void Awake()
        {
            DontDestroyOnLoad(this);
            audioSource.playOnAwake = false;
        }

        private void Update()
        {
            if (!audioSource.isPlaying)
            {
                PlayNextTrack();
            }
        }

        private void PlayNextTrack()
        {
            if (!IsPlaylistAvailable)
            {
                return;
            }

            audioSource.clip = musicPlaylists.GetNextTrack();
            audioSource.Play();
        }

        private void Play()
        {
            if (!IsPlaylistAvailable)
            {
                return;
            }

            audioSource.clip = musicPlaylists.GetNextTrack();
            audioSource.Play();
        }

        private void Stop()
        {
            audioSource.Stop();
        }

        public void PlayMusic()
        {
            Stop();
            Play();
        }

        public void Pause()
        {
            audioSource.Pause();
        }

        public void UnPause()
        {
            audioSource.UnPause();
        }
    }
}