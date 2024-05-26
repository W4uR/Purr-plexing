using UnityEngine;

public class BreezeAudioPlayer : MonoBehaviour
{
    [SerializeField]
    private AudioSource leftBreezeSource;
    [SerializeField]
    private AudioSource rightBreezeSource;
    [SerializeField]
    private AnimationCurve volumeFunction;

    private void FixedUpdate()
    {
        HandleBreezeAudio();
        HandleBreezeDisplay();
    }


    private void HandleBreezeAudio()
    {
        leftBreezeSource.volume = volumeFunction.Evaluate(WallDistanceToDirection(RelativeDirection.LEFT)); ;
      //  leftBreezeSource.pitch = leftBreezeSource.volume;
        if (leftBreezeSource.volume != 0f)
        {
            if (!leftBreezeSource.isPlaying)
                leftBreezeSource.Play();
        }
        else
        {
            leftBreezeSource.Pause();
        }

        rightBreezeSource.volume = volumeFunction.Evaluate(WallDistanceToDirection(RelativeDirection.RIGHT));
      //  rightBreezeSource.pitch = rightBreezeSource.volume;
        if (rightBreezeSource.volume != 0f)
        {
            if (!rightBreezeSource.isPlaying)
                rightBreezeSource.Play();
        }
        else
        {
            rightBreezeSource.Pause();
        }
    }

    private void HandleBreezeDisplay()
    {
        BreezeDisplay.DisplayLeft(leftBreezeSource.volume);
        BreezeDisplay.DisplayRight(rightBreezeSource.volume);
    }

    public float WallDistanceToDirection(RelativeDirection direction)
    {
        Ray ray;
        switch (direction)
        {
            case RelativeDirection.FORWARD:
                ray = new Ray(transform.position, transform.forward);
                break;
            case RelativeDirection.RIGHT:
                ray = new Ray(transform.position, transform.right);
                break;
            case RelativeDirection.BACK:
                ray = new Ray(transform.position, -transform.forward);
                break;
            case RelativeDirection.LEFT:
            default:
                ray = new Ray(transform.position, -transform.right);
                break;
        }
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        return hit.distance;

    }
}
