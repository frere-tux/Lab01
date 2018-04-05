using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarFieldSystem : MonoBehaviour {

    private Transform tx;
    private ParticleSystem partSys;

    private ParticleSystem.Particle[] points;

    private float boostRatio;
    public float BoostRatio
    {
        get
        {
            return boostRatio;
        }

        set
        {
            boostRatio = value;
            for (int i = 0; i < starsMax; ++i)
            {
                points[i].startSize = starSize * boostRatio;
            }
        }
    }


    public int starsMax = 500;
    public float starSize = 0.25f;
    public float distanceMax = 100.0f;
    public float clipDistance = 5.0f;

    // Use this for initialization
    void Start ()
    {
        tx = transform;
        partSys = GetComponent<ParticleSystem>();
        boostRatio = 1.0f;
	}

    private void CreateStars()
    {
        points = new ParticleSystem.Particle[starsMax];

        for (int i = 0; i < starsMax; ++i)
        {
            points[i].position = ComputePosition();
            points[i].startColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
            points[i].startSize = starSize * boostRatio;
        }
    }

    private Vector3 ComputePosition()
    {
        return Random.insideUnitSphere.normalized * distanceMax + tx.position;
    }
	
	// Update is called once per frame
	void Update ()
    {
		if (points == null)
        {
            CreateStars();
        }

        for (int i = 0; i < starsMax; ++i)
        {
            float sqrDistance = (points[i].position - tx.position).sqrMagnitude;

            if (sqrDistance > distanceMax * distanceMax)
            {
                points[i].position = ComputePosition();
            }

            float sqrClipDistance = clipDistance * clipDistance;
            if(sqrDistance < sqrClipDistance)
            {
                float ratio = sqrDistance / sqrClipDistance;
                points[i].startColor = new Color(1.0f, 1.0f, 1.0f, ratio);
                points[i].startSize = starSize * ratio * boostRatio;
            }

        }

        partSys.SetParticles(points, points.Length); 
	}
}
