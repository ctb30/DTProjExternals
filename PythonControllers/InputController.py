
import xarm 
import numpy as np 
arm = xarm.Controller('USB') # Connects the xarm through the USB 
Task = []
        
arm.servoOff([1,2,3,4,5,6])
arm.servoOff([1,2,3,4,5,6])
PoseServoPositions = np.array([str(0)]) 
position1 = str(arm.getPosition(1))
position2 = str(arm.getPosition(2))
position3 = str(arm.getPosition(3))
position4 = str(arm.getPosition(4))
position5 = str(arm.getPosition(5))
position6 = str(arm.getPosition(6))

myLines = position1 + '\n' + position2 +'\n'+ position3 +'\n'+ position4 +'\n'+ position5 +'\n'+ position6 +'\n' + 'STOP' +'\n'
try:
    myDataFile = open('C:/Users/court/OneDrive/Desktop/DigitalTwinProj/InputController_PoseData.txt', 'a')
    myDataFile.write(myLines)
except Exception as e:
    print(f'Error: {e}')
finally:
    myDataFile.close()

