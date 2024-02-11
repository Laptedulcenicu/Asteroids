using System;
using System.Collections;
using System.Collections.Generic;
using Scenes;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
   [SerializeField] private ParticleSystem shootParticle1;
   [SerializeField] private ParticleSystem shootParticle2;
   [SerializeField] private float speed;
   [SerializeField] private int index;
   [SerializeField] private GameObject bulletPrefab;
   [SerializeField] private GameObject leftGun;
   [SerializeField] private GameObject rightGun;
   [SerializeField] public float shootingTimer;
   private float m_Timer;

   private void Start()
   {
      m_Timer = shootingTimer;
   }

   private void Update()
   {
      m_Timer += Time.deltaTime;
      if (Input.GetKey(KeyCode.Space))
      {
         if (m_Timer >= shootingTimer)
         {
            m_Timer = 0;
            shootParticle1.Play();
            shootParticle2.Play();
            GameObject bulletRight = Instantiate(bulletPrefab, rightGun.transform.position, transform.rotation);
            GameObject bulletLeft = Instantiate(bulletPrefab, leftGun.transform.position, transform.rotation);
            bulletRight.transform.GetChild(index).gameObject.SetActive(true);
            bulletLeft.transform.GetChild(index).gameObject.SetActive(true);
            bulletRight.GetComponent<Bullet>().speed = speed;
            bulletLeft.GetComponent<Bullet>().speed = speed;
         }
      }
   }
}
