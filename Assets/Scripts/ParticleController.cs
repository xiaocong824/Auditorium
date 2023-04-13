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

    //协程函数（Coroutine Function）。在 Unity 游戏引擎中，协程函数是一种特殊的函数类型，它允许您暂停函数的执行，并在稍后恢复。在这个函数中，使用协程来实现一个淡出动画效果。
    private IEnumerator IE_FadeOut(float timeToFade)
    //这是一个私有函数，返回一个 IEnumerator 对象，函数名为 IE_FadeOut，它需要一个浮点数参数.timeToFade，表示淡出效果需要多长时间
    {
        float timer = 0f; //在函数开始时，定义一个计时器 timer，初始值为 0
        Material material = _meshRenderer.material; //获取对象的 _meshRenderer 组件的材质，存储到 material 变量中。

        while (timer < timeToFade) //始一个循环，直到计时器 timer 大于或等于淡出时间 timeToFade。
        {
            float percent = timer / timeToFade;//计算当前淡出进度百分比 percent，计算公式为 timer / timeToFade。
            Color matColor = material.color;//获取材质的颜色，存储到 matColor 变量中。
            matColor.a = Mathf.Lerp(1f, 0f, percent);//使用 Mathf.Lerp 函数在淡出进度范围内（从 0 到 1），将当前透明度值从 1（不透明）渐变到 0（透明），存储到 matColor.a 中。
            material.color = matColor;//将材质的颜色设置为新的颜色，包括新的透明度值。

            timer += Time.deltaTime;//将计时器 timer 增加上一帧所用的时间 Time.deltaTime。
            yield return new WaitForEndOfFrame();//使用 yield return 让当前函数暂停，等待下一帧更新之后继续执行循环。
        }

        Color finalColor = material.color;//循环结束后，获取最终材质的颜色，存储到 finalColor 变量中。
        finalColor.a = 0f;//将最终颜色的透明度值设置为 0（完全透明）。
        material.color = finalColor;//将材质的颜色设置为最终颜色。

        Destroy(gameObject);
    }

}
