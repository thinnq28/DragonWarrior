using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectionArrow : MonoBehaviour
{
    //Các option buttons
    [SerializeField] private RectTransform[] buttons;
    //Âm thanh khi di chuyển mũi tên lên hoặc xuống
    [SerializeField] private AudioClip changeSound;
    //Âm thanh khi chọn một button nào đó
    [SerializeField] private AudioClip interactSound;
    private RectTransform arrow;
    private int currentPosition;

    private void Awake()
    {
        arrow = GetComponent<RectTransform>();
    }
    private void OnEnable()
    {
        currentPosition = 0;
        ChangePosition(0);
    }
    private void Update()
    {
        //Change the position of the selection arrow
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            //mảng bắt đầu từ 0 nên khi muốn di chuyển lên trên phải -1
            ChangePosition(-1);
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            ChangePosition(1);

        //Interact with current option
        if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
            Interact();
    }

    private void ChangePosition(int _change)
    {
        //thay đổi vị trí hiện tại
        currentPosition += _change;

        if (_change != 0)
            SoundManager.instance.PlaySound(changeSound);

        if (currentPosition < 0)
            currentPosition = buttons.Length - 1;
        else if (currentPosition > buttons.Length - 1)
            currentPosition = 0;

        AssignPosition();
    }
    private void AssignPosition()
    {
        //Gán vị trí Y của button hiện tại cho mũi tên (về cơ bản là di chuyển nó lên và xuống)
        arrow.position = new Vector3(arrow.position.x, buttons[currentPosition].position.y);
    }
    private void Interact()
    {
        SoundManager.instance.PlaySound(interactSound);

        //Access the button component on each option and call its function
        buttons[currentPosition].GetComponent<Button>().onClick.Invoke();
    }
}
