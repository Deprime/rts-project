using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UnitSelected : MonoBehaviour
{
    [SerializeField] private Texture _selectTexture;

    private Ray _ray;
    private RaycastHit _rayHit;
    private Vector3 _mouseStartPosition; //Начинаем выделение
    private Vector3 _selectionStartPoint; //Начало и конец выделения на игровом поле
    private Vector3 _selectionEndPoint;
    private RtsGameController units; //Ссылка на наш скрипт
    private float _mouseX; //Конечные координаты выделения
    private float _mouseY; //Конечные координаты выделения
    private float _selectionHeight; //Ширина и высота квадрата выделения
    private float _selectionWidth;
    private bool _selecting; //Проверка на удержание кнопки

    void Start()
    {
        units = GetComponent<RtsGameController>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _selecting = true;
            _mouseStartPosition = Input.mousePosition;

            _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(_ray, out _rayHit))
            {
                _selectionStartPoint = _rayHit.point;
            }
        }

        _mouseX = Input.mousePosition.x;
        _mouseY = Screen.height - Input.mousePosition.y;
        _selectionWidth = _mouseStartPosition.x - _mouseX;
        _selectionHeight = Input.mousePosition.y - _mouseStartPosition.y;

        if (Input.GetMouseButtonUp(0))
        {
            _selecting = false;
            DeselectAll();

            if(_mouseStartPosition == Input.mousePosition)
            {
                SingleSelect();
            }

            else
            {
                _ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(_ray, out _rayHit))
                {
                    _selectionEndPoint = _rayHit.point;
                }

                SelectHightLighet();
            }
        }
    }

    private void OnGUI()
    {
        if (_selecting)
        {
            GUI.DrawTexture(new Rect(_mouseX, _mouseY, _selectionWidth, _selectionHeight), _selectTexture);
        }
    }

    private void SelectHightLighet()
    {
        foreach(GameObject unit in units.units)
        {
            float x = unit.transform.position.x;
            float z = unit.transform.position.z;

            if((x > _selectionStartPoint.x && x < _selectionEndPoint.x) || (x < _selectionStartPoint.x && x > _selectionEndPoint.x))
            {
                if ((z > _selectionStartPoint.z && z < _selectionEndPoint.z) || (z < _selectionStartPoint.z && z > _selectionEndPoint.z))
                {
                    unit.GetComponent<MoveRayCast>().isSelected = true;
                }
            }
        }
    }

    private void SingleSelect()
    {
        if (_rayHit.collider.gameObject.tag == "Player")
        {
            _rayHit.collider.gameObject.GetComponentInParent<MoveRayCast>().isSelected = true;
        }
    }

    private void DeselectAll()
    {
        foreach (GameObject unit in units.units)
        {
            unit.GetComponent<MoveRayCast>().isSelected = false;
        }
    }
}
