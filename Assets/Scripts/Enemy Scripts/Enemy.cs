using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : MonoBehaviour
{
    //Enemy State Machine
    private Dictionary<Type, IEnemyBehavior> enemyBehaviorsMap;
    private IEnemyBehavior behaviourCurrent;

    //Player
    [HideInInspector] public GameObject player;

    //Character Components
    public Rigidbody2D rigidbody;
    public Animator animator;
    public BoxCollider2D collider;

    //Enemy Components
    [HideInInspector] public Vector2 direction;
    [HideInInspector] public Vector3 startPos;
    [HideInInspector] public Vector3 nextPosition;
    [HideInInspector] public Vector3 targetPosition;
    [SerializeField] private int currentHealth = 140;
    [SerializeField] public float walkingRadius = 1f;
    [SerializeField] public float walkingSpeed = 1f;
    [SerializeField] public float veiwRange = 6f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collider = GetComponent<BoxCollider2D>();

        startPos = transform.position;
        player = GameObject.FindWithTag("Player");
    }

    private void Start()
    {
        this.InitBehaviors();
        this.SetBehaviorByDefault();
    }

    private void InitBehaviors()
    {
        this.enemyBehaviorsMap = new Dictionary<Type, IEnemyBehavior>();

        this.enemyBehaviorsMap[typeof(EnemyWalkBehavior)] = new EnemyWalkBehavior();
        this.enemyBehaviorsMap[typeof(EnemyCombatBehavior)] = new EnemyCombatBehavior();
    }

    private void SetBehavior(IEnemyBehavior newBehavior)
    {
        if (this.behaviourCurrent != null)
            this.behaviourCurrent.Exit(this);

        this.behaviourCurrent = newBehavior;
        this.behaviourCurrent.Enter(this);
    }

    private void SetBehaviorByDefault()
    {
        this.SetBehaviorWalk();
    }

    private IEnemyBehavior GetBehavior<T>() where T : IEnemyBehavior
    {
        var type = typeof(T);
        return this.enemyBehaviorsMap[type];
    }

    // <summary>
    /// Update Input and Condition Parameters
    /// </summary>
    private void Update()
    {
        if (this.behaviourCurrent != null)
            this.behaviourCurrent.Update(this);
    }

    /// <summary>
    /// Fixed update to maintain physics
    /// </summary>
    private void FixedUpdate()
    {
        if (this.behaviourCurrent != null)
            this.behaviourCurrent.FixedUpdate(this);
    }

    public void SetBehaviorWalk()
    {
        var behavior = this.GetBehavior<EnemyWalkBehavior>();
        this.SetBehavior(behavior);
    }

    public void SetCombatBehavior()
    {
        var behavior = this.GetBehavior<EnemyCombatBehavior>();
        this.SetBehavior(behavior);
    }

    //Combat Properties

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;


        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 0, 0, 255);

        Invoke("StopHurt", 0.25f);

        if (currentHealth <= 0)
        {
            Dies();
        }
    }

    private void StopHurt()
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
    }

    private void Dies()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingOrder = 0;
        enabled = false;
        animator.SetBool("Dies", true);

        Destroy(rigidbody);
        Destroy(collider);
    }
}
