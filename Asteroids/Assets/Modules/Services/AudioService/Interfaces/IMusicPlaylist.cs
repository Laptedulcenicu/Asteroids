using UnityEngine;

namespace Modules.Services.AudioService
{
    public interface IMusicPlaylist
    {
        bool HasTracks { get; }
        AudioClip GetNextTrack();
    }
}