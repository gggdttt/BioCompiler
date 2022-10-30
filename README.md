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

Input : Source code of `cdmf` file.

Output: JSON format 

> input demo:

``` 

```

output demo:

```xml

```

### Syntax 

#### Declaration 

``` java
droplet <name>;
// 		string
```

#### Input/Output

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

### Store

```java
store(<droplet_name>,x,y, time)
// string, int, int , float
```

## Syntax checker

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
