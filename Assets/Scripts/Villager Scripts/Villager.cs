using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{

    [SerializeField] private float _outterBorderMaxX = 15f;
    [SerializeField] private float _outterBorderMaxY = 11f;
    [SerializeField] private float _outterBorderMinX = 3f;
    [SerializeField] private float _outterBorderMinY = 5f;

    private List<Vector2> _harvestPoints;

    private float _walkSpeed = 2f;

    private Animator _animator;

    private bool _isHarvesting;

    private Vector2 _villagerDirection;

    // Start is called before the first frame update
    void Awake()
    {
        LoadVillager();
    }

    // Update is called once per frame
    void Update()
    {
        VillagerActivity();
    }

    private void LoadVillager()
    {
        LoadComponents();
        GeneratePoints();
    }

    private void LoadComponents()
    {
        _harvestPoints = new List<Vector2>();
        _animator = GetComponent<Animator>();
    }

    private void GeneratePoints()
    {
        for (int i = 0; i < 5; i++)
        {
            var randX = Random.Range(_outterBorderMinX, _outterBorderMaxX);
            var randY = Random.Range(_outterBorderMinY, _outterBorderMaxY);
            var harvestPoint = new Vector2(randX, randY);
            _harvestPoints.Add(harvestPoint);
        }
    }

    private void VillagerActivity()
    {
        ChooseHarvestPoints();
        TravelToPoints();
    }

    private void ChooseHarvestPoints()
    {
        if (_harvestPoints.Count < 5)
        {
            var randX = Random.Range(_outterBorderMinX, _outterBorderMaxX);
            var randY = Random.Range(_outterBorderMinY, _outterBorderMaxY);
            var harvestPoint = new Vector2(randX, randY);
            _harvestPoints.Add(harvestPoint);
        }
    }

    private void TravelToPoints()
    {
        if (_harvestPoints[0].x > transform.position.x)
            MoveToRight();
        else if (_harvestPoints[0].x < transform.position.x)
            MoveToLeft();
        else if (_harvestPoints[0].y > transform.position.y)
            MoveUp();
        else if (_harvestPoints[0].y < transform.position.y)
            MoveDown();

        _animator.SetFloat("Horizontal", _villagerDirection.x);
        _animator.SetFloat("Vertical", _villagerDirection.y);

        if (transform.position.x == _harvestPoints[0].x &&
            transform.position.y == _harvestPoints[0].y)
            Harvest();
    }

    private void MoveToRight()
    {
        _villagerDirection.x = 1;
        _villagerDirection.y = 0;

        transform.position =
             Vector2.MoveTowards(transform.position, new Vector2(_harvestPoints[0].x, transform.position.y), _walkSpeed * Time.deltaTime);
    }

    private void MoveToLeft()
    {
        _villagerDirection.x = -1;
        _villagerDirection.y = 0;

        transform.position =
            Vector2.MoveTowards(transform.position, new Vector2(_harvestPoints[0].x, transform.position.y), _walkSpeed * Time.deltaTime);
    }

    private void MoveUp()
    {
        _villagerDirection.x = 0;
        _villagerDirection.y = 1;

        transform.position =
            Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, _harvestPoints[0].y), _walkSpeed * Time.deltaTime);
    }

    private void MoveDown()
    { 
        _villagerDirection.x = 0;
        _villagerDirection.y = -1;

        transform.position =
            Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, _harvestPoints[0].y), _walkSpeed * Time.deltaTime);
    }

    private void Harvest()
    {

        _isHarvesting = true;
        _animator.SetBool("Harvesting", _isHarvesting);
        Invoke("EndHarvest", Random.Range(5,15));

    }

    private void EndHarvest()
    {
        _harvestPoints.RemoveAt(0);
        _isHarvesting = false;
        _animator.SetBool("Harvesting", _isHarvesting);

        CancelInvoke("EndHarvest");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _harvestPoints.RemoveAt(0);
        _isHarvesting = false;
        _animator.SetBool("Harvesting", _isHarvesting);

    }
}
