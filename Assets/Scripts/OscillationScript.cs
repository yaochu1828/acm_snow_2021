using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillationScript : MonoBehaviour
{
    // global parameters (same for all units)
    public static float globalAmplitude = 2; // maximum horizontal displacement from centeral axis
    public static float globalPeriod = 3; // time taken to swing from center to one side and back again
    public static float numUnits = 5;
    public static float unitLength;
    public static float y_origin;

    public float index; // index of given unit E [0,10]
    private float y; // vertical position of given unit E [0, pi]
                    // y = index * pi / total number of units

    private float y_position; // Real absolute positon of unit in scene on y axis
                             // y_position = beginning point of bridge + index * length of unit
    private float delta_x; // Real absolute displacement of unit from central axis in scene
    private Vector3 transform_origin; // Real absolute original position of unit in scene

    // Start is called before the first frame update
    void Start()
    {
        y = index * Mathf.PI / numUnits;
        y_position = y_origin + index * unitLength;
        y_position = transform.position.y;
        transform.position = new Vector3(transform.position.x, y_position, transform.position.z);

        transform_origin = transform.position;  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("y"))
        {
            StartCoroutine(Oscillate(1));
        }
        if (Input.GetKeyDown("u"))
        {
            StartCoroutine(Oscillate(-1));
        }
    }

    private IEnumerator Oscillate(int direction, float ampMultiplier = 1, float periodMultiplier = 1)
    {
        // direction E {-1,1}
        float periodElapsed = 0;
        float periodDuration = globalPeriod * periodMultiplier;
        float amplitude = 0;
        float maxAmplitude = globalAmplitude * ampMultiplier;

        while (periodElapsed < periodDuration)
        {
            amplitude = maxAmplitude * Mathf.Sin((Mathf.PI / periodDuration) * periodElapsed);

            delta_x = direction * amplitude * Mathf.Sin(y);
            transform.position =  transform_origin + new Vector3(delta_x, 0, 0);

            periodElapsed += Time.deltaTime;
            yield return null;
        }
    }
}
