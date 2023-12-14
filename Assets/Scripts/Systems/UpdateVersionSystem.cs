using TMPro;
using UnityEngine;

[ExecuteInEditMode]
public class UpdateVersionSystem : MonoBehaviour
{
    TMP_Text text;

    void Start()
    {
        text = GetComponent<TMP_Text>();
    }
    void Update()
    {
        if (text.text.Equals(Application.version)) return;
        text.text = Application.version;
    }
}
