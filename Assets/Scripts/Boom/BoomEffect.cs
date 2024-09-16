using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.Mathematics;
public class BoomEffect : MonoBehaviour
{
    // Start is called before the first frame update
    private Coroutine _curCo = null;
    private bool _counting = true;
    [SerializeField] private float _timeExplosion;

    [SerializeField] private GameObject _explodePrefab;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_curCo == null) {
            _curCo = StartCoroutine(begin());
        }
        if(_timeExplosion <= 0) {
            StopCoroutine(_curCo);
            explosion();
            deleteObj();
        }
    }
    IEnumerator begin() {
        transform.DOScaleY(1f, 0.2f).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(0.2f);
        transform.DOScaleY(1.2f, 0.2f).SetEase(Ease.InOutQuad);
        yield return new WaitForSeconds(0.2f);
        _curCo = null;
    }
    void FixedUpdate() {
        if(_counting){   
            timeCounter();
        }
    }
    private void timeCounter() {
        _timeExplosion -= Time.deltaTime;
    }
    public void explosion(){
        _counting = false;
        GetComponent<Collider2D>().enabled = false;
        if(_explodePrefab != null){
            int size = BoomManager.boomInstance.size;
            Instantiate(_explodePrefab, transform.position, Quaternion.identity);
            //UP
            for(int i = 0; i < size; i++) {
                Instantiate(_explodePrefab, new Vector2(transform.position.x, transform.position.y + i + 1), Quaternion.identity);
            }
            //DOWN
            for(int i = 0; i < size; i++) {
                Instantiate(_explodePrefab, new Vector2(transform.position.x, transform.position.y - i - 1), Quaternion.identity);
            }
            //RIGHT
            for(int i = 0; i < size; i++) {
                Instantiate(_explodePrefab, new Vector2(transform.position.x + i + 1, transform.position.y ), Quaternion.identity);
            }
            //LEFT
            for(int i = 0; i < size; i++) {
                Instantiate(_explodePrefab, new Vector2(transform.position.x - i - 1, transform.position.y), Quaternion.identity);
            }
        }
        
    }
    public void deleteObj(){
        Destroy(gameObject);
    }
    
   
}
