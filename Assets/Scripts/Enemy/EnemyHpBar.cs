using UnityEngine;

public class EnemyHpBar : MonoBehaviour
{
    private void LateUpdate()
    {
        if (Camera.main != null)
        {
            transform.LookAt(transform.position + Camera.main.transform.forward);
            transform.rotation = Quaternion.Euler(90, transform.rotation.eulerAngles.y, 0);
        }
    }
}
