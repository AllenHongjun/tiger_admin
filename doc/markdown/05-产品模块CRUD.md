### 需要完善的功能 一个模块

- 权限添加 权限本地化
- 查找数据添加。过滤条件的值返回前端。
- 其他业务逻辑接口单独返回
- 列表查询数据方法重写
- 晚上 控制器接口层方法。
- 表里面有一部分的数据冗余 重复 有些就不用join来查询。为模块化 微服务打下基础





### 问题

- xml文件  Swagger新添加的接口没有注释 
  
  - 项目选址生成的文件。重新打钩 重新生成
  
- ALTER TABLE [AppOrders] ALTER COLUMN [MemberId] uniqueidentifier NOT NULL;
   Microsoft.Data.SqlClient.SqlException (0x80131904): 操作数类型冲突: int 与 uniqueidentifier 不兼容  int 与GUId不兼容

- 

- 如何重新 ABP 框架自带的CRUD 方法。基础父类。然后重新
  
  - 官方文档有说明。可以参考。
  
- 效率变高很多。
  - 基础的CRUD方法就基本不用自己写了。。可以完成单表的增删改查
    - 有关联的表的 就需要继承调用父类的方法。然后完成其他关联表的操作。
  - 定义好实体类。定义好注释。定义好验证。
  - 基础的CRUD方法就已经完成了。
  
- An exception was thrown while activating Castle.Proxies.CategoryAppServiceProxy.
   ---> Autofac.Core.DependencyResolutionException: None of the constructors found with  
   
- 注意 EF context需要注入实体类。 错误 和 异常报错 不太相关。。看不太出来。

- ```
  021-08-21 17:12:02.911 +08:00 [ERR] An exception was thrown while activating Castle.Proxies.CategoryAppServiceProxy.
  Autofac.Core.DependencyResolutionException: An exception was thrown while activating Castle.Proxies.CategoryAppServiceProxy.
   ---> Autofac.Core.DependencyResolutionException: None of the constructors found with 'Autofac.Core.Activators.Reflection.DefaultConstructorFinder' on type 'Castle.Proxies.CategoryAppServiceProxy' can be invoked with the available services and parameters:
  Cannot resolve parameter 'Volo.Abp.Domain.Repositories.IRepository`2[Tiger.Basic.Category,System.Guid] repository' of constructor 'Void .ctor(Castle.DynamicProxy.IInterceptor[], Volo.Abp.Domain.Repositories.IRepository`2[Tiger.Basic.Category,System.Guid])'.
     at Autofac.Core.Activators.Reflection.ReflectionActivator.GetValidConstructorBindings(ConstructorInfo[] availableConstructors, IComponentContext context, IEnumerable`1 parameters)
     at Autofac.Core.Activators.Reflection.ReflectionActivator.ActivateInstance(IComponentContext cont
  ```

- [可能会导致循环或多重级联路径。请指定 ON DELETE NO ACTION 或 ON UPDATE NO ACTION，或修改其他 FOREIGN KEY 约束。](cnblogs.com/hao-1234-1234/p/8664922.html)

- [Specifying ON DELETE NO ACTION in Entity Framework 7? [duplicate]](https://stackoverflow.com/questions/34768976/specifying-on-delete-no-action-in-entity-framework-7)

- [EF Core 遇到“可能会导致循环或多重级联路径”](bbsmax.com/A/RnJWwOPoJq/)

- 有外键约束的时候 EF修改字段 迁移会报错。。。
  - 除了删除数据库 还有什么办法
  - EF的生成数据库功能是否好用？ 
    - 学习如何使用。。迁移需要注意的点。







- [EF code first 使用教程](cnblogs.com/caofangsheng/p/10685049.html)

- 订单退款表 添加订单的外键 级联删除会有问题、、

- 将 FOREIGN KEY 约束 'FK_AppCartItems_AppProducts_ProductId' 引入表 'AppCartItems' 可能会导致循环或多重级联路径。请指定 ON DELETE NO ACTION 或 ON UPDATE NO ACTION，或修改其他 FOREIGN KEY 约束。
  无法创建约束或索引。请参阅前面的错误。
  - [引入FOREIGN KEY约束可能会导致循环或多个级联路径-为什么？](https://qastack.cn/programming/17127351/introducing-foreign-key-constraint-may-cause-cycles-or-multiple-cascade-paths)