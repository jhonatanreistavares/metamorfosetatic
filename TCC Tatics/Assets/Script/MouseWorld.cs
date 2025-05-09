using UnityEngine;

public class MouseWorld : MonoBehaviour
{
    [SerializeField] private LayerMask mousePlaneLayerMask;
    private static MouseWorld instance;

    private void Awake()
    {
        instance = this;
    }

    void Update()
    {
        transform.position = MouseWorld.GetPosition();
    }

    public static Vector3 GetPosition()
    {
       //Iniciando o raycast pegando a posição da camera. 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Aqui traduzimos as coordenadas do mouse apenas para o chão, fazendo assim o raycast identificar apenas a layer Floor
        Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, instance.mousePlaneLayerMask);
        return raycastHit.point;
    }
}
