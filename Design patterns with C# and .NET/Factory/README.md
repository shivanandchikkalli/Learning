# Factory Pattern

## Motivation
* Object creation logic becomes too convoluted.
* Constructor is not descriptive
  * Name mandated by name of containing type.
  * Cannot overload with different sets of arguments with different names.
  * can turn into "Optional parameter hell".
* Object creation (non-piecewise, unlike Builder pattern) can be outsourced to 
  * A separate function ( Factory method)  
  * That may exist in separate class (factory)
  * Can create hierarchy of factories with abstract factory.

----
## What is a <i>Factory</i>?
> ### A component responsible solely for the wholesale (not piecewise) creation of objects.

----

## Projects

1. [Factory_One](https://github.com/shivanandchikkalli/Learning/tree/master/Design%20patterns%20with%20C%23%20and%20.NET/Factory/Factory_One)
    > Static Class member methods are implemented to create an object by calling the private class constructor.
2. [Factory_Two](https://github.com/shivanandchikkalli/Learning/tree/master/Design%20patterns%20with%20C%23%20and%20.NET/Factory/Factory_Two)
    > Asynchronously creating a class object by following the factory pattern.
    > If an object needs to be initialized asynchronously, constructor cannot have async and await , so can be achieved as shown in this project.
3. [Factory_Three](https://github.com/shivanandchikkalli/Learning/tree/master/Design%20patterns%20with%20C%23%20and%20.NET/Factory/Factory_Three)
    > Instead of having all the factory methods in the main class we could have a separate class for the factory methods, but to
    > achieve this the Point constructor needs to be made public.
    > => If Point constructor is made public the whole idea of creating factories is not achieved.
    > => If you are creating a library then the Point constructor can be made internal
    > => to hide the constructor for everyone, the factory class is implemented inside the Point class as inner class
    > This is called the Inner factory pattern.
4. [Factory_Four](https://github.com/shivanandchikkalli/Learning/tree/master/Design%20patterns%20with%20C%23%20and%20.NET/Factory/Factory_Four)
    > Factory pattern implementation with multiple factories involved.
    > This implementation violates the Open-Closed Principle, check [Factory_Five](https://github.com/shivanandchikkalli/Learning/tree/master/Design%20patterns%20with%20C%23%20and%20.NET/Factory/Factory_Five) for the new approach.
5. [Factory_Five](https://github.com/shivanandchikkalli/Learning/tree/master/Design%20patterns%20with%20C%23%20and%20.NET/Factory/Factory_Five)
    > Factory pattern implementation without returning any object to the consumer/user and using Reflection to access the Factories
6. [Factory_Coding_Exercise](https://github.com/shivanandchikkalli/Learning/tree/master/Design%20patterns%20with%20C%23%20and%20.NET/Factory/Factory_Coding_Exercise)

----

## Summary
#### 1. A factory method is a static method that creates objects
#### 2. A factory can take care of object creation.
#### 3. A factory can be external of reside inside the object as an inner class.
#### 4. Hierarchies of factories can be used to create related objects.







