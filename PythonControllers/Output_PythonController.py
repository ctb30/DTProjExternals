
import xarm 
arm = xarm.Controller('USB') # Connects the xarm through the USB 
        
arm.setPosition(3,390,2000,True)
