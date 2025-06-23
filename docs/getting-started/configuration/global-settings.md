# Global Settings

You can configure the builder settings through the `Db` static class by calling the `Configure` method.

The code below shows how to configure the builder settings.

```csharp
Db.Configure(new Db4NetSettings()
{
    parameterNameTemplate: "param", // The default is "p".
    reuseParameters: false, // The default is true.
    sqlCaptureMode: SqlCaptureMode.Never // Specifies the strategy for including SQL statements when throwing exceptions.
});
```