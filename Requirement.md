# Requirements:

> I want to name our language as CDMF( Convenient DMF) :) 

## General

Something like the image.  

**DESIGN->WRITE  CDMF-> COMPILE TO C# CODE -> FIND PATH -> GENERATING INSTRUCT FOR CHIP**

|=====User=========|=======Compiler======|=============Executor======================|

Once this is completed, we will add computer vision feedback to our solution.

<img src="C:\Users\Wenjie\AppData\Roaming\Typora\typora-user-images\image-20220929225512913.png" alt="image-20220929225512913" style="zoom:67%;" />

##  Compiler

> Input : Source code of `cdmf` file.
>
> Output: xml file ??? c# file????  (Just generating xml file which including droplet information and destination)

> * We need to consider the order of execution
> * Look at JSON

> Why not generate C# code/ advantage of using JOSN/XML
>
> Generating strcuture file instead of "STring" and writing  as `.cs` file

input demo:

``` 
droplet1 = Droplet(19,29,2,2);
droplet2 = Droplet(1,2,2,2);
move(droplet1,17,18);
move(droplet2,11,9);
droplet3 = 
move(droplet3)
merge(droplet1, droplet2, 1, 2);

```

output demo:

```xml
<droplet-list>
    <droplet>
        <name>droplet1</name>
        <x>19</x>
        <y>29</y>
        <width>2</width>
        <length>2</length>
    </droplet>
    <droplet>
        <name>droplet2</name>
        <x>1</x>
        <y>2</y>
        <width>2</width>
        <length>2</length>
    </droplet>
</droplet-list>

<action-list>
    <move>
        <droplet> droplet1 </droplet>
        <destination>
            <x>17</x>
            <y>18</y>
        </destination>
    </move>
    <move>
        <droplet> droplet2 </droplet>
        <destination>
            <x>11</x>
            <y>9</y>
        </destination>
    </move>
    <merge>
        <droplet1>droplet1</droplet1>
        <droplet2>droplet2</droplet2>
        <destination>
            <x>1</x>
            <y>2</y>
        </destination>
    </merge>
</action-list>
```



### Syntax 

> if , when, for
>
> support +,-
>
> sensing function

:question: Should we assume the chip is blank before every script?

* Define a droplet

  ``` 
  Droplet(name,x,y,width, length);
  ```

* `move` operation

  ``` 
  move(droplet_name,x,y);
  ```

* `merge` operation

  ```
  
  merge(new_droplet_name,droplet_name1,droplet_name2,x,y);
  
  merge(dp3, dp2,dp1, x, y )
  ```

* `split` operation

  ```
  // think about the list [a,b] = splitxxxx.
  split_by_size(droplet_name,size_of_the_left_droplet,name_left_droplet,name_right_droplet);
  
  split_average(droplet_name,name_left_droplet,name_right_droplet);
  ```

* `dispose` operation

  ```
  dispose(droplet_name)
  ```

* `mix` operation

  ```
  mix(droplet_name)// change the parameters
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
