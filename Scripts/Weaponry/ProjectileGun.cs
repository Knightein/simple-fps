using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ProjectileGun : MonoBehaviour
{
    [Header("Projectile Gun Settings")]
    public GameObject projectile;
    [Tooltip("How much force is applied to the projectile when it is fired")]
    public float shootForce;
    public float timeBetweenShooting, spread, reloadTime;
    public int magazineSize, bulletsPerTap; // bulletsPerTap is how many projectiles are fired per click
    [Tooltip("True = full-auto, false = semi-auto")]
    public bool allowButtonHold;
    
    int _bulletsLeft, _bulletsShot;
    
    bool _shooting, _readyToShoot, _reloading;
    
    [Header("Visual Settings")]
    public TextMeshProUGUI ammoDisplay;
    
    [Header("Camera Settings")]
    public Camera cam;
    public Transform attackPoint;
    
    [Header("Keybinds")]
    public KeyCode shootKey = KeyCode.Mouse0;
    public KeyCode reloadKey = KeyCode.R;

    private void Awake()
    {
        _bulletsLeft = magazineSize;
        _readyToShoot = true;
    }
    
    void FixedUpdate()
    {
        WeaponInput();
        if (ammoDisplay != null && !_reloading)
        { ammoDisplay.SetText("Ammo: " + _bulletsLeft / bulletsPerTap + " / " + magazineSize / bulletsPerTap); }
    }
    
    /// <summary>
    /// This method handles the input for the weapon.
    /// </summary>
    private void WeaponInput()
    {
        if (allowButtonHold)
        { _shooting = Input.GetKey(shootKey); }
        else
        { _shooting = Input.GetKeyDown(shootKey); }
        
        if (Input.GetKeyDown(reloadKey) && _bulletsLeft < magazineSize && !_reloading)
        { Reload(); }
        // This will auto reload when the player runs out of bullets and tries to fire the weapon
        if (_readyToShoot && _shooting && !_reloading && _bulletsLeft <= 0)
        { Reload(); }
        
        if (_readyToShoot && _shooting && !_reloading && _bulletsLeft > 0)
        { 
            _bulletsShot = 0;
            Shoot();
        }
    }
    
    /// <summary>
    /// This method handles the shooting of the weapon.
    /// </summary>
    private void Shoot()
    {
        _readyToShoot = false;
        
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;
        
        Vector3 targetPoint;
        
        if (Physics.Raycast(ray, out hit))
        { targetPoint = hit.point; }
        else { targetPoint = ray.GetPoint(75); } // Point that is far away from the player

        var position = attackPoint.position;
        Vector3 directionWithoutSpread = targetPoint - position;
        
        // Calculate spread
        float x = UnityEngine.Random.Range(-spread, spread);
        float y = UnityEngine.Random.Range(-spread, spread);
        
        Vector3 directionWithSpread = directionWithoutSpread + new Vector3(x, y, 0);
        
        // Instantiate projectile
        GameObject currentProjectile = Instantiate(projectile, position, Quaternion.identity);
        currentProjectile.transform.forward = directionWithSpread.normalized; // Forward direction
        
        currentProjectile.GetComponent<Rigidbody>()
            .AddForce(directionWithSpread.normalized * shootForce, ForceMode.Impulse);
        
        _bulletsLeft--;
        _bulletsShot++;
        
        Invoke(nameof(ResetShot), timeBetweenShooting);
        DespawnTimer(currentProjectile);
    }
    
    /// <summary>
    /// This method resets the weapon to be able to shoot again.
    /// </summary>
    private void ResetShot()
    {
        _readyToShoot = true;
    }
    
    /// <summary>
    /// This method handles the reloading of the weapon.
    /// </summary>
    private void Reload()
    {
        _reloading = true;
        Invoke(nameof(ReloadFinished), reloadTime);
        ammoDisplay.SetText("Reloading...");
    }
    
    /// <summary>
    /// This method resets the weapon to be able to shoot again after reloading.
    /// </summary>
    private void ReloadFinished()
    {
        _bulletsLeft = magazineSize;
        _reloading = false;
    }
    
    /// <summary>
    /// This method forces the destruction of the projectile after 5 seconds, should it not hit anything that would
    /// destroy it normally.
    /// </summary>
    /// <param name="spawnedProjectile">The projectile to destroy.</param>
    private void DespawnTimer(GameObject spawnedProjectile)
    {
        Destroy(spawnedProjectile, 5f);
    }
}
