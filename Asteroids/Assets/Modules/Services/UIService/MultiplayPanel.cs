using System;
using Modules.Common;
using UnityEngine;
using UnityEngine.UI;

public class MultiplayPanel : MonoBehaviour, IMultiplayPanel
{
    public event Action Onx1Multiplay = delegate { };
    public event Action Onx3Multiplay = delegate { };
    public event Action Onx5Multiplay = delegate { };
    
    [SerializeField] private Button x1Button;
    [SerializeField] private Button x3Button;
    [SerializeField] private Button x5Button;
    
    private void Awake()
    {
        x1Button.onClick.AddListener(() => Onx1Multiplay?.Invoke());
        x3Button.onClick.AddListener(() => Onx3Multiplay?.Invoke());
        x5Button.onClick.AddListener(() => Onx5Multiplay?.Invoke());
    }
}
