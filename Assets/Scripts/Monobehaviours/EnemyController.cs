using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Animator anim;
    [SerializeField] private Rigidbody enemyRb;

    private void Awake()
    {
        enemyRb = GetComponent<Rigidbody>();
        
    }
    void Start()
    {
        
    }
    
    public void TriggerAttackAnimation()
    {   
        anim.SetTrigger("IsAttacked");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
