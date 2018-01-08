
cube([1,5,1]);

translate([0,0,-2])
cube([1,1,3]);

difference(){
translate([0,0.75,-1])
cube([1,1.5,1]);

translate([-0.1,1,-.9])
cube([2,1.1,1]);
    
}