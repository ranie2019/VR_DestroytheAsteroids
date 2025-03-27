using UnityEngine;

public class ExitGame : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisão detectada! Fechando o jogo...");
        Application.Quit();

        // Para funcionar no editor do Unity
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
