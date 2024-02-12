using System.Collections;
using UnityEngine;

namespace Scenes
{
    public class LaserShooting:MonoBehaviour
    {
        [Header("Laser settings")] 
        [SerializeField] private GameObject startLaser;
        public float textureScrollSpeed = 8f; //How fast the texture scrolls along the beam
        public float textureLengthScale = 3; //Length of the beam texture
        [SerializeField] private Transform laserSpawnPoint = null;
        [SerializeField] private LayerMask whatIsEnemy;
        [SerializeField] private LineRenderer lineRenderer = null;
        [SerializeField] private float laserDistance;
        [SerializeField] private float laserDelay;
        [SerializeField] private float LaserRollbackTime;
        [SerializeField] private int LaserShots;
        [SerializeField] private int CurrentLaserShots;
        [SerializeField] private int LaserReloadTime;
        
        private Coroutine showLaser = null, restoreLaserCharge = null;

        private float nextShotTime = 0.0f, nextLaserShotTime = 0.0f;

        private float restoreSeconds;
        private int laserCharges, maximumCharges;

        private const float ONE_SECOND = 1.0f, ZERO_SECONDS = 0.0f;
        private const float STOPPED_TIME = 0.0f;
        private const int NONE_OF_CHARGES = 0;

        private void Start()
        {
            SetupLaserCharges(LaserShots, CurrentLaserShots);
        }

        public int LaserCharges
        {
            get => laserCharges;
            set
            {
                laserCharges = value;

                if (laserCharges >= maximumCharges)
                {
                    laserCharges = maximumCharges;
                }
            }
        }

        private bool isActive;
        private bool isworking;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                if (Time.time > nextLaserShotTime && LaserCharges > NONE_OF_CHARGES && Time.timeScale != STOPPED_TIME)
                {
                    isActive = true;
                    isworking = true;
                    nextLaserShotTime = Time.time + LaserRollbackTime;
                    LaserCharges--;
                }
            }

            if (isActive)
            {
                ShootLaser(  lineRenderer, whatIsEnemy, laserDistance, laserDelay);
            }
            
            CheckLaserCharges( LaserReloadTime);
        }

        public void ShootLaser( LineRenderer line, LayerMask enemy, float laserDistance, float showLaserDelay)
        {
            
                float firstIndexZOffset = -0.01f;

                RaycastHit2D[] raycastHits = Physics2D.RaycastAll(laserSpawnPoint.position, laserSpawnPoint.up, laserDistance, enemy.value);

              

                if (isworking)
                {
                    if (showLaser != null)
                    {
                        StopCoroutine(showLaser);
                
                    }
                    isworking = false;
                    showLaser = StartCoroutine(ShowAndDisableLaser(line, showLaserDelay));
                }
            
                
                line.positionCount = 2;
                Vector3 lastPos = laserSpawnPoint.up * laserDistance;
                line.SetPosition(0, new Vector3(laserSpawnPoint.position.x, laserSpawnPoint.position.y, firstIndexZOffset));
                line.SetPosition(1, lastPos);
                startLaser.SetActive(true);
                startLaser.transform.LookAt(lastPos);
                line.sharedMaterial.mainTextureScale = new Vector2(laserDistance / textureLengthScale, 1);
                line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
                
                foreach (RaycastHit2D hit in raycastHits)
                {
                    UIController.OnKill?.Invoke();
                    Destroy(hit.collider.gameObject);
                }

        
           
        }
        
        public float LaserRestoreTime
        {
            get => restoreSeconds;
        }
        
        public void SetupLaserCharges(int maxCharges, int currentCharges)
        {
            maximumCharges = maxCharges;
            LaserCharges = currentCharges;
        }
        
        public void CheckLaserCharges( float restoreTime)
        {
            if (laserCharges < maximumCharges && restoreLaserCharge == null)
            {
                restoreLaserCharge = StartCoroutine(RestoreLaserCharge(restoreTime));
            }
        }
        
        private IEnumerator ShowAndDisableLaser(LineRenderer line, float showLaserDelay)
        {
            line.enabled = true;
            Debug.Log("Booo");
            Debug.Log(showLaserDelay);
            yield return new WaitForSeconds(showLaserDelay);
            isActive = false;
            Debug.Log("ShowAndDisableLaser");
            startLaser.SetActive(false);
            line.enabled = false;
        }

        private IEnumerator RestoreLaserCharge(float restoreTime)
        {
            restoreSeconds = restoreTime;

            while (restoreSeconds >= ZERO_SECONDS)
            {
                restoreSeconds -= ONE_SECOND * Time.deltaTime;

                yield return new WaitForEndOfFrame();
            }

            LaserCharges++;

            restoreLaserCharge = null;
        }
    }
}