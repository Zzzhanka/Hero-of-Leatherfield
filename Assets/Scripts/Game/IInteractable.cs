public interface IInteractable
{
    void Interact();           // Вызывается при нажатии на кнопку
    void OnPlayerEnter();      // Вызывается, когда игрок входит в триггер
    void OnPlayerExit();       // Вызывается, когда игрок выходит из триггера
}
