# CSharp To Mindustry Logic
This is a code transpiler that will transpile C# code to mlog.  
This transpiler is highly documented.  
[Here](https://smolindiegame.github.io/CSharp-To-Mlog-Doc/index.html) is the documentation.  
Currently, this is just a console application and only support **windows**.
## How to install
You will need an IDE that can open `.csproj` for auto-completion.  
If you do not have one, I recommand visual studio,  
It can be downloaded [here](https://visualstudio.microsoft.com).

Goto [release](https://github.com/SmolIndieGame/CSharp-To-MLog/releases), download the latest version  
Open `Code Transpiler.lnk` and select the code you want to transpile.

[Here](https://www.youtube.com/watch?v=yCHuV9DJST0) is a video tutorial.
## Features
This transpiler support the below CSharp features:
* Fields.
* Methods.
* Binary operations. (`==`, `+`, `&&`, `||`...)
* Jump operations. (`break`, `continue`, `return`, `goto`)
* Conditional operations. (`if`, `else`, conditional expression: `<condition> ? <true> : <false>`)
* For loops.
* While loops.
* Unary operations. (`!`, `-`)
* Conversions. (`(int)12.3`)
* Enums.

**Not** supported:
* Pattern matching.
* Switch statements and expressions.
* Foreach loops.
* Local functions.
* Lambdas.
* Array.
* Recursion.
* Anything that is not mentioned in supported features.

Supported fields, local variable, return value, method argument types:
* `bool`, `sbyte`, `byte`, `short`, `ushort`, `int`, `uint`
* `long`, `ulong`, `decimal`, `float`, `double`, `string`
* any enum
* most of the types defined in MindustryLogics.
* array and char is **NOT** supported.

##
This is my first time doing something like this.  
I only know how to make games with Unity. lol  
So, it may contain bugs.
