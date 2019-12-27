﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : Menu {

    public override void PressSelectedButton() {

        switch(ElementIndex) {
            // RETRY
            case 0:
                SceneManager.LoadScene("Demo");
                break;
            // TITLE SCREEN
            case 1:
                SceneManager.LoadScene("Title Screen");
                break;
            default:
                break;
        }
    }
}
