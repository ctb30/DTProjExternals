using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO; 
using UnityEngine.UI;
public class OutPutController : MonoBehaviour
{
    
    public string pythonCode ; 
    public string pythonScriptPath = "C:/Users/kddmadmin/Desktop/DigitalTwinProj/Output_PythonController.py"; 
    //WARNING: The path is hard coded this will need to be changed 
    public Process process = new Process();
    public Text Arm_As_Default_Text; 
    public int CurrentServo; 
    public int CurrentPosition; 
    [SerializeField ]Animator ClawAnimator;
    [SerializeField] GameObject[] Robotic_obj;
    [SerializeField] Toggle[] myCheckBoxes; 

    void Start()
    {
        //ClawAnimator.enabled = false;
        myCheckBoxes[0].onValueChanged.AddListener(delegate{ToggleValueChanged(myCheckBoxes[0],2,360);}); //SERVO2
        myCheckBoxes[1].onValueChanged.AddListener(delegate{ToggleValueChanged(myCheckBoxes[1],3,360);}); //SERVO3
        myCheckBoxes[2].onValueChanged.AddListener(delegate{ToggleValueChanged(myCheckBoxes[2],4,360);}); //SERVO4
        myCheckBoxes[3].onValueChanged.AddListener(delegate{ToggleValueChanged(myCheckBoxes[3],5,360);}); //SERVO5
        myCheckBoxes[4].onValueChanged.AddListener(delegate{ToggleValueChanged(myCheckBoxes[4],6,540);}); //SERVO6
        //The above lines of code make it so that we can check the value of the check boxes. 
        //In unity myCheckBoxes[0] is accompanied by the text Servo 1 

         pythonCode =  @"
import xarm 
arm = xarm.Controller('USB') # Connects the xarm through the USB 
        ";
        StartExecuteMyPythonScript(pythonScriptPath); 

    }
    void Update()
    {
    }

