using System.Collections.Generic;
using UnityEngine;

public class WebrequestComponent : MonoBehaviour
{
    public bool logging;

    [HideInInspector] public List<Request> requests = new List<Request>();
    [HideInInspector] public Dictionary<string, string> responses = new Dictionary<string, string>();
    [HideInInspector] public Dictionary<string, Response> rawResponses = new Dictionary<string, Response>();
}