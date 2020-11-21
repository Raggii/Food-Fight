using UnityEngine;

[CreateAssetMenu(fileName = "New AttackAction", menuName = "AttackAction")]
public class AttackAction : ScriptableObject
{
    [Header("Attack Name")]
    public new string name;


    [Header("Attack Post Delay")]
    public float delay;

    
    [Header("General")]
    public int numProjs;
    public float fireRadius;
    public float spawnAngleOffset;
    public float inBetweenShootsDelta;
    public float timeDeltaShots;
    public int bulletLimit;


    [Header("Switches")]
    public bool objectRotatesIndepedentlyAlongZ;
    public bool dynamicOffset;
    public bool reverseOrderFiring;
    public bool loopFiring;
    public bool limitBullets;

    [Header("Projectile Data")]
    public GameObject projectile;
    public float timeToLive;

    [Header("Forces and Velocities (temporary)")]
    public float upwardsVelocity;
    public float sideVelocity;
    public float pullVelcoity;




}
