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

input demo:

``` 
droplet1 = Droplet(19,29,2,2);
droplet2 = Droplet(1,2,2,2);
move(droplet1,17,18);
move(droplet2,11,9);
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

* Define a droplet

  ``` 
  {name} = Droplet(x,y,width, length);
  ```

  

* 



### Exception/Errors





### Rules check



### Route-finding algorithm

