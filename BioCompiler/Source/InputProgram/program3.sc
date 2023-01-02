droplet d1;
droplet d2;
droplet d3;

input(d1,15,15,0.1);
input(d2,4,4,0.1);

merge(d3,d1,d2,5,9);

mix(d3,2,2,2,2,5);