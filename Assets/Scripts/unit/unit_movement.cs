using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unit_movement : UnityPoolObject
{
    public Rigidbody2D rg;
    public Collider2D wallDetector, groundDetector;
    public Animator anim;
    public GameObject commander;
    public SpriteRenderer bodySpr, armSpr;
    public Transform rotor, armRoot, shootPoint;
    public float speed, jumpHeight, minCommDis, fireRate, randomFireRate = 0, x_v, spreadMax, spreadMin;
    public bool grounded, walled;
    public int bulletType, dir = 1, idInArray = -1;
    float angle = 0, fixAngle = 0, timeToFire, horizJumpAcc,speedFactor = 1;
    Collider2D lastTouchGround, lastTouchWall;
    void OnEnable()
    {
        rg.velocity = Vector2.zero;
    }
    void OnDisable()
    {
        rg.velocity = Vector2.zero;
        walled = grounded = false;
        lastTouchGround = lastTouchWall = null;
        commander = null;
        SetMoveX(0);
        Jump(3);
        MouseLook(0, false, false);
        Flip(false);
        horizJumpAcc = 0;
        angle = 0;
    }
    void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.tag == "Wall")
        {
            if (groundDetector.IsTouching(coll))
            {
                grounded = true;
                lastTouchGround = coll;
            }
            if (wallDetector.IsTouching(coll))
            {
                walled = true;
                lastTouchWall = coll;
            }
            if (walled)
            {
                if (wallDetector.IsTouching(lastTouchWall))
                {
                    if (Mathf.Sign(lastTouchWall.transform.position.x - transform.position.x) == Mathf.Sign(dir))
                        walled = true;
                    else
                        walled = false;
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.tag == "Wall")
        {
            if (walled && !wallDetector.IsTouching(lastTouchWall))
                walled = false;
            if (grounded && !groundDetector.IsTouching(lastTouchGround))
                grounded = false;
        }
    }
    void FixedUpdate()
    {
        rg.velocity = new Vector2(x_v * ((speed*speedFactor) * (1 + horizJumpAcc)), rg.velocity.y);
    }
    void Update()
    {
        anim.SetInteger("x_v", (int)rg.velocity.x);
        anim.SetInteger("y_v", (int)rg.velocity.y);
        anim.SetBool("g", grounded);

        if (grounded)
            horizJumpAcc = 0;
        if (idInArray != 0)
        {
            float dist = commander.transform.position.x - transform.position.x;
            bool farFromCommandr = (dist > minCommDis || dist < -minCommDis);
            if (farFromCommandr)
            {
                SetMoveX(1 * (int)Mathf.Sign(dist));
                if (speedFactor < 1f)
                    speedFactor += 0.2f;
                else
                    speedFactor = 1f;
            }
            else
            {
                if (speedFactor > 0f)
                    speedFactor -= 0.2f;
                else
                    speedFactor = 0;
            }
            if (walled && farFromCommandr)
            {
                Jump(4);
                x_v = 0;
            }
        }
        else
        {
            speedFactor = 1;
            if (walled)
                x_v = 0;

        }
        MouseLook(groupManager.Instance.unitCurrentLook, groupManager.Instance.unitLook, groupManager.Instance.unitShoot);
    }
    public void SetMoveX(int _x_v)
    {
        switch (_x_v)
        {
            case 1:
                {
                    x_v = 1;
                    dir = 1;
                    Flip(false);
                    return;
                }
            case -1:
                {
                    x_v = -1;
                    dir = -1;
                    Flip(true);
                    return;
                }
            case 0:
                {
                    x_v = 0;
                    return;
                }
            default:
                {
                    return;
                }
        }
    }
    public void Jump(int type)
    {
        switch (type)
        {
            case 1:
                {
                    if ((grounded))
                    {
                        grounded = false;
                        rg.velocity = new Vector2(rg.velocity.x, jumpHeight);
                        horizJumpAcc = GameSettingManager.Instance.UP.standardUnitParameters.horizJumpAcc;
                    }
                    return;
                }
            case 2:
                {
                    if ((!grounded))
                    {
                        rg.velocity = new Vector2(rg.velocity.x, -jumpHeight);
                    }
                    horizJumpAcc = 0f;
                    return;
                }
            case 3:
                {
                    horizJumpAcc = GameSettingManager.Instance.UP.standardUnitParameters.horizJumpAcc;
                    return;
                }
            case 4:
                {
                    if (grounded)
                    {
                        grounded = false;
                        rg.velocity = new Vector2(rg.velocity.x, jumpHeight);
                        horizJumpAcc = 0;
                    }
                    return;
                }
            default:
                return;
        }
    }
    void Flip(bool _flip)
    {
        if (bodySpr.flipX == _flip)
            return;
        if (_flip)
        {
            shootPoint.localPosition = new Vector2((-1) * shootPoint.localPosition.x, shootPoint.localPosition.y);
            shootPoint.localRotation = Quaternion.Euler(new Vector3(0, 180, 0));
            armRoot.localPosition = new Vector2(0.3f, armRoot.localPosition.y);
        }
        else
        {
            shootPoint.localPosition = new Vector2(Mathf.Abs(shootPoint.localPosition.x), shootPoint.localPosition.y);
            shootPoint.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            armRoot.localPosition = new Vector2(-0.3f, armRoot.localPosition.y);
        }
        bodySpr.flipX = _flip;
        armSpr.flipX = _flip;
    }
    public void MouseLook(float _angle, bool _look, bool _shoot)
    {
        if (_look)
        {

            angle = _angle;
            if (bodySpr.flipX)

                fixAngle = 180;
            else
                fixAngle = 0;

            rotor.localRotation = Quaternion.Euler(new Vector3(0, 0, angle + fixAngle));
            if (_shoot)
            {
                ShootBullet();
            }
        }
        else
        {
            angle = 0;
            rotor.localRotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
    public void ShootBullet()
    {
        if (Time.time > timeToFire + randomFireRate)
        {
            timeToFire = Time.time + 1 / fireRate;
            UnityPoolObject obj = UnityPoolManager.Instance.Pop<UnityPoolObject>(bulletType);
            (obj as ExtendedBullet).SetProperty(GameSettingManager.Instance.WP.weapon[GameSettingManager.Instance.WP.currentWeaponType].damage, "Enemy");
            obj.SetTransform(shootPoint.position, Quaternion.Euler(new Vector3(0, 0, angle + Random.Range(spreadMin, spreadMax))), Vector3.one);
        }
    }
}