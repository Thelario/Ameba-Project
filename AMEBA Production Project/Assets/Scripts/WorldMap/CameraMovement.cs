using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private const string mouseWheelName = "Mouse ScrollWheel";
    private const float cameraMoveSpeed = 500f;

    // The limits need to be changed if we want to increase the size of the camera
    private const float upperLimit = -12.65f;
    private const float lowerLimit = 10f;

    private float direction;

    private CanvasManager cm;

    public RectTransform body;

    private void Start()
    {
        cm = CanvasManager.GetInstance();
    }

    void Update()
    {
        if (cm.lastActiveCanvas.canvasType == CanvasType.WorldMapMenu)
        {
            direction = Input.GetAxis(mouseWheelName);

            if (direction > 0f)
            {
                //Debug.Log("Moving UpWards");
                if (body.position.y > upperLimit)
                {
                    //Debug.Log(body.position.y);
                    body.Translate(Vector2.up * -direction * cameraMoveSpeed * Time.deltaTime);
                }
            }
            else if (direction < 0f)
            {
                //Debug.Log("Moving DownWards");
                if (body.position.y < lowerLimit)
                {
                    //Debug.Log(body.position.y);
                    body.Translate(Vector2.up * -direction * cameraMoveSpeed * Time.deltaTime);
                }
            }

            /* POSIBLE CÓDIGO PARA IMPLEMENTAR CON FLECHAS Y W,S, PERO NO TIENE SENTIDO PORQUE TODO EL JUEGO SE VA A JUGAR CON EL RATÓN */
            /*
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                direction = 1f;

                if (transform.position.y < upperLimit)
                {
                    transform.Translate(Vector2.up * direction * cameraMoveSpeed * Time.deltaTime);
                }
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                direction = -1f;

                if (transform.position.y > lowerLimit)
                {
                    transform.Translate(Vector2.up * direction * cameraMoveSpeed * Time.deltaTime);
                }
            }
            */
        }
    }
}
