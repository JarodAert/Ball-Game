cube([1,15,1]);

translate([0,0,-1.25])
cube([1,4,2]);

difference(){
translate([0,4,-1])
cube([1,1,2]);
    
translate([-0.1,4.1,-0.9])
cube([2,0.8,0.9]);
}