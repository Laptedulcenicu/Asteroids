using System;
using Scenes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public static Action OnKill;
    [SerializeField] private PlayerController PlayerController;
    [SerializeField] private Image activeCheck;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private TextMeshProUGUI rotationText;
    [SerializeField] private TextMeshProUGUI positionText;
    [SerializeField] private TextMeshProUGUI laserCountText;

    private int kills = 0;
    private LaserShooting LaserShooting;
    private void Start()
    {
        LaserShooting = PlayerController.GetComponent<LaserShooting>();
        scoreText.text = kills.ToString();
        laserCountText.text = 0.ToString();
        OnKill+= Kill;
    }

    private void Kill()
    {
        kills++;
        scoreText.text = kills.ToString();
    }

    private void Update()
    {
        activeCheck.enabled = LaserShooting.LaserCharges > 0;
        laserCountText.text = LaserShooting.LaserCharges.ToString();
        speedText.text = (PlayerController.Velocity.magnitude*10000).ToString("0");
        rotationText.text = "Z:" + PlayerController.transform.eulerAngles.z.ToString("00");
        positionText.text = "X:" + PlayerController.transform.position.x.ToString("00")+" "+"Y:" + PlayerController.transform.position.y.ToString("00");
    }
}
