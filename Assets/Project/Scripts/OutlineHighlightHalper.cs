﻿using System.Linq;
using UnityEngine;

public static class OutlineHighlightHalper
{
    public static IHighlightable Current => current;
    private static bool IsHighlighting { get => _IsHighlighting; set => _IsHighlighting = value; }

    private static IHighlightable current;
    private static bool _IsHighlighting = true;

    public static void Switch(IHighlightable newObject, GameObject sender)
    {
        if (current == newObject) return;
        StopHighlightSelectedObject(sender);
        current = newObject;
        StartHighlightSelectedObject(sender);
    }
    private static GameObject[] GetGameObjectsNearMouse(float radius, Vector2 mousePosition, LayerMask mask)
    {

        Collider2D[] hitCollidersNearMouse = Physics2D.OverlapCircleAll(mousePosition, radius, mask);

        return hitCollidersNearMouse.Select(x => x.gameObject).ToArray();
    }

    private static GameObject[] GetGameObjectsNearPlayer(float radius, Vector2 playerPosition, LayerMask mask)
    {
        Collider2D[] hitCollidersNearPlayer = Physics2D.OverlapCircleAll(playerPosition, radius, mask);

        return hitCollidersNearPlayer.Select(x => x.gameObject).ToArray();
    }

    private static GameObject[] GetGameObjectsNearMouseAndPlayer(float radiusNearMouse, float radiusNearPlayer, Vector2 playerPosition, Vector2 mousePosition, LayerMask mask)
    {
        var nearMouse = GetGameObjectsNearMouse(radiusNearMouse, mousePosition, mask);
        var nearPlayer = GetGameObjectsNearPlayer(radiusNearPlayer, playerPosition, mask);

        return nearMouse.Intersect(nearPlayer).ToArray();
    }

    private static GameObject[] FilterByType<T>(GameObject[] objects)
    {
        return objects.Where(x => x.TryGetComponent<T>(out _)).ToArray();
    }
    private static GameObject[] FilterByDistane(GameObject[] objects, Vector2 position)
    {
        return objects.OrderBy(x => Vector2.Distance(x.transform.position, position)).ToArray();
    }

    public static void SelectObjectNearMouseAndPlayer(Vector2 playerPosition, Vector2 mousePosition, float searchRadiusNearPlayer, float searchRadiusNearMouse, LayerMask mask, GameObject sender)
    {
        var nearPlayerAndMouse = GetGameObjectsNearMouseAndPlayer(searchRadiusNearMouse, searchRadiusNearPlayer, playerPosition, mousePosition, mask);
        if (nearPlayerAndMouse.Count() < 1)
        {
            StopHighlightSelectedObject(sender);
            return;
        }

        var filteredByType = FilterByType<IHighlightable>(nearPlayerAndMouse);
        var filteredByDistance = FilterByDistane(filteredByType, mousePosition);

        var NearestToMouse = filteredByDistance.First();
        var newInteractable = NearestToMouse.GetComponent<IHighlightable>();

        Switch(newInteractable, sender);
    }

    private static void StopHighlightSelectedObject(GameObject sender)
    {
        if (current == null) return;

        current?.StopHighLight(sender);
        _IsHighlighting = false;
    }

    private static void StartHighlightSelectedObject(GameObject sender)
    {
        if (current == null) return;

        current?.StartHighlight(sender);
        _IsHighlighting = true;
    }
}
