using TMPro;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private BombSpawner _bombSpawwner;
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private TextMeshProUGUI _textBomb;
    [SerializeField] private TextMeshProUGUI _textCube;
    [SerializeField] private TextMeshProUGUI _activeObjectCountText;

    private int _currentBombCounter = 0;
    private int _currentCubeCounter = 0;
    private int _activeObjectCounter = 0;

    private void Update()
    {
        _activeObjectCounter = _cubeSpawner.ObjectCounterNumber() + _bombSpawwner.ObjectCounterNumber();

        if (_currentBombCounter != _bombSpawwner.ObjectCounterNumber() || _currentCubeCounter != _cubeSpawner.ObjectCounterNumber())
        {
            ShowText();
        }
    }

    private void ShowText()
    {
        _textBomb.text = "Cчетчик бомб " + _bombSpawwner.ObjectCounterNumber().ToString("");
        _textCube.text = "Cчетчик кубов " + _cubeSpawner.ObjectCounterNumber().ToString("");
        _activeObjectCountText.text = "Cчетчик активных объектов " + _activeObjectCounter.ToString("");
        _currentBombCounter = _bombSpawwner.ObjectCounterNumber();
        _currentCubeCounter = _cubeSpawner.ObjectCounterNumber();        
    }
}
