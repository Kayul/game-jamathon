using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutputWindow : MonoBehaviour
{
    List<string> commandsList = new List<string>()
    {
        "plant",
        "harvest",
        "till",
        "dig",
        "info",
        "help"
    };

    private List<string> commandHistory = new List<string>();

    public GameObject inputField;
    public GameObject outputField;

    private bool DoesCommandExist(string command)
    {
        foreach (string s in commandsList)
        {
            if (s.ToLower().Equals(command.ToLower()))
            {
                return true;
            }
        }
        return false;
    }

    private string GetCommands()
    {
        commandHistory.Add("<b>Commands:</b>");
        foreach (string s in commandsList)
        {
            commandHistory.Add($"  {s} [x] [y] [args]");
        }
        commandHistory.Add("<b>Available Seeds/Plants:</b>");
        commandHistory.Add("   <color=#F87210>Carrot</color>");
        commandHistory.Add("   <color=#C1B338>Wheat</color>");
        return "";
    }

    // Update is called once per frame
    void Update()
    {
        List<string> commandArguments = new List<string>();
        foreach (string s in inputField.GetComponent<Text>().text.Split(' '))
        {
            commandArguments.Add(s);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            OnCommand(commandArguments[0], commandArguments.ToArray());
            inputField.GetComponent<Text>().text = "";
            inputField.transform.parent.GetComponent<InputField>().ActivateInputField();
            inputField.transform.parent.GetComponent<InputField>().text = "";
        }

        if (commandHistory.Count > 9) commandHistory.RemoveAt(0);
    }

    public void OnCommand(string command, string[] args=null)
    {
        if (!DoesCommandExist(command))
        {
            commandHistory.Add("<b><color=#D30000>That command does not exist. Use 'help' for more information</color></b>");
            RefreshOutputWindow();
            return;
        }

        int output = 0;
        try
        {
            TileHandler th = GameObject.Find("TileHandler").GetComponent<TileHandler>();
            switch (command.ToLower())
            {
                default:
                    break;
                case "plant":
                    output = th.TryPlantAt(Int32.Parse(args[1]), Int32.Parse(args[2]), args[3]);
                    break;
                case "harvest":
                    output = th.TryHarvestAt(Int32.Parse(args[1]), Int32.Parse(args[2]));
                    break;
                case "till":
                    output = th.TryTillAt(Int32.Parse(args[1]), Int32.Parse(args[2]));
                    break;
                case "dig":
                    output = th.TryDigAt(Int32.Parse(args[1]), Int32.Parse(args[2]));
                    break;
                case "info":
                    // Do this
                    break;
                case "help":
                    string ch = GetCommands();
                    commandHistory.Add(ch);
                    break;
            }
        }
        catch (Exception e)
        {

        }
        finally
        {
            string history = "";
            foreach (string s in args) { history += " " + s; }
            commandHistory.Add(history);
            if (output != 0)
            {
                commandHistory.Add($"<b><color=#D30000>Incorrect usage of {command}. Use 'help' for more information</color></b>");
            }
            RefreshOutputWindow();
        }
    }

    private void RefreshOutputWindow()
    {
        string output = "";

        foreach (string s in commandHistory)
        {
            output += ">> " + s + "\n";
        }

        outputField.GetComponent<Text>().text = output;
    }
}
