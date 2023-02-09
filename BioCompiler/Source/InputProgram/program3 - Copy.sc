droplet DNA;
droplet PCRmix;
# Input 10ul DNA sample
input(DNA, 0, 0, 0.01);
# Input 10ul PCRmix
input(PCRmix, 0, 15, 0.01);
# Merge the two droplets
# merge(PCRmix, PCRmix, DNA, 5, 5);

# Mix 30 times
mix(PCRmix, 5, 5, 5, 5, 30);

# Keep droplet at 95 C ,30 seconds (Initial Denaturation)
store(PCRmix, 28, 5, 0.0);

# For 30 cycles
repeat 30 times{
    # Keep droplet at 95 C , 30 seconds
    store(PCRmix, 28, 5, 0.0);
    # Keep droplet at 75 C , 30 seconds 
    store(PCRmix, 18, 5, 0.0);
    # Keep droplet at 65 C , 60 seconds 
    store(PCRmix, 8, 5, 0.0);
}

# Keep droplet at 95 C , 30 seconds (Final extension)
store(PCRmix, 28, 5, 0.0);
# Output droplet
output(PCRmix, 0, 0);