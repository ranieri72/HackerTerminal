using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hacker : MonoBehaviour
{
    // Game configuration data
    readonly int maximumAttempts = 4;
    readonly string[] sixLetterWords = {
        "budget",
"beyond",
"better",
"camera",
"cancer",
"chance",
"center",
"career",
"market",
"expert",
"modern",
"memory",
"nearly",
"parent",
    };

    readonly string[] sevenLetterWords = {
        "describe",
"anything",
"security",
"indicate",
"approach",
"Democrat",
"daughter",
"decision",
"pressure",
"increase",
"industry",
"director",
"national"
    };

    readonly string[] elevenLetterWords = {
        "information",
        "interesting",
        "institution",
        "participant",
        "environment",
        "development",
        "significant",
        "traditional",
        "performance",
        "opportunity"
    };

    // Game state
    string[] levelLetterWords;
    string password;
    List<int> attemptedPasswords;
    List<int> correctPositions;
    enum Screen { MainMenu, Password, Win };
    Screen currentScreen;

    // Start is called before the first frame update
    void Start()
    {
        attemptedPasswords = new List<int>();
        correctPositions = new List<int>();
        ShowMainMenu();
    }

    void ShowMainMenu()
    {
        currentScreen = Screen.MainMenu;
        attemptedPasswords.Clear();
        correctPositions.Clear();
        Terminal.ClearScreen();
        Terminal.WriteLine("What would you like to hack into?");
        Terminal.WriteLine("Press 1 for the local library");
        Terminal.WriteLine("Press 2 for the police station");
        Terminal.WriteLine("Press 3 for CIA");
        Terminal.WriteLine("Enter your selection:");
    }

    void ShowPasswordScreen()
    {
        currentScreen = Screen.Password;
        Terminal.ClearScreen();

        for (int i = 0; i < levelLetterWords.Length; i++)
        {
            Terminal.WriteLine(i + ". " + levelLetterWords[i]);
        }

        if (attemptedPasswords.Count > 0)
        {
            Terminal.WriteLine("Attempted password:");
        }

        for (int x = 0; x < attemptedPasswords.Count; x++)
        {
            Terminal.WriteLine(levelLetterWords[attemptedPasswords[x]] + " - Correct characters: " + correctPositions[x]);
        }

        Terminal.WriteLine((maximumAttempts - attemptedPasswords.Count) + " attempts left! Choose one word:");
    }

    void OnUserInput(string input)
    {
        if (input == "menu")
        {
            ShowMainMenu();
        }
        else if (currentScreen == Screen.MainMenu)
        {
            RunMainMenu(input);
        }
        else if (currentScreen == Screen.Password)
        {
            CheckPassword(input);
        }
    }

    void RunMainMenu(string input)
    {
        if (input == "1")
        {
            levelLetterWords = sixLetterWords;
            StartGame();
        }
        else if (input == "2")
        {
            levelLetterWords = sevenLetterWords;
            StartGame();
        }
        else if (input == "3")
        {
            levelLetterWords = elevenLetterWords;
            StartGame();
        }
        else
        {
            Terminal.WriteLine("Please choose a valid level!");
        }
    }

    void StartGame()
    {
        int randomIndex = UnityEngine.Random.Range(0, levelLetterWords.Length);
        password = levelLetterWords[randomIndex];
        ShowPasswordScreen();
    }

    void CheckPassword(string inputIndex)
    {
        bool isNumber = int.TryParse(inputIndex, out int index);
        if (!isNumber)
        {
            return;
        }
        if (levelLetterWords[index] == password) {
            Terminal.WriteLine("WELL DONE!");
            currentScreen = Screen.Win;
            attemptedPasswords.Clear();
        }
        else
        {
            CheckCharacter(index);
        }
    }

    void CheckCharacter(int inputIndex)
    {
        attemptedPasswords.Add(inputIndex);
        var correctCharacters = 0;
        var input = levelLetterWords[inputIndex];
        for (int i = 0; i < password.Length; i++)
        {
            if (password[i] == input[i])
            {
                correctCharacters++;
            }
        }
        correctPositions.Add(correctCharacters);
        ShowPasswordScreen();
    }
}
