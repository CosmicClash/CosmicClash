using UnityEngine;
using System.Collections;

public class TimerScript : MonoBehaviour
{
	public static bool	_On = false;
	public static float	_MaxTime;
	public static float	_TimeLeft;
//	{
//		get
//		{
//			if
//		}
//	}
	public static float	_TimeElapsed;
	public static float _TimeStartedAt;

	public static void _Initialize (float maxTimeSeconds, bool on)
	{
		_On = on;
		_TimeStartedAt = Time.time;
		float f = _GetTimeLeft();
		_MaxTime = maxTimeSeconds;
	}
	public static float _GetTimeElapsed ()
	{
		_TimeElapsed = Time.time - _TimeStartedAt;
		if (_TimeElapsed > _MaxTime)
		{
			_On = false;
			_TimeElapsed = 0.0f;
			_TimeLeft = _MaxTime;
			return 0.0f;
		}
		else return Mathf.Floor(_TimeElapsed);
	}
	public static float _GetTimeLeft ()
	{
		_TimeLeft = _MaxTime - _GetTimeElapsed ();
		return _TimeLeft;
	}
}
/*

string _myProperty;

public string MyProperty
{
    get { return _myProperty; }
    set { _myProperty = value; }
}
Now you can add code that validates the value in your setter:

set
{
    if (string.IsNullOrWhiteSpace(value))
        throw new ArgumentNullException();

    _myProperty = value;
}
Properties can also have different accessors for the getter and the setter:

public string MyProperty { get; private set; }
This way you create a property that can be read by everyone but can only be modified by the class itself.

You can also add a completely custom implementation for your getter:

public string MyProperty
{
    get
    {
        return DateTime.Now.Second.ToString();
    }
}

 */