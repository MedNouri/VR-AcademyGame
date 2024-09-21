using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Elevator : MonoBehaviour
{
    private AllowTeleportWhileAttachedToHand _allowTeleport = null;
    
    [Header( "Audio Clips" )]
    public AudioClip DoorOpenCloseClip;
    public AudioClip ElevatoAudioClipr;
   
    
    public AudioSource AudioSourceElevetor;
    public AudioSource AudioBAckgroundMusic;
    private bool _close = true;
    private Transform _curentPostion;
    private bool _dawn;
    private Vector3 _doorLeftclose;
    private Vector3 _doorRightclose;
    private Vector3 _floorZeroPostion;
    private bool _isActionPerformedDown;
    private bool _isActionPerformedUp;
    private readonly float _speed = 20f;
    private float _step;

    private bool _up;


    // Door Parmes

    public GameObject DoorLeft;

    public Vector3 DoorLeftopen;
    public GameObject DoorRight;
    public Vector3 DoorRightopen;

    public List<EventRelayFirstLevel.EventMessageType> EventHandeld =
        new List<EventRelayFirstLevel.EventMessageType>();

    public Transform FloorOnePostion;
    public GameObject Player;


    public void ElevatorSound()
    {
        Debug.Log("Play Music called");

        if ( AudioBAckgroundMusic.isPlaying)
            AudioBAckgroundMusic.Stop();
        else
            AudioBAckgroundMusic.Play();
    }


    private IEnumerator GetUp()
    {
        Debug.Log("player is here");
        EventRelayFirstLevel.RelayEvent(EventRelayFirstLevel.EventMessageType.ValidTask);
        CloseDoors();
        yield return new WaitForSeconds(5f);
        AudioSourceElevetor.clip = ElevatoAudioClipr;
        AudioSourceElevetor.Play();
        AudioSourceElevetor.loop = true;
        EventRelayFirstLevel.RelayEvent(EventRelayFirstLevel.EventMessageType.Talkinfo2);
        Go.to(transform, _speed, new GoTweenConfig().position(FloorOnePostion.position).setEaseType(GoEaseType.CircInOut)
        ).setOnCompleteHandler(c => GetInDestitionUp());
    }

    public void GetUpButton()
    {
        if (!_isActionPerformedUp && !_up)
        {
      
            StartCoroutine(GetUp());
            _isActionPerformedUp = true;
        }
        else
        {
            Debug.Log("Action is Already Performed Pleas wait ");
        }
    }

    private void GetInDestitionUp()
    {
        //hand.controller.TriggerHapticPulse( 500 );

        AudioSourceElevetor.Pause();
        AudioSourceElevetor.loop = false;
        OpenDoors();

        Debug.Log("we are in floor 1 ");
        EventRelayFirstLevel.RelayEvent(EventRelayFirstLevel.EventMessageType.GetOutOfElvator);
        _up = true;

        GetComponent<Collider>().enabled = false;
    }


    private void GetInDestitionDown()
    {
        AudioSourceElevetor.Pause();
        AudioSourceElevetor.loop = false;
        // chek for Th ecurrent Postion
        if (_dawn)
        {
            OpenDoors();
            Debug.Log("we are in Floor 0 ");
        }
    }


    public void GetDownButton()
    {
        StartCoroutine(GetDown());
    }


    private IEnumerator GetDown()
    {
        if (!_isActionPerformedDown && !_dawn)
        {
            _isActionPerformedDown = true;
            CloseDoors();
            yield return new WaitForSeconds(8f);
            AudioSourceElevetor.clip = ElevatoAudioClipr;
            AudioSourceElevetor.Play();
            AudioSourceElevetor.loop = false;
            Go.to(transform, 20f, new GoTweenConfig().position(_floorZeroPostion)
            ).setOnCompleteHandler(c => GetInDestitionDown());
        }
    }


    public void OpenDoors()
    {
        if (_close)
        {
            Debug.Log("Opining Doors");

            StartCoroutine(OpenDoor());
            AudioSourceElevetor.clip = DoorOpenCloseClip;
            AudioSourceElevetor.Play();
            AudioSourceElevetor.loop = false;
        }
    }


    private IEnumerator OpenDoor()
    {
        yield return new WaitForSeconds(1f);
        DoorLeft.transform.localPositionTo(3f, DoorLeftopen);
        DoorRight.transform.localPositionTo(3f, DoorRightopen);
    }


    private IEnumerator CloseDoorAction()
    {
        yield return new WaitForSeconds(1f);
        DoorLeft.transform.localPositionTo(3f, _doorLeftclose);
        DoorRight.transform.localPositionTo(3f, _doorRightclose);
    }


    public void CloseDoors()
    {
        if (!_close)
        {
            Debug.Log("Closing Door");

            StartCoroutine(CloseDoorAction());
            AudioSourceElevetor.clip = DoorOpenCloseClip;
            AudioSourceElevetor.Play();
        }
    }


    private void Start()
    {
        _doorLeftclose = DoorLeft.transform.localPosition;

        _doorRightclose = DoorRight.transform.localPosition;
        _floorZeroPostion = transform.position;
        _isActionPerformedUp = false;
    }

    private void OnEnable()
    {
       

        EventRelayFirstLevel.OnEventAction += HandleEvent;
    }


    private void OnDisable()
    {
        EventRelayFirstLevel.OnEventAction -= HandleEvent;
    }


    private string HandleEvent(EventRelayFirstLevel.EventMessageType type)

    {
        if (EventHandeld.Contains(type))
        {
            OpenDoors();
            Debug.Log("Event Is recived" + type);
        }

        return "";
    }

    private void OnTriggerEnter(Collider other)
    {
         

        if (other.gameObject.transform.CompareTag("Robot") )
        {
            if ( other.transform.parent != gameObject.transform){
            SetParent(transform, other.gameObject);
            Debug.Log("Robot is my child ");
        }
        }

        if (other.gameObject.transform.root.CompareTag("Player") || other.gameObject.name == "HeadCollider")
        {
            SetParent(transform, Player);
 
            EventRelayFirstLevel.RelayEvent(EventRelayFirstLevel.EventMessageType.Talkinfo1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Elvetor Tirgger Exi t Objects " + other.name);

        if (other.gameObject.CompareTag("Robot") && other.transform.parent == gameObject.transform)
            SetParent(null, other.gameObject);

        if (other.gameObject.CompareTag("Player") || other.gameObject.name == "HeadCollider")
        {
            AudioSourceElevetor.Stop();
            SetParent(null, Player);
        }
    }

    public void SetParent(Transform newParent, GameObject gameObject)
    {
        //Same as above, except this makes the player keep its local orientation rather than its global orientation.
        gameObject.transform.SetParent(newParent);
    }

    public void SetParentPlayer(Transform newParent, GameObject gameObject)
    {
    }


    private void FixedUpdate()
    {
        if (DoorLeft.transform.localPosition == _doorLeftclose)
            _close = true;
        else if (DoorLeft.transform.localPosition == DoorLeftopen) _close = false;

        if (transform.position == _floorZeroPostion)
        {
        }

        {
            _up = false;
            _dawn = true;
            _isActionPerformedDown = false;
        }
        if (transform.position == FloorOnePostion.transform.position)
        {
            _dawn = true;


            
        }
    }
}