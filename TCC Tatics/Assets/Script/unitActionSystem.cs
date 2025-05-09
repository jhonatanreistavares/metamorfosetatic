using UnityEngine;

public class unitActionSystem : MonoBehaviour
{
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;


    private void Update()
    {
        

        if(Input.GetMouseButtonDown(0))
        {
            if(TryHandleUnitSelection()) return;
            selectedUnit.Move(MouseWorld.GetPosition());
        }
    }


    private bool TryHandleUnitSelection()
    {
        //Iniciando o raycast pegando a posição da camera. 
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Aqui traduzimos as coordenadas do mouse apenas para o chão, fazendo assim o raycast identificar apenas a layer Floor
        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitLayerMask))
        {
            //utilizando o TryGetComponent conseguimos identificar os coliders das unidades em jogo utilizando uma layer e um collider
            if(raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                selectedUnit = unit;
                return true;
            }
        }
        return false;
    }
    
}
