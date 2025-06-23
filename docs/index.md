# 介绍.

**IceCoffee.Db4Net** 库的目的是尽可能减少数据访问层的重复工作，避免每个新项目从头开始构建数据访问层，使开发人员能够专注于业务层代码编写。

通过简化动态 SQL 查询的构造来增强 [Dapper](https://github.com/DapperLib/Dapper) 体验。利用 Fluent API 和 [字符串插值](https://learn.microsoft.com/dotnet/csharp/tutorials/string-interpolation)，它使开发人员能够轻松创建安全的参数化 SQL 查询。这种效率是通过利用 Lambda expression 和 FormattableString 来实现的，从而确保安全地捕获参数。

## 主要特点

* **开箱即用，采用外观模式（Facade Pattern），所有数据访问方法的主入口点为 Db 类**
* **用于构建 SQL 查询的链式方法和 Fluent API**
* **提供一种使用字符串插值编写复杂 SQL 查询的简单自然方法**
* **用于构建动态 SQL 查询的条件方法**
* **支持在查询中重用参数**
* **支持依赖注入**
* **支持多数据库**
* **高性能和内存效率**
* **支持 .NET Framework 4.62+ 或 .NET 8.0+**

## Packages

该库提供了多个包来满足您的需求。

[IceCoffee.Db4Net](https://www.nuget.org/packages/IceCoffee.Db4Net): 简单且高性能的 SQL 构建和执行器，使用 Fluent API 和 字符串插值 来构建安全的动态 SQL 查询。

[![Nuget](https://img.shields.io/nuget/v/IceCoffee.Db4Net?logo=nuget)](https://www.nuget.org/packages/IceCoffee.Db4Net) [![Nuget](https://img.shields.io/nuget/dt/IceCoffee.Db4Net?logo=nuget)](https://www.nuget.org/packages/IceCoffee.Db4Net)

[IceCoffee.Db4Net.DependencyInjection](https://www.nuget.org/packages/IceCoffee.Db4Net.DependencyInjection): 可选的依赖注入扩展。

[![Nuget](https://img.shields.io/nuget/v/IceCoffee.Db4Net.DependencyInjection?logo=nuget)](https://www.nuget.org/packages/IceCoffee.Db4Net.DependencyInjection) [![Nuget](https://img.shields.io/nuget/dt/IceCoffee.Db4Net.DependencyInjection?logo=nuget)](https://www.nuget.org/packages/IceCoffee.Db4Net.DependencyInjection)

## 数据库支持

**SQL Server，SQLite，PostgreSQL，MySQL，DaMeng**

## 分享您的反馈

如果您喜欢该库，请使用它、分享它并在 GitHub 上给予它⭐️。对于任何建议、功能请求或[问题](https://github.com/1249993110/IceCoffee.Db4Net/issues)，请随时在 GitHub 上创建问题以帮助改进该库。

## 后续步骤

* [快速开始](getting-started/quick-examples.md)
* [发行说明](getting-started/miscellaneous/release-notes.md)
* [License](https://github.com/1249993110/IceCoffee.Db4Net/blob/main/LICENSE)