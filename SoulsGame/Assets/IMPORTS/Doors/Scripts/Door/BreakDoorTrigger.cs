using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakDoorTrigger : MonoBehaviour
{
    public GameObject Door;
    public Animator _anim;

    public GameObject[] _BreakableArray;
    public List<GameObject> _Breakables = new List<GameObject>();

    public List<Rigidbody> _BreakableRigidbody = new List<Rigidbody>();

    public float _minValue = -1.5f;
    public float _maxValue = 1.5f;

    public bool _DoorActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        //_anim = Door.GetComponentInChildren<Animator>();

        _BreakableArray = GameObject.FindGameObjectsWithTag("BreakableParts");
        for (int i = 0; i < _BreakableArray.Length; i++)
        {
            _Breakables.Add(_BreakableArray[i]);
        }

    }

    // Update is called once per frame
    void Update()
    {
        DestroyParts();
    }

    public void ActivateDoor()
    {
        for (int i = 0; i < _Breakables.Count; i++)
        {
            // Add Rigidbody to breakable parts
            _Breakables[i].AddComponent<Rigidbody>();

            // Set the _rb component in each part
            _Breakables[i].GetComponent<BreakablePartDestroy>()._rb = _Breakables[i].GetComponent<Rigidbody>();

            // Add each parts Rigidbody into List BreakableRigidbody
            _BreakableRigidbody.Add(_Breakables[i].GetComponent<Rigidbody>());

            Rigidbody _rb = _Breakables[i].GetComponent<Rigidbody>();

            Vector3 _force = new Vector3(Random.Range(_minValue, _maxValue), Random.Range(_minValue, _maxValue), Random.Range(_minValue, _maxValue));
            _rb.AddForce(_force, ForceMode.Impulse);
            _DoorActivated = true;
        }
    }

    public void DestroyParts()
    {
        for (int i = 0; i < _BreakableRigidbody.Count; i++)
        {
            if(_BreakableRigidbody[i].velocity == Vector3.zero && _DoorActivated)
            {
                Destroy(_BreakableArray[i], 5);
                Debug.Log("Part Destroyed");
            }
        }
    }

    IEnumerator _DestroyParts()
    {
        yield return new WaitForSeconds(5);
        
    }
}
