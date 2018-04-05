
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public float slowdownFactor = 0.05f;
    public float slowdownLenght = 2.0f;

    private void Update()
    {
        if (Time.timeScale < 1.0f)
        {
            Time.timeScale += (1.0f / slowdownLenght) * Time.unscaledDeltaTime;
            Time.timeScale = Mathf.Clamp(Time.timeScale, 0.0f, 1.0f);
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }

    public void DoSlowMotion()
    {
        Time.timeScale = slowdownFactor;
        Time.fixedDeltaTime = Time.timeScale * 0.02f;
    }
}
