
import xarm 
arm = xarm.Controller('USB') # Connects the xarm through the USB
arm.setPosition(6,500,2000,True)
arm.setPosition(5,500,2000,True)
arm.setPosition(4,500,2000,True)
arm.setPosition(3,500,2000,True)
arm.setPosition(2,100,2000,True)
arm.setPosition(1,1000,2000,True)
        