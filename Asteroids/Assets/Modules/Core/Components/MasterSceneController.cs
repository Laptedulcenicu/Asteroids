using Modules.Core;
using UnityEngine;

public class MasterSceneController : MonoBehaviour
{
    private void Awake()
    {
        var bootstrapper = new Bootstrapper();
        bootstrapper.Initialize();
    }
}