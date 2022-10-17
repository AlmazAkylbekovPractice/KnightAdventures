using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour
{
    //State Machine
    private Dictionary<Type, IPlayerBehavior> behaviorsMap;
    private IPlayerBehavior behaviourCurrent;

    //Character Components
    [HideInInspector] public Rigidbody2D rigidbody;
    [HideInInspector] public Animator animator;

    //Character properties
    [HideInInspector] public Vector2 direction;
    [HideInInspector] public float side = 1f;
    [SerializeField] public float movementSpeed = 3f;
    [SerializeField] public int attackDamage = 20;
    [SerializeField] public float damageRange = 0.7f;

    //UI Elements
    [SerializeField] public Button slash_button;


    //Other Elements
    [SerializeField] public LayerMask enemiesLayer;
    [SerializeField] public Transform attackPoint;

    private void OnEnable()
    {
        AssingingToUI();
    }

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        this.InitBehaviors();
        this.SetBehaviorByDefault();
    }

    private void InitBehaviors()
    {
        this.behaviorsMap = new Dictionary<Type, IPlayerBehavior>();

        this.behaviorsMap[typeof(PlayerIdleBehavior)] = new PlayerIdleBehavior();
        this.behaviorsMap[typeof(PlayerMoveBehavior)] = new PlayerMoveBehavior();
        this.behaviorsMap[typeof(PlayerCombatBehavior)] = new PlayerCombatBehavior();
    }

    private void SetBehavior(IPlayerBehavior newBehavior)
    {
        if (this.behaviourCurrent != null)
            this.behaviourCurrent.Exit(this);

        this.behaviourCurrent = newBehavior;
        this.behaviourCurrent.Enter(this);
    }

    private void SetBehaviorByDefault()
    {
        this.SetBehaviorIdle();
    }

    private IPlayerBehavior GetBehavior<T>() where T : IPlayerBehavior
    {
        var type = typeof(T);
        return this.behaviorsMap[type];
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


    public void SetBehaviorIdle()
    {
        var behavior = this.GetBehavior<PlayerIdleBehavior>();
        this.SetBehavior(behavior);
    }

    public void SetBehaviorMove()
    {
        var behavior = this.GetBehavior<PlayerMoveBehavior>();
        this.SetBehavior(behavior);
    }

    public void SetBehaviorCombat()
    {
        var behavior = this.GetBehavior<PlayerCombatBehavior>();
        this.SetBehavior(behavior);
    }

    private void AssingingToUI()
    {
        slash_button.onClick.AddListener(SetBehaviorCombat);
    }
}
