using UnityEngine;

public class Cube_LMassSpawner : Base_Factory
{
    public GameObject cubePrefab;
    public Transform spawnParent;
    public override Cube SpawnCube()
    {
        GameObject CubeGO = Instantiate(cubePrefab, transform.position, Quaternion.identity, spawnParent);
        return CubeGO.GetComponent<Cube>(); 
    }
}
