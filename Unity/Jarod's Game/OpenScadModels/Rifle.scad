
cube([1,15,1]);

translate([0,0,-1])
cube([1,4,2]);

translate([0,4,-2])
cube([1,1,3]);

difference(){
translate([0,5,-1])
cube([1,1,2]);
    
translate([-0.1,5.1,-0.9])
cube([2,0.8,0.9]);
}

translate([0,8,-4])
cube([1,1.5,5]);

translate([0.5,8,1.5])
rotate([90,0,0]){
    cylinder(d=1,h=3,$fn=15);
}