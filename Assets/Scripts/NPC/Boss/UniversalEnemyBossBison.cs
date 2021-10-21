using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalEnemyBossBison : MonoBehaviour 
{
    public delegate void AIAction();
    AIAction Action;

    public Rigidbody2D rg;
    public bool alive, walk, charge,shoot, targetDetected;
    public int bulletType, damage;
    public float shootTime, prepareTime;
    public float defSpeed,chargeSpeed;
    public float distToCharge, distToMove, distToShoot;
    public Transform targetUnit, shootPoint;
    public Collider2D wallDetector;
    Vector2 vectorOfMove, dir;
    int stateType = 0;
    float speed, addShootTime = 0,addTime, currentDist = 0, rawDist,angelFix;


    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.tag == "Wall")
        {
            if (charge && !wallDetector.IsTouching(coll))
            {
                print("Walled");
                charge = false;
                Action = RestCharge;
            }

        }
    }

    public void Start()
    {
        Action = Idle;
        speed = defSpeed;
        vectorOfMove.Set(0, 0);
        dir.Set(0, 0);
    }

    public void FixedUpdate()
    {
        if (alive)
            rg.velocity = new Vector2(vectorOfMove.x * speed, rg.velocity.y);
    }
    void Update()
    {

        if (alive)
            Action();
        else
            this.enabled = false;

        currentDist = Mathf.Abs(rawDist);

        if (!charge && currentDist >= distToCharge)
        {
            Action = PrepareCharge;
        }

        if (!targetDetected)
            Action = Idle;
        else 
            Action = Move;


    }
    public void Move()
    {
        print("Move");
        speed = defSpeed;
        rawDist = (targetUnit.position.x - transform.position.x);         
        vectorOfMove.Set(Mathf.Sign(rawDist), 0);
    }
    public void Idle()
    {
        print("Idle");
        vectorOfMove.Set(0, 0);
    }
    public void PrepareCharge()
    {
        print("Prepare charge");
        charge = true;
        addTime += Time.deltaTime;
        vectorOfMove.Set(0, 0);
        if (addTime >= prepareTime)
        {
            addTime = 0;
            Action = Сharge;
        }
    }
    public void Сharge()
    {
        print("Charge");
        speed = chargeSpeed;
        vectorOfMove.Set(Mathf.Sign(rawDist), 0);
    }
    public void RestCharge()
    {
        print("Rest");
        addTime += Time.deltaTime;
        vectorOfMove.Set(0, 0);
        if (addTime >= prepareTime)
        {
            addTime = 0;
            charge = false;
            Action = Idle;
            
        }
    }
    public void ShootBullet()
    {
        if (addShootTime > shootTime)
        {
            UnityPoolObject obj = UnityPoolManager.Instance.Pop<UnityPoolObject>(bulletType);
            (obj as ExtendedBullet).SetProperty(damage, "unit");
            obj.SetTransform(shootPoint.position, Quaternion.Euler(new Vector3(0, 0, shootPoint.rotation.eulerAngles.z + angelFix)), Vector3.one);
            addShootTime = 0;
        }
        addShootTime += Time.deltaTime;
    }
}