    void StartExecuteMyPythonScript(string myScriptPath){
        process.StartInfo.FileName = "python";
        process.StartInfo.Arguments = $"{myScriptPath}";
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.CreateNoWindow = true;
    }
    void EndExecuteMyPythonScript(string myScriptPath){
        process.Start();
        string output = process.StandardOutput.ReadToEnd();
        Console.WriteLine(output);
        process.WaitForExit();
    }
    void ToggleValueChanged(Toggle change, int ServoNumber, int PositionValue){
        CurrentServo = ServoNumber; 
        CurrentPosition = PositionValue;
        //These lines check to see which Servo has been toggled 
        //Then save the Servo number and Positon value as public integers

        //Arm_As_Default_Text.text = "HELLO_WORLD"; //This debug Tool changes the Arm as Output Text 
    }
    public void UpClicked(){ // This function is run when the Up button is clicked 
        int PhysicalPosition;
        string myServoPosition;
        string servoLine;
        string myCommands;
        CurrentPosition = CurrentPosition+10; //Increases the ServoPosition Value by 10 
        if(CurrentServo == 6 || CurrentServo == 2)
        {
            Robotic_obj[CurrentServo].transform.localRotation = Quaternion.Euler(0, CurrentPosition, 0 ); 
            //Change the Virtual Twin Position, vertical rotation
            switch(CurrentServo)
            {
                case 6: //SERVO6
                    //Arm_As_Default_Text.text = "I MADE IT CASE 4UP!"; //Debug Tool 
                    PhysicalPosition = (CurrentPosition*-4)+2660; // Conversion from Virtual Position to Physical Position for Servo 6 - Determined with Slope Intercept Equation 
                    myServoPosition = PhysicalPosition.ToString(); 
                    servoLine = "\narm.setPosition(6," +myServoPosition+","+"2000,True)\n";
                    myCommands = pythonCode + servoLine; 
                    File.WriteAllText(pythonScriptPath,myCommands);
                    EndExecuteMyPythonScript(pythonScriptPath);
                    break;
                case 2: //SERVO2
                    PhysicalPosition = (int)(CurrentPosition*-0.2)+340; // Conversion from Virtual Position to Physical Position for Servo 6 - Determined with Slope Intercept Equation 
                    myServoPosition = PhysicalPosition.ToString(); 
                    servoLine = "\narm.setPosition(2," +myServoPosition+","+"2000,True)\n";
                    myCommands = pythonCode + servoLine; 
                    File.WriteAllText(pythonScriptPath,myCommands);
                    EndExecuteMyPythonScript(pythonScriptPath);
                    Arm_As_Default_Text.text = "I MADE IT CASE 2UP!"; //Debug Tool 
                    break;

            }
        }
        else{
            Robotic_obj[CurrentServo].transform.localRotation = Quaternion.Euler(0,0, CurrentPosition ); 
            //Change the Virtual Twin Position, horizontal rotation
            switch(CurrentServo)
            {
                case 3:
                    //Arm_As_Default_Text.text = "I MADE IT CASE 1UP!"; //Debug Tool 
                    PhysicalPosition = (CurrentPosition*4)-930; // Conversion from Virtual Position to Physical Position for Servo 6 - Determined with Slope Intercept Equation 
                    myServoPosition = PhysicalPosition.ToString(); 
                    servoLine = "\narm.setPosition(3," +myServoPosition+","+"2000,True)\n";
                    myCommands = pythonCode + servoLine; 
                    File.WriteAllText(pythonScriptPath,myCommands);
                    EndExecuteMyPythonScript(pythonScriptPath);
                    break;
                case 4: //Servo 4 
                    //Arm_As_Default_Text.text = "I MADE IT CASE 2UP!"; //Debug Tool 
                    PhysicalPosition = (CurrentPosition*-4)+1930; // Conversion from Virtual Position to Physical Position for Servo 6 - Determined with Slope Intercept Equation 
                    myServoPosition = PhysicalPosition.ToString(); 
                    servoLine = "\narm.setPosition(4," +myServoPosition+","+"2000,True)\n";
                    myCommands = pythonCode + servoLine; 
                    File.WriteAllText(pythonScriptPath,myCommands);
                    EndExecuteMyPythonScript(pythonScriptPath);
                    break;
                case 5: //SERVO5
                    //Arm_As_Default_Text.text = "I MADE IT CASE 3UP!"; //Debug Tool 
                    PhysicalPosition = (CurrentPosition*4)-930; // Conversion from Virtual Position to Physical Position for Servo 5 - Determined with Slope Intercept Equation 
                    myServoPosition = PhysicalPosition.ToString(); 
                    servoLine = "\narm.setPosition(5," +myServoPosition+","+"2000,True)\n";
                    myCommands = pythonCode + servoLine; 
                    File.WriteAllText(pythonScriptPath,myCommands);
                    EndExecuteMyPythonScript(pythonScriptPath);
                    break;

            }
        }
        
    }
    public void DownClicked(){// This function is run when the Down button is clicked 
        CurrentPosition = CurrentPosition-10;//Decreases the ServoPosition Value by 10
        int PhysicalPosition;
        string myServoPosition;
        string servoLine;
        string myCommands;
        if(CurrentServo == 6 || CurrentServo == 2)
        {
            Robotic_obj[CurrentServo].transform.localRotation = Quaternion.Euler(0, CurrentPosition, 0 ); //Change the Virtual Twin Position 
            switch(CurrentServo)
            {
                case 6: //SERVO6
                    //Arm_As_Default_Text.text = "I MADE IT CASE 4UP!"; //Debug Tool 
                    PhysicalPosition = (CurrentPosition*-4)+2660; // Conversion from Virtual Position to Physical Position for Servo 6 - Determined with Slope Intercept Equation 
                    myServoPosition = PhysicalPosition.ToString(); 
                    servoLine = "\narm.setPosition(6," +myServoPosition+","+"2000,True)\n";
                    myCommands = pythonCode + servoLine; 
                    File.WriteAllText(pythonScriptPath,myCommands);
                    EndExecuteMyPythonScript(pythonScriptPath);
                    break;
                case 2: //SERVO2
                    PhysicalPosition = (int)(CurrentPosition*-0.2)+340; // Conversion from Virtual Position to Physical Position for Servo 6 - Determined with Slope Intercept Equation 
                    myServoPosition = PhysicalPosition.ToString(); 
                    servoLine = "\narm.setPosition(2," +myServoPosition+","+"2000,True)\n";
                    myCommands = pythonCode + servoLine; 
                    File.WriteAllText(pythonScriptPath,myCommands);
                    EndExecuteMyPythonScript(pythonScriptPath);
                    //Arm_As_Default_Text.text = "I MADE IT CASE 0UP!"; //Debug Tool 
                    break;

            }

        }
        else{
            Robotic_obj[CurrentServo].transform.localRotation = Quaternion.Euler(0,0, CurrentPosition ); //Change the Virtual Twin Position 
            switch(CurrentServo)
            {
                case 3:
                    //Arm_As_Default_Text.text = "I MADE IT CASE 1UP!"; //Debug Tool 
                    PhysicalPosition = (CurrentPosition*4)-930; // Conversion from Virtual Position to Physical Position for Servo 6 - Determined with Slope Intercept Equation 
                    myServoPosition = PhysicalPosition.ToString(); 
                    servoLine = "\narm.setPosition(3," +myServoPosition+","+"2000,True)\n";
                    myCommands = pythonCode + servoLine; 
                    File.WriteAllText(pythonScriptPath,myCommands);
                    EndExecuteMyPythonScript(pythonScriptPath);
                    break;
                case 4: //Servo 4 
                    //Arm_As_Default_Text.text = "I MADE IT CASE 2UP!"; //Debug Tool 
                    PhysicalPosition = (CurrentPosition*-4)+1930; // Conversion from Virtual Position to Physical Position for Servo 6 - Determined with Slope Intercept Equation 
                    myServoPosition = PhysicalPosition.ToString(); 
                    servoLine = "\narm.setPosition(4," +myServoPosition+","+"2000,True)\n";
                    myCommands = pythonCode + servoLine; 
                    File.WriteAllText(pythonScriptPath,myCommands);
                    EndExecuteMyPythonScript(pythonScriptPath);
                    break;
                case 5: //SERVO5
                    //Arm_As_Default_Text.text = "I MADE IT CASE 3UP!"; //Debug Tool 
                    PhysicalPosition = (CurrentPosition*4)-930; // Conversion from Virtual Position to Physical Position for Servo 5 - Determined with Slope Intercept Equation 
                    myServoPosition = PhysicalPosition.ToString(); 
                    servoLine = "\narm.setPosition(5," +myServoPosition+","+"2000,True)\n";
                    myCommands = pythonCode + servoLine; 
                    File.WriteAllText(pythonScriptPath,myCommands);
                    EndExecuteMyPythonScript(pythonScriptPath);
                    break;

            }
        }
       
    }
}
