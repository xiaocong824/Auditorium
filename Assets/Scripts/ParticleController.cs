using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleController : MonoBehaviour
{
   
    [SerializeField]
    private float _minimumVelocityMagnitude = 0.1f;

    private Rigidbody2D _rigidbody;

    private MeshRenderer _meshRenderer;
    private bool _isFlaggedForDestroy;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    private void FixedUpdate()
    {
        if (_rigidbody != null)
        {
            if (_rigidbody.velocity.magnitude < _minimumVelocityMagnitude && !_isFlaggedForDestroy)
            {
                _isFlaggedForDestroy = true;
                StartCoroutine(IE_FadeOut(0.5f));
               // Debug.Log("Particle velocity magnitude: " + _rigidbody.velocity.magnitude);
            }
        }
        
    }

    //Э�̺�����Coroutine Function������ Unity ��Ϸ�����У�Э�̺�����һ������ĺ������ͣ�����������ͣ������ִ�У������Ժ�ָ�������������У�ʹ��Э����ʵ��һ����������Ч����
    private IEnumerator IE_FadeOut(float timeToFade)
    //����һ��˽�к���������һ�� IEnumerator ���󣬺�����Ϊ IE_FadeOut������Ҫһ������������.timeToFade����ʾ����Ч����Ҫ�೤ʱ��
    {
        float timer = 0f; //�ں�����ʼʱ������һ����ʱ�� timer����ʼֵΪ 0
        Material material = _meshRenderer.material; //��ȡ����� _meshRenderer ����Ĳ��ʣ��洢�� material �����С�

        while (timer < timeToFade) //ʼһ��ѭ����ֱ����ʱ�� timer ���ڻ���ڵ���ʱ�� timeToFade��
        {
            float percent = timer / timeToFade;//���㵱ǰ�������Ȱٷֱ� percent�����㹫ʽΪ timer / timeToFade��
            Color matColor = material.color;//��ȡ���ʵ���ɫ���洢�� matColor �����С�
            matColor.a = Mathf.Lerp(1f, 0f, percent);//ʹ�� Mathf.Lerp �����ڵ������ȷ�Χ�ڣ��� 0 �� 1��������ǰ͸����ֵ�� 1����͸�������䵽 0��͸�������洢�� matColor.a �С�
            material.color = matColor;//�����ʵ���ɫ����Ϊ�µ���ɫ�������µ�͸����ֵ��

            timer += Time.deltaTime;//����ʱ�� timer ������һ֡���õ�ʱ�� Time.deltaTime��
            yield return new WaitForEndOfFrame();//ʹ�� yield return �õ�ǰ������ͣ���ȴ���һ֡����֮�����ִ��ѭ����
        }

        Color finalColor = material.color;//ѭ�������󣬻�ȡ���ղ��ʵ���ɫ���洢�� finalColor �����С�
        finalColor.a = 0f;//��������ɫ��͸����ֵ����Ϊ 0����ȫ͸������
        material.color = finalColor;//�����ʵ���ɫ����Ϊ������ɫ��

        Destroy(gameObject);
    }

}
