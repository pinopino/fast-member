Fast access to .net fields/properties
=====================================

这个fork版本我主要想提供私有字段的访问设置功能，原本的代码库不支持作者似乎也不打算支持。

将`FastMember.Signed`和`FastMember.Tests`两个项目直接删掉，其它保持不变。

> 另：原始代码库的更新会在master分支进行，不定期同步。

#### 简单区别
从作者自己给出的例子以及源代码上关于二者的解释可以窥探一二：
```csharp
/// <summary>
/// Provides by-name member-access to objects of a given type
/// </summary>
public abstract class TypeAccessor { ... }

/// <summary>
/// Represents an individual object, allowing access to members by-name
/// </summary>
public abstract class ObjectAccessor { ... }
```
`ObjectAccessor`是`TypeAccessor`的一个wapper，typeaccessor的调用需要这样：
```csharp
accessor[target, name] = 10;
```
但objectaccessor可以直接这样：
```csharp
obj["propertyName"] = 10;
```

#### 嵌套对象
fast-member本身是不支持这种调用的：
```csharp
public class A
{
    public B First { get; set; }
}

public class B
{
    public string Second { get; set; }
}

var b = new B{ Second = "some value here" };
var a = new A{ First = b };
var accessor = ObjectAccessor.Create(a);
accessor["First.Second"] = value; // this does not work and gives ArgumentOutOfRangeException
```
解决方案在[这里](https://stackoverflow.com/questions/40305645/how-to-set-nested-property-value-using-fastmember)，这部分我也已经整合了进来使用起来会更佳方便。



#### 参考链接：
1. https://stackoverflow.com/questions/6158768/c-sharp-reflection-fastest-way-to-update-a-property-value
2. https://stackoverflow.com/questions/26803788/fastmember-access-non-public-properties
3. https://stackoverflow.com/questions/21976125/how-to-get-the-attribute-data-of-a-member-with-fastmember


