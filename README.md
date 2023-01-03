## Preview

In our research group, we currently work on the software, firmware, and hardware development related to digital microfluidic biochips. 

Digital microfluidic biochips allow the execution of biological and chemical protocols on a chip-scaled device delivering cost and performance advantages over the traditional benchtop wet lab processes. Building reconfigurable and programmable digital biochips that can be used for a broad range of laboratory protocols inevitably shows the need for an easy and structured process to capture a protocol and translate it into a sequence of steps that can be run on a DMF biochip. This process can be captured with the classical software tool-chain consisting of a programming language, a compiler, and an execution platform. This project aims at defining a domain-specific language to capture biochemical protocols and to develop the necessary front-end compilation tools for such a language. Besides capturing the biochemical protocol, the language should allow the programmer to define behavior when special conditions arise due to droplet missed-movements events and other phenomena that may disrupt the protocol's normal execution. 

If time allows, the project may also include the development and implementation of back-end techniques for scheduling and routing of droplets and generation of low-level commands.



## Design

Our project's basic structure looks like: 

**DESIGN->WRITE  CDMF-> COMPILE TO C# CODE -> FIND PATH -> GENERATING INSTRUCT FOR CHIP**

|=====User=========|=======Compiler======|=============Executor======================|



<img src="https://raw.githubusercontent.com/gggdttt/ImageBeds/master/img202210282155579.png" alt="image-20220929225512913" style="zoom:67%;" />

##  Compiler

### Syntax 

#### Declaration 

``` java
droplet <name>;
// 		string
```

#### Input

```java
input(<droplet_name>, x,y, size);
//      string, int, int, float
```

#### Move

``` java
move(<droplet_name>, x_dest, y_dest);
//		string, int ,int 
```

#### Merge

```java
merge(<out_dest_droplet_name>,<in_1_droplet_name>,<in_2_droplet_name>,x_dest,y_dest);
// 		string, string, string, int, int 
```

#### Split

```java
split(<out_dest_name1>,<out_dest_name2>,<in_droplet_name>,left_x_dest, left_y_dest, right_x_dest, right_y_dest, ratio);
// string, string, strig, int, int ,int, int, real
// note: ratio is D1/(D1+D2)
```

#### Mix

```java
mix(<droplet_name>,x_mix,y_mix,size_x,size_y,repeat_times)
// 	string, int ,int ,int ,int int
```

#### Output

``` java
output(<droplet_name>, x, y)
// string, int ,int 
```

#### Store

```java
store(<droplet_name>,x,y, time)
// string, int, int , float
```

#### *Repeat

```java
repeat <N> times{ <operation1>; <operation2>; ... <operation_N>;}
// example:
repeat 10 times{
move(d1,3,3);
move(d2,7,7);
move(d1,5,5);
move(d2,10,10);
}
```
### Input and Output

Input : Source code of `sc` file.

Output: JSON format 

#### Input example:

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

#### output example:

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

## Exception Diagnostics

### Exceptions of Compiler 

#### 1.'{0}' is not declared

Exception message: C_DROPLET_NOT_DECLARED

Exception code: 00001

Code example1:

```apl
# the beginning of file
droplet d1;
droplet d2;

input(d3,10,10,3.2);
```

Code example2:

```apl
# the beginning of file
input(d1000,10,10,3.2);
```

#### 2.'{0}' is declared more than once

Exception message: C_DROPLET_DECLRATED_MORE_THAN_ONCE

Exception code: 00002

Code example:

```apl
droplet d1;
droplet d1;
```

#### 3.Incorrect syntax

Exception message: C_INCORRECT_SYNTAX

Exception code: 00003

Code example1:

```apl
droplet; d1#
inputtt();
```

#### 4. Occupied variable has not been released

Exception message: C_VARIABLE_NOT_RELEASED

Exception code: 00004

Code example:

```apl
droplet d1;
input(d1,3,3,3);
input(d1,3,3,3);
```

#### 5. Variable does not assign a value

Exception message: C_VARIABLE_NOT_ASSIGN_VALUE

Exception code: 00005

Code example:

```apl
droplet d1;
move(d1,3,3);
```



### Exceptions of Executer



#### 1. Illegal position value

Exception message: E_ILLEGAL_POSITION

Exception code: 00001

Code example1:

```apl
#in config.xml, the chip is 32x20
droplet d1;
input(d1, 33, 10, 0.05) # x value bigger than chip's width
input(d1, -1, 10, 0.05) # x value smaller than 0
input(d1, 10, 21, 0.05) # y value bigger than chip's width
input(d1, 10, -1, 0.05) # y value smaller than 0
```

#### 2.Illegal droplet's size value

Exception message: E_ILLEGAL_DROPLET_SIZE

Exception code: 00002

Code example:

```apl
input(d1, 1, 1, 5.0) # Here the droplet is 3ml, the chip can not contain such size droplet
```

#### 3.

Exception message: C_DROPLET_DECLRATED_MORE_THAN_ONCE

Exception code: 00002

Code example:

```apl

```

#### 4.

Exception message: C_DROPLET_DECLRATED_MORE_THAN_ONCE

Exception code: 00002

Code example:

```apl

```

#### 5.

Exception message: C_DROPLET_DECLRATED_MORE_THAN_ONCE

Exception code: 00002

Code example:

```apl

```

#### 6.

Exception message: C_DROPLET_DECLRATED_MORE_THAN_ONCE

Exception code: 00002

Code example:

```apl

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





### Path-finding algorithm

> Now only Astar and Simple XY supported.
>
> Conflict-based searching?

#### AStar

For example, there is a red droplet (2x2) wants to move to the white space. 

<img src="https://raw.githubusercontent.com/gggdttt/ImageBeds/master/img202212020157764.png" alt="image-20221202015710641" style="zoom:50%;" />

Our astar router will use its left-top position as the moving element. And at the same time, all other droplets (and the right-bottom bounds) will be reckoned as block elements.

<img src="https://raw.githubusercontent.com/gggdttt/ImageBeds/master/img202212020157797.png" alt="image-20221202015755720" style="zoom:50%;" />

Assume there is a red droplet(3x3) wants to move to the white space:

<img src="https://raw.githubusercontent.com/gggdttt/ImageBeds/master/img202212020157121.png" alt="image-20221202015735058" style="zoom:50%;" />

The moving element is shown in the following image:

<img src="https://raw.githubusercontent.com/gggdttt/ImageBeds/master/img202212020158163.png" alt="image-20221202015804096" style="zoom:50%;" />
