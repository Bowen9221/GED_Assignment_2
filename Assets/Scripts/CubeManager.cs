using TMPro;
using UnityEngine.Pool;
using UnityEngine;
using Unity.VisualScripting;

public class CubeManager : MonoBehaviour
{
    [SerializeField] private float spawnDelay = 4f;
    [SerializeField] private TMP_Text _cubeCounter;
    [SerializeField] private TMP_Text _usingPooling;
    [SerializeField] private bool usePooling = true;
    [SerializeField] private int maxCount = 960;
    [SerializeField] private float spawnRadius = 3f;   

    public int cubeCount = 0;

    private Cube_LMassSpawner _spawner;
    private ObjectPool<Cube> _pool;
    private float _spawnTimer;
    private int _shownCount = -1;

    public bool GetIsPooling() => usePooling;

    private void Awake()
    {
        _spawner = FindAnyObjectByType<Cube_LMassSpawner>();
        _spawnTimer = spawnDelay;
        if (_usingPooling)
        {
            _usingPooling.text = "Currently Using: Pooling";
        }
        else
        {
            _usingPooling.text = "Currently Using: Instantiate + Destroy";
        }

            _pool = new ObjectPool<Cube>(
                createFunc: CreateCube,
                actionOnGet: c => c.gameObject.SetActive(true),
                actionOnRelease: c => c.gameObject.SetActive(false),
                actionOnDestroy: c => Destroy(c.gameObject),
                collectionCheck: false,
                defaultCapacity: maxCount,
                maxSize: maxCount + 100);
    }

    private void Start()
    {
        //for (int i = 0; i < maxCount; i++) GetCube();
    }

    
    private Cube CreateCube()
    {
        Cube c = _spawner.SpawnCube();
        c.Expired += HandleExpired;   
        return c;
    }

    private void GetCube()
    {
        if (cubeCount >= maxCount) return;   

        Cube c = usePooling ? _pool.Get() : CreateCube();

        
        c.transform.position = _spawner.transform.position + Random.insideUnitSphere * spawnRadius;
        c.rb.linearVelocity = Vector3.zero;
        c.rb.angularVelocity = Vector3.zero;

        c.Begin();
        cubeCount++;
    }

    private void HandleExpired(Cube c)
    {
        cubeCount--;
        if (usePooling) _pool.Release(c);
        else Destroy(c.gameObject);

        GetCube();   
    }

    private void Update()
    {
        _spawnTimer -= Time.deltaTime;
        SetPoolingBool();

        if (_spawnTimer <= 0f)
        {
            GetCube();
            _spawnTimer = spawnDelay;
        }

        if (cubeCount != _shownCount)   
        {
            _shownCount = cubeCount;
            _cubeCounter.SetText("{0}", cubeCount);
        }
    }

    void SetPoolingBool()
    {
        if (Input.GetKeyDown(KeyCode.Space) && usePooling)
        {
            usePooling = false;
            _usingPooling.text = "Currently Using: Instantiate + Destroy";
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !usePooling)
        {
            usePooling = true;
            _usingPooling.text = "Currently Using: Pooling";
        }

        
    }
}


