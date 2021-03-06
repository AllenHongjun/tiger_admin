## 0810  ABP开发笔记问题 01

> elemntui 框架使用。 abp项目介绍使用



## why

- 熟悉一套自己趁手的工具
- 学习一些新的开发方式 使用的组件
- 独立能完成一个项目的架构和所有用到功能的开发
- 完善一个脚手架。方便以后快速开发
- 熟悉abp 的源码 demo 学习一种好的 优秀的开发模式
- 学习dotnet 的微服务 高并发 的相关知识。





- abp vnetxt 使用步骤

  - 在domain 中定义实体类 在 domain.share 中定义枚举类型  将实体类添加到 dbcontext当中

  - 数据迁移

    - ```bash
      // 选择 Migration项目
      Add-Migration "Created_Book_Entity"
      
      //在类 TigerAdminDataSeederContributor 添加种子数据  
      
      // DbMigrator 是一个数据库迁移的项目。 运行来生成数据库。
      ```

- 

- 

- 整一个 多租户 多店铺 sass 微服务。

  - 主要搞一个后台功能 其他配送打包称 PAD APP接口 不做。留着扩展。 

- 前台客户 和后台用户 分开 如何使用identityServer来管理

  - 或者是用户表添加一个类型来管理。感觉不太好。
  - 新建一个表的用户。使用identityServer来授权登录管理。

- 这么多控制器器模板类库如何管理

  - 按模块来分类。同一个模块下的放在一个文件夹
  - 然后安装分层来管理。
  - 有些业务不是很明确的就先放到basic里面。后期再整合。

- 

- 手段按需要搭建一个abp  框架的项目。按需要引入哪一层。。用框架但是不要被框架限制死。

- 分析abp的表数据结构。abp框架使用学习。

- - - 

- [ABP解决方法结构介绍](https://docs.abp.io/zh-Hans/abp/3.2/Startup-Templates/Application)

- [ABP vNEXT使用教程](https://www.cnblogs.com/xhznl/tag/ABP%20vNext/)

- ABP 框架 配置使用swaggerUI

  - 和demo基本的用法是一直
  - 主要有 http.host.api  http.api http.AppService 几个项目下都要生成xml 文档。然后 注意接口最先的来源来哪里。就把接口的注释写在哪个地方
  - 后来在地址栏上加一个参数就不会展开了。
    [http://XXXXX/swagger2/index.html?docExpansion=none](http://xxxxx/swagger2/index.html?docExpansion=none)

- 完善abp 原来有的基础的功能。。

- 使用abp 的依赖框架。swagger redis 等。

- 迁移站点到linux 

- .net core iis 部署文件被占用 

- [abp发布后遇到 405 - 不允许用于访问此页的 HTTP 谓词的解决方法](https://www.jianshu.com/p/d4a61d27960c)

  - [iis6.5部署项目put delete不行](https://stackoverflow.com/questions/6739124/iis-7-5-enable-put-and-delete-for-restful-service-extensionless)

  - [解决IIS6 不支持PUT DELETE请求问题](https://www.jianshu.com/p/af486e23125c)

  - # [IIS 7.5 + enable PUT and DELETE for RESTFul service, extensionless](https://stackoverflow.com/questions/6739124/iis-7-5-enable-put-and-delete-for-restful-service-extensionless)

    - webconfig 配置项 注意不要加错位置。不然会报错。

- 部署后配置文件中的路径需要修改为正式服务器中的路径

- 添加组织结构管理。

  - 用户部门添加组织机构列表。

- 添加账号密码 注册登录修改密码。用户权限设置功能。

- 整理一个项目用到的组件 环境 安装运行步骤。简要的。

- tiger后台 参考 [vol](http://v2.volcore.xyz/formChart) 项目完善一些通用后台的功能 包括前后端

  - 用空修改一下服务器上数据库的密码。然后正式库的密码就不要保存在代码中了。

- 正价 批量删除。 批量修改状态。。列表排序。

- 开发组织机构管理的功能

- **js和C#递归将一个树形对象转换为另外一个对象代码实现**

- 树形菜单组件使用。

  - 用列表要开发一个树形菜单
  - 开发一个树形菜单的演示界面

- 代码生成器

  - 前端生成器 
  - 后端生成器

- elment-ui 表单验证 使用 
  - 表单验证要么弹出消息框。要么在输入框下面显示错误消息。 如果是显示。那么输入框下面也要预留足够的空间。一般使用组件的表单空间都会自动预留好位置。。手机端也是一样的道理。
  - [ElementUI的表单验证及常用规则](https://www.jianshu.com/p/d71dc34193b4)
  - [ElementUI表单验证使用。](https://segmentfault.com/a/1190000020410128)
  - [表单验证中ref的功能](https://blog.csdn.net/DianaGreen7/article/details/80369482)
  - [element-ui 表单验证数字类型提示 xx is not a string](https://segmentfault.com/q/1010000010354929)  v-model.nubmer 转换为数字类型来表单验证。
- 多列合并显示
- 表单。图表。柱状图。饼状图。折线图。
  - [echarts](https://echarts.apache.org/zh/index.html)  图表显示。
  - webpack中使用EChart的文档
  - Echarts按需引入模块文档
  - vue中组件使用。配置自己的组件
- 增加批处理功能。表单校验。 一个是复杂table .一个是文章。文章内容比较多新增就是一个页面。不然就是一个model.
- 列表增加图片显示。
- 增加单列表单。多列表单。



## 问题

- mockjs 无法使用

  - /mock/index.js 里面需要引入文件

- excel无法导出  Error: Cannot find module 'file-saver'  报错

  - ```js
    npm install file-saver --save  // 需要安装 file-saver 包
    npm install xlsx --save
    
    ```

- 添加 编辑 删除 详细  create update delete detail 这个命名要规范统一。

- 为什么添加了一个浮动，添加了一个相对定位，里面的框就自动的变小了。

- 样式命名规则是什么样子的。

- 上传视频功能。。多图上传功能。

- [elementui TAB跟随上一步 下一步切换实现。](https://segmentfault.com/q/1010000023794629)

- chrome 浏览器。下载完成自动打开excel的功能。

- abp pro的界面先话起来。

- 有些功能。

- [vscode格式化文档时设置成不换行](https://blog.csdn.net/weixin_42555643/article/details/108054253)

- [vscode 设置自动换行](https://zhuanlan.zhihu.com/p/65268106)

- 变量的命名 maxFailedAccessAttempts  allowedForNewUsers requireUppercase 可以多个单词组合一起。长一点没有关系。能够表达清楚意思就可以

- 前端文件 或者文件夹如果有两个单词的页面和组件统一使用中划线

- 修改后台主题验证。弄一个变色的选项

- 功能管理 和权限设置的区别在哪里？？、看一下数据表结构。