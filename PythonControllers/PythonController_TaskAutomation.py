
import xarm 
arm = xarm.Controller('USB') # Connects the xarm through the USB 
arm.setPosition(1,491,2000,True)
arm.setPosition(2,499,2000,True)
arm.setPosition(3,501,2000,True)
arm.setPosition(4,520,2000,True)
arm.setPosition(5,500,2000,True)
arm.setPosition(6,497,2000,True)

print('Thank you, have a wonderful day!')