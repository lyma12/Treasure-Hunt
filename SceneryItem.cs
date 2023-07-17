using System.Net.NetworkInformation;
using UnityEngine;

namespace VectorForestScenery
{
    public class SceneryItem : MonoBehaviour
    {
        #region "Inspector"
        public ParticleSystem _particle;
        public Animator _animator;
        public bool _randomizeSize;
        public float _minSize = 0.85f;
        public float _maxSize = 1.15f;
        private float timeActive = 10f;
        public SpriteRenderer[] _lightAffectedRenderers;
        #endregion

        public virtual void Start()
        {
            if (_particle != null)
            {
                _particle.gameObject.SetActive(false);
            }
            if (_randomizeSize)
            {
                RandomizeSize();
            }
        }

        private void Update()
        {
            int type = UnityEngine.Random.Range(0, 2);
            if (timeActive <= 0)
            {
                if (type == 0) WindLeft();
                else if (type == 1) WindRight();
                timeActive = 10f;
            }
            else timeActive -= Time.deltaTime;
            
        }

        

        public void RandomizeSize()
        {
            float size = Random.Range(_minSize, _maxSize);
            transform.localScale = new Vector2(size, size);
        }
        

        public void WindLeft()
        {
            Invoke("idleTree", 2);
            _animator.SetTrigger("WindLeft");
            TriggerParticle();
        }
       
        public void WindRight()
        {
            Invoke("idleTree", 2);
            _animator.SetTrigger("WindRight");
            TriggerParticle();
        }
        public void idleTree()
        {
            _animator.ResetTrigger("Shake");
            _animator.ResetTrigger("WindRight");
            _animator.ResetTrigger("WindLeft");
            _animator.SetTrigger("idle");
            if (_particle != null)
            {
                _particle.Stop();
                _particle.gameObject.SetActive(false);
            }
        }

        public void Shake()
        {
            Invoke("idleTree", 2);
            _animator.SetTrigger("Shake");
        }

        

        public void TriggerParticle()
        {
            if (_particle != null)
            {
                _particle.gameObject.SetActive(true);
                _particle.Stop();
                _particle.Play();
            }
        }

        private void OnTriggerEnter2D(Collider2D collider2D)
        {
            Shake();
           
        }
        
    }
}