using System;
using UnityEngine;
using UnityEngine.UI;

/*
 * ButtonUIHandler is responsible for listening for button press events and calling the appropriate 
 * function in the PointAndLinePlacementController to add, remove, or finish a line/point.
 */
public class ButtonUIHandler : MonoBehaviour
{
    [SerializeField] Button addNewPointButton = default;
    [SerializeField] Button finishLineButton = default;
    [SerializeField] Button deletePointButton = default;
    [SerializeField] Button movePointButton = default;
    [SerializeField] Button placePointButton = default;
    [SerializeField] Button trashButton = default;
    [SerializeField] PointAndLinePlacementController pointAndLinePlacementController = default;
    [SerializeField] CrossHairController crossHairController = default;


    public enum State { DEFAULT, PLACING_NEXT_POINT, HOVERING_OVER_POINT, MOVING_POINT};
    public State state = State.DEFAULT;

    private void Start()
    {
        addNewPointButton.onClick.AddListener(AddNewPointButtonClicked);
        finishLineButton.onClick.AddListener(FinishLineButtonClicked);
        deletePointButton.onClick.AddListener(DeletePointButtonClicked);
        movePointButton.onClick.AddListener(MovePointButtonClicked);
        placePointButton.onClick.AddListener(PlacePointButtonClicked);
        trashButton.onClick.AddListener(TrashButtonClicked);
    }

    private void OnDestroy()
    {
        addNewPointButton.onClick.RemoveListener(AddNewPointButtonClicked);
        finishLineButton.onClick.RemoveListener(FinishLineButtonClicked);
        deletePointButton.onClick.RemoveListener(DeletePointButtonClicked);
        movePointButton.onClick.RemoveListener(MovePointButtonClicked);
        placePointButton.onClick.RemoveListener(PlacePointButtonClicked);
        trashButton.onClick.RemoveListener(TrashButtonClicked);
    }


    private void Update()
    {
        CheckState();
        UpdateUI();
    }

    private void CheckState()
    {
        switch (state)
        {
            case State.DEFAULT:
                if (crossHairController.isHoveringOverPoint)
                    state = State.HOVERING_OVER_POINT;
                break;
            case State.PLACING_NEXT_POINT:
                break;
            case State.HOVERING_OVER_POINT:
                if (!crossHairController.isHoveringOverPoint)
                    state = State.DEFAULT;
                break;
            case State.MOVING_POINT:
                break;
        }
    }

    private void UpdateUI()
    {
        switch (state) { 
            case State.DEFAULT:
                addNewPointButton.gameObject.SetActive(true);
                finishLineButton.gameObject.SetActive(false);
                deletePointButton.gameObject.SetActive(false);
                movePointButton.gameObject.SetActive(false);
                placePointButton.gameObject.SetActive(false);
                break;
            case State.PLACING_NEXT_POINT:
                addNewPointButton.gameObject.SetActive(true);
                finishLineButton.gameObject.SetActive(true);
                deletePointButton.gameObject.SetActive(false);
                movePointButton.gameObject.SetActive(false);
                placePointButton.gameObject.SetActive(false);
                break;
            case State.HOVERING_OVER_POINT:
                addNewPointButton.gameObject.SetActive(true);
                finishLineButton.gameObject.SetActive(false);
                deletePointButton.gameObject.SetActive(true);
                movePointButton.gameObject.SetActive(true);
                placePointButton.gameObject.SetActive(false);
                break;
            case State.MOVING_POINT:
                addNewPointButton.gameObject.SetActive(false);
                finishLineButton.gameObject.SetActive(false);
                deletePointButton.gameObject.SetActive(false);
                movePointButton.gameObject.SetActive(false);
                placePointButton.gameObject.SetActive(true);
                break;
        }
    }

    private void DeletePointButtonClicked()
    {
        if(state == State.HOVERING_OVER_POINT)
        {
            pointAndLinePlacementController.RemovePoint(crossHairController.hoveredOverPoint);
        }
    }

    private void FinishLineButtonClicked()
    {
        pointAndLinePlacementController.FinishCurrentLine();
        state = State.DEFAULT;
    }

    private void AddNewPointButtonClicked()
    {
        pointAndLinePlacementController.AddNewPoint();
        state = State.PLACING_NEXT_POINT;
    }

    void MovePointButtonClicked()
    {
        if (state == State.HOVERING_OVER_POINT)
        {
            pointAndLinePlacementController.PickUpPoint(crossHairController.hoveredOverPoint);
            state = State.MOVING_POINT;
        }
    }

    private void PlacePointButtonClicked()
    {
        pointAndLinePlacementController.PlacePickedUpPoint();
        state = State.DEFAULT;
    }

    private void TrashButtonClicked()
    {
        pointAndLinePlacementController.DeleteAllPoints();
    }
}
