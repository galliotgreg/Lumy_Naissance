using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollExpander : MonoBehaviour {
    private bool _updated = false;
    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    void Update()
    {

        if (!_updated)
        {
            RectTransform rectTransform = (RectTransform)transform;
            float yMin = 0.0f;
            float yMax = 0.0f;
            foreach (RectTransform child in transform)
            {
                yMin = Mathf.Min(yMin, child.offsetMin.y);
                yMax = Mathf.Max(yMax, child.offsetMax.y);
            }

            float finalSize = yMax - yMin;
            rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, finalSize);
            _updated = true;
        }
    }
}
