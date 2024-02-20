
import xarm 
arm = xarm.Controller('USB') # Connects the xarm through the USB 
        
arm.setPosition(5,670,2000,True)
