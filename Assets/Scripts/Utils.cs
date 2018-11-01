using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils: MySingleton<Utils>
{
    // Corners
    public const int LBF = 0;
    public const int LBB = 1;
    public const int LTF = 2;
    public const int LTB = 3;
    public const int RBF = 4;
    public const int RBB = 5;
    public const int RTF = 6;
    public const int RTB = 7;
    
    public Vector3 GetObjectBoundSize(GameObject gameObject)
    {
        var size = Vector3.zero;
        var boxCollider = gameObject.GetComponent<BoxCollider>();
        if (boxCollider != null)
        {
            size = boxCollider.size;
        }

        foreach (Transform child in gameObject.transform)
        {
            var boundSize = GetObjectBoundSize(child.gameObject);
            size.x = boundSize.x > size.x ? boundSize.x : size.x;
            size.y = boundSize.y > size.y ? boundSize.y : size.y;
            size.z = boundSize.z > size.z ? boundSize.z : size.z;
        }
        return Vector3.Scale(gameObject.transform.localScale, size);
    }
    
    public Rigidbody AddRigbody(GameObject obj)
    {
        var rigbody = obj.AddComponent<Rigidbody>();
        return rigbody;
    }

    public void SetParent(Transform child, Transform parent)
    {
        var backup = parent.rotation;
        var backupPosition = parent.position;
        parent.rotation = Quaternion.identity;
        parent.position = Vector3.zero;
        child.parent = parent;
        parent.rotation = backup;
        parent.position = backupPosition;
    }

    public void RemoveChildrenFromParent(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }

    public float GetDiagonalLength(GameObject gameObject)
    {
        var catercorner = GetObjectCatercorner(gameObject);
        return Vector3.Distance(catercorner[0], catercorner[1]);
    }

    public Vector3 GetObjectSize(GameObject gameObject)
    {
        var catercorner = GetObjectCatercorner(gameObject);
        return new Vector3(Mathf.Abs(catercorner[0].x - catercorner[1].x), Mathf.Abs(catercorner[0].y - catercorner[1].y), Mathf.Abs(catercorner[0].z - catercorner[1].z));
    }

    public Vector3[] GetObjectCatercorner(GameObject gameObject)
    {
        var boundsPoints = new List<Vector3>();
        Vector3[] corners = null;
        Vector3[] rectTransformCorners = new Vector3[4];
        MeshFilter[] meshFilters = gameObject.GetComponentsInChildren<MeshFilter>();
        for (int i = 0; i < meshFilters.Length; i++)
        {
            var meshFilterObj = meshFilters[i];

            Bounds meshBounds = meshFilterObj.sharedMesh.bounds;
            GetCornerPositions(meshBounds, meshFilterObj.transform, ref corners);
            boundsPoints.AddRange(corners);
        }
        RectTransform[] rectTransforms = gameObject.GetComponentsInChildren<RectTransform>();
        for (int i = 0; i < rectTransforms.Length; i++)
        {
            rectTransforms[i].GetWorldCorners(rectTransformCorners);
            boundsPoints.AddRange(rectTransformCorners);
        }

        var max = boundsPoints[0];
        var min = boundsPoints[0];
        foreach (var point in boundsPoints)
        {
            max.x = Mathf.Max(max.x, point.x);
            max.y = Mathf.Max(max.y, point.y);
            max.z = Mathf.Max(max.z, point.z);

            min.x = Mathf.Min(min.x, point.x);
            min.y = Mathf.Min(min.y, point.y);
            min.z = Mathf.Min(min.z, point.z);
        }
        var array = new Vector3[] { max, min };
        return array;
    }

    public Vector3 GetMeshFilterCenter(GameObject gameObject)
    {
        var catercorner = GetObjectCatercorner(gameObject);
        return (catercorner[0] + catercorner[1]) * 0.5f;
    }

    public void GetCornerPositions(Bounds bounds, Transform transform, ref Vector3[] positions)
    {
        // Calculate the local points to transform.
        Vector3 center = bounds.center;
        Vector3 extents = bounds.extents;
        float leftEdge = center.x - extents.x;
        float rightEdge = center.x + extents.x;
        float bottomEdge = center.y - extents.y;
        float topEdge = center.y + extents.y;
        float frontEdge = center.z - extents.z;
        float backEdge = center.z + extents.z;

        // Allocate the array if needed.
        const int numPoints = 8;
        if (positions == null || positions.Length != numPoints)
        {
            positions = new Vector3[numPoints];
        }

        // Transform all the local points to world space.
        positions[LBF] = transform.TransformPoint(leftEdge, bottomEdge, frontEdge);
        positions[LBB] = transform.TransformPoint(leftEdge, bottomEdge, backEdge);
        positions[LTF] = transform.TransformPoint(leftEdge, topEdge, frontEdge);
        positions[LTB] = transform.TransformPoint(leftEdge, topEdge, backEdge);
        positions[RBF] = transform.TransformPoint(rightEdge, bottomEdge, frontEdge);
        positions[RBB] = transform.TransformPoint(rightEdge, bottomEdge, backEdge);
        positions[RTF] = transform.TransformPoint(rightEdge, topEdge, frontEdge);
        positions[RTB] = transform.TransformPoint(rightEdge, topEdge, backEdge);
    }
    
    public List<T2> Map<T1, T2>(List<T1> list, Func<T1, T2> func)
    {
        var newList = new List<T2>();
        foreach (var obj in list)
            newList.Add(func(obj));
        return newList;
    }

    public void DelayCall(MonoBehaviour obj, float seconds, Action func)
    {
        obj.StartCoroutine(DelayCallCoroutine(seconds, func));
    }

    private IEnumerator DelayCallCoroutine(float seconds, Action func)
    {
        yield return new WaitForSeconds(seconds);
        func();
    }

    public void FadeMaterial(MonoBehaviour obj, bool fadeAway, GameObject model, float seconds, Action callback)
    {
        obj.StartCoroutine(FadeMaterial(fadeAway, model, seconds, callback));
    }

    private IEnumerator FadeMaterial(bool fadeAway, GameObject model, float seconds, Action callback)
    {
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over n second backwards
            for (float i = seconds; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                var renderer = model.GetComponent<Renderer>();
                var material = renderer.material;
                var color = material.color;
                color.a = i / seconds;
                material.color = color;
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over n second
            for (float i = 0; i <= seconds; i += Time.deltaTime)
            {
                // set color with i as alpha
                var renderer = model.GetComponent<Renderer>();
                var material = renderer.material;
                var color = material.color;
                color.a = i / seconds;
                material.color = color;
                yield return null;
            }
        }
        if (callback != null)
            callback();
    }
}
