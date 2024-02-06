using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

namespace Modules.Services.AudioService
{
    [Serializable]
    public class MusicPlaylist : IMusicPlaylist
    {
        [SerializeField] private List<AudioClip> tracks;

        private int _current;

        public bool HasTracks => tracks.Count > 0;

        public AudioClip GetNextTrack()
        {
            if (_current >= tracks.Count)
            {
                _current = 0;
            }

            var clip = tracks[_current];
            _current++;
            return clip;
        }


        public void ShufflePlayList()
        {
            var random = new Random();
            var orderedTracks = tracks.OrderBy(a => random.Next());
            tracks.Clear();
            tracks.AddRange(orderedTracks);
        }
    }
}