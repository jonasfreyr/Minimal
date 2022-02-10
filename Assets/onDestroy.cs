using UnityEngine;

public class onDestroy : MonoBehaviour
{
    private void OnDestroy() {Destroy(transform.parent.gameObject);}
}
