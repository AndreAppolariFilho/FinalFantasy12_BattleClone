using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class HeroController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator animator;
    public GameObject targetLine;
    public Rigidbody playerRb;
    public float moveSpeed = 3.5f;
    private GameManager gameManager;
    //CharacterController charController;
    Camera cam;
    CharacterStats stats;
    public float range = 25.0f;
    Queue<AttackDefinition> attacks;
    AttackDefinition currentAttack = null;
    GameObject target = null;
    public bool isAttacking = false;
    private Vector3 moveInput;
    private Vector3 moveVelocity;
    private float attackTime;
    private float currentAttackTime;
    private float analogVertical;
    private float analogHorizontal;
    BezierCurveScript bezierCurve;
    private LayerMask ignoreLayers;
    void Awake()
    {
        bezierCurve = new BezierCurveScript();
        playerRb = GetComponent<Rigidbody>();
        playerRb.angularDrag = 999;
        playerRb.drag = 4;
        playerRb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        //charController = GetComponent<CharacterController>();
        attacks = new Queue<AttackDefinition>();
        cam = Camera.main;
        gameManager = FindObjectOfType<GameManager>();
        stats = GetComponent<CharacterStats>();
        gameObject.layer = 8;
        ignoreLayers = ~(1 << 9);
        
    }
    

    // Update is called once per frame
    void Update()
    {
        //animator.SetTrigger("Attack");
        
        if (stats == null)
            stats = GetComponent<CharacterStats>();
        if (gameManager.gameState == GameManager.GameState.Running)
        {
            animator.speed = 1;
            float horizontal = Input.GetAxis("Left X Axis");
            float vertical = Input.GetAxis("Left Y Axis");
            if(!isAttacking)
                MovePlayer(horizontal, vertical);
            
        }
        else
        {
            animator.speed = 0;
            moveVelocity = new Vector3(0, 0, 0);
        }
        

    }
    private void FixedUpdate()
    {
        if(target != null && attacks.Count > 0)
        {
            Debug.Log("target");
            currentAttack = attacks.Peek();
        }

        if (currentAttack != null && !isAttacking)
        {
            Debug.Log("Increase ATB");
            stats.IncreaseATB(2);
            
            if (stats.GetCurrentATB() == stats.GetMaxATB())
            {
                Debug.Log(currentAttack.attackType.ToString());
                if (currentAttack.attackType == AttackDefinition.Type.Physical)
                {

                    if (GetCurrentWeapon().CanAttackTarget(this.gameObject, target))
                    {
                        isAttacking = true;
                        animator.SetTrigger("Attack");
                        animator.SetFloat("Vertical", 0);
                        moveVelocity = new Vector3(0, 0, 0);
                    }
                }
                else if (currentAttack.attackType == AttackDefinition.Type.Magical)
                {
                    if(((SpellAttack)currentAttack).CanCastSpell(this.gameObject, target))
                    {
                        isAttacking = true;
                        animator.SetTrigger("MagickAttack");
                        animator.SetFloat("Vertical", 0);
                        moveVelocity = new Vector3(0, 0, 0);
                    }
                }
            }
            
        }

        if (!isAttacking)
        {
            playerRb.velocity = moveVelocity;
        }
        else
        {
            playerRb.velocity = new Vector3(0, 0, 0);
        }
    }
    public void Attack()
    {
        if (currentAttack.attackType == AttackDefinition.Type.Physical)
        {
            GetCurrentWeapon().ExecuteAttack(this.gameObject, target);
        }
        else if(currentAttack.attackType == AttackDefinition.Type.Magical)
        {
            ((SpellAttack)currentAttack).CastSpell(this.gameObject, target);
        }
    }
    public void DrawTarget(GameObject enemy)
    {
        if(!targetLine.active)
        { 
            targetLine.SetActive(true);
            targetLine.GetComponent<BezierCurveScript>().SetTarget(this.gameObject, enemy);
        }
    }
    public void StopDraw()
    {
        targetLine.SetActive(false);
        targetLine.GetComponent<BezierCurveScript>().Reset();
    }
    public void StopAttack()
    {
        Debug.Log("Stop Attack");
        isAttacking = false;
        currentAttack = null;
        attacks.Dequeue();
        stats.clearATB();
    }
    void MovePlayer(float horizontal, float vertical)
    {
        analogVertical = vertical;
        analogHorizontal = horizontal;
        Vector3 forward = cam.transform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = cam.transform.right;
        right.y = 0f;
        right.Normalize();
        

        Quaternion relativeCameraRotation = Quaternion.FromToRotation(Vector3.forward, forward);

        moveInput = new Vector3(horizontal, 0, vertical);

        Vector3 lookToward = relativeCameraRotation * moveInput;

        if(moveInput.sqrMagnitude > 0)
        {
            Ray lookRay = new Ray(transform.position, lookToward);
            transform.LookAt(lookRay.GetPoint(1));
        }


        Vector3 desiredMoveDirection = (forward * vertical + right * horizontal).normalized ;
        float moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        if(moveAmount > 0 || !OnGround())
        {
            playerRb.drag = 0;

        }
        else
        {
            playerRb.drag = 4;
        }

        //moveVelocity = Vector3.ClampMagnitude(transform.forward * moveInput.sqrMagnitude * moveSpeed, moveSpeed);
        moveVelocity = desiredMoveDirection * (moveSpeed * moveAmount);
        Vector3 targetDir = desiredMoveDirection;
        targetDir.y = 0;
        if(targetDir == Vector3.zero)
        {
            targetDir = transform.forward;
        }
        //Quaternion tr = Quaternion.LookRotation(desiredMoveDirection);
        //Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, Time.deltaTime * 5);
        //transform.rotation = targetRotation;
        //animator.SetFloat("Speed", playerRb.velocity.magnitude);
        animator.SetFloat("Vertical", moveAmount, 0.4f, Time.deltaTime);

    }

    public AttackDefinition GetCurrentAttack()
    {
        return currentAttack;
    }
    public int GetCurrentHealth()
    {
        
        return stats.GetCurrentHealth();
    }
    public int GetCurrentMana()
    {
        return stats.GetCurrentMana();
    }
    public int GetCurrentDamage()
    {
        return stats.GetCurrentDamage();
    }
    public int GetCurrentATB()
    {
        return stats.GetCurrentATB();
    }
    public Weapon GetCurrentWeapon()
    {
        return stats.GetCurrentWeapon();
    }

    public int GetMaxHealth()
    {
        return stats.GetMaxHealth();
    }
    public int GetMaxMana()
    {
        return stats.GetMaxMana();
    }
    public int GetMaxDamage()
    {
        return stats.GetMaxDamage();
    }
    public int GetMaxATB()
    {
        return stats.GetMaxATB();
    }
    public List<AttackDefinition> GetMeelleAttacks()
    {
        return stats.GetMeelleAttacks();
    }
    public List<AttackDefinition> GetBlackMages()
    {
        return stats.GetBlackMages();
    }
    public List<GameObject> nearbyEnemies()
    {
       
        List<GameObject> enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        return enemies.FindAll(delegate (GameObject enemy)
        {
            
            return Vector3.Distance(this.transform.position, enemy.transform.position) < range;
            
        });
    }
    public void selectAttack(AttackDefinition attack, GameObject enemyTarget)
    {   
        currentAttack = attack;
        target = enemyTarget;
    }

    public void selectTarget(GameObject enemyTarget)
    {
        target = enemyTarget;
    }
    public void addAttack(AttackDefinition attack)
    {
        if(attacks.Count > 0 && (isAttacking || GetCurrentATB() == GetMaxATB()))
        { 
            if(!attacks.Peek().name.Equals(attack.name))
                attacks.Enqueue(attack);
        }
        if(attacks.Count == 0)
        {
            attacks.Enqueue(attack);
        }
    }
    public bool OnGround()
    {
        bool r = false;
        Vector3 origin = transform.position + (Vector3.up * 0.5f);
        Vector3 dir = -Vector3.up;
        float dist = 0.5f + 0.3f;
        RaycastHit hit;
        if(Physics.Raycast(origin, dir, out hit, dist, ignoreLayers))
        {
            r = true;
            Vector3 targetPosition = hit.point;
            transform.position = targetPosition;
        }

        return r;
    }
}
