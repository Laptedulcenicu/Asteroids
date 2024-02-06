using UnityEngine;

namespace Modules.Common
{
    public interface IAssetProviderService
    {
        GameObject Instantiate(string path, Vector3 at);
        GameObject Instantiate(string path);
    }
}