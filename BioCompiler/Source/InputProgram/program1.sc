# this is a demo

# droplet declaration
droplet d1;
droplet d2;
droplet d3;

# droplet input
input(d1,15,15,0.1);
input(d2,4,4,0.1);
input(d3,10,10,0.1);

# move
move(d1,3,3);
move(d2,7,7);
move(d3,9,9);

# split 
# d3-> d4, d5
droplet d4;
droplet d5;
split(d4,d5,d3,12,12,15,15,0.5);

# merging
# d4,d5->d3
merge(d3,d4,d5,5,9);

# mixing
mix(d3,2,2,2,2,5);

# store
store(d3,5,5,2.0);

# output
output(d1,0,0);
output(d2,0,0);
output(d3,0,0);
