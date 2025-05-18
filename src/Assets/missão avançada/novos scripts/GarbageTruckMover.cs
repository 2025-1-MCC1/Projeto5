using UnityEngine;

public class GarbageTruckMover : MonoBehaviour
{
    public Transform[] waypoints;       // Array de pontos (Transform) que o caminhão vai seguir
    public float speed = 5f;            // Velocidade do caminhão
    private int currentWaypoint = 0;   // Índice do waypoint atual para onde o caminhão está indo
    private bool moving = false;        // Flag que indica se o caminhão está em movimento

    void Update()
    {
        // Se não estiver movendo ou se não houver waypoints definidos, não faz nada
        if (!moving || waypoints.Length == 0) return;

        // Ponto atual para onde o caminhão deve se mover
        Transform target = waypoints[currentWaypoint];

        // Direção normalizada do caminhão até o waypoint atual
        Vector3 direction = (target.position - transform.position).normalized;

        // Move o caminhão na direção do waypoint multiplicado pela velocidade e pelo tempo do frame
        transform.position += direction * speed * Time.deltaTime;

        // Rotaciona o caminhão suavemente para "olhar" na direção do movimento
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2f);

        // Verifica se o caminhão está perto o suficiente do waypoint (distância menor que 0.5)
        if (Vector3.Distance(transform.position, target.position) < 0.5f)
        {
            // Passa para o próximo waypoint
            currentWaypoint++;

            // Se chegou ao último waypoint, para o movimento e destrói o objeto (caminhão)
            if (currentWaypoint >= waypoints.Length)
            {
                moving = false; // Para o movimento
                Destroy(gameObject); // Destrói o caminhão
            }
        }
    }

    // Método público para iniciar o movimento do caminhão
    public void StartMoving()
    {
        currentWaypoint = 0;  // Reinicia para o primeiro waypoint
        moving = true;        // Começa o movimento
    }
}
