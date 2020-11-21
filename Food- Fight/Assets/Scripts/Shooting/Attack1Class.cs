public class Attack1Class
{  
    public ShootingPatternGenerator gen;

    public Attack1Class(ShootingPatternGenerator gen)
    {
        this.gen = gen;
    }

    public void SetInitialParams()
    {
        gen.numberOfProjectiles = 10;
        gen.firingStarRadius = 1f;
        gen.spawnAngleOffset = 90f;
        gen.inBetweenShotsDelay = 0.1f;
        gen.shotsTimeDelta = 0.5f;
        gen.numberBulletsLimit = 5;

        gen.objectRotatesIndepedentlyAlongZ = false;
        gen.dynamicOffset = false;
        gen.loopFiring = false;
        gen.reverseOrderFiring = false;
        gen.limitBullets = true;
        
    }

    private void Sleep(int amt)
    {
        System.Diagnostics.Stopwatch stopwatch = System.Diagnostics.Stopwatch.StartNew();
        while (true)
        {
            if (stopwatch.ElapsedMilliseconds >= amt)
            {
                break;
            }
        }
        
    }

    public void Attack1()
    {
        gen.reverseOrderFiring = false;
        gen.spawnAngleOffset = 90f;
        gen.Shoot();
    }

    public void Attack2()
    {
        gen.spawnAngleOffset = -54f;
        gen.reverseOrderFiring = true;
        gen.Shoot();
    }

}
