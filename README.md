## Preview

In our research group, we currently work on the software, firmware, and hardware development related to digital microfluidic biochips. 

Digital microfluidic biochips allow the execution of biological and chemical protocols on a chip-scaled device delivering cost and performance advantages over the traditional benchtop wet lab processes. Building reconfigurable and programmable digital biochips that can be used for a broad range of laboratory protocols inevitably shows the need for an easy and structured process to capture a protocol and translate it into a sequence of steps that can be run on a DMF biochip. This process can be captured with the classical software tool-chain consisting of a programming language, a compiler, and an execution platform. This project aims at defining a domain-specific language to capture biochemical protocols and to develop the necessary front-end compilation tools for such a language. Besides capturing the biochemical protocol, the language should allow the programmer to define behavior when special conditions arise due to droplet missed-movements events and other phenomena that may disrupt the protocol's normal execution. 

If time allows, the project may also include the development and implementation of back-end techniques for scheduling and routing of droplets and generation of low-level commands.



## Design

Our project's basic structure looks like: 

![image-20221117191437952](https://raw.githubusercontent.com/gggdttt/ImageBeds/master/img202211171914048.png)

<img src="https://raw.githubusercontent.com/gggdttt/ImageBeds/master/img202210282155579.png" alt="image-20220929225512913" style="zoom:67%;" />

##  Compiler

Input : Source code of `sc` file.

Output: JSON format 

Input demo:

``` java
# this is a demo

# droplet declaration
droplet d1;
droplet d2;
droplet d3;

# droplet input
input(d1,1,1,1.0);
input(d2,4,4,0.5);
input(d3,10,10,3.2);

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
```

Output demo:

```json
[
  {
    "$type": "Executor.Model.Operation.DropletDeclarator, Executor",
    "name": "d1",
    "line": 4
  },
  {
    "$type": "Executor.Model.Operation.DropletDeclarator, Executor",
    "name": "d2",
    "line": 5
  },
  {
    "$type": "Executor.Model.Operation.DropletDeclarator, Executor",
    "name": "d3",
    "line": 6
  },
  {
    "$type": "Executor.Model.Operation.DropletInputer, Executor",
    "line": 9,
    "name": "d1",
    "xValue": 1,
    "yValue": 1,
    "size": 1.0
  },
  {
    "$type": "Executor.Model.Operation.DropletInputer, Executor",
    "line": 10,
    "name": "d2",
    "xValue": 4,
    "yValue": 4,
    "size": 0.5
  },
  {
    "$type": "Executor.Model.Operation.DropletInputer, Executor",
    "line": 11,
    "name": "d3",
    "xValue": 10,
    "yValue": 10,
    "size": 3.2
  },
  {
    "$type": "Executor.Model.Operation.DropletMover, Executor",
    "line": 14,
    "name": "d1",
    "xDest": 3,
    "yDest": 3
  },
  {
    "$type": "Executor.Model.Operation.DropletMover, Executor",
    "line": 15,
    "name": "d2",
    "xDest": 7,
    "yDest": 7
  },
  {
    "$type": "Executor.Model.Operation.DropletMover, Executor",
    "line": 16,
    "name": "d3",
    "xDest": 9,
    "yDest": 9
  },
  {
    "$type": "Executor.Model.Operation.DropletDeclarator, Executor",
    "name": "d4",
    "line": 20
  },
  {
    "$type": "Executor.Model.Operation.DropletDeclarator, Executor",
    "name": "d5",
    "line": 21
  },
  {
    "$type": "Executor.Model.Operation.DropletSplitter, Executor",
    "line": 22,
    "outDestName1": "d4",
    "outDestName2": "d5",
    "inDropletName": "d3",
    "outDest1X": 12,
    "outDest1Y": 12,
    "outDest2X": 15,
    "outDest2Y": 15,
    "ratio": 0.5,
    "_order_id": 0
  },
  {
    "$type": "Executor.Model.Operation.DropletMerger, Executor",
    "line": 26,
    "outDropletName": "d3",
    "inDroplet1Name": "d4",
    "inDroplet2Name": "d5",
    "xDest": 5,
    "yDest": 9
  },
  {
    "$type": "Executor.Model.Operation.DropletMixer, Executor",
    "line": 29,
    "name": "d3",
    "xMix": 2,
    "yMix": 2,
    "xDistance": 2,
    "yDistance": 2,
    "repeatTimes": 5
  },
  {
    "$type": "Executor.Model.Operation.DropletStorer, Executor",
    "line": 32,
    "name": "d3",
    "xValue": 5,
    "yValue": 5,
    "latency": 2.0,
    "time": 0
  },
  {
    "$type": "Executor.Model.Operation.DropletOutputer, Executor",
    "line": 35,
    "name": "d1",
    "xValue": 0,
    "yValue": 0
  },
  {
    "$type": "Executor.Model.Operation.DropletOutputer, Executor",
    "line": 36,
    "name": "d2",
    "xValue": 0,
    "yValue": 0
  },
  {
    "$type": "Executor.Model.Operation.DropletOutputer, Executor",
    "line": 37,
    "name": "d3",
    "xValue": 0,
    "yValue": 0
  }
]
```

### Syntax 

#### Declaration 

``` java
/// <summary>
/// droplet <name>;
///         string
/// </summary>
droplet <name>;
```

#### Input

```java
/// <summary>
/// input(       <droplet_name>, x,   y,   size);
/// member type:      string,   int, int, float
/// </summary>
input(<droplet_name>, x,y, size);
```

#### Move

``` java
/// <summary>
/// move(<droplet_name>, x_dest, y_dest);
///         string, int ,int 
/// </summary>
move(<droplet_name>, x_dest, y_dest);
```

#### Merge

```java
/// <summary>
/// merge(<out_dest_droplet_name>,<in_1_droplet_name>,<in_2_droplet_name>,x_dest,y_dest
///         string, string, string, int, int 
/// </summary>
merge(<out_dest_droplet_name>,<in_1_droplet_name>,<in_2_droplet_name>,x_dest,y_dest);
```

#### Split

```java
/// <summary>
/// split(<out_dest_name1>,<out_dest_name2>,<in_droplet_name>,left_x_dest, left_y_dest, right_x_dest, right_y_dest, ratio);
/// string, string, strig, int, int ,int, int, real
/// note: ratio is D1/(D1+D2)
/// </summary>
split(<out_dest_name1>,<out_dest_name2>,<in_droplet_name>,left_x_dest, left_y_dest, right_x_dest, right_y_dest, ratio);
```

#### Mix

```java
/// <summary>
/// mix(<droplet_name>,x_mix,y_mix,size_x,size_y,repeat_times)
///             string, int ,int ,int ,int int
/// </summary>
mix(<droplet_name>,x_mix,y_mix,size_x,size_y,repeat_times)
```

#### Output

``` java
/// <summary>
/// output(<droplet_name>, x, y)
///         string, int ,int 
/// </summary>
output(<droplet_name>, x, y)
```

### Store

```java
/// <summary>
/// store(<droplet_name>,x,y, latency)
///     string, int, int , float
/// </summary>
store(<droplet_name>,x,y, time)
```

## Syntax checker

### Invalid Source Code1

```apl
# droplet declaration
droplet d1;
droplet d2;

# droplet input
input(d3,10,10,3.2);

```

### Invalid Source Code2

```api
# droplet declaration
droplet d1;
droplet d1;
```





### Exception/Errors

> The exception is not throwed by compiler, it is throwed by executor

* Error001: droplet out of bound
* Error002: can not split droplet for there is not enough space
* Error003: can not mix droplet for there is not enough space (maybe merged with error02)
* Error004 : can not find a route

### Rules check

> By compiler

* Invalid 1: droplet is not defined before
* Invalid 2: position is out of bound *(should this be handled as exception?)*



### Route-finding algorithm

> I will use a* firstly.
>
> This step will be done in executor.
