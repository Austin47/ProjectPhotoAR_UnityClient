using Domain.LayoutHandlerService;
using UnityEngine;
using Zenject;

public class BackButton : MonoBehaviour
{
    private ILayoutHandler layoutHandler;

    [Inject]
    private void Construct(ILayoutHandler layoutHandler)
    {
        this.layoutHandler = layoutHandler;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            layoutHandler.GoBack();
            return;
        }
    }
}
