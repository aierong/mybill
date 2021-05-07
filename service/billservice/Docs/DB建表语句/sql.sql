-- 角色表 目前就2种角色 admin user
CREATE TABLE roles
(
    ids INT IDENTITY ,
    -- 角色名
    name NVARCHAR(28) NOT NULL DEFAULT ( '' ) PRIMARY KEY
)

-- 先插入2个角色
INSERT INTO [dbo].[roles] ( [name] )
            SELECT  'admin'
            UNION ALL
            SELECT  'user'
go



-- 用户表
CREATE TABLE users
(
    ids INT IDENTITY ,
    -- 角色名 
    rolename NVARCHAR(28) NOT NULL DEFAULT ( 'user' ) ,
    --电话号码
    mobile VARCHAR(25) NOT NULL PRIMARY KEY ,
    --头像
    avatar NVARCHAR(88) NOT NULL DEFAULT ( '' ) ,
    -- 密码
    password NVARCHAR(188) NOT NULL DEFAULT ( '' ) ,
    -- 姓名
    name NVARCHAR(188) NOT NULL DEFAULT ( '' ) ,
    -- email
    email NVARCHAR(188) NOT NULL DEFAULT ( '' ) ,
    -- 最后一次登录时间
    lastlogindate DATETIME ,
    -- 累计登录次数
    logintimes INT NOT NULL DEFAULT ( 0 ) ,

    -- 记录的 添加 修改 删除时间
    adddate DATETIME NOT NULL DEFAULT ( GETDATE ()) ,
    updatedate DATETIME ,
    deletedate DATETIME ,

    --删除标志,暂时无用
    delmark VARCHAR(1) NOT NULL DEFAULT ( 'N' )
)

-- 202cb962ac59075b964b07152d234b70             123前端密码加密
-- 2bHX20zW5wk1Nooe+xDjdw==                     123数据库中密码

--先默认插入 一个管理员
INSERT INTO [dbo].[users] (
                              [rolename] ,
                              [mobile] ,
                              [avatar] ,
                              [password] ,
                              [name] ,
                              [email]
                          )
            SELECT  'admin' ,
                    '13912345678' ,
                    'smile-o' ,
                    '2bHX20zW5wk1Nooe+xDjdw==' ,
                    '管理员' ,
                    'aierong@126.com'

CREATE TABLE billtype
(
    ids INT IDENTITY PRIMARY KEY ,
    -- 是支出类型
    isout BIT NOT NULL DEFAULT ( 0 ) ,
    -- 是系统预定义类型
    issystemtype BIT NOT NULL DEFAULT ( 1 ) ,
    -- 类型名称
    typename NVARCHAR(38) NOT NULL DEFAULT ( '' ) ,
	-- 图标
	avatar NVARCHAR(88) NOT NULL DEFAULT ( '' ) ,

    -- 哪个用户的类型
    mobile VARCHAR(25) NOT NULL DEFAULT ( '' ) ,

    -- 记录的 添加 修改 删除时间
    adddate DATETIME NOT NULL DEFAULT ( GETDATE ()) ,
    updatedate DATETIME ,
    deletedate DATETIME
)

-- 一些系统内置的类型
INSERT INTO [dbo].[billtype]
           ([isout]
           ,[issystemtype]
           ,[typename])
SELECT 1,1,'餐饮'  
UNION ALL
SELECT 1,1,'娱乐'  
UNION ALL
SELECT 1,1,'住房'  
UNION ALL
SELECT 1,1,'购物'  
UNION ALL
SELECT 0,1,'工资'  
UNION ALL
SELECT 0,1,'奖金'  
UNION ALL
SELECT 0,1,'红包'  
UNION ALL
SELECT 0,1,'转账'  



-- 账目
CREATE TABLE bills
(
    ids INT IDENTITY PRIMARY KEY ,
    -- 哪个用户
    mobile VARCHAR(25) NOT NULL DEFAULT ( '' ) ,
    -- 类型id
    billtypeid INT NOT NULL DEFAULT ( 0 ) ,
	-- 是支出类型
    isout BIT NOT NULL DEFAULT ( 0 ) ,
    -- 金额 
    moneys MONEY NOT NULL DEFAULT ( 0 ) ,
    -- 金额发生日期 2020-01-01
    moneydate NVARCHAR(168) NOT NULL DEFAULT ( '' ) ,
	
	moneyyear INT NOT NULL DEFAULT(0),
	moneymonth INT NOT NULL DEFAULT(0) ,
	moneyday INT NOT NULL DEFAULT(0),

    --备注
    memo NVARCHAR(168) DEFAULT ( '' ) ,

	-- 来源,暂时无用
	sources  NVARCHAR(168) DEFAULT ( '' ) ,

    -- 记录的 添加 修改 删除时间
    adddate DATETIME NOT NULL DEFAULT ( GETDATE ()) ,
    updatedate DATETIME ,
    deletedate DATETIME ,

    --删除标志
    delmark VARCHAR(1) NOT NULL DEFAULT ( 'N' )
)

-- 搞一个索引
CREATE INDEX idx_bills
    ON bills ( mobile )
