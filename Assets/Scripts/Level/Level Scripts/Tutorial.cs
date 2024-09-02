using System;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Awaitable;

public class Tutorial : MonoBehaviour
{
    private Transform _player;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        Introduction();
    }


    private Bindings bindings;

    private void Awake()
    {
        bindings = new Bindings();
    }

    private void OnEnable()
    {
        bindings.Player.MoveForward.performed += CheckWallBump;
        bindings.Player.MoveForward.Enable();
    }

    private void OnDisable()
    {
        bindings.Player.MoveForward.performed -= CheckWallBump;
        bindings.Player.MoveForward.Disable();
    }

    Coroutine wallDialog = null;
    bool moveTutorial = false;
    void CheckWallBump(InputAction.CallbackContext context)
    {
        Debug.Log(context);
        if (context.ReadValue<float>() > .5f)
        {
            Debug.Log("Check wall bump");
            Vector3 endPosition = _player.position + _player.forward;
            if (LevelManager.CurrentLevel.GetCell(endPosition) == null && wallDialog == null && moveTutorial == true)
                 wallDialog = StartCoroutine(Narrator.PlayAudioClip("tutorial.6"));
        }
    }

    async void Introduction()
    {
        // Welcome player
        for (int i = 1; i <= 3; i++)
        {
            await Narrator.PlayAudioClip("tutorial." + i);
            await WaitForSecondsAsync(1.4f);
        }

        // Introduce cats and cat calling
        await Narrator.PlayAudioClip("tutorial.4");
        while (!Input.GetKeyDown(KeyCode.Space)){
            await NextFrameAsync(); 
        }
        await WaitForSecondsAsync(3f);

        // Explain movement
        await Narrator.PlayAudioClip("tutorial.5");
        var currentPos = _player.position;
        var currentRot = _player.rotation;
        while (currentPos == _player.position && currentRot == _player.rotation){
            await NextFrameAsync();
        }
        moveTutorial = true;

        // Wait for breeze in right ear
        while(Physics.Raycast(_player.position,_player.right,1f)){
            await NextFrameAsync();
        }

        // Introduce breeze sfx
        await Narrator.PlayAudioClip("tutorial.7");


        // Wait for capturing a cat then play tutorial.8
        while(_player.GetComponentInChildren<Cat>() == null){
            await NextFrameAsync();
        }

        await WaitForSecondsAsync(.7f);
        await Narrator.PlayAudioClip("tutorial.8");

    }

    public static IEnumerator LevelFinished()
    {
        yield return Narrator.PlayAudioClip("tutorial.9");
        yield return new WaitForSeconds(0.2f);
        Narrator.Clear();
    }

}
