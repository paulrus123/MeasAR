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
    [SerializeField] PointAndLinePlacementController pointAndLinePlacementController = default;
    [SerializeField] CrossHairController crossHairController = default;

    private void Start()
    {
        addNewPointButton.onClick.AddListener(AddNewPointButtonClicked);
        finishLineButton.onClick.AddListener(FinishLineButtonClicked);
        deletePointButton.onClick.AddListener(DeletePointButtonClicked);
    }

    private void Update()
    {
        deletePointButton.gameObject.SetActive(crossHairController.isHoveringOverPoint);
        finishLineButton.gameObject.SetActive(pointAndLinePlacementController.CheckIfAnyPointIsActive());
    }

    private void OnDestroy()
    {
        addNewPointButton.onClick.RemoveListener(AddNewPointButtonClicked);
        finishLineButton.onClick.RemoveListener(FinishLineButtonClicked);
        deletePointButton.onClick.RemoveListener(DeletePointButtonClicked);
    }

    private void DeletePointButtonClicked()
    {
        if(crossHairController.isHoveringOverPoint)
        {
            pointAndLinePlacementController.RemovePoint(crossHairController.hoveredOverPoint);
        }
    }

    private void FinishLineButtonClicked()
    {
        pointAndLinePlacementController.FinishCurrentLine();
    }

    private void AddNewPointButtonClicked()
    {
        pointAndLinePlacementController.AddNewPoint();
    }
}
