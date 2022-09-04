using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villager : MonoBehaviour
{
    [SerializeField] private List<Vector2> _harvestPoints;

    [SerializeField] private float _outterBorderMaxX = 15f;
    [SerializeField] private float _outterBorderMaxY = 11f;
    [SerializeField] private float _outterBorderMinX = 3f;
    [SerializeField] private float _outterBorderMinY = 5f;

    private float _walkSpeed = 2f;
    private Animator _animator;


    private bool _isHarvesting;

    private float XAxis;
    private float YAxis;

    // Start is called before the first frame update
    void Awake()
    {
        _harvestPoints = new List<Vector2>();
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void FixedUpdate()
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

        if (_harvestPoints.Count == 0)
        {
            for (int i = 0; i < 5; i++)
            {
                var randX = Random.Range(_outterBorderMinX, _outterBorderMaxX);
                var randY = Random.Range(_outterBorderMinY, _outterBorderMaxY);
                var harvestPoint = new Vector2(randX, randY);
                _harvestPoints.Add(harvestPoint);
            }
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

        if (transform.position.x == _harvestPoints[0].x &&
            transform.position.y == _harvestPoints[0].y)
            Harvest();

    }

    private void MoveToRight()
    {
        _animator.SetFloat("Horizontal", 1);
        _animator.SetFloat("Vertical", 0);

        transform.position =
            Vector2.MoveTowards(transform.position, new Vector2(_harvestPoints[0].x, transform.position.y), _walkSpeed * Time.deltaTime);
    }

    private void MoveToLeft()
    {
        _animator.SetFloat("Horizontal", -1);
        _animator.SetFloat("Vertical", 0);

        transform.position =
            Vector2.MoveTowards(transform.position, new Vector2(_harvestPoints[0].x, transform.position.y), _walkSpeed * Time.deltaTime);
    }

    private void MoveUp()
    {
        _animator.SetFloat("Horizontal", 0);
        _animator.SetFloat("Vertical", 1);


        transform.position =
            Vector2.MoveTowards(transform.position, new Vector2(transform.position.x, _harvestPoints[0].y), _walkSpeed * Time.deltaTime);
    }

    private void MoveDown()
    {
        _animator.SetFloat("Horizontal", 0);
        _animator.SetFloat("Vertical", -1);

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

        Debug.Log("Collsiiong occured");

    }
}
