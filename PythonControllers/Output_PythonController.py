
import xarm 
arm = xarm.Controller('USB') # Connects the xarm through the USB 
            
arm.setPosition(3,470,2000,True)
