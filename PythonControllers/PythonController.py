import xarm
arm = xarm.Controller('USB') 
arm.setPosition(6,900,2000,True)
arm.setPosition(3,800,2000,True)
arm.setPosition(4,250,2000,True)