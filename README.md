# Owen Brandon: #100969833

## The Cube Of Bouncing Cubes
Essentially, the loop is all about how cubes spawn. You're in control... You can either instantiate and destroy all of the cubes, or. You know... Use Object Pooling...

# Profiler Screenshots
# Before Pooling

![Pre-Pooling](<Screenshot 2026-11-18 005704.png>)

### Take a look at those instantiation numbers, 13 draw calls in one frame. Multiply that by 60 and the computer is making 780 draw calls per second (I totally did that math without a calculator).

### Whats more interesting is what is happening with the Destroy calls...

![Pre-Pooling](<Screenshot 2026-11-18 010438.png>)

### Its not taking too long for the computer to actually compute the function. However, it is adding a significant amount of draw calls per frame that could be allocated elsewhere. But How?

# After Pooling

![Post-Pooling](<Screenshot 2026-11-18 010755.png>)

### The Instantiation calls are down to one per frame, and its not even to instantiate a new cube. That alone is allowing more computing space and speed for other processes in the project.

### Once again, the draw calls seem to be significantly better after implementing pooling. Though it may still look like a lot of draw calls, this isn't actually being called on the cubes, it's being called on other processes.

![Post-Pooling](<Screenshot 2026-11-18 010411.png>)

# Reflection Questions

### - What did you notice was impacting the performance of your project?

The biggest impacts on my projects' performance were physics, rendering, and scripts that were constantly being called and changed. There were lots of verticies and faces being drawn too.

### - Which pattern(s) did you implement, and what behavior did they replace?

I decided to implement Factory for spawning cubes and Object Pooling to reduce draw overhead. These both replaced regular instantiation for abstracted instantiation, as well as "Destroy()" function calls.

### - Do your patterns / technique implementations improve performance? Why or why not?

Factory doesn't do a whole lot performance wise. But this is where Object pooling comes in. Essentially the CubeManager sets a strict limit of how many cubes can be spawned at one time and never lets it surpass that. But it also relies on setting the gameObject active and inactive in the hierarchy rather than Instantiating and Destroying new cubes when their lifetime hits 0.


### - Can you demonstrate what the profiling process revealed?

Yes, essentially what you see is a before and after of the Instantiation processes and Destruction Processes. This reduces a fair amount of overhead and can lead to plenty of scalability later on in any project regardless of use.

The profiling process also shows the differences in performance spikes and GPU processing speeds (I.E The red bars on the top of the graph). While Pooling was off, the red bars were constant indicating a CPU slowdown/bottleneck. While on, it was still there and still frequent. But a noticable difference from the previous image set.