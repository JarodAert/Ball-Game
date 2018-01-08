
buildingX=10;
buildingY=10;
floorHeight=5;
doorWidth=2;
doorHeight=3;
wallThickness=0.25;

cube([wallThickness,buildingY,floorHeight]);

translate([buildingX-wallThickness,0,0])
cube([wallThickness,buildingY,floorHeight]);

difference(){
cube([buildingX,wallThickness,floorHeight]);

translate([buildingX/2-doorWidth/2,0,0])
cube([doorWidth,wallThickness*10,doorHeight]);
}

difference(){
translate([0,buildingY-wallThickness,0])
cube([buildingX,wallThickness,floorHeight]);

translate([buildingX/2-doorWidth/2,buildingY-wallThickness,0])
cube([doorWidth,wallThickness*10,doorHeight]);
}

cube([buildingX,buildingY,0.1]);