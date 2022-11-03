# There is a wrong syntax in this program!
# Sytanx checker shoud find them

# droplet declaration
droplet d1;
droplet d2;

# droplet input
input(d3,10,10,3.2);

# move
move(d3,9,9);

# split 
split(d4,d5,d3,12,12,15,15,0.5);

# merging
# d4,d5->d3
merge(d3,d4,d5,5,9);

# mixing
mix(d3,2,2,2,2,5);

# store
store(d3,5,5,2.0);

# output
output(d3,0,0);
