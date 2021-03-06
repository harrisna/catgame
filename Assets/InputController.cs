﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public enum InputButton { Left = 0, Right = 1, Up = 2, Down = 3, Jump = 4, Attack = 5 };

public class InputController : MonoBehaviour {

	private static readonly KeyCode[,] keymap = { 
		{ KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S, KeyCode.Space, KeyCode.F }, 
		{ KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow, KeyCode.DownArrow, KeyCode.RightShift, KeyCode.RightAlt }
	};

    //private static readonly int[,] buttonmap = { { }, { } };

    [SerializeField] private static bool[,] buttonState = {{ false, false, false, false, false, false }, { false, false, false, false, false, false } };

    private SerialPort serial;

    void Start() {
        serial = new SerialPort("/dev/tty.usbmodem1421", 9600);
        serial.Open();
    }


    void Update() {
        while (serial.BytesToRead > 0) {
            buttonState[serial.ReadByte(), serial.ReadByte()] = (serial.ReadByte() == 1);    
        }
    }

    public static bool GetButton(int player, InputButton b) {
		return Input.GetKey(keymap[player - 1, (int)b]) || buttonState[player - 1, (int)b];
	}

	public static float GetAxis(int player) {
		float result = 0.0f;

		if (Input.GetKey(keymap[player - 1, (int)InputButton.Left]) || buttonState[player - 1, (int)InputButton.Left])
			result += -1.0f;
		if (Input.GetKey(keymap[player - 1, (int)InputButton.Right]) || buttonState[player - 1, (int)InputButton.Right])
			result += 1.0f;

		return result;
	}
}
