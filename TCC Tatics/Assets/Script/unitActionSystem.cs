using UnityEngine;
using System;

public class UnitActionSystem : MonoBehaviour
{

    public static UnitActionSystem Instance{get; private set; }
    public event EventHandler OnSelectedUnitChanged;
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitLayerMask;

    private void Awake()
    {
       if (Instance != null)
       {
            Debug.LogError("Tem mais de uma UnitActionSystem!"+ transform + "-" + Instance);
            Destroy(gameObject);
            return;
       }
       Instance = this;
    }
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
                SetSelectedUnit(unit);
                return true;
            }
        }
        return false;
    }

    private void SetSelectedUnit(Unit unit)
    {
        selectedUnit = unit;
        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
    }
    public Unit GetSelectedUnit()
    {
        return selectedUnit;
    }
}
