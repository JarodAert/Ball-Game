
buildingX=10;
buildingY=10;
floorHeight=5;
windowWidth=2;
windowHeight=2;
wallThickness=0.25;
rampWidth=2.5;

cube([wallThickness,buildingY,floorHeight]);

translate([buildingX-wallThickness,0,0])
cube([wallThickness,buildingY,floorHeight]);

difference(){
cube([buildingX,wallThickness,floorHeight]);

translate([buildingX/2-windowWidth/2,0,floorHeight/2-windowHeight/2])
cube([windowWidth,wallThickness*10,windowHeight]);
}

difference(){
translate([0,buildingY-wallThickness,0])
cube([buildingX,wallThickness,floorHeight]);

translate([buildingX/2-windowWidth/2,buildingY-wallThickness,floorHeight/2-windowHeight/2])
cube([windowWidth,wallThickness*10,windowHeight]);
}

cube([buildingX,buildingY-rampWidth,0.1]);

translate([0,buildingY-rampWidth,0])
rotate([0,atan(floorHeight/buildingX),0]){
    cube([buildingX,rampWidth,0.1]);
}